using System;
using System.Collections.Generic;
using System.Linq;

namespace C45Algorithm
{
    public class C45Algorithm
    {
        public DecisionTreeNode BuildTree(double[][] features, string[] labels, bool[] isContinuous, string[] featureNames)
        {
            int sampleCount = labels.Length;
            List<int> indices = new List<int>();
            for (int i = 0; i < sampleCount; i++)
            {
                indices.Add(i);
            }

            bool[] usedAttributes = new bool[featureNames.Length];
            return BuildTreeInternal(features, labels, isContinuous, featureNames, indices, usedAttributes);
        }

        private DecisionTreeNode BuildTreeInternal(double[][] features, string[] labels, bool[] isContinuous, string[] featureNames, List<int> indices, bool[] usedAttributes)
        {
            if (AllLabelsSame(indices, labels))
            {
                DecisionTreeNode leaf = new DecisionTreeNode();
                leaf.IsLeaf = true;
                leaf.ClassLabel = labels[indices[0]];
                return leaf;
            }

            if (indices.Count == 0)
            {
                DecisionTreeNode fallbackLeaf = new DecisionTreeNode();
                fallbackLeaf.IsLeaf = true;
                fallbackLeaf.ClassLabel = "";
                return fallbackLeaf;
            }

            bool attributeAvailable = false;
            for (int i = 0; i < featureNames.Length; i++)
            {
                if (isContinuous[i] || (!isContinuous[i] && !usedAttributes[i]))
                {
                    attributeAvailable = true;
                    break;
                }
            }

            if (!attributeAvailable)
            {
                DecisionTreeNode majorityLeaf = new DecisionTreeNode();
                majorityLeaf.IsLeaf = true;
                majorityLeaf.ClassLabel = MajorityLabel(indices, labels);
                return majorityLeaf;
            }

            double bestGainRatio = 0.0;
            int bestAttributeIndex = -1;
            bool bestIsContinuous = false;
            double bestThreshold = 0.0;
            for (int j = 0; j < featureNames.Length; j++)
            {
                if (isContinuous[j])
                {
                    double currentThreshold;
                    double gainRatio = GetBestContinuousGainRatio(features, labels, indices, j, out currentThreshold);
                    if (gainRatio > bestGainRatio)
                    {
                        bestGainRatio = gainRatio;
                        bestAttributeIndex = j;
                        bestIsContinuous = true;
                        bestThreshold = currentThreshold;
                    }
                }
                else
                {
                    if (usedAttributes[j])
                    {
                        continue;
                    }

                    double gainRatio = GetDiscreteGainRatio(features, labels, indices, j);
                    if (gainRatio > bestGainRatio)
                    {
                        bestGainRatio = gainRatio;
                        bestAttributeIndex = j;
                        bestIsContinuous = false;
                    }
                }
            }

            if (bestAttributeIndex == -1 || bestGainRatio <= 0.0)
            {
                DecisionTreeNode majorityLeaf = new DecisionTreeNode();
                majorityLeaf.IsLeaf = true;
                majorityLeaf.ClassLabel = MajorityLabel(indices, labels);
                return majorityLeaf;
            }

            DecisionTreeNode node = new DecisionTreeNode();
            node.IsLeaf = false;
            node.SplittingFeatureIndex = bestAttributeIndex;
            node.FeatureName = featureNames[bestAttributeIndex];
            node.IsContinuous = bestIsContinuous;
            if (bestIsContinuous)
            {
                node.Threshold = bestThreshold;
                List<int> leftIndices = new List<int>();
                List<int> rightIndices = new List<int>();
                for (int i = 0; i < indices.Count; i++)
                {
                    int index = indices[i];
                    if (features[index][bestAttributeIndex] <= bestThreshold)
                    {
                        leftIndices.Add(index);
                    }
                    else
                    {
                        rightIndices.Add(index);
                    }
                }

                node.Left = BuildTreeInternal(features, labels, isContinuous, featureNames, leftIndices, usedAttributes);
                node.Right = BuildTreeInternal(features, labels, isContinuous, featureNames, rightIndices, usedAttributes);
            }
            else
            {
                bool[] newUsedAttributes = new bool[usedAttributes.Length];
                for (int k = 0; k < usedAttributes.Length; k++)
                {
                    newUsedAttributes[k] = usedAttributes[k];
                }

                newUsedAttributes[bestAttributeIndex] = true;
                Dictionary<string, List<int>> partitions = new Dictionary<string, List<int>>();
                for (int i = 0; i < indices.Count; i++)
                {
                    int index = indices[i];
                    string key = features[index][bestAttributeIndex].ToString();
                    if (!partitions.ContainsKey(key))
                    {
                        partitions[key] = new List<int>();
                    }

                    partitions[key].Add(index);
                }

                foreach (KeyValuePair<string, List<int>> partition in partitions)
                {
                    DecisionTreeNode child = BuildTreeInternal(features, labels, isContinuous, featureNames, partition.Value, newUsedAttributes);
                    node.Children.Add(partition.Key, child);
                }
            }

            return node;
        }

