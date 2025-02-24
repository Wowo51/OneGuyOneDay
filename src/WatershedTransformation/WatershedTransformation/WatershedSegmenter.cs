using System;
using System.Collections.Generic;

namespace WatershedTransformation
{
    public class WatershedSegmenter : WatershedAlgorithmBase
    {
        public override int[, ] SegmentImage(int[, ]? image)
        {
            if (image == null)
            {
                return new int[0, 0];
            }

            int height = image.GetLength(0);
            int width = image.GetLength(1);
            int[, ] labels = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    labels[i, j] = -1;
                }
            }

            const int WSHED = 0;
            int currentLabel = 1;
            List<Pixel> pixelList = new List<Pixel>();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Pixel p = new Pixel
                    {
                        Row = i,
                        Col = j,
                        Intensity = image[i, j]
                    };
                    pixelList.Add(p);
                }
            }

            pixelList.Sort(Comparison);
            for (int index = 0; index < pixelList.Count; index++)
            {
                Pixel p = pixelList[index];
                int i = p.Row;
                int j = p.Col;
                List<(int, int)> neighbors = GetNeighbors(i, j, height, width);
                HashSet<int> neighborLabels = new HashSet<int>();
                for (int k = 0; k < neighbors.Count; k++)
                {
                    int ni = neighbors[k].Item1;
                    int nj = neighbors[k].Item2;
                    int neighborLabel = labels[ni, nj];
                    if (neighborLabel > 0)
                    {
                        neighborLabels.Add(neighborLabel);
                    }
                }

                if (neighborLabels.Count == 0)
                {
                    labels[i, j] = currentLabel;
                    currentLabel++;
                }
                else if (neighborLabels.Count == 1)
                {
                    foreach (int label in neighborLabels)
                    {
                        labels[i, j] = label;
                        break;
                    }
                }
                else
                {
                    labels[i, j] = WSHED;
                }
            }

            return labels;
        }

        private static int Comparison(Pixel a, Pixel b)
        {
            if (a.Intensity < b.Intensity)
            {
                return -1;
            }
            else if (a.Intensity > b.Intensity)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private class Pixel
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int Intensity { get; set; }
        }
    }
}