using System;
using System.Collections.Generic;
using System.Text;

namespace GeneExpressionProgramming
{
    public class GEPAlgorithm
    {
        public List<Chromosome> Population { get; set; }
        public int PopulationSize { get; set; }
        public double MutationRate { get; set; }
        public int GeneLength { get; set; }
        public int Generations { get; set; }
        private Random RandomGen { get; set; }

        public GEPAlgorithm(int populationSize, int geneLength, double mutationRate, int generations)
        {
            this.PopulationSize = populationSize;
            this.GeneLength = geneLength;
            this.MutationRate = mutationRate;
            this.Generations = generations;
            this.Population = new List<Chromosome>();
            this.RandomGen = new Random();
        }

        public void InitializePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                StringBuilder geneBuilder = new StringBuilder();
                for (int j = 0; j < GeneLength; j++)
                {
                    char geneChar = (char)('a' + RandomGen.Next(26));
                    geneBuilder.Append(geneChar);
                }

                Population.Add(new Chromosome(geneBuilder.ToString()));
            }
        }

        public void EvaluateFitness()
        {
            double target = 42.0;
            foreach (Chromosome chromosome in Population)
            {
                double result = ProgramInterpreter.Execute(chromosome.Gene);
                double error = Math.Abs(result - target);
                chromosome.Fitness = Math.Max(0, 100.0 - error);
            }
        }

        public Chromosome TournamentSelection(int tournamentSize)
        {
            int index = RandomGen.Next(Population.Count);
            Chromosome best = Population[index].Clone();
            for (int i = 1; i < tournamentSize; i++)
            {
                int idx = RandomGen.Next(Population.Count);
                Chromosome contender = Population[idx];
                if (contender.Fitness > best.Fitness)
                {
                    best = contender.Clone();
                }
            }

            return best;
        }

        public void Evolve()
        {
            InitializePopulation();
            EvaluateFitness();
            for (int generation = 0; generation < Generations; generation++)
            {
                List<Chromosome> newPopulation = new List<Chromosome>();
                while (newPopulation.Count < PopulationSize)
                {
                    Chromosome parent1 = TournamentSelection(3);
                    Chromosome parent2 = TournamentSelection(3);
                    Chromosome child = Chromosome.Crossover(parent1, parent2);
                    child = child.Mutate(MutationRate);
                    newPopulation.Add(child);
                }

                Population = newPopulation;
                EvaluateFitness();
            }
        }
    }
}