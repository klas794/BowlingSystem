namespace AccountLib
{
    public class WinningEntry
    {
        public int WinningEntryId { get; set; }

        public WinningAccount Account { get; set; }
        public int WinningAccountId { get; set; }

        public int Amount { get; set; }
    }
}