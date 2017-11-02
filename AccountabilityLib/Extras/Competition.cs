﻿using AccountabilityLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountabilityLib
{
    public class Competition
    {
        public Competition()
        {
            CompetitionGuid = Guid.NewGuid();
        }

        public Guid CompetitionGuid { get; set; }

        public int CompetitionId { get; set; }
        public string Name { get; set; }

        public List<GameAccountability> Games { get; set; }

        public TimePeriod TimePeriod { get; set; }

        public PlayerParty PlayerOne { get; set; }
        public PlayerParty PlayerTwo { get; set; }


    }
}
