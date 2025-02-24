using System;
using System.Numerics;

namespace EllipticCurveCryptography
{
    public static class ECCryptography
    {
        public static ECPoint Add(EllipticCurve curve, ECPoint p, ECPoint q)
        {
            if (p == null || q == null)
            {
                return ECPoint.Infinity;
            }

            if (p.IsInfinity)
            {
                return q;
            }

            if (q.IsInfinity)
            {
                return p;
            }

            if (p.X == q.X)
            {
                if ((p.Y + q.Y) % curve.Prime == 0)
                {
                    return ECPoint.Infinity;
                }

                return Double(curve, p);
            }

            BigInteger delta = (q.X - p.X) % curve.Prime;
            if (delta < 0)
            {
                delta += curve.Prime;
            }

            BigInteger inverse = ModInverse(delta, curve.Prime);
            BigInteger slope = (((q.Y - p.Y) % curve.Prime) * inverse) % curve.Prime;
            if (slope < 0)
            {
                slope += curve.Prime;
            }

            BigInteger xR = (BigInteger.ModPow(slope, 2, curve.Prime) - p.X - q.X) % curve.Prime;
            if (xR < 0)
            {
                xR += curve.Prime;
            }

            BigInteger yR = (slope * (p.X - xR) - p.Y) % curve.Prime;
            if (yR < 0)
            {
                yR += curve.Prime;
            }

            return new ECPoint(xR, yR);
        }

        public static ECPoint Double(EllipticCurve curve, ECPoint p)
        {
            if (p == null || p.IsInfinity)
            {
                return ECPoint.Infinity;
            }

            if (p.Y == 0)
            {
                return ECPoint.Infinity;
            }

            BigInteger numerator = (3 * BigInteger.ModPow(p.X, 2, curve.Prime) + curve.A) % curve.Prime;
            if (numerator < 0)
            {
                numerator += curve.Prime;
            }

            BigInteger denominator = (2 * p.Y) % curve.Prime;
            if (denominator < 0)
            {
                denominator += curve.Prime;
            }

            BigInteger inverse = ModInverse(denominator, curve.Prime);
            BigInteger slope = (numerator * inverse) % curve.Prime;
            if (slope < 0)
            {
                slope += curve.Prime;
            }

            BigInteger xR = (BigInteger.ModPow(slope, 2, curve.Prime) - 2 * p.X) % curve.Prime;
            if (xR < 0)
            {
                xR += curve.Prime;
            }

            BigInteger yR = (slope * (p.X - xR) - p.Y) % curve.Prime;
            if (yR < 0)
            {
                yR += curve.Prime;
            }

            return new ECPoint(xR, yR);
        }

        public static ECPoint Multiply(EllipticCurve curve, BigInteger k, ECPoint p)
        {
            ECPoint result = ECPoint.Infinity;
            ECPoint addend = p;
            while (k > 0)
            {
                if ((k & 1) == 1)
                {
                    result = Add(curve, result, addend);
                }

                addend = Double(curve, addend);
                k = k >> 1;
            }

            return result;
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            a = a % m;
            if (a < 0)
            {
                a += m;
            }

            BigInteger m0 = m;
            BigInteger x0 = 0;
            BigInteger x1 = 1;
            if (m == 1)
            {
                return 0;
            }

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }
    }
}