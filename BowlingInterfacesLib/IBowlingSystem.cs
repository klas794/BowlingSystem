using AccountabilityLib;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingDbLib;

namespace BowlingInterfacesLib
{
    public interface IBowlingSystem
    {
        PlayerParty WinnerOfYear(int year);

        PlayerParty CreatePlayer(string name, string legalId);

        GameAccountability PlayGame(PlayerParty player1, PlayerParty player2, bool rigged);

        int RegisterCompetition(string Name, TimePeriod Period);

        bool RegisterCompetitionPlayer(int competitionId, int partyId);

        void RunCompetition(int competitionId);

        List<Competition> ListCompetitions();
    }
}
