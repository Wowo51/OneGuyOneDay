using System;
using System.Collections.Generic;
using System.Text;

namespace SequiturAlgorithm
{
    public class Sequitur
    {
        public Grammar Compress(string input)
        {
            // Initialize the grammar with the main rule S containing each character as a token.
            Grammar grammar = new Grammar();
            grammar.MainRule = new Rule();
            grammar.MainRule.Name = "S";
            grammar.MainRule.Definition = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                grammar.MainRule.Definition.Add(input.Substring(i, 1));
            }

            // The Rules property is already initialized by default.
            int ruleCounter = 1;
            bool replaced = true;
            while (replaced)
            {
                replaced = false;
                Dictionary<string, int> pairCount = new Dictionary<string, int>();
                // Count adjacent pairs in the main rule.
                for (int i = 0; i < grammar.MainRule.Definition.Count - 1; i++)
                {
                    string token1 = grammar.MainRule.Definition[i];
                    string token2 = grammar.MainRule.Definition[i + 1];
                    string pair = token1 + " " + token2;
                    if (pairCount.ContainsKey(pair))
                    {
                        pairCount[pair] = pairCount[pair] + 1;
                    }
                    else
                    {
                        pairCount[pair] = 1;
                    }
                }

                // Identify the first pair that occurs at least twice.
                string targetPair = "";
                foreach (KeyValuePair<string, int> entry in pairCount)
                {
                    if (entry.Value >= 2)
                    {
                        targetPair = entry.Key;
                        break;
                    }
                }

                if (targetPair != "")
                {
                    string[] parts = targetPair.Split(' ');
                    string t1 = parts[0];
                    string t2 = parts[1];
                    string ruleName = "R" + ruleCounter.ToString();
                    ruleCounter++;
                    // Create a new rule for the identified pair.
                    Rule newRule = new Rule();
                    newRule.Name = ruleName;
                    newRule.Definition = new List<string>
                    {
                        t1,
                        t2
                    };
                    grammar.Rules.Add(newRule);
                    // Replace every occurrence of the pair in the main rule with the new rule name.
                    List<string> newDefinition = new List<string>();
                    int index = 0;
                    while (index < grammar.MainRule.Definition.Count)
                    {
                        if (index < grammar.MainRule.Definition.Count - 1 && grammar.MainRule.Definition[index] == t1 && grammar.MainRule.Definition[index + 1] == t2)
                        {
                            newDefinition.Add(ruleName);
                            index += 2;
                            replaced = true;
                        }
                        else
                        {
                            newDefinition.Add(grammar.MainRule.Definition[index]);
                            index++;
                        }
                    }

                    grammar.MainRule.Definition = newDefinition;
                }
            }

            return grammar;
        }
    }

    public class Grammar
    {
        public Rule MainRule { get; set; } = new Rule();
        public List<Rule> Rules { get; set; } = new List<Rule>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("S ->");
            foreach (string token in MainRule.Definition)
            {
                sb.Append(" " + token);
            }

            sb.AppendLine();
            foreach (Rule rule in Rules)
            {
                sb.Append(rule.Name + " ->");
                foreach (string token in rule.Definition)
                {
                    sb.Append(" " + token);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public class Rule
    {
        public string Name { get; set; } = "";
        public List<string> Definition { get; set; } = new List<string>();
    }
}