using FactoryEnumsLib;
using FactoryInterfacesLib;
using FactoryLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class DeluxLaneFactory : LaneFactory
    {
        protected internal override ILane SelectLane(string name, LaneStyle style)
        {
            return new OakLane { PriceRange = LanePriceRange.Pros, Name = name, LaneStyle = style };
        }
    }
}
