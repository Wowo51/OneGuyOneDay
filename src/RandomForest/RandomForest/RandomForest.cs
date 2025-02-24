using System;
using System.Collections.Generic;

namespace RandomForest
{
    public class RandomForest
    {
        private List<DecisionTree> _trees;
        private Random _random;
        public int NumTrees
        {
            get
            {
                return _trees.Count;
            }
        }

        public RandomForest(int numberOfTrees)
        {
            _trees = new List<DecisionTree>();
            for (int i = 0; i < numberOfTrees; i++)
            {
                _trees.Add(new DecisionTree());
            }

            _random = new Random();
        }

        public void Train(IList<DataPoint> trainingData)
        {
            if (trainingData == null || trainingData.Count == 0)
            {
                return;
            }

            foreach (DecisionTree tree in _trees)
            {
                List<DataPoint> sample = GetBootstrapSample(trainingData);
                tree.Train(sample);
            }
        }

        public int Predict(double[] features)
        {
            Dictionary<int, int> votes = new Dictionary<int, int>();
            foreach (DecisionTree tree in _trees)
            {
                int prediction = tree.Predict(features);
                if (votes.ContainsKey(prediction))
                {
                    votes[prediction] = votes[prediction] + 1;
                }
                else
                {
                    votes.Add(prediction, 1);
                }
            }

            int bestLabel = 0;
            int maxVotes = -1;
            foreach (KeyValuePair<int, int> vote in votes)
            {
                if (vote.Value > maxVotes)
                {
                    maxVotes = vote.Value;
                    bestLabel = vote.Key;
                }
            }

            return bestLabel;
        }

        private List<DataPoint> GetBootstrapSample(IList<DataPoint> trainingData)
        {
            int n = trainingData.Count;
            List<DataPoint> sample = new List<DataPoint>();
            for (int i = 0; i < n; i++)
            {
                int index = _random.Next(n);
                sample.Add(trainingData[index]);
            }

            return sample;
        }
    }
}