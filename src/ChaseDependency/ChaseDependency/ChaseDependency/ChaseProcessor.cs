using System;
using System.Collections.Generic;

namespace ChaseDependency
{
    public class ChaseProcessor
    {
        public static Database RunChase(Database database, List<Dependency> dependencies)
        {
            bool changesMade = true;
            while (changesMade)
            {
                changesMade = false;
                foreach (Dependency dependency in dependencies)
                {
                    List<Dictionary<string, string>> mappings = FindMappings(dependency.Antecedent, database.Atoms, new Dictionary<string, string>());
                    foreach (Dictionary<string, string> mapping in mappings)
                    {
                        foreach (Atom cons in dependency.Consequent)
                        {
                            Atom groundCons = Substitute(cons, mapping);
                            if (!database.ContainsAtom(groundCons))
                            {
                                database.AddAtom(groundCons);
                                changesMade = true;
                            }
                        }
                    }
                }
            }

            return database;
        }

        private static Atom Substitute(Atom atom, Dictionary<string, string> mapping)
        {
            List<string> newArgs = new List<string>();
            foreach (string arg in atom.Arguments)
            {
                if (arg.Length > 0 && arg[0] == '?')
                {
                    if (mapping.ContainsKey(arg))
                    {
                        newArgs.Add(mapping[arg]);
                    }
                    else
                    {
                        newArgs.Add(arg);
                    }
                }
                else
                {
                    newArgs.Add(arg);
                }
            }

            return new Atom(atom.Relation, newArgs);
        }

        private static List<Dictionary<string, string>> FindMappings(List<Atom> patterns, List<Atom> facts, Dictionary<string, string> currentMapping)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            if (patterns.Count == 0)
            {
                results.Add(new Dictionary<string, string>(currentMapping));
                return results;
            }

            Atom first = patterns[0];
            List<Atom> rest = patterns.GetRange(1, patterns.Count - 1);
            foreach (Atom fact in facts)
            {
                Dictionary<string, string>? newMapping = Unify(first, fact, currentMapping);
                if (newMapping != null)
                {
                    List<Dictionary<string, string>> subResults = FindMappings(rest, facts, newMapping);
                    results.AddRange(subResults);
                }
            }

            return results;
        }

        private static Dictionary<string, string>? Unify(Atom pattern, Atom fact, Dictionary<string, string> mapping)
        {
            if (!pattern.Relation.Equals(fact.Relation))
            {
                return null;
            }

            if (pattern.Arguments.Count != fact.Arguments.Count)
            {
                return null;
            }

            Dictionary<string, string> localMapping = new Dictionary<string, string>(mapping);
            for (int i = 0; i < pattern.Arguments.Count; i++)
            {
                string patArg = pattern.Arguments[i];
                string factArg = fact.Arguments[i];
                if (patArg.Length > 0 && patArg[0] == '?')
                {
                    if (localMapping.ContainsKey(patArg))
                    {
                        if (!localMapping[patArg].Equals(factArg))
                        {
                            return null;
                        }
                    }
                    else
                    {
                        localMapping[patArg] = factArg;
                    }
                }
                else
                {
                    if (!patArg.Equals(factArg))
                    {
                        return null;
                    }
                }
            }

            return localMapping;
        }
    }
}