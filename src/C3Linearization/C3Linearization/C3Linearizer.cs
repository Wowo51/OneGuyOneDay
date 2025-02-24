using System;
using System.Collections.Generic;

namespace C3Linearization
{
    public static class C3Linearizer
    {
        public static List<T> Linearize<T>(T node, IDictionary<T, List<T>> inheritance)
            where T : notnull
        {
            List<T> linearization = new List<T>();
            if (inheritance.TryGetValue(node, out List<T>? bases) && bases != null)
            {
                List<List<T>> sequences = new List<List<T>>();
                foreach (T baseNode in bases)
                {
                    sequences.Add(Linearize(baseNode, inheritance));
                }

                sequences.Add(new List<T>(bases));
                List<T> merged = Merge(sequences);
                linearization.Add(node);
                linearization.AddRange(merged);
            }
            else
            {
                linearization.Add(node);
            }

            return linearization;
        }

        private static List<T> Merge<T>(List<List<T>> sequences)
            where T : notnull
        {
            List<T> result = new List<T>();
            while (true)
            {
                bool allEmpty = true;
                T candidate = default(T)!;
                bool foundCandidate = false;
                foreach (List<T> seq in sequences)
                {
                    if (seq.Count > 0)
                    {
                        allEmpty = false;
                        candidate = seq[0];
                        bool inTail = false;
                        foreach (List<T> otherSeq in sequences)
                        {
                            if (otherSeq.Count > 0 && !EqualityComparer<T>.Default.Equals(otherSeq[0], candidate) && otherSeq.IndexOf(candidate) > 0)
                            {
                                inTail = true;
                                break;
                            }
                        }

                        if (!inTail)
                        {
                            foundCandidate = true;
                            break;
                        }
                    }
                }

                if (allEmpty)
                {
                    return result;
                }

                if (!foundCandidate)
                {
                    return new List<T>();
                }

                result.Add(candidate);
                foreach (List<T> seq in sequences)
                {
                    if (seq.Count > 0 && EqualityComparer<T>.Default.Equals(seq[0], candidate))
                    {
                        seq.RemoveAt(0);
                    }
                }
            }
        }
    }
}