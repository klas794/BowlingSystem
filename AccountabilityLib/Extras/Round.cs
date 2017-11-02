using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountabilityLib
{
    public class Round
    {
        public int RoundId { get; set; }

        public Serie PlayerOneSerie { get; set; }

        public Serie PlayerTwoSerie { get; set; }
    }
}
