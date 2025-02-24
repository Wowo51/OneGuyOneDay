using System;
using System.Collections.Generic;

namespace MedianFiltering
{
    public static class MedianFilter
    {
        public static int[, ] ApplyMedianFilter(int[, ] image, int windowSize)
        {
            if (image == null)
            {
                return new int[0, 0];
            }

            if (windowSize % 2 == 0)
            {
                windowSize = windowSize + 1;
            }

            int height = image.GetLength(0);
            int width = image.GetLength(1);
            int[, ] output = new int[height, width];
            int offset = windowSize / 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    List<int> window = new List<int>();
                    for (int a = -offset; a <= offset; a++)
                    {
                        for (int b = -offset; b <= offset; b++)
                        {
                            int row = i + a;
                            int col = j + b;
                            if (row >= 0 && row < height && col >= 0 && col < width)
                            {
                                window.Add(image[row, col]);
                            }
                        }
                    }

                    window.Sort();
                    int median = window[window.Count / 2];
                    output[i, j] = median;
                }
            }

            return output;
        }
    }
}