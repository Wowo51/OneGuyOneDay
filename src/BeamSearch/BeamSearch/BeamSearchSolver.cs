using System;
using System.Collections.Generic;

namespace BeamSearch
{
    public static class BeamSearchSolver
    {
        /// <summary>
        /// Performs a beam search starting from the given state.
        /// Returns the first state that passes the goal test, or null if none is found.
        /// Lower evaluation values indicate better candidates.
        /// </summary>
        /// <typeparam name = "T">The state type. Must be a reference type.</typeparam>
        /// <param name = "start">The initial state.</param>
        /// <param name = "getSuccessors">A function to generate successors of a state.</param>
        /// <param name = "evaluate">A function that computes the evaluation score of a state.</param>
        /// <param name = "isGoal">A predicate to test if a state is a goal state.</param>
        /// <param name = "beamWidth">The maximum number of nodes to keep at each level.</param>
        /// <returns>The goal state if found; otherwise, null.</returns>
        public static T? FindSolution<T>(T start, Func<T, IEnumerable<T>> getSuccessors, Func<T, double> evaluate, Predicate<T> isGoal, int beamWidth)
            where T : class
        {
            if (beamWidth <= 0)
            {
                return null;
            }

            List<T> currentNodes = new List<T>
            {
                start
            };
            while (currentNodes.Count > 0)
            {
                List<T> nextNodes = new List<T>();
                // Check for goal state in the current beam.
                foreach (T node in currentNodes)
                {
                    if (isGoal(node))
                    {
                        return node;
                    }
                }

                // Expand all nodes in the current beam.
                foreach (T node in currentNodes)
                {
                    IEnumerable<T> successors = getSuccessors(node);
                    foreach (T successor in successors)
                    {
                        nextNodes.Add(successor);
                    }
                }

                if (nextNodes.Count == 0)
                {
                    return null;
                }

                // Sort successors by evaluation (ascending: lower is better).
                List<T> orderedNodes = new List<T>(nextNodes);
                orderedNodes.Sort(delegate (T a, T b)
                {
                    double diff = evaluate(a) - evaluate(b);
                    if (diff < 0)
                    {
                        return -1;
                    }
                    else if (diff > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });
                // Select the top beamWidth nodes.
                List<T> beamNodes = new List<T>();
                int count = 0;
                foreach (T candidate in orderedNodes)
                {
                    if (count >= beamWidth)
                    {
                        break;
                    }

                    beamNodes.Add(candidate);
                    count++;
                }

                currentNodes = beamNodes;
            }

            return null;
        }
    }
}