using System.Numerics;

namespace EllipticCurveCryptography
{
    public class EllipticCurve
    {
        public BigInteger A { get; }
        public BigInteger B { get; }
        public BigInteger Prime { get; }

        public EllipticCurve(BigInteger a, BigInteger b, BigInteger prime)
        {
            A = a;
            B = b;
            Prime = prime;
        }

        public bool IsOnCurve(ECPoint point)
        {
            if (point == null)
            {
                return false;
            }

            if (point.IsInfinity)
            {
                return true;
            }

            BigInteger x = point.X;
            BigInteger y = point.Y;
            BigInteger left = BigInteger.ModPow(y, 2, Prime);
            BigInteger right = (BigInteger.ModPow(x, 3, Prime) + A * x + B) % Prime;
            if (right < 0)
            {
                right += Prime;
            }

            return left == (right % Prime);
        }
    }
}