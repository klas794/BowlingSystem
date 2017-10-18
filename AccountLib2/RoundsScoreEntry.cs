namespace AccountLib
{
    public class RoundsScoreEntry
    {
        public int RoundsScoreEntryId { get; set; }

        public RoundsScoreAccount Account { get; set; }
        public int ScoreAccountId { get; set; }

        public int Amount { get; set; }
    }
}