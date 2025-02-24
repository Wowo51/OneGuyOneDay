using System;
using System.Collections.Generic;

namespace SchreierSimsAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int domainSize = 3;
            Permutation generator1 = new Permutation(new int[] { 1, 0, 2 });
            Permutation generator2 = new Permutation(new int[] { 0, 2, 1 });
            List<Permutation> generators = new List<Permutation>();
            generators.Add(generator1);
            generators.Add(generator2);
            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(generators, domainSize);
            Console.WriteLine("Base Points:");
            for (int i = 0; i < basePoints.Count; i++)
            {
                Console.WriteLine(basePoints[i]);
            }

            Console.WriteLine("Strong Generators:");
            for (int i = 0; i < strongGenerators.Count; i++)
            {
                Console.WriteLine(strongGenerators[i].ToString());
            }
        }
    }
}