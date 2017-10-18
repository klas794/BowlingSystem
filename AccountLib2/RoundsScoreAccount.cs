using System;
using System.Collections.Generic;

namespace AccountLib
{
    public class RoundsScoreAccount
    {
        public int RoundsScoreAccountId { get; set; }
        public List<RoundsScoreEntry> Entries { get; set; }
    }
}
