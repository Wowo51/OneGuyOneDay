using System;
using System.Collections.Generic;

namespace RegionGrowing
{
    public class RegionGrower
    {
        public bool[, ] GrowRegion(int[, ] image, int seedRow, int seedCol, int threshold)
        {
            if (image == null)
            {
                return new bool[0, 0];
            }

            int rows = image.GetLength(0);
            int cols = image.GetLength(1);
            if (seedRow < 0 || seedRow >= rows || seedCol < 0 || seedCol >= cols)
            {
                return new bool[rows, cols];
            }

            bool[, ] region = new bool[rows, cols];
            bool[, ] visited = new bool[rows, cols];
            int seedValue = image[seedRow, seedCol];
            Queue<Pixel> queue = new Queue<Pixel>();
            queue.Enqueue(new Pixel(seedRow, seedCol));
            while (queue.Count > 0)
            {
                Pixel current = queue.Dequeue();
                if (visited[current.Row, current.Col])
                {
                    continue;
                }

                visited[current.Row, current.Col] = true;
                int currentValue = image[current.Row, current.Col];
                if (AbsoluteDifference(currentValue, seedValue) <= threshold)
                {
                    region[current.Row, current.Col] = true;
                    List<Pixel> neighbors = GetNeighbors(current, rows, cols);
                    for (int index = 0; index < neighbors.Count; index++)
                    {
                        Pixel neighbor = neighbors[index];
                        if (!visited[neighbor.Row, neighbor.Col])
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return region;
        }

        private int AbsoluteDifference(int a, int b)
        {
            return (a > b) ? a - b : b - a;
        }

        private List<Pixel> GetNeighbors(Pixel pixel, int maxRows, int maxCols)
        {
            List<Pixel> neighbors = new List<Pixel>();
            if (pixel.Row > 0)
            {
                neighbors.Add(new Pixel(pixel.Row - 1, pixel.Col));
            }

            if (pixel.Row < maxRows - 1)
            {
                neighbors.Add(new Pixel(pixel.Row + 1, pixel.Col));
            }

            if (pixel.Col > 0)
            {
                neighbors.Add(new Pixel(pixel.Row, pixel.Col - 1));
            }

            if (pixel.Col < maxCols - 1)
            {
                neighbors.Add(new Pixel(pixel.Row, pixel.Col + 1));
            }

            return neighbors;
        }

        private struct Pixel
        {
            public int Row;
            public int Col;
            public Pixel(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
    }
}