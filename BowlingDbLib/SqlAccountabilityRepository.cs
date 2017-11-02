using AccountabilityInterfacesLib;
using BowlingDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountabilityLib
{
    public class SqlAccountabilityRepository : IAccountabilityRepository
    {
        private BowlingContext _context;

        public SqlAccountabilityRepository(BowlingContext context)
        {
            _context = context;
        }

        public GameAccountability AddGameAccountability(PlayerParty playerOne, PlayerParty playerTwo, GameAccountabilityType accountabilityType)
        {
            
            var accountability = new GameAccountability
            {
                PlayerOne = playerOne,
                PlayerTwo = playerTwo,
                GameType = accountabilityType,
                TimePoint = DateTime.Now,
            };

            _context.Add(accountability);
            _context.SaveChanges();

            return accountability;
        }

        public void AddCompetition(ref Competition competition)
        {
            _context.Add(competition.TimePeriod);
            _context.Add(competition);
            _context.SaveChanges();
        }

        public List<GameAccountability> AllGames()
        {
            return _context.Accountabilities.ToList();
        }


        public void Update(GameAccountability game)
        {
            _context.Update(game);
            _context.SaveChanges();
        }

        public void Update(Competition competition)
        {
            //_context.AddRange(competition.Games);
            _context.Update(competition);
            _context.SaveChanges();
        }

        public List<Competition> AllCompetitions()
        {
            return _context.Competitions.ToList();
        }

        public Competition GetCompetition(Guid competitionGuid)
        {
            return _context.Competitions.SingleOrDefault(x => x.CompetitionGuid == competitionGuid);
        }

        public void StoreGameRound(Round round)
        {
            _context.Add(round.PlayerOneSerie);
            _context.Add(round.PlayerTwoSerie);
            _context.Add(round.PlayerOneSerie.Score);
            _context.Add(round.PlayerTwoSerie.Score);
            _context.Add(round);
        }

    }
}
