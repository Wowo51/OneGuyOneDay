namespace DancingLinks
{
    public static class DancingLinksBuilder
    {
        public static ColumnNode BuildDLXBoard(int[, ] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            ColumnNode header = new ColumnNode("header");
            System.Collections.Generic.List<ColumnNode> columnNodes = new System.Collections.Generic.List<ColumnNode>(cols);
            ColumnNode prevCol = header;
            for (int j = 0; j < cols; j++)
            {
                ColumnNode col = new ColumnNode(j.ToString());
                columnNodes.Add(col);
                prevCol.Right = col;
                col.Left = prevCol;
                prevCol = col;
            }

            prevCol.Right = header;
            header.Left = prevCol;
            for (int i = 0; i < rows; i++)
            {
                DancingNode? firstNodeInRow = null;
                DancingNode? prevNodeInRow = null;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        ColumnNode col = columnNodes[j];
                        DancingNode newNode = new DancingNode();
                        newNode.Column = col;
                        newNode.Up = col.Up;
                        newNode.Down = col;
                        col.Up.Down = newNode;
                        col.Up = newNode;
                        col.Size++;
                        if (firstNodeInRow == null)
                        {
                            firstNodeInRow = newNode;
                            prevNodeInRow = newNode;
                        }
                        else
                        {
                            newNode.Left = prevNodeInRow;
                            prevNodeInRow.Right = newNode;
                            prevNodeInRow = newNode;
                        }
                    }
                }

                if (firstNodeInRow != null && prevNodeInRow != null)
                {
                    firstNodeInRow.Left = prevNodeInRow;
                    prevNodeInRow.Right = firstNodeInRow;
                }
            }

            return header;
        }
    }
}