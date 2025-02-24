using System;
using System.Collections.Generic;

namespace LexBfs
{
    public static class LexBfsAlgorithm
    {
        public static List<T> LexBfs<T>(Graph<T> graph)
            where T : notnull
        {
            List<T> ordering = new List<T>();
            Dictionary<T, PartitionCell<T>> vertexToCell = new Dictionary<T, PartitionCell<T>>();
            Dictionary<T, LinkedListNode<T>> vertexToNode = new Dictionary<T, LinkedListNode<T>>();
            List<PartitionCell<T>> partitions = new List<PartitionCell<T>>();
            PartitionCell<T> initialCell = new PartitionCell<T>();
            foreach (T vertex in graph.Vertices)
            {
                LinkedListNode<T> node = initialCell.Vertices.AddLast(vertex);
                vertexToCell[vertex] = initialCell;
                vertexToNode[vertex] = node;
            }

            partitions.Add(initialCell);
            while (partitions.Count > 0)
            {
                PartitionCell<T> currentCell = partitions[0];
                if (currentCell.Vertices.Count == 0)
                {
                    partitions.RemoveAt(0);
                    continue;
                }

                LinkedListNode<T> currentNode = currentCell.Vertices.First!;
                T v = currentNode.Value;
                currentCell.Vertices.RemoveFirst();
                vertexToCell.Remove(v);
                vertexToNode.Remove(v);
                ordering.Add(v);
                Dictionary<PartitionCell<T>, List<LinkedListNode<T>>> splits = new Dictionary<PartitionCell<T>, List<LinkedListNode<T>>>();
                foreach (T neighbor in graph.GetNeighbors(v))
                {
                    if (vertexToCell.ContainsKey(neighbor))
                    {
                        PartitionCell<T> cell = vertexToCell[neighbor];
                        if (!splits.ContainsKey(cell))
                        {
                            splits[cell] = new List<LinkedListNode<T>>();
                        }

                        splits[cell].Add(vertexToNode[neighbor]);
                    }
                }

                foreach (KeyValuePair<PartitionCell<T>, List<LinkedListNode<T>>> pair in splits)
                {
                    PartitionCell<T> cell = pair.Key;
                    List<LinkedListNode<T>> nodesToMove = pair.Value;
                    if (nodesToMove.Count == cell.Vertices.Count)
                    {
                        continue;
                    }

                    PartitionCell<T> newCell = new PartitionCell<T>();
                    foreach (LinkedListNode<T> node in nodesToMove)
                    {
                        cell.Vertices.Remove(node);
                        LinkedListNode<T> newNode = newCell.Vertices.AddLast(node.Value);
                        vertexToCell[node.Value] = newCell;
                        vertexToNode[node.Value] = newNode;
                    }

                    int index = partitions.IndexOf(cell);
                    if (index >= 0)
                    {
                        partitions.Insert(index, newCell);
                    }
                    else
                    {
                        partitions.Add(newCell);
                    }
                }
            }

            return ordering;
        }
    }

    internal class PartitionCell<T>
        where T : notnull
    {
        public LinkedList<T> Vertices { get; }

        public PartitionCell()
        {
            Vertices = new LinkedList<T>();
        }
    }
}