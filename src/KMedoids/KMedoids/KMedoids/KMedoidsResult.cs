using System;
using System.Collections.Generic;

namespace KMedoids
{
    public class KMedoidsResult
    {
        public List<DataPoint> Medoids { get; set; }
        public Dictionary<DataPoint, List<DataPoint>> Clusters { get; set; }

        public KMedoidsResult()
        {
            Medoids = new List<DataPoint>();
            Clusters = new Dictionary<DataPoint, List<DataPoint>>();
        }
    }
}