using System;
using System.Collections.Generic;

namespace InsideOutsideAlgorithm
{
    public class InsideOutsideCalculator
    {
        public static void Reestimate(ProbabilisticGrammar grammar, string[] sentence)
        {
            int n = sentence.Length;
            Dictionary<string, double>[, ] inside = new Dictionary<string, double>[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j <= n; j++)
                {
                    inside[i, j] = new Dictionary<string, double>();
                }
            }

            // Initialization for terminal productions with unary closure
            for (int i = 0; i < n; i++)
            {
                string token = sentence[i];
                for (int r = 0; r < grammar.Rules.Count; r++)
                {
                    GrammarRule rule = grammar.Rules[r];
                    if (rule.RHS.Length == 1 && rule.RHS[0] == token)
                    {
                        if (inside[i, i + 1].ContainsKey(rule.LHS))
                        {
                            inside[i, i + 1][rule.LHS] += rule.Probability;
                        }
                        else
                        {
                            inside[i, i + 1][rule.LHS] = rule.Probability;
                        }
                    }
                }

                ApplyUnaryClosureInside(inside[i, i + 1], grammar);
            }

            // CKY for binary productions with unary closure
            for (int span = 2; span <= n; span++)
            {
                for (int i = 0; i <= n - span; i++)
                {
                    int j = i + span;
                    for (int k = i + 1; k < j; k++)
                    {
                        for (int r = 0; r < grammar.Rules.Count; r++)
                        {
                            GrammarRule rule = grammar.Rules[r];
                            if (rule.RHS.Length == 2)
                            {
                                string B = rule.RHS[0];
                                string C = rule.RHS[1];
                                if (inside[i, k].ContainsKey(B) && inside[k, j].ContainsKey(C))
                                {
                                    double value = rule.Probability * inside[i, k][B] * inside[k, j][C];
                                    if (inside[i, j].ContainsKey(rule.LHS))
                                    {
                                        inside[i, j][rule.LHS] += value;
                                    }
                                    else
                                    {
                                        inside[i, j][rule.LHS] = value;
                                    }
                                }
                            }
                        }
                    }

                    ApplyUnaryClosureInside(inside[i, j], grammar);
                }
            }

            // Initialize outside probabilities
            Dictionary<string, double>[, ] outside = new Dictionary<string, double>[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j <= n; j++)
                {
                    outside[i, j] = new Dictionary<string, double>();
                }
            }

            if (inside[0, n].ContainsKey(grammar.StartSymbol))
            {
                outside[0, n][grammar.StartSymbol] = 1.0;
            }

