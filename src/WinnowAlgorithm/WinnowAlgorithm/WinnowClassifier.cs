using System;

namespace WinnowAlgorithm
{
    public class WinnowClassifier
    {
        private readonly double[] _weights;
        private readonly double _threshold;
        private readonly double _alpha;
        public WinnowClassifier(int featureCount, double threshold, double alpha)
        {
            _weights = new double[featureCount];
            int index;
            for (index = 0; index < featureCount; index++)
            {
                _weights[index] = 1.0;
            }

            _threshold = threshold;
            _alpha = alpha;
        }

        public bool Predict(bool[] features)
        {
            if (features == null)
            {
                return false;
            }

            double sum = 0.0;
            int length = features.Length;
            int index;
            for (index = 0; index < length; index++)
            {
                if (features[index])
                {
                    sum += _weights[index];
                }
            }

            return sum >= _threshold;
        }

        public void Update(bool[] features, bool label)
        {
            if (features == null)
            {
                return;
            }

            bool prediction = Predict(features);
            if (prediction == label)
            {
                return;
            }

            int index;
            if (label && !prediction)
            {
                for (index = 0; index < features.Length; index++)
                {
                    if (features[index])
                    {
                        _weights[index] *= _alpha;
                    }
                }
            }
            else if (!label && prediction)
            {
                for (index = 0; index < features.Length; index++)
                {
                    if (features[index])
                    {
                        _weights[index] /= _alpha;
                    }
                }
            }
        }

        public double[] GetWeights()
        {
            int length = _weights.Length;
            double[] copy = new double[length];
            int index;
            for (index = 0; index < length; index++)
            {
                copy[index] = _weights[index];
            }

            return copy;
        }
    }
}