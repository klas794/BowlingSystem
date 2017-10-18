using FactoryEnumsLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryInterfacesLib
{
    public interface ILaneFactory
    {
        ILane Build(LaneStyle style);
    }
}
