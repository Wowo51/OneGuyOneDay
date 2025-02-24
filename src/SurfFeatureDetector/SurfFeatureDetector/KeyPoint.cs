namespace SurfFeatureDetector
{
    public class KeyPoint
    {
        public double X { get; }
        public double Y { get; }
        public double Scale { get; }
        public double Orientation { get; }

        public KeyPoint(double x, double y, double scale, double orientation)
        {
            this.X = x;
            this.Y = y;
            this.Scale = scale;
            this.Orientation = orientation;
        }
    }
}