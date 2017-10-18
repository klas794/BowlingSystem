using AccountabilityInterfacesLib;
using AccountabilityLib;
using AccountInterfacesLib;
using AccountLib;
using BowlingDbLib;
using BowlingInterfacesLib;
using FactoryEnumsLib;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Linq;
using FactoryInterfacesLib;
using FactoryLib;
using FactoryExtensionsLib;

namespace BowlingLib
{
    public class BowlingSystem : IBowlingSystem
    {
        private IPartyRepository _partyContext;
        private IAccountabilityRepository _accountabilityContext;
        private IAccountRepository _accountContext;
        private BowlingContext _bowlingContext;

        private const int _gamingRounds = 3;
        private const int _competitionGames = 10;

        private GameAccountabilityType _gameType { get; set; }
        private Unit _scoreUnit { get; set; }
        private PhenomenonType _phenomenonType { get; set; }

        private List<Lane> _lanes { get; set; }
        private List<Lane> _customLanes { get; set; }

        public BowlingSystem(
            IPartyRepository partyContext, 
            IAccountabilityRepository accountabilityContext,
            IAccountRepository accountContext)
        {
            _partyContext = partyContext;
            _accountabilityContext = accountabilityContext;
            _accountContext = accountContext;

            PrepareGameObjects();
        }

        public BowlingSystem(
            IPartyRepository partyContext, 
            IAccountabilityRepository accountabilityContext,
            IAccountRepository accountContext,
            BowlingContext bowlingContext)
        {
            _partyContext = partyContext;
            _accountabilityContext = accountabilityContext;
            _accountContext = accountContext;

            PrepareGameObjects();

            _bowlingContext = bowlingContext;

            _bowlingContext.AccountabilityTypes.Add(_gameType);
            _bowlingContext.Units.Add(_scoreUnit);
            _bowlingContext.PhenomenonTypes.Add(_phenomenonType);
            _bowlingContext.Lanes.AddRange(_customLanes);
            _bowlingContext.Lanes.AddRange(_lanes);

            _bowlingContext.SaveChanges();
        }
        
        private void PrepareGameObjects()
        {
            _gameType = new GameAccountabilityType() { Name = "Bowling Game" };
            _scoreUnit = new Unit() { Name = "Points" };
            _phenomenonType = new PhenomenonType { Name = "Score" };

            _lanes = new List<Lane>();
            _customLanes = new List<Lane>();
            _customLanes.Add(GetCustomLane());
            _lanes.Add(new Lane());
        }

        private Lane GetCustomLane()
        {
            var factory = new DeluxLaneFactory();

            var specialLane = (OakLane)factory.Build(LaneStyle.WildWest);

            return specialLane.ConvertToLane();
        }

        public PlayerParty WinnerOfYear(int year)
        {
            PlayerParty leader = null;

            var yearStart = new DateTime(year, 1, 1);
            var nextYearStart = new DateTime(year+1, 1, 1);

            var accountabilities = _accountabilityContext.AllGames().Where(
                x => x.TimePoint > yearStart && x.TimePoint < nextYearStart);

            foreach (var party in _partyContext.All())
            {

                if (leader == null ||
                    accountabilities.Count(x => x.Winner == party)
                    >
                    accountabilities.Count(x => x.Winner == leader)
                    )
                {
                    leader = party;
                }
            }

            return leader;
        }

        
        public PlayerParty CreatePlayer(string name, string legalId)
        {
            return _partyContext.Create(name, legalId);
        }
        
        public GameAccountability PlayGame(PlayerParty player1, PlayerParty player2, bool rigged)
        {
            var rand = new Random();
            PlayerParty winner, looser;

            if (rand.Next(0,10) < 5 || rigged)
            {
                winner = player1;
                looser = player2;
            }
            else
            {
                winner = player2;
                looser = player1;
            }

            var game = _accountabilityContext.AddAccountability(winner, looser, _gameType);

            GenerateGameRounds(ref game, winner, looser);

            _accountabilityContext.Update(game);

            LogGameResult(ref game);

            return game;
        }

        private void GenerateGameRounds(ref GameAccountability game, PlayerParty winner, PlayerParty looser)
        {
            var rand = new Random();

            game.Rounds = new List<Round>();

            for (int i = 0; i < _gamingRounds; i++)
            {
                var round = new Round()
                {
                    Winner = winner
                };

                round.WinnerSerie = new Serie
                {
                    Score = new Measurement
                    {
                        Quantity = new Quantity { Unit = _scoreUnit, Number = rand.Next(2, 201) },
                        PhenomenonType = _phenomenonType
                    }
                };

                round.LooserSerie = new Serie
                {
                    Score = new Measurement
                    {
                        Quantity = new Quantity { Unit = _scoreUnit,
                            Number = rand.Next(1, round.WinnerSerie.Score.Quantity.Number) },
                        PhenomenonType = _phenomenonType
                    }
                };


                game.Rounds.Add(round);

                _partyContext.StoreGameRound(round);

                LogRoundScore(ref winner, round.WinnerSerie.Score);
                LogRoundScore(ref looser, round.LooserSerie.Score);
            }
        }

        private void LogRoundScore(ref PlayerParty player, Measurement score)
        {
            _accountContext.AddRoundsScoreEntry(player, score);
        }

        private void LogGameResult(ref GameAccountability game)
        {
            _accountContext.AddWinningAccountsTransaction(ref game);
            _accountabilityContext.Update(game);
            
        }

        public int RegisterCompetition(string name, TimePeriod period)
        {
            var competition = new Competition
            {
                Games = new List<GameAccountability>(),
                Name = name,
                TimePeriod = period,
                Lane = _customLanes.FirstOrDefault()
            };

            _accountabilityContext.AddCompetition(ref competition);

            return competition.CompetitionId;
        }

        public bool RegisterCompetitionPlayer(int competitionId, int partyId)
        {
            var competition = _accountabilityContext.GetCompetition(competitionId);
            var party = _partyContext.GetPlayerParty(partyId);

            if(competition.PlayerOne == null)
            {
                competition.PlayerOne = party;
                return true;
            }
            else if (competition.PlayerTwo == null)
            {
                competition.PlayerTwo = party;
                return true;
            }

            return false;

        }

        public List<Competition> ListCompetitions()
        {
            return _accountabilityContext.AllCompetitions();
        }

        public List<GameAccountability> ListMatches(int competitionId)
        {
            return _accountabilityContext.AllGames()
                .Where( x => x.GameAccountabilityId == competitionId)
                .ToList();
        }

        public void RunCompetition(int competitionId)
        {
            var competition = _accountabilityContext.GetCompetition(competitionId);
            competition.Games = new List<GameAccountability>();

            for (int i = 0; i < _competitionGames; i++)
            {
                var game = PlayGame(competition.PlayerOne, competition.PlayerTwo, true);
                competition.Games.Add(game);
            }

            _accountabilityContext.Update(competition);
        }
    }
}
