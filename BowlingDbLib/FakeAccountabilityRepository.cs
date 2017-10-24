using AccountabilityInterfacesLib;
using AccountabilityLib;
using BowlingDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingDbLib
{
    public class FakeAccountabilityRepository : IAccountabilityRepository
    {
        private List<GameAccountability> _accountabilities;
        private List<Competition> _competitions;

        public FakeAccountabilityRepository()
        {
            _accountabilities = new List<GameAccountability>();
            _competitions = new List<Competition>();
        }

        public GameAccountability AddAccountability(PlayerParty commissioner, PlayerParty responsible, GameAccountabilityType accountabilityType)
        {
            var accountability = new GameAccountability
            {
                Winner = commissioner,
                Looser = responsible,
                GameType = accountabilityType,
                TimePoint = DateTime.Now
            };

            _accountabilities.Add(accountability);

            return accountability;
        }

        public void AddCompetition(ref Competition competition)
        {
            _competitions.Add(competition);
        }

        public List<GameAccountability> AllGames()
        {
            return _accountabilities;
        }

        public List<Competition> AllCompetitions()
        {
            return _competitions;
        }

        public void Update(GameAccountability game)
        {
            var accountability = _accountabilities.SingleOrDefault(x => x.GameGuid == game.GameGuid);

            if(accountability != null)
            {
                accountability.GameType = game.GameType;
                accountability.Rounds = game.Rounds;
                
            }
        }

        public void Update(Competition competition)
        {
            var competitionLocal = _competitions.SingleOrDefault(
                x => x.CompetitionGuid == competition.CompetitionGuid);

            if(competitionLocal != null)
            {
                competitionLocal.Games = competition.Games;
                competitionLocal.Name = competition.Name;
                competitionLocal.PlayerOne = competition.PlayerOne;
                competitionLocal.PlayerTwo = competition.PlayerTwo;
                competition.TimePeriod = competition.TimePeriod;
            }
        }

        public Competition GetCompetition(Guid competitionGuid)
        {
            return _competitions.Find(x => x.CompetitionGuid == competitionGuid);
        }

        public void StoreGameRound(Round round)
        {
            return;
        }
    }
}
