using System;
using System.Collections.Generic;

namespace WatershedTransformation
{
    public abstract class WatershedAlgorithmBase : IWatershedSegmenter
    {
        public abstract int[, ] SegmentImage(int[, ]? image);
        protected static List<(int, int)> GetNeighbors(int i, int j, int height, int width)
        {
            List<(int, int)> neighbors = new List<(int, int)>();
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    if (di == 0 && dj == 0)
                    {
                        continue;
                    }

                    int ni = i + di;
                    int nj = j + dj;
                    if (ni >= 0 && ni < height && nj >= 0 && nj < width)
                    {
                        neighbors.Add((ni, nj));
                    }
                }
            }

            return neighbors;
        }
    }
}