using System;
using System.Collections.Generic;

namespace ID3Algorithm
{
    public class Example
    {
        public Dictionary<string, string> Attributes { get; set; }
        public string Label { get; set; }

        public Example(Dictionary<string, string> attributes, string label)
        {
            if (attributes == null)
            {
                this.Attributes = new Dictionary<string, string>();
            }
            else
            {
                this.Attributes = attributes;
            }

            this.Label = label;
        }
    }

    public class TreeNode
    {
        public bool IsLeaf { get; set; }
        public string? Label { get; set; }
        public string? Attribute { get; set; }
        public Dictionary<string, TreeNode>? Children { get; set; }

        public static TreeNode CreateLeaf(string label)
        {
            TreeNode node = new TreeNode();
            node.IsLeaf = true;
            node.Label = label;
            return node;
        }

        public static TreeNode CreateDecisionNode(string attribute, Dictionary<string, TreeNode> children)
        {
            TreeNode node = new TreeNode();
            node.IsLeaf = false;
            node.Attribute = attribute;
            node.Children = children;
            return node;
        }
    }

    public class ID3
    {
        public static TreeNode BuildTree(IList<Example> examples, IList<string> attributes)
        {
            if (examples.Count == 0)
            {
                return TreeNode.CreateLeaf("Unknown");
            }

            bool allSame = true;
            string firstLabel = examples[0].Label;
            foreach (Example ex in examples)
            {
                if (ex.Label != firstLabel)
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame)
            {
                return TreeNode.CreateLeaf(firstLabel);
            }

            if (attributes.Count == 0)
            {
                return TreeNode.CreateLeaf(MajorityLabel(examples));
            }

            string bestAttribute = attributes[0];
            double bestGain = InfoGain(examples, bestAttribute);
            foreach (string attribute in attributes)
            {
                double gain = InfoGain(examples, attribute);
                if (gain > bestGain)
                {
                    bestGain = gain;
                    bestAttribute = attribute;
                }
            }

            // Heuristic: if the best information gain is not positive, stop splitting.
            if (bestGain <= 0.0)
            {
                return TreeNode.CreateLeaf(MajorityLabel(examples));
            }

            Dictionary<string, TreeNode> children = new Dictionary<string, TreeNode>();
            HashSet<string> values = new HashSet<string>();
            foreach (Example ex in examples)
            {
                string value = "";
                if (ex.Attributes.TryGetValue(bestAttribute, out string? attrValue))
                {
                    value = attrValue;
                }

                values.Add(value);
            }

            List<string> remainingAttributes = new List<string>(attributes);
            remainingAttributes.Remove(bestAttribute);
            foreach (string value in values)
            {
                List<Example> subset = new List<Example>();
                foreach (Example ex in examples)
                {
                    string currentValue = "";
                    if (ex.Attributes.TryGetValue(bestAttribute, out string? attrValue))
                    {
                        currentValue = attrValue;
                    }

                    if (currentValue == value)
                    {
                        subset.Add(ex);
                    }
                }

                if (subset.Count == 0)
                {
                    children[value] = TreeNode.CreateLeaf(MajorityLabel(examples));
                }
                else
                {
                    children[value] = BuildTree(subset, remainingAttributes);
                }
            }

            return TreeNode.CreateDecisionNode(bestAttribute, children);
        }

        private static double Entropy(IList<Example> examples)
        {
            Dictionary<string, int> labelCounts = new Dictionary<string, int>();
            foreach (Example ex in examples)
            {
                if (labelCounts.ContainsKey(ex.Label))
                {
                    labelCounts[ex.Label] = labelCounts[ex.Label] + 1;
                }
                else
                {
                    labelCounts[ex.Label] = 1;
                }
            }

            double entropy = 0.0;
            int total = examples.Count;
            foreach (KeyValuePair<string, int> pair in labelCounts)
            {
                double probability = (double)pair.Value / total;
                if (probability > 0.0)
                {
                    entropy -= probability * Math.Log(probability, 2.0);
                }
            }

            return entropy;
        }

        private static double InfoGain(IList<Example> examples, string attribute)
        {
            double totalEntropy = Entropy(examples);
            Dictionary<string, List<Example>> subsets = new Dictionary<string, List<Example>>();
            foreach (Example ex in examples)
            {
                string key = "";
                if (ex.Attributes.TryGetValue(attribute, out string? attrValue))
                {
                    key = attrValue;
                }

                if (!subsets.ContainsKey(key))
                {
                    subsets[key] = new List<Example>();
                }

                subsets[key].Add(ex);
            }

            double subsetEntropy = 0.0;
            int total = examples.Count;
            foreach (KeyValuePair<string, List<Example>> pair in subsets)
            {
                double weight = (double)pair.Value.Count / total;
                subsetEntropy += weight * Entropy(pair.Value);
            }

            return totalEntropy - subsetEntropy;
        }

        private static string MajorityLabel(IList<Example> examples)
        {
            Dictionary<string, int> labelCounts = new Dictionary<string, int>();
            foreach (Example ex in examples)
            {
                if (labelCounts.ContainsKey(ex.Label))
                {
                    labelCounts[ex.Label] = labelCounts[ex.Label] + 1;
                }
                else
                {
                    labelCounts[ex.Label] = 1;
                }
            }

            string majority = "";
            int maxCount = -1;
            foreach (KeyValuePair<string, int> pair in labelCounts)
            {
                if (pair.Value > maxCount)
                {
                    majority = pair.Key;
                    maxCount = pair.Value;
                }
            }

            return majority;
        }
    }
}