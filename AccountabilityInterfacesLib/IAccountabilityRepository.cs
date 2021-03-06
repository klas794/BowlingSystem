﻿using AccountabilityLib;
using System;
using System.Collections.Generic;

namespace AccountabilityInterfacesLib
{
    public interface IAccountabilityRepository
    {
        GameAccountability AddGameAccountability(PlayerParty winner, PlayerParty looser, GameAccountabilityType accountabilityType);

        List<GameAccountability> AllGames();

        void Update(GameAccountability game);

        void Update(Competition competition);

        void AddCompetition(ref Competition competition);

        List<Competition> AllCompetitions();

        Competition GetCompetition(Guid competitionGuid);

        void StoreGameRound(Round round);
    }
}
