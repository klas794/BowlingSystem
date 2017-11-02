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

            var winner = game.Winner;

            var playerOneEntry = new WinningEntry()
            {
                Account = game.PlayerOne.WinningAccount,
                Amount = winner == game.PlayerOne ? 1 : -1
            };

            var playerTwoEntry = new WinningEntry()
            {
                Account = game.PlayerTwo.WinningAccount,
                Amount = winner == game.PlayerOne ? -1 : 1
            };

            transaction.Entries = new List<WinningEntry>();
            transaction.Entries.Add(playerOneEntry);
            transaction.Entries.Add(playerTwoEntry);

            _winningEntries.Add(playerOneEntry);
            _winningEntries.Add(playerTwoEntry);
            _winningTransactions.Add(transaction);

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

            player.ScoreAccount.Entries.Add(entry);

            _partyContext.Update(player);

            _roundsScoreEntries.Add(entry);
        }
    }
}
