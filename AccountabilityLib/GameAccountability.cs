﻿using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;

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

        public PlayerParty Winner { get; set; }

        public PlayerParty Looser { get; set; }

        public GameAccountabilityType GameType { get; set; }
        public int GameTypeId { get; set; }

        public DateTime TimePoint { get; set; }

        public List<Round> Rounds { get; set; }

        public Lane Lane { get; set; }

    }
}