        private bool AllLabelsSame(List<int> indices, string[] labels)
        {
            string firstLabel = labels[indices[0]];
            for (int i = 1; i < indices.Count; i++)
            {
                if (labels[indices[i]] != firstLabel)
                {
                    return false;
                }
            }

            return true;
        }

        private string MajorityLabel(List<int> indices, string[] labels)
        {
            Dictionary<string, int> frequency = new Dictionary<string, int>();
            for (int i = 0; i < indices.Count; i++)
            {
                string label = labels[indices[i]];
                if (!frequency.ContainsKey(label))
                {
                    frequency[label] = 0;
                }

                frequency[label]++;
            }

            int maxCount = -1;
            string majority = "";
            foreach (KeyValuePair<string, int> pair in frequency)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    majority = pair.Key;
                }
            }

            return majority;
        }

        private double ComputeEntropy(List<int> indices, string[] labels)
        {
            Dictionary<string, int> frequency = new Dictionary<string, int>();
            for (int i = 0; i < indices.Count; i++)
            {
                string label = labels[indices[i]];
                if (!frequency.ContainsKey(label))
                {
                    frequency[label] = 0;
                }

                frequency[label]++;
            }

            double entropy = 0.0;
            double total = indices.Count;
            foreach (KeyValuePair<string, int> pair in frequency)
            {
                double proportion = pair.Value / total;
                entropy -= proportion * Math.Log(proportion, 2);
            }

            return entropy;
        }

        private double GetDiscreteGainRatio(double[][] features, string[] labels, List<int> indices, int featureIndex)
        {
            double totalEntropy = ComputeEntropy(indices, labels);
            Dictionary<string, List<int>> partitions = new Dictionary<string, List<int>>();
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                string key = features[index][featureIndex].ToString();
                if (!partitions.ContainsKey(key))
                {
                    partitions[key] = new List<int>();
                }

                partitions[key].Add(index);
            }

            double weightedEntropy = 0.0;
            double splitInfo = 0.0;
            double total = indices.Count;
            foreach (KeyValuePair<string, List<int>> partition in partitions)
            {
                double partitionSize = partition.Value.Count;
                double proportion = partitionSize / total;
                weightedEntropy += proportion * ComputeEntropy(partition.Value, labels);
                splitInfo -= proportion * Math.Log(proportion, 2);
            }

            double infoGain = totalEntropy - weightedEntropy;
            if (splitInfo == 0.0)
            {
                return 0.0;
            }

            return infoGain / splitInfo;
        }

        private double GetBestContinuousGainRatio(double[][] features, string[] labels, List<int> indices, int featureIndex, out double bestThreshold)
        {
            bestThreshold = 0.0;
            List<KeyValuePair<double, string>> values = new List<KeyValuePair<double, string>>();
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                values.Add(new KeyValuePair<double, string>(features[index][featureIndex], labels[index]));
            }

            values.Sort((a, b) => a.Key.CompareTo(b.Key));
            double bestGainRatio = 0.0;
            double totalEntropy = ComputeEntropy(indices, labels);
            double total = indices.Count;
            bool foundCandidate = false;
            for (int i = 0; i < values.Count - 1; i++)
            {
                if (values[i].Value != values[i + 1].Value)
                {
                    double candidateThreshold = (values[i].Key + values[i + 1].Key) / 2.0;
                    List<int> leftIndices = new List<int>();
                    List<int> rightIndices = new List<int>();
                    for (int j = 0; j < indices.Count; j++)
                    {
                        int index = indices[j];
                        if (features[index][featureIndex] <= candidateThreshold)
                        {
                            leftIndices.Add(index);
                        }
                        else
                        {
                            rightIndices.Add(index);
                        }
                    }

                    if (leftIndices.Count == 0 || rightIndices.Count == 0)
                    {
                        continue;
                    }

                    double leftEntropy = ComputeEntropy(leftIndices, labels);
                    double rightEntropy = ComputeEntropy(rightIndices, labels);
                    double weightedEntropy = (leftIndices.Count / total) * leftEntropy + (rightIndices.Count / total) * rightEntropy;
                    double infoGain = totalEntropy - weightedEntropy;
                    double leftProportion = leftIndices.Count / total;
                    double rightProportion = rightIndices.Count / total;
                    double splitInfo = 0.0;
                    splitInfo -= leftProportion * Math.Log(leftProportion, 2);
                    splitInfo -= rightProportion * Math.Log(rightProportion, 2);
                    if (splitInfo == 0.0)
                    {
                        continue;
                    }

                    double gainRatio = infoGain / splitInfo;
                    if (gainRatio > bestGainRatio)
                    {
                        bestGainRatio = gainRatio;
                        bestThreshold = candidateThreshold;
                        foundCandidate = true;
                    }
                }
            }

            if (!foundCandidate)
            {
                bestGainRatio = 0.0;
            }

            return bestGainRatio;
        }
    }
}