using FactoryEnumsLib;
using FactoryInterfacesLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryLib
{
    public class AbstractLane : ILane
    {
        public int AbstractLaneId { get; set; }

        public LanePriceRange PriceRange { get; set; }

        public LaneStyle LaneStyle { get; set; }

        public string Name { get; set; }
    }
}
