using System.Collections.Generic;

namespace CanopyClusteringAlgorithm
{
    public class Canopy
    {
        public DataPoint Center { get; set; }
        public List<DataPoint> Members { get; set; }

        public Canopy(DataPoint center)
        {
            Center = center;
            Members = new List<DataPoint>();
        }
    }
}