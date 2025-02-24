using System;
using System.Collections.Generic;

namespace CanopyClusteringAlgorithm
{
    public class DataPoint
    {
        public string Id { get; set; }
        public double[] Features { get; set; }

        public DataPoint(string id, double[] features)
        {
            this.Id = id;
            this.Features = features;
        }
    }
}