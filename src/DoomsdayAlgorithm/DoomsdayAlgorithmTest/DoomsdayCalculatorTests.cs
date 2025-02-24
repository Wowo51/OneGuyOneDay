using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoomsdayAlgorithm;

namespace DoomsdayAlgorithmTest
{
    [TestClass]
    public class DoomsdayCalculatorTests
    {
        [TestMethod]
        public void TestValidDates()
        {
            DateTime testDate = new DateTime(2021, 6, 6);
            DayOfWeek expected = testDate.DayOfWeek;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(2021, 6, 6);
            Assert.AreEqual(expected, actual);
            testDate = new DateTime(2020, 2, 29);
            expected = testDate.DayOfWeek;
            actual = DoomsdayCalculator.GetDayOfWeek(2020, 2, 29);
            Assert.AreEqual(expected, actual);
            testDate = new DateTime(2019, 2, 28);
            expected = testDate.DayOfWeek;
            actual = DoomsdayCalculator.GetDayOfWeek(2019, 2, 28);
            Assert.AreEqual(expected, actual);
            testDate = new DateTime(2000, 1, 1);
            expected = testDate.DayOfWeek;
            actual = DoomsdayCalculator.GetDayOfWeek(2000, 1, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidYearLow()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(0, 1, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidYearHigh()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(10000, 1, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidMonthLow()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(2021, 0, 15);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidMonthHigh()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(2021, 13, 15);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidDayLow()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(2021, 6, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidDayHigh()
        {
            DayOfWeek expected = DayOfWeek.Sunday;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(2021, 6, 31);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetDayOfWeek_DateTimeOverload()
        {
            DateTime testDate = new DateTime(2021, 12, 25);
            DayOfWeek expected = testDate.DayOfWeek;
            DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(testDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSyntheticDateDataGeneration()
        {
            for (int year = 2020; year <= 2021; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        DateTime date = new DateTime(year, month, day);
                        DayOfWeek expected = date.DayOfWeek;
                        DayOfWeek actual = DoomsdayCalculator.GetDayOfWeek(year, month, day);
                        Assert.AreEqual(expected, actual, string.Format("Failed for {0}/{1}/{2}", year, month, day));
                    }
                }
            }
        }
    }
}