using System;

namespace DifferentialEvolution
{
    public class Candidate
    {
        public double[] Vector { get; set; }
        public double Fitness { get; set; }

        public Candidate(double[] vector, double fitness)
        {
            this.Vector = vector;
            this.Fitness = fitness;
        }
    }

    public class DifferentialEvolutionAlgorithm
    {
        private int PopulationSize;
        private int Dimensions;
        private double[] LowerBounds;
        private double[] UpperBounds;
        private int MaxGenerations;
        private double F;
        private double CR;
        private Random RandomGenerator;
        private Func<double[], double> ObjectiveFunction;
        private Candidate[] Population;
        public DifferentialEvolutionAlgorithm(int populationSize, int dimensions, double[] lowerBounds, double[] upperBounds, int maxGenerations, double f, double cr, Func<double[], double> objectiveFunction)
        {
            if (populationSize < 4)
            {
                populationSize = 4;
            }

            this.PopulationSize = populationSize;
            this.Dimensions = dimensions;
            if (lowerBounds == null || lowerBounds.Length != dimensions)
            {
                this.LowerBounds = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    this.LowerBounds[j] = 0.0;
                }
            }
            else
            {
                this.LowerBounds = lowerBounds;
            }

            if (upperBounds == null || upperBounds.Length != dimensions)
            {
                this.UpperBounds = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    this.UpperBounds[j] = 1.0;
                }
            }
            else
            {
                this.UpperBounds = upperBounds;
            }

            if (objectiveFunction == null)
            {
                this.ObjectiveFunction = (vector) =>
                {
                    double sum = 0.0;
                    for (int j = 0; j < vector.Length; j++)
                    {
                        sum += vector[j] * vector[j];
                    }

                    return sum;
                };
            }
            else
            {
                this.ObjectiveFunction = objectiveFunction;
            }

            this.MaxGenerations = maxGenerations;
            this.F = f;
            this.CR = cr;
            this.RandomGenerator = new Random();
            this.Population = new Candidate[this.PopulationSize];
            InitializePopulation();
        }

        private void InitializePopulation()
        {
            for (int i = 0; i < this.PopulationSize; i++)
            {
                double[] vector = new double[this.Dimensions];
                for (int j = 0; j < this.Dimensions; j++)
                {
                    vector[j] = this.LowerBounds[j] + this.RandomGenerator.NextDouble() * (this.UpperBounds[j] - this.LowerBounds[j]);
                }

                double fitness = this.ObjectiveFunction(vector);
                this.Population[i] = new Candidate(vector, fitness);
            }
        }

        public Candidate Optimize()
        {
            for (int gen = 0; gen < this.MaxGenerations; gen++)
            {
                for (int i = 0; i < this.PopulationSize; i++)
                {
                    int a = GetRandomIndexExcluding(i);
                    int b;
                    do
                    {
                        b = GetRandomIndexExcluding(i);
                    }
                    while (b == a);
                    int c;
                    do
                    {
                        c = GetRandomIndexExcluding(i);
                    }
                    while (c == a || c == b);
                    double[] mutant = new double[this.Dimensions];
                    for (int j = 0; j < this.Dimensions; j++)
                    {
                        mutant[j] = this.Population[a].Vector[j] + this.F * (this.Population[b].Vector[j] - this.Population[c].Vector[j]);
                        if (mutant[j] < this.LowerBounds[j])
                        {
                            mutant[j] = this.LowerBounds[j];
                        }

                        if (mutant[j] > this.UpperBounds[j])
                        {
                            mutant[j] = this.UpperBounds[j];
                        }
                    }

                    double[] trial = new double[this.Dimensions];
                    int jRand = this.RandomGenerator.Next(this.Dimensions);
                    for (int j = 0; j < this.Dimensions; j++)
                    {
                        if (this.RandomGenerator.NextDouble() < this.CR || j == jRand)
                        {
                            trial[j] = mutant[j];
                        }
                        else
                        {
                            trial[j] = this.Population[i].Vector[j];
                        }
                    }

                    double trialFitness = this.ObjectiveFunction(trial);
                    if (trialFitness < this.Population[i].Fitness)
                    {
                        this.Population[i] = new Candidate(trial, trialFitness);
                    }
                }
            }

            Candidate best = this.Population[0];
            for (int i = 1; i < this.PopulationSize; i++)
            {
                if (this.Population[i].Fitness < best.Fitness)
                {
                    best = this.Population[i];
                }
            }

            return best;
        }

        private int GetRandomIndexExcluding(int excludeIndex)
        {
            int index = this.RandomGenerator.Next(this.PopulationSize);
            while (index == excludeIndex)
            {
                index = this.RandomGenerator.Next(this.PopulationSize);
            }

            return index;
        }
    }
}