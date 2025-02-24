using System;
using System.Collections.Generic;

namespace VectorQuantization
{
    public class VectorQuantizer
    {
        public List<double[]> Train(List<double[]> data, int codebookSize, int iterations)
        {
            if (data == null)
            {
                return new List<double[]>();
            }

            int dataCount = data.Count;
            int effectiveCodebookSize = codebookSize;
            if (codebookSize > dataCount)
            {
                effectiveCodebookSize = dataCount;
            }

            List<double[]> codebook = InitializeCodebook(data, effectiveCodebookSize);
            for (int iter = 0; iter < iterations; iter++)
            {
                List<int> assignments = new List<int>();
                for (int i = 0; i < dataCount; i++)
                {
                    int nearestIdx = FindNearestCodebookIndex(data[i], codebook);
                    assignments.Add(nearestIdx);
                }

                List<double[]> newCodebook = new List<double[]>();
                for (int j = 0; j < effectiveCodebookSize; j++)
                {
                    List<double[]> clusterData = new List<double[]>();
                    for (int i = 0; i < dataCount; i++)
                    {
                        if (assignments[i] == j)
                        {
                            clusterData.Add(data[i]);
                        }
                    }

                    if (clusterData.Count == 0)
                    {
                        int index = j % dataCount;
                        newCodebook.Add(data[index]);
                    }
                    else
                    {
                        int dimension = clusterData[0].Length;
                        double[] centroid = new double[dimension];
                        for (int d = 0; d < dimension; d++)
                        {
                            double sum = 0;
                            for (int i = 0; i < clusterData.Count; i++)
                            {
                                sum += clusterData[i][d];
                            }

                            centroid[d] = sum / clusterData.Count;
                        }

                        newCodebook.Add(centroid);
                    }
                }

                codebook = newCodebook;
            }

            return codebook;
        }

        public List<int> Quantize(List<double[]> data, List<double[]> codebook)
        {
            List<int> indices = new List<int>();
            if (data == null || codebook == null)
            {
                return indices;
            }

            for (int i = 0; i < data.Count; i++)
            {
                int nearestIndex = FindNearestCodebookIndex(data[i], codebook);
                indices.Add(nearestIndex);
            }

            return indices;
        }

        private List<double[]> InitializeCodebook(List<double[]> data, int codebookSize)
        {
            List<double[]> codebook = new List<double[]>();
            for (int i = 0; i < codebookSize; i++)
            {
                codebook.Add(data[i]);
            }

            return codebook;
        }

        private int FindNearestCodebookIndex(double[] vector, List<double[]> codebook)
        {
            int nearestIndex = 0;
            double minimumDistance = ComputeEuclideanDistanceSquared(vector, codebook[0]);
            for (int j = 1; j < codebook.Count; j++)
            {
                double distance = ComputeEuclideanDistanceSquared(vector, codebook[j]);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestIndex = j;
                }
            }

            return nearestIndex;
        }

        private double ComputeEuclideanDistanceSquared(double[] vector1, double[] vector2)
        {
            double sum = 0;
            int dimension = vector1.Length;
            for (int i = 0; i < dimension; i++)
            {
                double diff = vector1[i] - vector2[i];
                sum += diff * diff;
            }

            return sum;
        }
    }
}