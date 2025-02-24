using System;
using System.Collections.Generic;

namespace SurfFeatureDetector
{
    public class SurfDetector
    {
        public SurfDetector()
        {
        }

        public List<KeyPoint> DetectFeatures(int[, ]? image)
        {
            if (image == null)
            {
                return new List<KeyPoint>();
            }

            int height = image.GetLength(0);
            int width = image.GetLength(1);
            double[, ] responseMap = new double[height, width];
            // Compute approximate Hessian response using finite differences
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    double Dxx = (double)image[i, j + 1] - 2.0 * image[i, j] + (double)image[i, j - 1];
                    double Dyy = (double)image[i + 1, j] - 2.0 * image[i, j] + (double)image[i - 1, j];
                    double Dxy = ((double)image[i + 1, j + 1] - (double)image[i + 1, j - 1] - (double)image[i - 1, j + 1] + (double)image[i - 1, j - 1]) / 4.0;
                    double response = Dxx * Dyy - 0.81 * (Dxy * Dxy);
                    responseMap[i, j] = response;
                }
            }

            List<KeyPoint> keypoints = new List<KeyPoint>();
            double threshold = 100.0;
            // Non-maximum suppression in a 3x3 neighborhood
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    double currentResponse = responseMap[i, j];
                    if (currentResponse < threshold)
                    {
                        continue;
                    }

                    bool isLocalMax = true;
                    for (int di = -1; di <= 1 && isLocalMax; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (di == 0 && dj == 0)
                            {
                                continue;
                            }

                            if (responseMap[i + di, j + dj] > currentResponse)
                            {
                                isLocalMax = false;
                                break;
                            }
                        }
                    }

                    if (!isLocalMax)
                    {
                        continue;
                    }

                    // Compute orientation using gradient estimation
                    double dx = (double)image[i, j + 1] - (double)image[i, j - 1];
                    double dy = (double)image[i + 1, j] - (double)image[i - 1, j];
                    double orientation = Math.Atan2(dy, dx) * (180.0 / Math.PI);
                    keypoints.Add(new KeyPoint(j, i, 1.2, orientation));
                }
            }

            return keypoints;
        }
    }
}