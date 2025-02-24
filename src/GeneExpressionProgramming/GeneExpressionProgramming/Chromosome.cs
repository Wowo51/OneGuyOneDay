using System;
using System.Text;

namespace GeneExpressionProgramming
{
    public class Chromosome
    {
        public string Gene { get; set; }
        public double Fitness { get; set; }

        private static readonly Random s_random = new Random();
        public Chromosome(string gene)
        {
            if (gene == null)
            {
                gene = "";
            }

            this.Gene = gene;
            this.Fitness = 0;
        }

        public Chromosome Clone()
        {
            Chromosome clone = new Chromosome(this.Gene);
            clone.Fitness = this.Fitness;
            return clone;
        }

        public static Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            if (parent1.Gene.Length == 0 || parent2.Gene.Length == 0)
            {
                return parent1.Clone();
            }

            int minLen = Math.Min(parent1.Gene.Length, parent2.Gene.Length);
            int crossPoint = (minLen > 1) ? 1 + s_random.Next(minLen - 1) : 1;
            string newGene = parent1.Gene.Substring(0, crossPoint) + parent2.Gene.Substring(crossPoint);
            Chromosome child = new Chromosome(newGene);
            return child;
        }

        public Chromosome Mutate(double mutationRate)
        {
            StringBuilder geneBuilder = new StringBuilder(this.Gene);
            for (int i = 0; i < geneBuilder.Length; i++)
            {
                if (s_random.NextDouble() < mutationRate)
                {
                    char mutatedChar = (char)('a' + s_random.Next(26));
                    geneBuilder[i] = mutatedChar;
                }
            }

            return new Chromosome(geneBuilder.ToString());
        }
    }
}