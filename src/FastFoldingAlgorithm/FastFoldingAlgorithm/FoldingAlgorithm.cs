using System;
using System.Collections.Generic;

namespace FastFoldingAlgorithm
{
    public class FoldingAlgorithm
    {
        public List<int> DetectApproximatePeriod(double[] timeSeries, int minPeriod, int maxPeriod, double threshold)
        {
            List<int> detectedPeriods = new List<int>();
            if (timeSeries == null || timeSeries.Length == 0)
            {
                return detectedPeriods;
            }

            for (int period = minPeriod; period <= maxPeriod; period++)
            {
                if (period <= 0)
                {
                    continue;
                }

                double[] foldedProfile = FoldTimeSeries(timeSeries, period);
                double maxValue = ComputeMax(foldedProfile);
                if (maxValue > threshold)
                {
                    detectedPeriods.Add(period);
                }
            }

            return detectedPeriods;
        }

        public double[] FoldTimeSeries(double[] timeSeries, int period)
        {
            double[] foldProfile = new double[period];
            for (int i = 0; i < period; i++)
            {
                foldProfile[i] = 0;
            }

            for (int j = 0; j < timeSeries.Length; j++)
            {
                int index = j % period;
                foldProfile[index] += timeSeries[j];
            }

            return foldProfile;
        }

        private double ComputeMax(double[] array)
        {
            double max = double.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }
    }
}