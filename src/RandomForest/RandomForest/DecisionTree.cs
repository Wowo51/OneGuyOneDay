using System.Collections.Generic;

namespace RandomForest
{
    public class DecisionTree
    {
        private int? _predictedLabel;
        public void Train(IList<DataPoint> trainingData)
        {
            if (trainingData == null || trainingData.Count == 0)
            {
                _predictedLabel = 0;
                return;
            }

            Dictionary<int, int> frequency = new Dictionary<int, int>();
            foreach (DataPoint data in trainingData)
            {
                if (frequency.ContainsKey(data.Label))
                {
                    frequency[data.Label] = frequency[data.Label] + 1;
                }
                else
                {
                    frequency.Add(data.Label, 1);
                }
            }

            int mostCommon = 0;
            int maxCount = -1;
            foreach (KeyValuePair<int, int> kvp in frequency)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostCommon = kvp.Key;
                }
            }

            _predictedLabel = mostCommon;
        }

        public int Predict(double[] features)
        {
            return _predictedLabel.HasValue ? _predictedLabel.Value : 0;
        }
    }
}