            ApplyUnaryClosureOutside(outside[0, n], grammar);
            // Backward propagation for binary productions with unary closure
            for (int span = n; span >= 2; span--)
            {
                for (int i = 0; i <= n - span; i++)
                {
                    int j = i + span;
                    Dictionary<string, double> parentDict = new Dictionary<string, double>(outside[i, j]);
                    foreach (KeyValuePair<string, double> parent in parentDict)
                    {
                        string A = parent.Key;
                        double outsideA = parent.Value;
                        for (int r = 0; r < grammar.Rules.Count; r++)
                        {
                            GrammarRule rule = grammar.Rules[r];
                            if (rule.RHS.Length == 2 && rule.LHS == A)
                            {
                                for (int k = i + 1; k < j; k++)
                                {
                                    if (inside[i, k].ContainsKey(rule.RHS[0]) && inside[k, j].ContainsKey(rule.RHS[1]))
                                    {
                                        double contribution = outsideA * rule.Probability;
                                        double addLeft = contribution * inside[k, j][rule.RHS[1]];
                                        if (outside[i, k].ContainsKey(rule.RHS[0]))
                                        {
                                            outside[i, k][rule.RHS[0]] += addLeft;
                                        }
                                        else
                                        {
                                            outside[i, k][rule.RHS[0]] = addLeft;
                                        }

                                        double addRight = contribution * inside[i, k][rule.RHS[0]];
                                        if (outside[k, j].ContainsKey(rule.RHS[1]))
                                        {
                                            outside[k, j][rule.RHS[1]] += addRight;
                                        }
                                        else
                                        {
                                            outside[k, j][rule.RHS[1]] = addRight;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ApplyUnaryClosureOutside(outside[i, j], grammar);
                }
            }

            // Expected counts computation
            Dictionary<GrammarRule, double> ruleCounts = new Dictionary<GrammarRule, double>();
            double sentenceProb = 0.0;
            if (inside[0, n].ContainsKey(grammar.StartSymbol))
            {
                sentenceProb = inside[0, n][grammar.StartSymbol];
            }

            for (int span = 2; span <= n; span++)
            {
                for (int i = 0; i <= n - span; i++)
                {
                    int j = i + span;
                    for (int k = i + 1; k < j; k++)
                    {
                        for (int r = 0; r < grammar.Rules.Count; r++)
                        {
                            GrammarRule rule = grammar.Rules[r];
                            if (rule.RHS.Length == 2)
                            {
                                if (inside[i, k].ContainsKey(rule.RHS[0]) && inside[k, j].ContainsKey(rule.RHS[1]) && outside[i, j].ContainsKey(rule.LHS))
                                {
                                    double contrib = outside[i, j][rule.LHS] * rule.Probability * inside[i, k][rule.RHS[0]] * inside[k, j][rule.RHS[1]];
                                    if (ruleCounts.ContainsKey(rule))
                                    {
                                        ruleCounts[rule] += contrib;
                                    }
                                    else
                                    {
                                        ruleCounts[rule] = contrib;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                int j = i + 1;
                for (int r = 0; r < grammar.Rules.Count; r++)
                {
                    GrammarRule rule = grammar.Rules[r];
                    if (rule.RHS.Length == 1 && inside[i, j].ContainsKey(rule.LHS) && outside[i, j].ContainsKey(rule.LHS))
                    {
                        double contrib = outside[i, j][rule.LHS] * rule.Probability;
                        if (ruleCounts.ContainsKey(rule))
                        {
                            ruleCounts[rule] += contrib;
                        }
                        else
                        {
                            ruleCounts[rule] = contrib;
                        }
                    }
                }
            }

            Dictionary<string, double> lhsSums = new Dictionary<string, double>();
            foreach (KeyValuePair<GrammarRule, double> entry in ruleCounts)
            {
                string lhs = entry.Key.LHS;
                if (lhsSums.ContainsKey(lhs))
                {
                    lhsSums[lhs] += entry.Value;
                }
                else
                {
                    lhsSums[lhs] = entry.Value;
                }
            }

            for (int r = 0; r < grammar.Rules.Count; r++)
            {
                GrammarRule rule = grammar.Rules[r];
                double count = ruleCounts.ContainsKey(rule) ? ruleCounts[rule] : 0.0;
                double total = lhsSums.ContainsKey(rule.LHS) ? lhsSums[rule.LHS] : 0.0;
                rule.Probability = total > 0.0 ? count / total : 0.0;
            }
        }

        private static void ApplyUnaryClosureInside(Dictionary<string, double> cell, ProbabilisticGrammar grammar)
        {
            bool updated = true;
            while (updated)
            {
                updated = false;
                for (int i = 0; i < grammar.Rules.Count; i++)
                {
                    GrammarRule rule = grammar.Rules[i];
                    if (rule.RHS.Length == 1)
                    {
                        string b = rule.RHS[0];
                        bool isNonTerminal = false;
                        for (int j = 0; j < grammar.Rules.Count; j++)
                        {
                            if (grammar.Rules[j].LHS == b)
                            {
                                isNonTerminal = true;
                                break;
                            }
                        }

                        if (!isNonTerminal)
                        {
                            continue;
                        }

                        if (cell.ContainsKey(b))
                        {
                            double addition = cell[b] * rule.Probability;
                            if (addition > 1e-12)
                            {
                                double oldValue;
                                if (cell.TryGetValue(rule.LHS, out oldValue))
                                {
                                    double newValue = oldValue + addition;
                                    if (Math.Abs(newValue - oldValue) > 1e-12)
                                    {
                                        cell[rule.LHS] = newValue;
                                        updated = true;
                                    }
                                }
                                else
                                {
                                    cell[rule.LHS] = addition;
                                    updated = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void ApplyUnaryClosureOutside(Dictionary<string, double> cell, ProbabilisticGrammar grammar)
        {
            bool updated = true;
            while (updated)
            {
                updated = false;
                for (int i = 0; i < grammar.Rules.Count; i++)
                {
                    GrammarRule rule = grammar.Rules[i];
                    if (rule.RHS.Length == 1)
                    {
                        string b = rule.RHS[0];
                        bool isNonTerminal = false;
                        for (int j = 0; j < grammar.Rules.Count; j++)
                        {
                            if (grammar.Rules[j].LHS == b)
                            {
                                isNonTerminal = true;
                                break;
                            }
                        }

                        if (!isNonTerminal)
                        {
                            continue;
                        }

                        if (cell.ContainsKey(rule.LHS))
                        {
                            double addition = cell[rule.LHS] * rule.Probability;
                            if (addition > 1e-12)
                            {
                                double oldValue;
                                if (cell.TryGetValue(b, out oldValue))
                                {
                                    double newValue = oldValue + addition;
                                    if (Math.Abs(newValue - oldValue) > 1e-12)
                                    {
                                        cell[b] = newValue;
                                        updated = true;
                                    }
                                }
                                else
                                {
                                    cell[b] = addition;
                                    updated = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}