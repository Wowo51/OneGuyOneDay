using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithmEngine
    {
        public int PopulationSize { get; set; }
        public int IndividualLength { get; set; }
        public int MaxGenerations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
        private Random RandomInstance { get; set; }

        public GeneticAlgorithmEngine(int populationSize, int individualLength, int maxGenerations, double crossoverRate, double mutationRate)
        {
            this.PopulationSize = populationSize;
            this.IndividualLength = individualLength;
            this.MaxGenerations = maxGenerations;
            this.CrossoverRate = crossoverRate;
            this.MutationRate = mutationRate;
            this.RandomInstance = new Random();
        }

        public BinaryIndividual Run()
        {
            List<BinaryIndividual> population = InitializePopulation();
            EvaluatePopulation(population);
            BinaryIndividual best = GetBest(population);
            int generation = 0;
            while (generation < this.MaxGenerations)
            {
                List<BinaryIndividual> newPopulation = new List<BinaryIndividual>();
                while (newPopulation.Count < this.PopulationSize)
                {
                    BinaryIndividual parent1 = TournamentSelection(population);
                    BinaryIndividual parent2 = TournamentSelection(population);
                    BinaryIndividual offspring1;
                    BinaryIndividual offspring2;
                    if (this.RandomInstance.NextDouble() < this.CrossoverRate)
                    {
                        Tuple<BinaryIndividual, BinaryIndividual> offspringPair = Crossover(parent1, parent2);
                        offspring1 = offspringPair.Item1;
                        offspring2 = offspringPair.Item2;
                    }
                    else
                    {
                        offspring1 = parent1.Clone();
                        offspring2 = parent2.Clone();
                    }

                    Mutate(offspring1);
                    Mutate(offspring2);
                    offspring1.EvaluateFitness();
                    offspring2.EvaluateFitness();
                    newPopulation.Add(offspring1);
                    if (newPopulation.Count < this.PopulationSize)
                    {
                        newPopulation.Add(offspring2);
                    }
                }

                population = newPopulation;
                BinaryIndividual generationBest = GetBest(population);
                if (generationBest.Fitness > best.Fitness)
                {
                    best = generationBest;
                }

                generation = generation + 1;
            }

            return best;
        }

        private List<BinaryIndividual> InitializePopulation()
        {
            List<BinaryIndividual> population = new List<BinaryIndividual>();
            for (int i = 0; i < this.PopulationSize; i++)
            {
                BinaryIndividual individual = new BinaryIndividual(this.IndividualLength);
                individual.Randomize(this.RandomInstance);
                individual.EvaluateFitness();
                population.Add(individual);
            }

            return population;
        }

        private void EvaluatePopulation(List<BinaryIndividual> population)
        {
            foreach (BinaryIndividual ind in population)
            {
                ind.EvaluateFitness();
            }
        }

        private BinaryIndividual TournamentSelection(List<BinaryIndividual> population)
        {
            int tournamentSize = 3;
            BinaryIndividual best = population[this.RandomInstance.Next(population.Count)];
            for (int i = 1; i < tournamentSize; i++)
            {
                int index = this.RandomInstance.Next(population.Count);
                BinaryIndividual contender = population[index];
                if (contender.Fitness > best.Fitness)
                {
                    best = contender;
                }
            }

            return best.Clone();
        }

        private Tuple<BinaryIndividual, BinaryIndividual> Crossover(BinaryIndividual parent1, BinaryIndividual parent2)
        {
            int length = parent1.Genes.Length;
            if (length < 2)
            {
                return new Tuple<BinaryIndividual, BinaryIndividual>(parent1.Clone(), parent2.Clone());
            }

            int crossoverPoint = this.RandomInstance.Next(1, length - 1);
            BinaryIndividual offspring1 = parent1.Clone();
            BinaryIndividual offspring2 = parent2.Clone();
            for (int i = crossoverPoint; i < length; i++)
            {
                bool temp = offspring1.Genes[i];
                offspring1.Genes[i] = offspring2.Genes[i];
                offspring2.Genes[i] = temp;
            }

            return new Tuple<BinaryIndividual, BinaryIndividual>(offspring1, offspring2);
        }

        private void Mutate(BinaryIndividual individual)
        {
            int length = individual.Genes.Length;
            for (int i = 0; i < length; i++)
            {
                if (this.RandomInstance.NextDouble() < this.MutationRate)
                {
                    individual.Genes[i] = !individual.Genes[i];
                }
            }
        }

        private BinaryIndividual GetBest(List<BinaryIndividual> population)
        {
            BinaryIndividual best = population[0];
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i].Fitness > best.Fitness)
                {
                    best = population[i];
                }
            }

            return best.Clone();
        }
    }

    public class BinaryIndividual
    {
        public bool[] Genes { get; set; }
        public int Fitness { get; set; }

        public BinaryIndividual(int length)
        {
            this.Genes = new bool[length];
        }

        public void Randomize(Random randomInstance)
        {
            for (int i = 0; i < this.Genes.Length; i++)
            {
                this.Genes[i] = (randomInstance.NextDouble() < 0.5);
            }
        }

        public void EvaluateFitness()
        {
            int count = 0;
            for (int i = 0; i < this.Genes.Length; i++)
            {
                if (this.Genes[i])
                {
                    count = count + 1;
                }
            }

            this.Fitness = count;
        }

        public BinaryIndividual Clone()
        {
            BinaryIndividual clone = new BinaryIndividual(this.Genes.Length);
            for (int i = 0; i < this.Genes.Length; i++)
            {
                clone.Genes[i] = this.Genes[i];
            }

            clone.Fitness = this.Fitness;
            return clone;
        }
    }
}