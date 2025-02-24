using System;
using System.Collections.Generic;

namespace ChaitinRegisterAllocation
{
    public class ChaitinRegisterAllocator
    {
        public Dictionary<string, int> AllocateRegisters(InterferenceGraph graph, int k)
        {
            if (graph == null || graph.Nodes == null)
            {
                return new Dictionary<string, int>();
            }

            List<GraphNode> remainingNodes = new List<GraphNode>(graph.Nodes);
            Stack<GraphNode> stack = new Stack<GraphNode>();
            while (remainingNodes.Count > 0)
            {
                GraphNode? candidate = null;
                foreach (GraphNode node in remainingNodes)
                {
                    int activeDegree = 0;
                    foreach (GraphNode neighbor in node.Neighbors)
                    {
                        if (remainingNodes.Contains(neighbor))
                        {
                            activeDegree++;
                        }
                    }

                    if (activeDegree < k)
                    {
                        candidate = node;
                        break;
                    }
                }

                if (candidate == null)
                {
                    double minMetric = double.MaxValue;
                    foreach (GraphNode node in remainingNodes)
                    {
                        int activeDegree = 0;
                        foreach (GraphNode neighbor in node.Neighbors)
                        {
                            if (remainingNodes.Contains(neighbor))
                            {
                                activeDegree++;
                            }
                        }

                        double metric = (activeDegree > 0) ? node.Cost / activeDegree : node.Cost;
                        if (metric < minMetric)
                        {
                            minMetric = metric;
                            candidate = node;
                        }
                    }
                }

                if (candidate != null)
                {
                    remainingNodes.Remove(candidate);
                    stack.Push(candidate);
                }
            }

            Dictionary<string, int> allocation = new Dictionary<string, int>();
            while (stack.Count > 0)
            {
                GraphNode node = stack.Pop();
                HashSet<int> usedRegisters = new HashSet<int>();
                foreach (GraphNode neighbor in node.Neighbors)
                {
                    if (neighbor.AssignedRegister.HasValue)
                    {
                        usedRegisters.Add(neighbor.AssignedRegister.Value);
                    }
                }

                int chosenRegister = -1;
                for (int reg = 0; reg < k; reg++)
                {
                    if (!usedRegisters.Contains(reg))
                    {
                        chosenRegister = reg;
                        break;
                    }
                }

                node.AssignedRegister = chosenRegister;
                allocation[node.Name] = chosenRegister;
            }

            return allocation;
        }
    }
}