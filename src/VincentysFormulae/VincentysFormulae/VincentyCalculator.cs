using System;

namespace VincentysFormulae
{
    public static class VincentyCalculator
    {
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double pi = Math.PI;
            double toRad = pi / 180.0;
            double latitude1 = lat1 * toRad;
            double longitude1 = lon1 * toRad;
            double latitude2 = lat2 * toRad;
            double longitude2 = lon2 * toRad;
            // WGS-84 ellipsoid parameters
            double a = 6378137.0;
            double b = 6356752.314245;
            double f = 1.0 / 298.257223563;
            double L = longitude2 - longitude1;
            double U1 = Math.Atan((1 - f) * Math.Tan(latitude1));
            double U2 = Math.Atan((1 - f) * Math.Tan(latitude2));
            double sinU1 = Math.Sin(U1);
            double cosU1 = Math.Cos(U1);
            double sinU2 = Math.Sin(U2);
            double cosU2 = Math.Cos(U2);
            double lambda = L;
            double lambdaP = 0.0;
            int iterationLimit = 100;
            double sinSigma = 0.0;
            double cosSigma = 0.0;
            double sigma = 0.0;
            double sinAlpha = 0.0;
            double cosSqAlpha = 0.0;
            double cos2SigmaM = 0.0;
            double C = 0.0;
            int iteration = 0;
            while (iteration < iterationLimit)
            {
                double sinLambda = Math.Sin(lambda);
                double cosLambda = Math.Cos(lambda);
                sinSigma = Math.Sqrt((cosU2 * sinLambda) * (cosU2 * sinLambda) + (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) * (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda));
                if (sinSigma == 0)
                {
                    return 0.0; // coincident points
                }

                cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda;
                sigma = Math.Atan2(sinSigma, cosSigma);
                sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
                cosSqAlpha = 1 - sinAlpha * sinAlpha;
                if (cosSqAlpha == 0)
                {
                    cos2SigmaM = 0; // equatorial line
                }
                else
                {
                    cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha;
                }

                C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha));
                lambdaP = lambda;
                lambda = L + (1 - C) * f * sinAlpha * (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));
                if (Math.Abs(lambda - lambdaP) <= 1e-12)
                {
                    break;
                }

                iteration = iteration + 1;
            }

            if (iteration >= iterationLimit)
            {
                // formula failed to converge
                return double.NaN;
            }

            double uSq = cosSqAlpha * (a * a - b * b) / (b * b);
            double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
            double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
            double deltaSigma = B * sinSigma * (cos2SigmaM + B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) - B / 6 * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));
            double distance = b * A * (sigma - deltaSigma);
            return distance;
        }
    }
}