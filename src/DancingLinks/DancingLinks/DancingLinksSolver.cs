using System.Collections.Generic;

namespace DancingLinks
{
    public class DancingLinksSolver
    {
        private ColumnNode header;
        private List<List<DancingNode>> solutions;
        public DancingLinksSolver(ColumnNode? header)
        {
            this.header = header ?? new ColumnNode("header");
            this.solutions = new List<List<DancingNode>>();
        }

        public List<List<DancingNode>> Solve()
        {
            List<DancingNode> partialSolution = new List<DancingNode>();
            Search(0, partialSolution);
            return solutions;
        }

        private void Search(int k, List<DancingNode> partial)
        {
            if (header.Right == header)
            {
                solutions.Add(new List<DancingNode>(partial));
                return;
            }

            ColumnNode? column = null;
            int minSize = int.MaxValue;
            for (DancingNode node = header.Right; node != header; node = node.Right)
            {
                ColumnNode col = (ColumnNode)node;
                if (col.Size < minSize)
                {
                    minSize = col.Size;
                    column = col;
                }
            }

            if (column == null)
            {
                return;
            }

            column.Cover();
            for (DancingNode row = column.Down; row != column; row = row.Down)
            {
                partial.Add(row);
                for (DancingNode node = row.Right; node != row; node = node.Right)
                {
                    node.Column.Cover();
                }

                Search(k + 1, partial);
                partial.RemoveAt(partial.Count - 1);
                for (DancingNode node = row.Left; node != row; node = node.Left)
                {
                    node.Column.Uncover();
                }
            }

            column.Uncover();
        }
    }
}