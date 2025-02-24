using System;
using System.Collections.Generic;

namespace ConnectedComponentLabeling
{
    public class ConnectedComponentLabeler
    {
        public static int[, ] LabelComponents(bool[, ] image)
        {
            if (image == null)
            {
                return new int[0, 0];
            }

            int rows = image.GetLength(0);
            int cols = image.GetLength(1);
            int[, ] labels = new int[rows, cols];
            int currentLabel = 1;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (image[row, col] && labels[row, col] == 0)
                    {
                        DFSMark(image, labels, row, col, rows, cols, currentLabel);
                        currentLabel++;
                    }
                }
            }

            return labels;
        }

        private static void DFSMark(bool[, ] image, int[, ] labels, int row, int col, int rows, int cols, int label)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((row, col));
            while (stack.Count > 0)
            {
                (int, int) current = stack.Pop();
                int r = current.Item1;
                int c = current.Item2;
                if (r < 0 || r >= rows || c < 0 || c >= cols)
                {
                    continue;
                }

                if (!image[r, c])
                {
                    continue;
                }

                if (labels[r, c] != 0)
                {
                    continue;
                }

                labels[r, c] = label;
                stack.Push((r - 1, c));
                stack.Push((r + 1, c));
                stack.Push((r, c - 1));
                stack.Push((r, c + 1));
            }
        }
    }
}