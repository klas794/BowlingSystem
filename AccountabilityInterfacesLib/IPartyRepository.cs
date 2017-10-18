using AccountabilityLib;
using System;
using System.Collections.Generic;

namespace AccountabilityInterfacesLib
{
    public interface IPartyRepository
    {
        PlayerParty Create(string name, string legalId);

        List<PlayerParty> Search(string term);

        void Delete(int id, bool hard);

        List<PlayerParty> All();

        void Update(PlayerParty player);

        void StoreGameRound(Round round);

        PlayerParty GetPlayerParty(int playerPartyId);

    }
}
