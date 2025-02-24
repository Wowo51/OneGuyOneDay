using System;
using System.Collections.Generic;

namespace DinicsAlgorithm
{
    public class Dinic
    {
        private readonly int _nodeCount;
        private readonly List<List<Edge>> _graph;
        private readonly int[] _level;
        private readonly int[] _start;
        public Dinic(int nodeCount)
        {
            _nodeCount = nodeCount;
            _graph = new List<List<Edge>>();
            for (int i = 0; i < nodeCount; i++)
            {
                _graph.Add(new List<Edge>());
            }

            _level = new int[nodeCount];
            _start = new int[nodeCount];
        }

        public void AddEdge(int from, int to, int capacity)
        {
            if (from < 0 || from >= _nodeCount || to < 0 || to >= _nodeCount)
            {
                return;
            }

            Edge forward = new Edge(from, to, capacity);
            Edge backward = new Edge(to, from, 0);
            forward.Reverse = backward;
            backward.Reverse = forward;
            _graph[from].Add(forward);
            _graph[to].Add(backward);
        }

        private bool BuildLevelGraph(int source, int sink)
        {
            for (int i = 0; i < _nodeCount; i++)
            {
                _level[i] = -1;
            }

            Queue<int> queue = new Queue<int>();
            _level[source] = 0;
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                foreach (Edge edge in _graph[node])
                {
                    if (_level[edge.To] < 0 && edge.ResidualCapacity() > 0)
                    {
                        _level[edge.To] = _level[node] + 1;
                        queue.Enqueue(edge.To);
                    }
                }
            }

            return _level[sink] >= 0;
        }

        private int SendFlow(int node, int flow, int sink)
        {
            if (node == sink)
            {
                return flow;
            }

            for (; _start[node] < _graph[node].Count; _start[node]++)
            {
                Edge edge = _graph[node][_start[node]];
                if (_level[edge.To] == _level[node] + 1 && edge.ResidualCapacity() > 0)
                {
                    int currentFlow = (flow < edge.ResidualCapacity() ? flow : edge.ResidualCapacity());
                    int tempFlow = SendFlow(edge.To, currentFlow, sink);
                    if (tempFlow > 0)
                    {
                        edge.Flow += tempFlow;
                        edge.Reverse.Flow -= tempFlow;
                        return tempFlow;
                    }
                }
            }

            return 0;
        }

        public int MaxFlow(int source, int sink)
        {
            int totalFlow = 0;
            while (BuildLevelGraph(source, sink))
            {
                for (int i = 0; i < _nodeCount; i++)
                {
                    _start[i] = 0;
                }

                while (true)
                {
                    int flow = SendFlow(source, int.MaxValue, sink);
                    if (flow <= 0)
                    {
                        break;
                    }

                    totalFlow += flow;
                }
            }

            return totalFlow;
        }
    }
}