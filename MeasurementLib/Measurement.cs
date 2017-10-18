using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementLib
{
    public class Measurement
    {
        public int MeasurementId { get; set; }

        public Quantity Quantity { get; set; }
        public int QuantityId { get; set; }

        public PhenomenonType PhenomenonType { get; set; }
        public int PhenomenonTypeId { get; set; }
    }
}
