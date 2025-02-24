using System;
using System.Collections.Generic;

namespace AverageLinkageClustering
{
    public class Cluster<T>
    {
        public List<T> Members { get; private set; }

        public Cluster(T member)
        {
            if (member == null)
            {
                Members = new List<T>();
            }
            else
            {
                Members = new List<T>
                {
                    member
                };
            }
        }

        public Cluster(List<T> members)
        {
            if (members == null)
            {
                Members = new List<T>();
            }
            else
            {
                Members = new List<T>(members);
            }
        }

        public static Cluster<T> Merge(Cluster<T> cluster1, Cluster<T> cluster2)
        {
            List<T> mergedMembers = new List<T>();
            if (cluster1 != null && cluster1.Members != null)
            {
                mergedMembers.AddRange(cluster1.Members);
            }

            if (cluster2 != null && cluster2.Members != null)
            {
                mergedMembers.AddRange(cluster2.Members);
            }

            return new Cluster<T>(mergedMembers);
        }
    }
}