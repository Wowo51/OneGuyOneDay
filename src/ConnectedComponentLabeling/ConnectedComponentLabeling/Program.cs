using System;

namespace ConnectedComponentLabeling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool[, ] image = new bool[, ]
            {
                {
                    true,
                    true,
                    false,
                    false
                },
                {
                    true,
                    false,
                    false,
                    true
                },
                {
                    false,
                    false,
                    true,
                    true
                },
                {
                    false,
                    true,
                    true,
                    false
                }
            };
            int[, ] labeledImage = ConnectedComponentLabeler.LabelComponents(image);
            int rows = labeledImage.GetLength(0);
            int cols = labeledImage.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(labeledImage[row, col] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}