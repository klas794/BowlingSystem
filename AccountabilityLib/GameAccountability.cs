using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AccountabilityLib
{
    public class GameAccountability
    {
        public GameAccountability()
        {
            GameGuid = Guid.NewGuid();
        }

        public int GameAccountabilityId { get; set; }

        public Guid GameGuid { get; set; }

        public PlayerParty Winner {
            get
            {
                if(Rounds == null || Rounds.Count == 0)
                {
                    return null;
                }

                var playerOneWins = Rounds.Count(x => 
                    x.PlayerOneSerie.Score.Quantity.Number > x.PlayerTwoSerie.Score.Quantity.Number);

                var playerTwoWins = Rounds.Count - playerOneWins;

                return playerOneWins > playerTwoWins ? PlayerOne : PlayerTwo;
            }
        }

        public PlayerParty PlayerOne { get; set; }

        public PlayerParty PlayerTwo { get; set; }

        public GameAccountabilityType GameType { get; set; }
        public int GameTypeId { get; set; }

        public DateTime TimePoint { get; set; }

        public List<Round> Rounds { get; set; }

        public Lane Lane { get; set; }

    }
}
