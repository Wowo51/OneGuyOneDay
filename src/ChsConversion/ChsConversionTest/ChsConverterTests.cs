using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChsConversion;

namespace ChsConversionTest
{
    [TestClass]
    public class ChsConverterTests
    {
        [TestMethod]
        public void TestChsToLba_Valid()
        {
            int cylinder = 0;
            int head = 0;
            int sector = 1;
            int sectorsPerTrack = 63;
            int headsPerCylinder = 16;
            long expectedLba = ((long)cylinder * headsPerCylinder + head) * sectorsPerTrack + (sector - 1);
            long actualLba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(expectedLba, actualLba, "LBA conversion did not match expected value.");
        }

        [TestMethod]
        public void TestChsToLba_Invalid()
        {
            int cylinder;
            int head;
            int sector;
            int sectorsPerTrack;
            int headsPerCylinder;
            long lba;
            // Invalid cylinder
            cylinder = -1;
            head = 0;
            sector = 1;
            sectorsPerTrack = 63;
            headsPerCylinder = 16;
            lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(-1, lba, "Invalid cylinder should result in -1.");
            // Invalid head
            cylinder = 0;
            head = -1;
            sector = 1;
            lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(-1, lba, "Invalid head should result in -1.");
            // Invalid sector
            cylinder = 0;
            head = 0;
            sector = 0;
            lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(-1, lba, "Sector equal to 0 should result in -1.");
            // Invalid sectorsPerTrack
            cylinder = 0;
            head = 0;
            sector = 1;
            sectorsPerTrack = 0;
            lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(-1, lba, "Invalid sectorsPerTrack (0) should result in -1.");
            // Invalid headsPerCylinder
            sectorsPerTrack = 63;
            headsPerCylinder = 0;
            lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
            Assert.AreEqual(-1, lba, "Invalid headsPerCylinder (0) should result in -1.");
        }

        [TestMethod]
        public void TestLbaToChs_Valid()
        {
            int sectorsPerTrack = 63;
            int headsPerCylinder = 16;
            int cylinder;
            int head;
            int sector;
            // Test LBA = 0 should be CHS 0,0,1
            ChsConverter.LbaToChs(0, sectorsPerTrack, headsPerCylinder, out cylinder, out head, out sector);
            Assert.AreEqual(0, cylinder);
            Assert.AreEqual(0, head);
            Assert.AreEqual(1, sector);
            // Test a specific conversion
            int testCylinder = 1;
            int testHead = 2;
            int testSector = 10;
            long lba = ChsConverter.ChsToLba(testCylinder, testHead, testSector, sectorsPerTrack, headsPerCylinder);
            ChsConverter.LbaToChs(lba, sectorsPerTrack, headsPerCylinder, out cylinder, out head, out sector);
            Assert.AreEqual(testCylinder, cylinder);
            Assert.AreEqual(testHead, head);
            Assert.AreEqual(testSector, sector);
        }

        [TestMethod]
        public void TestLbaToChs_Invalid()
        {
            int sectorsPerTrack = 63;
            int headsPerCylinder = 16;
            int cylinder;
            int head;
            int sector;
            // Negative LBA
            ChsConverter.LbaToChs(-5, sectorsPerTrack, headsPerCylinder, out cylinder, out head, out sector);
            Assert.AreEqual(-1, cylinder);
            Assert.AreEqual(-1, head);
            Assert.AreEqual(-1, sector);
            // Invalid sectorsPerTrack
            ChsConverter.LbaToChs(123, 0, headsPerCylinder, out cylinder, out head, out sector);
            Assert.AreEqual(-1, cylinder);
            Assert.AreEqual(-1, head);
            Assert.AreEqual(-1, sector);
            // Invalid headsPerCylinder
            ChsConverter.LbaToChs(123, sectorsPerTrack, 0, out cylinder, out head, out sector);
            Assert.AreEqual(-1, cylinder);
            Assert.AreEqual(-1, head);
            Assert.AreEqual(-1, sector);
        }

        [TestMethod]
        public void TestChsConversion_RoundTrip()
        {
            int sectorsPerTrack = 63;
            int headsPerCylinder = 16;
            System.Random random = new System.Random(42);
            for (int index = 0; index < 1000; index++)
            {
                int cylinder = random.Next(0, 100);
                int head = random.Next(0, headsPerCylinder);
                int sector = random.Next(1, sectorsPerTrack + 1);
                long lba = ChsConverter.ChsToLba(cylinder, head, sector, sectorsPerTrack, headsPerCylinder);
                int rcylinder;
                int rhead;
                int rsector;
                ChsConverter.LbaToChs(lba, sectorsPerTrack, headsPerCylinder, out rcylinder, out rhead, out rsector);
                Assert.AreEqual(cylinder, rcylinder, "Cylinder roundtrip failed.");
                Assert.AreEqual(head, rhead, "Head roundtrip failed.");
                Assert.AreEqual(sector, rsector, "Sector roundtrip failed.");
            }
        }
    }
}