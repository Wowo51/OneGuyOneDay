namespace GrabcutGraphCuts
{
    public class ImageData
    {
        public int Width { get; }
        public int Height { get; }
        public int[, ] Pixels { get; set; }

        public ImageData(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new int[height, width];
        }
    }
}