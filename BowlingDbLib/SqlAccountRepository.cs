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

            var playerOneEntry = new WinningEntry()
            {
                Account = game.PlayerOne.WinningAccount,
                Amount = 1
            };

            var playerTwoEntry = new WinningEntry()
            {
                Account = game.PlayerTwo.WinningAccount,
                Amount = -1
            };

            transaction.Entries = new List<WinningEntry>();
            transaction.Entries.Add(playerOneEntry);
            transaction.Entries.Add(playerTwoEntry);

            _context.AddRange(playerTwoEntry, playerOneEntry);
            _context.Add(transaction);

            game.PlayerOne.WinningAccount.Entries.Add(playerOneEntry);
            game.PlayerTwo.WinningAccount.Entries.Add(playerTwoEntry);

        }

        public void AddRoundsScoreEntry(PlayerParty player, Measurement score)
        {
            var entry = new RoundsScoreEntry()
            {
                ScoreAccount = player.ScoreAccount,
                Amount = score.Quantity.Number
            };

            _context.Add(entry);

            player.ScoreAccount.Entries.Add(entry);
            _context.Update(player);

            _context.SaveChanges();
        }
    }
}
