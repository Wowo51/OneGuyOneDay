using System;
using System.Collections.Generic;

namespace BlockTruncationCoding
{
    public class BTCBlock
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public double LowLevel { get; set; }
        public double HighLevel { get; set; }
        public bool[, ] BitPlane { get; set; }

        public BTCBlock(int startX, int startY, int blockWidth, int blockHeight)
        {
            StartX = startX;
            StartY = startY;
            BlockWidth = blockWidth;
            BlockHeight = blockHeight;
            BitPlane = new bool[blockHeight, blockWidth];
        }
    }

    public class BTCCompressor
    {
        public static List<BTCBlock> Compress(byte[, ] image, int blockSize)
        {
            List<BTCBlock> blocks = new List<BTCBlock>();
            int imageHeight = image.GetLength(0);
            int imageWidth = image.GetLength(1);
            for (int row = 0; row < imageHeight; row += blockSize)
            {
                for (int col = 0; col < imageWidth; col += blockSize)
                {
                    int currentBlockHeight = Math.Min(blockSize, imageHeight - row);
                    int currentBlockWidth = Math.Min(blockSize, imageWidth - col);
                    double sum = 0;
                    double sumSq = 0;
                    for (int i = 0; i < currentBlockHeight; i++)
                    {
                        for (int j = 0; j < currentBlockWidth; j++)
                        {
                            byte pixel = image[row + i, col + j];
                            sum += pixel;
                            sumSq += pixel * pixel;
                        }
                    }

                    int totalPixels = currentBlockHeight * currentBlockWidth;
                    double mean = sum / totalPixels;
                    double variance = (sumSq / totalPixels) - (mean * mean);
                    if (variance < 0)
                    {
                        variance = 0;
                    }

                    double s = Math.Sqrt(variance);
                    BTCBlock block = new BTCBlock(col, row, currentBlockWidth, currentBlockHeight);
                    int p = 0;
                    for (int i = 0; i < currentBlockHeight; i++)
                    {
                        for (int j = 0; j < currentBlockWidth; j++)
                        {
                            byte pixel = image[row + i, col + j];
                            if (pixel >= mean)
                            {
                                block.BitPlane[i, j] = true;
                                p++;
                            }
                            else
                            {
                                block.BitPlane[i, j] = false;
                            }
                        }
                    }

                    if (p == 0 || p == totalPixels)
                    {
                        block.LowLevel = mean;
                        block.HighLevel = mean;
                    }
                    else
                    {
                        block.LowLevel = mean - s * Math.Sqrt((double)p / (totalPixels - p));
                        block.HighLevel = mean + s * Math.Sqrt((double)(totalPixels - p) / p);
                    }

                    blocks.Add(block);
                }
            }

            return blocks;
        }

        public static byte[, ] Decompress(List<BTCBlock> blocks, int imageWidth, int imageHeight, int blockSize)
        {
            byte[, ] image = new byte[imageHeight, imageWidth];
            foreach (BTCBlock block in blocks)
            {
                for (int i = 0; i < block.BlockHeight; i++)
                {
                    for (int j = 0; j < block.BlockWidth; j++)
                    {
                        double pixelValue = block.BitPlane[i, j] ? block.HighLevel : block.LowLevel;
                        if (pixelValue < 0)
                        {
                            pixelValue = 0;
                        }
                        else if (pixelValue > 255)
                        {
                            pixelValue = 255;
                        }

                        image[block.StartY + i, block.StartX + j] = (byte)Math.Round(pixelValue);
                    }
                }
            }

            return image;
        }
    }
}