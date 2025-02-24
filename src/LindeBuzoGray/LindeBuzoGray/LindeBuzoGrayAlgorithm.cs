using System;
using System.Collections.Generic;

namespace LindeBuzoGray
{
    public static class LindeBuzoGrayAlgorithm
    {
        public static List<double[]> GenerateCodebook(List<double[]> trainingData, int codebookSize, double epsilon, double convergenceThreshold)
        {
            if (trainingData == null || trainingData.Count == 0)
            {
                return new List<double[]>();
            }

            if (codebookSize < 1)
            {
                return new List<double[]>();
            }

            int dimension = trainingData[0].Length;
            double[] centroid = new double[dimension];
            int validCount = 0;
            for (int i = 0; i < trainingData.Count; i++)
            {
                double[] vector = trainingData[i];
                if (vector == null || vector.Length != dimension)
                {
                    continue;
                }

                validCount++;
                for (int j = 0; j < dimension; j++)
                {
                    centroid[j] += vector[j];
                }
            }

            if (validCount > 0)
            {
                for (int j = 0; j < dimension; j++)
                {
                    centroid[j] /= validCount;
                }
            }

            List<double[]> codebook = new List<double[]>
            {
                centroid
            };
            while (codebook.Count < codebookSize)
            {
                if (codebook.Count * 2 <= codebookSize)
                {
                    List<double[]> newCodebook = new List<double[]>();
                    for (int i = 0; i < codebook.Count; i++)
                    {
                        double[] currentCentroid = codebook[i];
                        double[] plus = new double[dimension];
                        double[] minus = new double[dimension];
                        for (int j = 0; j < dimension; j++)
                        {
                            plus[j] = currentCentroid[j] * (1 + epsilon);
                            minus[j] = currentCentroid[j] * (1 - epsilon);
                        }

                        newCodebook.Add(plus);
                        newCodebook.Add(minus);
                    }

                    codebook = RefineCodebook(trainingData, newCodebook, convergenceThreshold);
                }
                else
                {
                    int splitsNeeded = codebookSize - codebook.Count;
                    double[] distortions = new double[codebook.Count];
                    int[] assignments = new int[trainingData.Count];
                    for (int i = 0; i < trainingData.Count; i++)
                    {
                        double[] vector = trainingData[i];
                        if (vector == null || vector.Length != dimension)
                        {
                            continue;
                        }

                        int bestIndex = 0;
                        double bestDistance = DistanceSquared(vector, codebook[0]);
                        for (int j = 1; j < codebook.Count; j++)
                        {
                            double distance = DistanceSquared(vector, codebook[j]);
                            if (distance < bestDistance)
                            {
                                bestDistance = distance;
                                bestIndex = j;
                            }
                        }

                        assignments[i] = bestIndex;
                        distortions[bestIndex] += bestDistance;
                    }

                    List<int> indicesToSplit = new List<int>();
                    for (int i = 0; i < splitsNeeded; i++)
                    {
                        int maxIndex = 0;
                        double maxDist = distortions[0];
                        for (int j = 1; j < distortions.Length; j++)
                        {
                            if (distortions[j] > maxDist)
                            {
                                maxDist = distortions[j];
                                maxIndex = j;
                            }
                        }

                        indicesToSplit.Add(maxIndex);
                        distortions[maxIndex] = -1;
                    }

                    List<double[]> newCodebook = new List<double[]>();
                    for (int i = 0; i < codebook.Count; i++)
                    {
                        if (indicesToSplit.Contains(i))
                        {
                            double[] currentCentroid = codebook[i];
                            double[] plus = new double[dimension];
                            double[] minus = new double[dimension];
                            for (int j = 0; j < dimension; j++)
                            {
                                plus[j] = currentCentroid[j] * (1 + epsilon);
                                minus[j] = currentCentroid[j] * (1 - epsilon);
                            }

                            newCodebook.Add(plus);
                            newCodebook.Add(minus);
                        }
                        else
                        {
                            newCodebook.Add(codebook[i]);
                        }
                    }

                    while (newCodebook.Count > codebookSize)
                    {
                        newCodebook.RemoveAt(newCodebook.Count - 1);
                    }

                    codebook = RefineCodebook(trainingData, newCodebook, convergenceThreshold);
                }
            }

            return codebook;
        }

        private static List<double[]> RefineCodebook(List<double[]> trainingData, List<double[]> codebook, double convergenceThreshold)
        {
            int dimension = codebook[0].Length;
            List<double[]> refinedCodebook = new List<double[]>();
            const int maxIterations = 100;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                List<double[]> newCodebook = new List<double[]>();
                for (int i = 0; i < codebook.Count; i++)
                {
                    double[] zeroVector = new double[dimension];
                    newCodebook.Add(zeroVector);
                }

                int[] counts = new int[codebook.Count];
                for (int i = 0; i < trainingData.Count; i++)
                {
                    double[] vector = trainingData[i];
                    if (vector == null || vector.Length != dimension)
                    {
                        continue;
                    }

                    int bestIndex = 0;
                    double bestDistance = DistanceSquared(vector, codebook[0]);
                    for (int j = 1; j < codebook.Count; j++)
                    {
                        double distance = DistanceSquared(vector, codebook[j]);
                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestIndex = j;
                        }
                    }

                    counts[bestIndex]++;
                    for (int d = 0; d < dimension; d++)
                    {
                        newCodebook[bestIndex][d] += vector[d];
                    }
                }

                double maxChange = 0;
                for (int i = 0; i < newCodebook.Count; i++)
                {
                    if (counts[i] > 0)
                    {
                        for (int d = 0; d < dimension; d++)
                        {
                            newCodebook[i][d] /= counts[i];
                        }
                    }

                    double change = DistanceSquared(codebook[i], newCodebook[i]);
                    if (change > maxChange)
                    {
                        maxChange = change;
                    }
                }

                codebook = new List<double[]>();
                for (int i = 0; i < newCodebook.Count; i++)
                {
                    double[] centroidCopy = new double[dimension];
                    for (int d = 0; d < dimension; d++)
                    {
                        centroidCopy[d] = newCodebook[i][d];
                    }

                    codebook.Add(centroidCopy);
                }

                if (maxChange < convergenceThreshold)
                {
                    refinedCodebook = codebook;
                    break;
                }

                refinedCodebook = codebook;
            }

            return refinedCodebook;
        }

        private static double DistanceSquared(double[] vectorA, double[] vectorB)
        {
            int length = vectorA.Length;
            double sum = 0;
            for (int i = 0; i < length; i++)
            {
                double diff = vectorA[i] - vectorB[i];
                sum += diff * diff;
            }

            return sum;
        }
    }
}