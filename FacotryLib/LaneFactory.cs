using System;
using System.Collections.Generic;
using System.Text;
using FactoryEnumsLib;
using FactoryInterfacesLib;

namespace FactoryLib
{

    public abstract class LaneFactory : ILaneFactory
    {
        public ILane Build(LaneStyle style)
        {
            switch(style)
            {
                case LaneStyle.Classic:
                    return SelectLane("Classy", style);
                case LaneStyle.WildWest:
                    return SelectLane("Cowboy", style);
                case LaneStyle.Disco:
                default:
                    return SelectLane("Saturday night", style);
            }
        }

        protected internal abstract ILane SelectLane(string name, LaneStyle style);
    }
}
