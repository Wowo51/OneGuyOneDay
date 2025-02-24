namespace NTRUEncrypt
{
    public class NTRUParameters
    {
        public int N { get; }
        public int p { get; }
        public int q { get; }

        public NTRUParameters()
        {
            N = 11;
            p = 3;
            q = 32;
        }

        public NTRUParameters(int n, int pVal, int qVal)
        {
            N = n;
            p = pVal;
            q = qVal;
        }
    }
}