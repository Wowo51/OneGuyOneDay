using System.Collections.Generic;

namespace StateSpaceSearch
{
    public class SearchNode<TState>
    {
        public TState State { get; }
        public SearchNode<TState>? Parent { get; }
        public int Cost { get; }
        public int F { get; }

        public SearchNode(TState state, SearchNode<TState>? parent, int cost, int f)
        {
            State = state;
            Parent = parent;
            Cost = cost;
            F = f;
        }
    }

    public static class StateSpaceSearchAlgorithm
    {
        public static List<TState> Search<TState>(IStateSpaceProblem<TState> problem)
        {
            if (problem == null)
            {
                return new List<TState>();
            }

            PriorityQueue<SearchNode<TState>, int> open = new PriorityQueue<SearchNode<TState>, int>();
            HashSet<TState> closed = new HashSet<TState>();
            int initialHeuristic = problem.Heuristic(problem.InitialState);
            SearchNode<TState> initialNode = new SearchNode<TState>(problem.InitialState, null, 0, initialHeuristic);
            open.Enqueue(initialNode, initialNode.F);
            while (open.Count > 0)
            {
                SearchNode<TState> currentNode = open.Dequeue();
                if (problem.IsGoalState(currentNode.State))
                {
                    return ConstructPath(currentNode);
                }

                closed.Add(currentNode.State);
                IEnumerable<TState> neighbors = problem.GetNeighbors(currentNode.State);
                foreach (TState neighbor in neighbors)
                {
                    if (closed.Contains(neighbor))
                    {
                        continue;
                    }

                    int stepCost = problem.GetCost(currentNode.State, neighbor);
                    int newCost = currentNode.Cost + stepCost;
                    int heuristic = problem.Heuristic(neighbor);
                    int newF = newCost + heuristic;
                    SearchNode<TState> neighborNode = new SearchNode<TState>(neighbor, currentNode, newCost, newF);
                    open.Enqueue(neighborNode, neighborNode.F);
                }
            }

            return new List<TState>();
        }

        private static List<TState> ConstructPath<TState>(SearchNode<TState>? node)
        {
            List<TState> path = new List<TState>();
            for (SearchNode<TState>? current = node; current != null; current = current.Parent)
            {
                path.Insert(0, current.State);
            }

            return path;
        }
    }
}