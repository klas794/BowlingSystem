using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountabilityLib
{
    public class Round
    {
        public int RoundId { get; set; }

        public Serie WinnerSerie { get; set; }

        public Serie LooserSerie { get; set; }

        public PlayerParty Winner { get; set; }
    }
}
