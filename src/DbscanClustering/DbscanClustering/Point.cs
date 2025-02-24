using System;

namespace DbscanClustering
{
    public class Point
    {
        public double[] Coordinates { get; private set; }
        public bool Visited { get; set; }
        public int ClusterId { get; set; }

        public Point(double[] coordinates)
        {
            if (coordinates == null)
            {
                this.Coordinates = new double[0];
            }
            else
            {
                this.Coordinates = coordinates;
            }

            this.Visited = false;
            this.ClusterId = 0;
        }
    }
}