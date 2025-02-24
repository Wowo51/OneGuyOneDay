using System.Collections.Generic;

namespace WACAClusteringAlgorithm
{
    public class Node
    {
        public int Id { get; set; }
        public List<int> Neighbors { get; set; }

        public Node(int id)
        {
            Id = id;
            Neighbors = new List<int>();
        }
    }
}