using System;

namespace ParticleSwarmOptimization
{
    public class Particle
    {
        public double[] Position { get; set; }
        public double[] Velocity { get; set; }
        public double[] BestPosition { get; set; }
        public double BestFitness { get; set; }

        public Particle(int dimension, double minBound, double maxBound, Random random)
        {
            Position = new double[dimension];
            Velocity = new double[dimension];
            BestPosition = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                Position[i] = minBound + (maxBound - minBound) * random.NextDouble();
                Velocity[i] = (random.NextDouble() * 2 - 1) * (maxBound - minBound) * 0.1;
                BestPosition[i] = Position[i];
            }

            BestFitness = double.MaxValue;
        }

        public void UpdateVelocity(double[] globalBestPosition, double inertiaWeight, double cognitiveCoefficient, double socialCoefficient, Random random)
        {
            int dimension = Position.Length;
            for (int i = 0; i < dimension; i++)
            {
                double r1 = random.NextDouble();
                double r2 = random.NextDouble();
                double cognitiveVelocity = cognitiveCoefficient * r1 * (BestPosition[i] - Position[i]);
                double socialVelocity = socialCoefficient * r2 * (globalBestPosition[i] - Position[i]);
                Velocity[i] = inertiaWeight * Velocity[i] + cognitiveVelocity + socialVelocity;
            }
        }

        public void UpdatePosition(double minBound, double maxBound)
        {
            int dimension = Position.Length;
            for (int i = 0; i < dimension; i++)
            {
                Position[i] += Velocity[i];
                if (Position[i] < minBound)
                {
                    Position[i] = minBound;
                    Velocity[i] = 0;
                }
                else if (Position[i] > maxBound)
                {
                    Position[i] = maxBound;
                    Velocity[i] = 0;
                }
            }
        }

        public void Evaluate(Func<double[], double> objectiveFunction)
        {
            double fitness = objectiveFunction(Position);
            if (fitness < BestFitness)
            {
                BestFitness = fitness;
                int dimension = Position.Length;
                for (int i = 0; i < dimension; i++)
                {
                    BestPosition[i] = Position[i];
                }
            }
        }
    }
}