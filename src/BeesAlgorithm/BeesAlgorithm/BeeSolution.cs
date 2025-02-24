using System;

namespace BeesAlgorithm
{
    public class BeeSolution
    {
        public double[] Position { get; set; }
        public double Fitness { get; set; }

        public BeeSolution(double[] position, double fitness)
        {
            if (position == null)
            {
                Position = new double[0];
            }
            else
            {
                Position = position;
            }

            Fitness = fitness;
        }
    }
}