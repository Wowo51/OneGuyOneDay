using System;

namespace GeneticAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GeneticAlgorithmEngine engine = new GeneticAlgorithmEngine(20, 10, 100, 0.7, 0.01);
            BinaryIndividual best = engine.Run();
            Console.WriteLine("Best fitness: " + best.Fitness);
            Console.Write("Genes: ");
            for (int i = 0; i < best.Genes.Length; i++)
            {
                Console.Write(best.Genes[i] ? "1" : "0");
            }

            Console.WriteLine();
        }
    }
}