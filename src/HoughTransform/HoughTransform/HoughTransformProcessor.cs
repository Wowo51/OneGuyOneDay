using System;
using System.Collections.Generic;

namespace HoughTransform
{
    public class HoughTransformProcessor
    {
        public List<HoughLine> ProcessImage(bool[, ] image, int threshold)
        {
            if (image == null)
            {
                return new List<HoughLine>();
            }

            int rows = image.GetLength(0);
            int cols = image.GetLength(1);
            double diagonal = Math.Ceiling(Math.Sqrt((double)(cols * cols + rows * rows)));
            int rhoBins = (int)(2 * diagonal + 1);
            int thetaBins = 180;
            int[, ] accumulator = new int[rhoBins, thetaBins];
            double[] cosTable = new double[thetaBins];
            double[] sinTable = new double[thetaBins];
            for (int theta = 0; theta < thetaBins; theta++)
            {
                double thetaRad = Math.PI * theta / 180.0;
                cosTable[theta] = Math.Cos(thetaRad);
                sinTable[theta] = Math.Sin(thetaRad);
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (image[y, x])
                    {
                        for (int theta = 0; theta < thetaBins; theta++)
                        {
                            double rho = x * cosTable[theta] + y * sinTable[theta];
                            int rhoIndex = (int)Math.Round(rho + diagonal);
                            if (rhoIndex >= 0 && rhoIndex < rhoBins)
                            {
                                accumulator[rhoIndex, theta]++;
                            }
                        }
                    }
                }
            }

            List<HoughLine> lines = new List<HoughLine>();
            for (int rhoIndex = 0; rhoIndex < rhoBins; rhoIndex++)
            {
                for (int theta = 0; theta < thetaBins; theta++)
                {
                    if (accumulator[rhoIndex, theta] >= threshold)
                    {
                        double rhoValue = rhoIndex - diagonal;
                        HoughLine line = new HoughLine();
                        line.Theta = theta;
                        line.Rho = rhoValue;
                        line.Votes = accumulator[rhoIndex, theta];
                        lines.Add(line);
                    }
                }
            }

            return lines;
        }
    }
}