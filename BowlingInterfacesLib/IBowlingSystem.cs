using AccountabilityLib;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingDbLib;
using MeasurementLib;

namespace BowlingInterfacesLib
{
    public interface IBowlingSystem
    {
        PlayerParty WinnerOfYear(int year);

        PlayerParty CreatePlayer(string name, string legalId);

        GameAccountability PlayGame(PlayerParty player1, PlayerParty player2, bool rigged = false, Lane lane = null);

        Lane GetDefaultLane();

        Guid RegisterCompetition(string Name, TimePeriod Period);

        bool RegisterCompetitionPlayer(Guid competitionGuid, Guid partyGuid);

        List<Competition> ListCompetitions();

        List<GameAccountability> ListMatches(Guid competitionGuid);

        void RunCompetition(Guid competitionGuid);
    }
}
