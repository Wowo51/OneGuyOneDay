using System;
using System.Collections.Generic;

namespace SubcluAlgorithm
{
    public class SubspaceCluster
    {
        public List<int> Dimensions { get; set; }
        public List<DataPoint> Points { get; set; }

        public SubspaceCluster(List<int> dimensions, List<DataPoint> points)
        {
            Dimensions = dimensions ?? new List<int>();
            Points = points ?? new List<DataPoint>();
        }
    }
}