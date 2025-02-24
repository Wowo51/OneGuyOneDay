using System;

namespace KruskalAlgorithm
{
    public class DisjointSet
    {
        private int[] parent;
        private int[] rank;
        public DisjointSet(int size)
        {
            this.parent = new int[size];
            this.rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                this.parent[i] = i;
                this.rank[i] = 0;
            }
        }

        public int Find(int i)
        {
            if (this.parent[i] != i)
            {
                this.parent[i] = Find(this.parent[i]);
            }

            return this.parent[i];
        }

        public void Union(int x, int y)
        {
            int xroot = Find(x);
            int yroot = Find(y);
            if (xroot == yroot)
            {
                return;
            }

            if (this.rank[xroot] < this.rank[yroot])
            {
                this.parent[xroot] = yroot;
            }
            else if (this.rank[xroot] > this.rank[yroot])
            {
                this.parent[yroot] = xroot;
            }
            else
            {
                this.parent[yroot] = xroot;
                this.rank[xroot]++;
            }
        }
    }
}