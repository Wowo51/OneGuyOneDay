using System;

namespace ChsConversion
{
    public static class ChsConverter
    {
        public static long ChsToLba(int cylinder, int head, int sector, int sectorsPerTrack, int headsPerCylinder)
        {
            if (cylinder < 0 || head < 0 || sector <= 0 || sectorsPerTrack <= 0 || headsPerCylinder <= 0)
            {
                return -1;
            }

            long lba = ((long)cylinder * headsPerCylinder + head) * sectorsPerTrack + (sector - 1);
            return lba;
        }

        public static void LbaToChs(long lba, int sectorsPerTrack, int headsPerCylinder, out int cylinder, out int head, out int sector)
        {
            if (lba < 0 || sectorsPerTrack <= 0 || headsPerCylinder <= 0)
            {
                cylinder = -1;
                head = -1;
                sector = -1;
                return;
            }

            int totalSectorsPerCylinder = headsPerCylinder * sectorsPerTrack;
            cylinder = (int)(lba / totalSectorsPerCylinder);
            long remaining = lba % totalSectorsPerCylinder;
            head = (int)(remaining / sectorsPerTrack);
            sector = (int)(remaining % sectorsPerTrack) + 1;
        }
    }
}