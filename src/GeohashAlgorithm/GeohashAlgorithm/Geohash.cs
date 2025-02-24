using System;

namespace GeohashAlgorithm
{
    public static class Geohash
    {
        public static string Encode(double latitude, double longitude, int precision = 12)
        {
            if (precision <= 0)
            {
                return string.Empty;
            }

            string base32 = "0123456789bcdefghjkmnpqrstuvwxyz";
            double latMin = -90.0;
            double latMax = 90.0;
            double lonMin = -180.0;
            double lonMax = 180.0;
            bool isEven = true;
            int bit = 0;
            int ch = 0;
            string geohash = "";
            while (geohash.Length < precision)
            {
                if (isEven)
                {
                    double mid = (lonMin + lonMax) / 2.0;
                    if (longitude >= mid)
                    {
                        ch = (ch << 1) | 1;
                        lonMin = mid;
                    }
                    else
                    {
                        ch = (ch << 1);
                        lonMax = mid;
                    }
                }
                else
                {
                    double mid = (latMin + latMax) / 2.0;
                    if (latitude >= mid)
                    {
                        ch = (ch << 1) | 1;
                        latMin = mid;
                    }
                    else
                    {
                        ch = (ch << 1);
                        latMax = mid;
                    }
                }

                isEven = !isEven;
                bit++;
                if (bit == 5)
                {
                    geohash = geohash + base32[ch];
                    bit = 0;
                    ch = 0;
                }
            }

            return geohash;
        }
    }
}