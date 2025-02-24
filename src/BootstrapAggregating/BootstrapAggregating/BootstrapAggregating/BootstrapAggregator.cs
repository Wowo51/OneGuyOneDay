using System;
using System.Collections.Generic;

namespace BootstrapAggregating
{
    public class BootstrapAggregator : IPredictor
    {
        private readonly IList<IPredictor> predictors;
        public BootstrapAggregator(IList<IPredictor> predictors)
        {
            if (predictors == null)
            {
                this.predictors = new List<IPredictor>();
            }
            else
            {
                this.predictors = predictors;
            }
        }

        public double Predict(double[] input)
        {
            if (input == null)
            {
                return 0;
            }

            double sum = 0;
            int count = 0;
            for (int i = 0; i < predictors.Count; i++)
            {
                IPredictor predictor = predictors[i];
                if (predictor != null)
                {
                    sum += predictor.Predict(input);
                    count++;
                }
            }

            if (count == 0)
            {
                return 0;
            }

            return sum / count;
        }
    }
}