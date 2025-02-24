using System;
using System.Collections.Generic;
using System.Linq;

namespace KWayMergeAlgorithm
{
    public static class KWayMerge
    {
        public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> sequences)
            where T : IComparable<T>
        {
            if (sequences == null)
            {
                yield break;
            }

            PriorityQueue<QueueNode<T>, T> priorityQueue = new PriorityQueue<QueueNode<T>, T>();
            foreach (IEnumerable<T> sequence in sequences)
            {
                if (sequence != null)
                {
                    IEnumerator<T> enumerator = sequence.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        QueueNode<T> node = new QueueNode<T>(current, enumerator);
                        priorityQueue.Enqueue(node, current);
                    }
                }
            }

            while (priorityQueue.Count > 0)
            {
                QueueNode<T> smallest = priorityQueue.Dequeue();
                yield return smallest.Value;
                if (smallest.Enumerator.MoveNext())
                {
                    smallest.Value = smallest.Enumerator.Current;
                    priorityQueue.Enqueue(smallest, smallest.Value);
                }
                else
                {
                    smallest.Enumerator.Dispose();
                }
            }
        }

        private class QueueNode<T>
        {
            internal T Value;
            internal IEnumerator<T> Enumerator;
            internal QueueNode(T value, IEnumerator<T> enumerator)
            {
                Value = value;
                Enumerator = enumerator;
            }
        }
    }
}