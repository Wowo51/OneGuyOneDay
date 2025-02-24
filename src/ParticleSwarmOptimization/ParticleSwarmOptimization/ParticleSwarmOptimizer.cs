using System;
using System.Collections.Generic;

namespace ParticleSwarmOptimization
{
    public class ParticleSwarmOptimizer
    {
        private List<Particle> Particles;
        private int ParticleCount;
        private int Dimension;
        private double MinBound;
        private double MaxBound;
        private double InertiaWeight;
        private double CognitiveCoefficient;
        private double SocialCoefficient;
        private int Iterations;
        private Random RandomGenerator;
        private double[] GlobalBestPosition;
        private double GlobalBestFitness;
        public ParticleSwarmOptimizer(int particleCount, int dimension, double minBound, double maxBound, int iterations, double inertiaWeight, double cognitiveCoefficient, double socialCoefficient)
        {
            ParticleCount = particleCount;
            Dimension = dimension;
            MinBound = minBound;
            MaxBound = maxBound;
            Iterations = iterations;
            InertiaWeight = inertiaWeight;
            CognitiveCoefficient = cognitiveCoefficient;
            SocialCoefficient = socialCoefficient;
            RandomGenerator = new Random();
            Particles = new List<Particle>();
            GlobalBestPosition = new double[dimension];
            GlobalBestFitness = double.MaxValue;
            for (int i = 0; i < ParticleCount; i++)
            {
                Particle particle = new Particle(Dimension, MinBound, MaxBound, RandomGenerator);
                Particles.Add(particle);
            }
        }

        public OptimizationResult Optimize(Func<double[], double> objectiveFunction)
        {
            for (int i = 0; i < ParticleCount; i++)
            {
                Particle particle = Particles[i];
                particle.Evaluate(objectiveFunction);
                if (particle.BestFitness < GlobalBestFitness)
                {
                    GlobalBestFitness = particle.BestFitness;
                    Array.Copy(particle.BestPosition, GlobalBestPosition, Dimension);
                }
            }

            for (int iter = 0; iter < Iterations; iter++)
            {
                for (int i = 0; i < ParticleCount; i++)
                {
                    Particle particle = Particles[i];
                    particle.UpdateVelocity(GlobalBestPosition, InertiaWeight, CognitiveCoefficient, SocialCoefficient, RandomGenerator);
                    particle.UpdatePosition(MinBound, MaxBound);
                    particle.Evaluate(objectiveFunction);
                    if (particle.BestFitness < GlobalBestFitness)
                    {
                        GlobalBestFitness = particle.BestFitness;
                        Array.Copy(particle.BestPosition, GlobalBestPosition, Dimension);
                    }
                }
            }

            double[] bestPos = new double[Dimension];
            Array.Copy(GlobalBestPosition, bestPos, Dimension);
            return new OptimizationResult
            {
                BestPosition = bestPos,
                BestFitness = GlobalBestFitness
            };
        }
    }

    public class OptimizationResult
    {
        public double[] BestPosition { get; set; }
        public double BestFitness { get; set; }
    }
}