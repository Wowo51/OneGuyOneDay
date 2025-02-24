using System;
using System.Collections.Generic;

namespace UniformCostSearch
{
    public static class UniformCostSearchAlgorithm
    {
        // Finds the lowest-cost path from the start state to a state that satisfies the goalTest.
        // getNeighbors returns a sequence of (next state, cost) pairs for a given state.
        public static List<TState>? FindPath<TState>(TState start, Func<TState, bool> goalTest, Func<TState, IEnumerable<(TState next, double cost)>> getNeighbors)
            where TState : notnull
        {
            Dictionary<TState, double> costs = new Dictionary<TState, double>();
            Dictionary<TState, TState?> parents = new Dictionary<TState, TState?>();
            List<Node<TState>> frontier = new List<Node<TState>>();
            costs[start] = 0.0;
            parents[start] = default;
            frontier.Add(new Node<TState>(start, 0.0));
            while (frontier.Count > 0)
            {
                int bestIndex = 0;
                double bestCost = frontier[0].Cost;
                for (int i = 1; i < frontier.Count; i++)
                {
                    if (frontier[i].Cost < bestCost)
                    {
                        bestCost = frontier[i].Cost;
                        bestIndex = i;
                    }
                }

                Node<TState> currentNode = frontier[bestIndex];
                frontier.RemoveAt(bestIndex);
                if (goalTest(currentNode.State))
                {
                    return ReconstructPath(parents, currentNode.State);
                }

                foreach ((TState next, double cost)in getNeighbors(currentNode.State))
                {
                    TState nextState = next;
                    double newCost = currentNode.Cost + cost;
                    if (!costs.ContainsKey(nextState) || newCost < costs[nextState])
                    {
                        costs[nextState] = newCost;
                        parents[nextState] = currentNode.State;
                        frontier.Add(new Node<TState>(nextState, newCost));
                    }
                }
            }

            return null;
        }

        private static List<TState> ReconstructPath<TState>(Dictionary<TState, TState?> parents, TState goal)
            where TState : notnull
        {
            List<TState> path = new List<TState>();
            TState? current = goal;
            while (current != null)
            {
                path.Add(current);
                if (parents.TryGetValue(current, out TState? parent))
                {
                    current = parent;
                }
                else
                {
                    break;
                }
            }

            path.Reverse();
            return path;
        }

        private class Node<TState>
        {
            public TState State { get; set; }
            public double Cost { get; set; }

            public Node(TState state, double cost)
            {
                this.State = state;
                this.Cost = cost;
            }
        }
    }
}