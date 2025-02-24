using System;
using System.Collections.Generic;

namespace SubcluAlgorithm
{
    public class DataPoint
    {
        public double[] Coordinates { get; set; }

        public DataPoint(double[] coordinates)
        {
            Coordinates = coordinates ?? new double[0];
        }
    }
}