using AccountLib;
using System;

namespace AccountabilityLib
{
    public class PlayerParty
    {
        public int PlayerPartyId { get; set; }

        public string Name { get; set; }
        public string LegalId { get; set; }

        public RoundsScoreAccount ScoreAccount { get; set; }
        public int ScoreAccountId { get; set; }

        public WinningAccount WinningAccount { get; set; }
        public int WinningAccountId { get; set; }
    }
}
