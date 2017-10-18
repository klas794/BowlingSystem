using FactoryEnumsLib;
using FactoryInterfacesLib;
using FactoryLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class BudgetLaneFactory : LaneFactory
    {
        protected internal override ILane SelectLane(string name, LaneStyle style)
        {
            return new PineWoodLane { PriceRange = LanePriceRange.Beginners, Name = name, LaneStyle = style };
        }
    }
}
