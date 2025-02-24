using System;
using System.Collections.Generic;

namespace LPBoost
{
    public class LPBoostAlgorithm
    {
        private List<double> trainingData;
        private double optimalMargin;
        public LPBoostAlgorithm()
        {
            this.trainingData = new List<double>();
            this.optimalMargin = 0.0;
        }

        public void Train(double[] data)
        {
            if (data == null)
            {
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                this.trainingData.Add(data[i]);
            }

            RunLPBoostAlgorithm();
        }

        // Uses LinearProgramSolver to compute an optimal margin via LP formulation
        private void RunLPBoostAlgorithm()
        {
            if (this.trainingData.Count == 0)
            {
                this.optimalMargin = 0.0;
                return;
            }

            this.optimalMargin = LinearProgramSolver.Solve(this.trainingData);
        }

        public double Predict(double input)
        {
            return this.optimalMargin * input;
        }
    }
}