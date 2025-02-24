using System;
using System.Collections.Generic;
using System.Text;

namespace LocalitySensitiveHashing
{
    public class LSH
    {
        private int dimensions;
        private int hashSize;
        private List<List<double>> randomProjections;
        public LSH(int dimensions, int hashSize)
        {
            this.dimensions = dimensions;
            this.hashSize = hashSize;
            this.randomProjections = new List<List<double>>();
            Random random = new Random();
            for (int i = 0; i < hashSize; i++)
            {
                List<double> projection = new List<double>();
                for (int j = 0; j < dimensions; j++)
                {
                    double sample = GenerateGaussian(random);
                    projection.Add(sample);
                }

                randomProjections.Add(projection);
            }
        }

        private double GenerateGaussian(Random random)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        }

        public string ComputeSignature(List<double> vector)
        {
            if (vector == null || vector.Count != dimensions)
            {
                return "";
            }

            StringBuilder signatureBuilder = new StringBuilder();
            for (int i = 0; i < hashSize; i++)
            {
                double dotProduct = 0;
                for (int j = 0; j < dimensions; j++)
                {
                    dotProduct += vector[j] * randomProjections[i][j];
                }

                signatureBuilder.Append(dotProduct >= 0 ? "1" : "0");
            }

            return signatureBuilder.ToString();
        }
    }
}