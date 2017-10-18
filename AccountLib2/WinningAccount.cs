using System;
using System.Collections.Generic;

namespace AccountLib
{
    public class WinningAccount
    {
        public int WinningAccountId { get; set; }
        public List<WinningEntry> Entries { get; set; }
    }
}
