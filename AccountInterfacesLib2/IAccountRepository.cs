using AccountabilityLib;
using System;
using AccountLib;
using MeasurementLib;

namespace AccountInterfacesLib
{
    public interface IAccountRepository
    {
        int SummaryScoreAccount(PlayerParty player);
        int SummaryWinningAccount(PlayerParty player);
        void AddWinningAccountsTransaction(ref GameAccountability game);
        void AddRoundsScoreEntry(PlayerParty player, Measurement score);
    }
}
