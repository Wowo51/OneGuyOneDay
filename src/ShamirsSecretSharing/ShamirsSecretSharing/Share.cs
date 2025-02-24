using System;

namespace ShamirsSecretSharing
{
    public class Share
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Share(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}