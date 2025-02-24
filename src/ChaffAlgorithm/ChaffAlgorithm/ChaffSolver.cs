using System;
using System.Collections.Generic;

namespace ChaffAlgorithm
{
    public class ChaffSolver
    {
        public Dictionary<int, bool>? Solve(List<List<int>> clauses)
        {
            HashSet<int> variablesSet = new HashSet<int>();
            foreach (List<int> clause in clauses)
            {
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    variablesSet.Add(variable);
                }
            }

            List<int> variables = new List<int>(variablesSet);
            Dictionary<int, bool> assignment = new Dictionary<int, bool>();
            return Search(clauses, variables, assignment);
        }

        private Dictionary<int, bool>? Search(List<List<int>> clauses, List<int> variables, Dictionary<int, bool> assignment)
        {
            if (HasConflict(clauses, assignment))
            {
                return null;
            }

            if (assignment.Count == variables.Count)
            {
                if (IsSatisfied(clauses, assignment))
                {
                    return assignment;
                }

                return null;
            }

            int chosenVariable = 0;
            foreach (int variable in variables)
            {
                if (!assignment.ContainsKey(variable))
                {
                    chosenVariable = variable;
                    break;
                }
            }

            assignment[chosenVariable] = true;
            Dictionary<int, bool>? resultTrue = Search(clauses, variables, assignment);
            if (resultTrue != null)
            {
                return resultTrue;
            }

            assignment[chosenVariable] = false;
            Dictionary<int, bool>? resultFalse = Search(clauses, variables, assignment);
            if (resultFalse != null)
            {
                return resultFalse;
            }

            assignment.Remove(chosenVariable);
            return null;
        }

        private bool HasConflict(List<List<int>> clauses, Dictionary<int, bool> assignment)
        {
            foreach (List<int> clause in clauses)
            {
                bool allAssigned = true;
                bool clauseSatisfied = false;
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    if (!assignment.ContainsKey(variable))
                    {
                        allAssigned = false;
                    }
                    else
                    {
                        bool value = assignment[variable];
                        if ((literal > 0 && value) || (literal < 0 && !value))
                        {
                            clauseSatisfied = true;
                            break;
                        }
                    }
                }

                if (allAssigned && !clauseSatisfied)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSatisfied(List<List<int>> clauses, Dictionary<int, bool> assignment)
        {
            foreach (List<int> clause in clauses)
            {
                bool clauseSatisfied = false;
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    if (assignment.ContainsKey(variable))
                    {
                        bool value = assignment[variable];
                        if ((literal > 0 && value) || (literal < 0 && !value))
                        {
                            clauseSatisfied = true;
                            break;
                        }
                    }
                }

                if (!clauseSatisfied)
                {
                    return false;
                }
            }

            return true;
        }
    }
}