using AccountabilityLib;
using FactoryLib;
using System;

namespace FactoryExtensionsLib
{
    public static class LaneExtensions
    {
        public static Lane ConvertToLane(this OakLane oakLane) {

            var lane = new Lane();

            lane.AbstractLaneId = oakLane.AbstractLaneId;
            lane.LaneStyle = oakLane.LaneStyle;
            lane.Name = oakLane.Name;
            lane.PriceRange = oakLane.PriceRange;

            return lane;
        }

        public static Lane ConvertToLane(this PineWoodLane pineWoodLane)
        {
            var lane = new Lane();

            lane.AbstractLaneId = pineWoodLane.AbstractLaneId;
            lane.LaneStyle = pineWoodLane.LaneStyle;
            lane.Name = pineWoodLane.Name;
            lane.PriceRange = pineWoodLane.PriceRange;

            return lane;
        }
    }

}
