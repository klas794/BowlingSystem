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


        public PlayerParty GetPlayerParty(Guid playerPartyGuid)
        {
            return _context.Parties.SingleOrDefault(x => x.PlayerGuid == playerPartyGuid);
        }
    }
}
