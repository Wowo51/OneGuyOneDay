using System;

namespace StructuredSvmPredictor
{
    public class Svm
    {
        private double[] Weights;
        public Svm()
        {
            this.Weights = new double[0];
        }

        public virtual double Predict(double[] features)
        {
            if (features == null)
            {
                return 0.0;
            }

            double result = 0.0;
            for (int i = 0; i < features.Length; i++)
            {
                double weight = (i < this.Weights.Length) ? this.Weights[i] : 0;
                result += features[i] * weight;
            }

            return result;
        }
    }

    public class StructuredSvm : Svm
    {
        public StructuredSvm() : base()
        {
        }

        public string PredictStructured(double[] features)
        {
            if (features == null)
            {
                return "Invalid input";
            }

            double basePrediction = base.Predict(features);
            return basePrediction >= 0.0 ? "Positive Label" : "Negative Label";
        }
    }
}