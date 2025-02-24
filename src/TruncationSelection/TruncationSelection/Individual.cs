using System;

namespace TruncationSelection
{
    public class Individual
    {
        public double Fitness { get; set; }

        public Individual(double fitness)
        {
            Fitness = fitness;
        }
    }
}