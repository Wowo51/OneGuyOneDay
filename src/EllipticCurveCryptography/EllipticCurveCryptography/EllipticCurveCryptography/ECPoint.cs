using System.Numerics;

namespace EllipticCurveCryptography
{
    public class ECPoint
    {
        public BigInteger X { get; }
        public BigInteger Y { get; }
        public bool IsInfinity { get; }

        public ECPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
            IsInfinity = false;
        }

        private ECPoint()
        {
            X = 0;
            Y = 0;
            IsInfinity = true;
        }

        public static ECPoint Infinity => new ECPoint();

        public override string ToString()
        {
            if (IsInfinity)
            {
                return "Infinity";
            }

            return $"({X}, {Y})";
        }
    }
}