using AccountabilityInterfacesLib;
using AccountabilityLib;
using AccountLib;
using BowlingDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingDbLib
{
    public class FakePartyRepository : IPartyRepository
    {
        private List<PlayerParty> _parties;

        public FakePartyRepository()
        {
            _parties = new List<PlayerParty>();
        }

        public List<PlayerParty> All()
        {
            return _parties;
        }

        public PlayerParty Create(string name, string legalId)
        {
            var party = new PlayerParty
            {
                Name = name,
                LegalId = legalId,
                ScoreAccount = new RoundsScoreAccount() { Entries = new List<RoundsScoreEntry>() },
                WinningAccount = new WinningAccount() { Entries = new List<WinningEntry>() }
            };

            _parties.Add(party);

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

        public void StoreGameRound(Round round)
        {
            return;
        }

        public void Update(PlayerParty player)
        {
           
            var localPlayer = _parties.SingleOrDefault(x => x.LegalId == player.LegalId);

            if(localPlayer != null)
            {
                localPlayer.Name = player.Name;
                localPlayer.ScoreAccount = player.ScoreAccount;
                localPlayer.WinningAccount = player.WinningAccount;
            }
        }

        public PlayerParty GetPlayerParty(int playerPartyId)
        {
            return _parties.Find(x => x.PlayerPartyId == playerPartyId);
        }
    }
}
