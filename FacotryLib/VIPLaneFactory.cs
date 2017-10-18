using FactoryEnumsLib;
using FactoryInterfacesLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryLib
{
    class VIPLaneFactory : LaneFactory
    {
        protected internal override ILane SelectLane(string name, LaneStyle style)
        {
            return new OakLane { PriceRange = LanePriceRange.AllStars, Name = name, LaneStyle = style };
        }
    }
}
