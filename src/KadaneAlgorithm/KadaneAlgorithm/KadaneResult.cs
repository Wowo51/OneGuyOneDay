namespace KadaneAlgorithm
{
    public class KadaneResult
    {
        public int MaxSum { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int[] SubArray { get; set; } = System.Array.Empty<int>();
    }
}