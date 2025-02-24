using System;
using System.Collections.Generic;
using System.Linq;

namespace BeamStackSearch
{
    /// <summary>
    /// Implements the Beam Stack Search algorithm that integrates beam search with backtracking
    /// to balance memory efficiency and search completeness.
    /// </summary>
    public class BeamStackSearchAlgorithm<T>
    {
        public int BeamWidth { get; private set; }

        public BeamStackSearchAlgorithm(int beamWidth)
        {
            this.BeamWidth = beamWidth;
        }

        public List<T> Search(T initialState, Func<T, bool> isGoal, Func<T, IEnumerable<T>> getSuccessors, Func<T, double> scoreFunc)
        {
            if (initialState == null)
            {
                return new List<T>();
            }

            Node initialNode = new Node(initialState, new List<T>(), scoreFunc(initialState));
            initialNode.Path.Add(initialState);
            List<Node> currentBeam = new List<Node>
            {
                initialNode
            };
            List<Node> backtrackingNodes = new List<Node>();
            while (currentBeam.Count > 0)
            {
                foreach (Node node in currentBeam)
                {
                    if (isGoal(node.State))
                    {
                        return node.Path;
                    }
                }

                List<Node> candidateNodes = new List<Node>();
                foreach (Node node in currentBeam)
                {
                    IEnumerable<T> successors = getSuccessors(node.State);
                    if (successors != null)
                    {
                        foreach (T successor in successors)
                        {
                            double score = scoreFunc(successor);
                            List<T> newPath = new List<T>(node.Path);
                            newPath.Add(successor);
                            Node newNode = new Node(successor, newPath, score);
                            candidateNodes.Add(newNode);
                        }
                    }
                }

                if (candidateNodes.Count == 0)
                {
                    if (backtrackingNodes.Count > 0)
                    {
                        Node? nextNode = null;
                        double bestScore = double.MinValue;
                        for (int i = 0; i < backtrackingNodes.Count; i++)
                        {
                            Node candidate = backtrackingNodes[i];
                            if (candidate.Score > bestScore)
                            {
                                bestScore = candidate.Score;
                                nextNode = candidate;
                            }
                        }

                        if (nextNode != null)
                        {
                            backtrackingNodes.Remove(nextNode);
                            currentBeam = new List<Node>
                            {
                                nextNode
                            };
                            continue;
                        }
                    }
                    else
                    {
                        return new List<T>();
                    }
                }

                List<Node> sortedCandidates = candidateNodes.OrderByDescending(x => x.Score).ToList();
                List<Node> nextBeam = new List<Node>();
                int count = 0;
                foreach (Node candidate in sortedCandidates)
                {
                    if (count < this.BeamWidth)
                    {
                        nextBeam.Add(candidate);
                    }
                    else
                    {
                        backtrackingNodes.Add(candidate);
                    }

                    count++;
                }

                currentBeam = nextBeam;
            }

            return new List<T>();
        }

        private class Node
        {
            public T State;
            public List<T> Path;
            public double Score;
            public Node(T state, List<T> path, double score)
            {
                this.State = state;
                this.Path = path;
                this.Score = score;
            }
        }
    }
}