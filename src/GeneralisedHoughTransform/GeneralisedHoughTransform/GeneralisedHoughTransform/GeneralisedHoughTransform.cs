using System;
using System.Collections.Generic;

namespace GeneralisedHoughTransform
{
    public class GeneralisedHoughTransform
    {
        private Dictionary<int, List<Offset>> angleOffsets;
        private const double EdgeThreshold = 10.0;
        private struct Offset
        {
            public int Row;
            public int Col;
            public Offset(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }

        public GeneralisedHoughTransform()
        {
            angleOffsets = new Dictionary<int, List<Offset>>();
        }

        public void SetTemplate(double[, ] template)
        {
            if (template is null)
            {
                return;
            }

            int templateRows = template.GetLength(0);
            int templateCols = template.GetLength(1);
            int centerRow = templateRows / 2;
            int centerCol = templateCols / 2;
            angleOffsets.Clear();
            for (int row = 1; row < templateRows - 1; row++)
            {
                for (int col = 1; col < templateCols - 1; col++)
                {
                    double gradX;
                    double gradY;
                    ComputeGradient(template, row, col, out gradX, out gradY);
                    double magnitude = Math.Sqrt(gradX * gradX + gradY * gradY);
                    if (magnitude >= EdgeThreshold)
                    {
                        double angle = Math.Atan2(gradY, gradX) * (180.0 / Math.PI);
                        if (angle < 0)
                        {
                            angle += 360;
                        }

                        int quantizedAngle = (int)Math.Round(angle) % 360;
                        int offsetRow = row - centerRow;
                        int offsetCol = col - centerCol;
                        Offset offset = new Offset(offsetRow, offsetCol);
                        if (!angleOffsets.ContainsKey(quantizedAngle))
                        {
                            angleOffsets[quantizedAngle] = new List<Offset>();
                        }

                        angleOffsets[quantizedAngle].Add(offset);
                    }
                }
            }
        }

        public int[, ] Detect(double[, ] image)
        {
            if (image is null)
            {
                return new int[0, 0];
            }

            int imageRows = image.GetLength(0);
            int imageCols = image.GetLength(1);
            int[, ] accumulator = new int[imageRows, imageCols];
            for (int row = 1; row < imageRows - 1; row++)
            {
                for (int col = 1; col < imageCols - 1; col++)
                {
                    double gradX;
                    double gradY;
                    ComputeGradient(image, row, col, out gradX, out gradY);
                    double magnitude = Math.Sqrt(gradX * gradX + gradY * gradY);
                    if (magnitude >= EdgeThreshold)
                    {
                        double angle = Math.Atan2(gradY, gradX) * (180.0 / Math.PI);
                        if (angle < 0)
                        {
                            angle += 360;
                        }

                        int quantizedAngle = (int)Math.Round(angle) % 360;
                        for (int a = quantizedAngle - 1; a <= quantizedAngle + 1; a++)
                        {
                            int key = (a + 360) % 360;
                            if (angleOffsets.ContainsKey(key))
                            {
                                List<Offset> offsets = angleOffsets[key];
                                for (int i = 0; i < offsets.Count; i++)
                                {
                                    int posRow = row + offsets[i].Row;
                                    int posCol = col + offsets[i].Col;
                                    if (posRow >= 0 && posRow < imageRows && posCol >= 0 && posCol < imageCols)
                                    {
                                        accumulator[posRow, posCol]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return accumulator;
        }

        private void ComputeGradient(double[, ] image, int row, int col, out double gradX, out double gradY)
        {
            gradX = image[row, col + 1] - image[row, col - 1];
            gradY = image[row + 1, col] - image[row - 1, col];
        }
    }
}