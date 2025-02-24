using System;
using System.Collections.Generic;
using System.Text;

namespace SchenstedAlgorithm
{
    public class YoungTableau
    {
        public List<List<int>> Rows { get; }

        public YoungTableau()
        {
            Rows = new List<List<int>>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (List<int> row in Rows)
            {
                sb.AppendLine(string.Join(" ", row));
            }

            return sb.ToString();
        }
    }
}