using System;

namespace GrabcutGraphCuts
{
    public class GrabcutSegmenter
    {
        public void Run()
        {
            ImageData image = new ImageData(5, 5);
            GraphCutSolver solver = new GraphCutSolver();
            bool[, ] segmentation = solver.Segment(image);
            PrintSegmentation(segmentation);
        }

        private void PrintSegmentation(bool[, ] segmentation)
        {
            int rows = segmentation.GetLength(0);
            int cols = segmentation.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(segmentation[i, j] ? "1 " : "0 ");
                }

                Console.WriteLine();
            }
        }
    }
}