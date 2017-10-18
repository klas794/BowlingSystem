using AccountInterfacesLib;
using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib;
using AccountLib;
using System.Linq;
using MeasurementLib;
using AccountabilityInterfacesLib;

namespace BowlingDbLib
{
    public class FakeAccountRepository : IAccountRepository
    {
        private List<WinningTransaction> _winningTransactions;
        private List<WinningEntry> _winningEntries;
        private List<RoundsScoreEntry> _roundsScoreEntries;
        private IPartyRepository _partyContext;

        public FakeAccountRepository(IPartyRepository partyContext)
        {
            _winningTransactions = new List<WinningTransaction>();
            _winningEntries = new List<WinningEntry>();
            _roundsScoreEntries = new List<RoundsScoreEntry>();
            _partyContext = partyContext;
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

            _winningEntries.Add(loosingEntry);
            _winningEntries.Add(winningEntry);
            _winningTransactions.Add(transaction);

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

            _partyContext.Update(player);

            _roundsScoreEntries.Add(entry);
        }
    }
}
