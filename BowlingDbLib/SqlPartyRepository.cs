using AccountabilityInterfacesLib;
using AccountLib;
using BowlingDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountabilityLib
{
    public class SqlPartyRepository : IPartyRepository
    {
        private BowlingContext _context;

        public SqlPartyRepository(BowlingContext context)
        {
            _context = context;
        }
        
        public List<PlayerParty> All()
        {
            return _context.Parties.ToList();
        }

        public PlayerParty Create(string name, string legalId)
        {
            var party = new PlayerParty {
                Name = name,
                LegalId = legalId,
                ScoreAccount = new RoundsScoreAccount() { Entries = new List<RoundsScoreEntry>() },
                WinningAccount = new WinningAccount() { Entries = new List<WinningEntry>() }
            };

            _context.Add(party.ScoreAccount);
            _context.Add(party.WinningAccount);
            _context.Add(party);
            _context.SaveChanges();

            return party;
        }

        public void Delete(int id, bool hard)
        {
            throw new NotImplementedException();
        }

        public List<PlayerParty> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(PlayerParty player)
        {
            _context.Update(player);
        }

        public void StoreGameRound(Round round)
        {
            _context.Add(round.WinnerSerie);
            _context.Add(round.LooserSerie);
            _context.Add(round.WinnerSerie.Score);
            _context.Add(round.LooserSerie.Score);
            _context.Add(round);
        }

        public PlayerParty GetPlayerParty(int playerPartyId)
        {
            return _context.Parties.Find(playerPartyId);
        }
    }
}
