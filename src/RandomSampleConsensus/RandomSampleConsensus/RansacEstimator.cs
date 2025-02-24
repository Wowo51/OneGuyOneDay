using System;
using System.Collections.Generic;

namespace RandomSampleConsensus
{
    public class RansacEstimator<TModel, TData>
    {
        public delegate TModel ModelEstimatorDelegate(List<TData> sample);
        public delegate double ErrorEvaluatorDelegate(TModel model, TData data);
        public int MaxIterations { get; set; }
        public double Threshold { get; set; }
        public int SampleSize { get; set; }
        public int ConsensusThreshold { get; set; }

        public RansacEstimator(int maxIterations, double threshold, int sampleSize, int consensusThreshold)
        {
            this.MaxIterations = maxIterations;
            this.Threshold = threshold;
            this.SampleSize = sampleSize;
            this.ConsensusThreshold = consensusThreshold;
        }

        public TModel? Estimate(List<TData> data, ModelEstimatorDelegate modelEstimator, ErrorEvaluatorDelegate errorEvaluator)
        {
            if (data == null || data.Count < this.SampleSize)
            {
                return default;
            }

            TModel? bestModel = default;
            int bestConsensus = 0;
            Random random = new Random();
            for (int iteration = 0; iteration < this.MaxIterations; iteration++)
            {
                List<TData> sample = new List<TData>();
                List<TData> tempData = new List<TData>(data);
                for (int i = 0; i < this.SampleSize; i++)
                {
                    int index = random.Next(tempData.Count);
                    sample.Add(tempData[index]);
                    tempData.RemoveAt(index);
                }

                TModel model = modelEstimator(sample);
                List<TData> inliers = new List<TData>();
                foreach (TData datum in data)
                {
                    double error = errorEvaluator(model, datum);
                    if (error < this.Threshold)
                    {
                        inliers.Add(datum);
                    }
                }

                if (inliers.Count > bestConsensus && inliers.Count >= this.ConsensusThreshold)
                {
                    bestConsensus = inliers.Count;
                    bestModel = model;
                }
            }

            return bestModel;
        }
    }
}