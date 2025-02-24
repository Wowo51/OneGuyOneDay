using System;
using System.Text;

namespace EmbeddedZerotreeWavelet
{
    public class EZWProcessor
    {
        public static string Encode(double[, ] coefficients)
        {
            int numberOfRows = coefficients.GetLength(0);
            int numberOfCols = coefficients.GetLength(1);
            double maxCoefficient = 0.0;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfCols; j++)
                {
                    double absValue = Math.Abs(coefficients[i, j]);
                    if (absValue > maxCoefficient)
                    {
                        maxCoefficient = absValue;
                    }
                }
            }

            double threshold = 1.0;
            while (threshold <= maxCoefficient)
            {
                threshold = threshold * 2.0;
            }

            threshold = threshold / 2.0;
            double initialThreshold = threshold;
            StringBuilder outputBuilder = new StringBuilder();
            while (threshold >= 1.0)
            {
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int j = 0; j < numberOfCols; j++)
                    {
                        double value = coefficients[i, j];
                        if (Math.Abs(value) >= threshold)
                        {
                            if (value > 0)
                            {
                                outputBuilder.Append("P");
                            }
                            else
                            {
                                outputBuilder.Append("N");
                            }
                        }
                        else
                        {
                            outputBuilder.Append("Z");
                        }
                    }
                }

                outputBuilder.Append("R");
                threshold = threshold / 2.0;
            }

            string header = "T=" + initialThreshold + ";";
            return header + outputBuilder.ToString();
        }

        public static double[, ] Decode(string bitStream, int rows, int cols)
        {
            int headerEnd = bitStream.IndexOf(";");
            double initialThreshold = 0.0;
            if (headerEnd > 0)
            {
                string header = bitStream.Substring(0, headerEnd);
                if (header.StartsWith("T="))
                {
                    string thresholdStr = header.Substring(2);
                    initialThreshold = double.Parse(thresholdStr);
                }
            }

            string data = bitStream.Substring(headerEnd + 1);
            double[, ] outputMatrix = new double[rows, cols];
            double currentThreshold = initialThreshold;
            string[] passes = data.Split(new char[] { 'R' }, StringSplitOptions.RemoveEmptyEntries);
            for (int p = 0; p < passes.Length; p++)
            {
                string pass = passes[p];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        int index = i * cols + j;
                        char symbol = pass[index];
                        if (symbol == 'P')
                        {
                            outputMatrix[i, j] += currentThreshold;
                        }
                        else if (symbol == 'N')
                        {
                            outputMatrix[i, j] -= currentThreshold;
                        }
                    }
                }

                currentThreshold = currentThreshold / 2.0;
            }

            return outputMatrix;
        }
    }
}