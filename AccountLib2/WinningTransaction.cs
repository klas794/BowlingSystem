using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLib
{
    public class WinningTransaction
    {
        public int WinningTransactionId { get; set; }

        public List<WinningEntry> Entries { get; set; }

        public DateTime TimePoint { get; set; }
    }
}
