using System;
using System.Numerics;

namespace LenstraEllipticFactorization
{
    public class EllipticCurve
    {
        public BigInteger Modulus { get; }
        public BigInteger A { get; }
        public BigInteger B { get; }

        public EllipticCurve(BigInteger modulus, BigInteger a, BigInteger b)
        {
            this.Modulus = modulus;
            this.A = a;
            this.B = b;
        }

        public EllipticCurvePoint Add(EllipticCurvePoint p, EllipticCurvePoint q, out BigInteger factor)
        {
            factor = 0;
            if (p.IsInfinity)
            {
                return q;
            }

            if (q.IsInfinity)
            {
                return p;
            }

            BigInteger mod = this.Modulus;
            BigInteger px = ((p.X % mod) + mod) % mod;
            BigInteger qx = ((q.X % mod) + mod) % mod;
            if (px == qx)
            {
                BigInteger py = ((p.Y % mod) + mod) % mod;
                BigInteger qy = ((q.Y % mod) + mod) % mod;
                if ((((py + qy) % mod) + mod) % mod == 0)
                {
                    return EllipticCurvePoint.Infinity;
                }
                else
                {
                    return this.Double(p, out factor);
                }
            }

            BigInteger numerator = ((q.Y - p.Y) % mod + mod) % mod;
            BigInteger denominator = ((q.X - p.X) % mod + mod) % mod;
            BigInteger inv;
            BigInteger tempFactor;
            if (!MathHelpers.TryModularInverse(denominator, mod, out inv, out tempFactor))
            {
                factor = tempFactor;
                return EllipticCurvePoint.Infinity;
            }

            BigInteger slope = (numerator * inv) % mod;
            BigInteger xR = ((slope * slope) - p.X - q.X) % mod;
            xR = (xR + mod) % mod;
            BigInteger yR = (slope * (p.X - xR) - p.Y) % mod;
            yR = (yR + mod) % mod;
            return new EllipticCurvePoint(xR, yR);
        }

        public EllipticCurvePoint Double(EllipticCurvePoint p, out BigInteger factor)
        {
            factor = 0;
            if (p.IsInfinity)
            {
                return EllipticCurvePoint.Infinity;
            }

            BigInteger mod = this.Modulus;
            BigInteger numerator = (3 * p.X * p.X + this.A) % mod;
            numerator = (numerator + mod) % mod;
            BigInteger denominator = (2 * p.Y) % mod;
            denominator = (denominator + mod) % mod;
            BigInteger inv;
            BigInteger tempFactor;
            if (!MathHelpers.TryModularInverse(denominator, mod, out inv, out tempFactor))
            {
                factor = tempFactor;
                return EllipticCurvePoint.Infinity;
            }

            BigInteger slope = (numerator * inv) % mod;
            BigInteger xR = ((slope * slope) - 2 * p.X) % mod;
            xR = (xR + mod) % mod;
            BigInteger yR = (slope * (p.X - xR) - p.Y) % mod;
            yR = (yR + mod) % mod;
            return new EllipticCurvePoint(xR, yR);
        }

        public EllipticCurvePoint Multiply(EllipticCurvePoint p, BigInteger k, out BigInteger factor)
        {
            factor = 0;
            EllipticCurvePoint result = EllipticCurvePoint.Infinity;
            EllipticCurvePoint addend = p;
            while (k > 0)
            {
                if ((k & 1) != 0)
                {
                    result = this.Add(result, addend, out factor);
                    if (factor != 0)
                    {
                        return EllipticCurvePoint.Infinity;
                    }
                }

                k = k >> 1;
                if (k > 0)
                {
                    addend = this.Double(addend, out factor);
                    if (factor != 0)
                    {
                        return EllipticCurvePoint.Infinity;
                    }
                }
            }

            return result;
        }
    }

    public class EllipticCurvePoint
    {
        public BigInteger X { get; }
        public BigInteger Y { get; }
        public bool IsInfinity { get; }

        private EllipticCurvePoint(BigInteger x, BigInteger y, bool isInfinity)
        {
            this.X = x;
            this.Y = y;
            this.IsInfinity = isInfinity;
        }

        public EllipticCurvePoint(BigInteger x, BigInteger y) : this(x, y, false)
        {
        }

        public static EllipticCurvePoint Infinity { get; } = new EllipticCurvePoint(0, 0, true);
    }

    public static class MathHelpers
    {
        public static bool TryModularInverse(BigInteger a, BigInteger modulus, out BigInteger inverse, out BigInteger factor)
        {
            BigInteger x;
            BigInteger y;
            BigInteger gcd = ExtendedGcd(a, modulus, out x, out y);
            factor = gcd;
            if (gcd != 1)
            {
                inverse = 0;
                return false;
            }

            inverse = ((x % modulus) + modulus) % modulus;
            return true;
        }

        private static BigInteger ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            BigInteger gcd = ExtendedGcd(b, a % b, out BigInteger x1, out BigInteger y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return gcd;
        }
    }
}