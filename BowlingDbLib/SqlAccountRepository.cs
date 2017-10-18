using AccountInterfacesLib;
using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib;
using AccountLib;
using System.Linq;
using MeasurementLib;

namespace BowlingDbLib
{
    public class SqlAccountRepository : IAccountRepository
    {
        private BowlingContext _context;

        public SqlAccountRepository(BowlingContext context)
        {
            _context = context;
        }

        public int SummaryScoreAccount(PlayerParty player)
        {
            return player.ScoreAccount.Entries.Sum(x => x.Amount);
        }

        public int SummaryWinningAccount(PlayerParty player)
        {
            return player.WinningAccount.Entries.Sum(x => x.Amount);
        }

        public void AddWinningAccountsTransaction(ref GameAccountability game)
        {

            var transaction = new WinningTransaction() { TimePoint = DateTime.Now };

            var winningEntry = new WinningEntry()
            {
                Account = game.Winner.WinningAccount,
                Amount = 1
            };

            var loosingEntry = new WinningEntry()
            {
                Account = game.Looser.WinningAccount,
                Amount = -1
            };

            transaction.Entries = new List<WinningEntry>();
            transaction.Entries.Add(loosingEntry);
            transaction.Entries.Add(winningEntry);

            _context.AddRange(loosingEntry, winningEntry);
            _context.Add(transaction);

            game.Winner.WinningAccount.Entries.Add(winningEntry);
            game.Looser.WinningAccount.Entries.Add(loosingEntry);

        }

        public void AddRoundsScoreEntry(PlayerParty player, Measurement score)
        {
            var entry = new RoundsScoreEntry()
            {
                Account = player.ScoreAccount,
                Amount = score.Quantity.Number
            };

            player.ScoreAccount.Entries.Add(entry);
            
            _context.Update(player);
            _context.Add(entry);
            _context.SaveChanges();
        }
    }
}
