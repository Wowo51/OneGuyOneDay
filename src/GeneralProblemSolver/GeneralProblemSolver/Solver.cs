using System;
using System.Collections.Generic;

namespace GeneralProblemSolver
{
    public class Solver
    {
        public List<Operator> Operators { get; set; }

        public Solver(List<Operator> operators)
        {
            Operators = operators ?? new List<Operator>();
        }

        public bool Solve(ProblemState initial, ProblemState goal, out List<string> solutionSteps)
        {
            solutionSteps = new List<string>();
            if (initial == null || goal == null)
            {
                return false;
            }

            if (string.Equals(initial.StateDescription, goal.StateDescription, StringComparison.Ordinal))
            {
                return true;
            }

            HashSet<string> visited = new HashSet<string>();
            visited.Add(initial.StateDescription);
            return RecursiveSolve(initial, goal, solutionSteps, visited);
        }

        private bool RecursiveSolve(ProblemState current, ProblemState goal, List<string> steps, HashSet<string> visited)
        {
            if (string.Equals(current.StateDescription, goal.StateDescription, StringComparison.Ordinal))
            {
                return true;
            }

            foreach (Operator op in Operators)
            {
                if (op.Applicable(current))
                {
                    ProblemState newState = op.Apply(current);
                    if (newState == null || visited.Contains(newState.StateDescription))
                    {
                        continue;
                    }

                    steps.Add("Applied " + op.Name);
                    visited.Add(newState.StateDescription);
                    if (RecursiveSolve(newState, goal, steps, visited))
                    {
                        return true;
                    }

                    steps.RemoveAt(steps.Count - 1);
                    visited.Remove(newState.StateDescription);
                }
            }

            return false;
        }
    }
}