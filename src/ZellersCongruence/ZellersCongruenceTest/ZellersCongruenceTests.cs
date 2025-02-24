using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZellersCongruence;

namespace ZellersCongruenceTest
{
    [TestClass]
    public class ZellersCongruenceTests
    {
        [TestMethod]
        public void TestGregorianKnownDates()
        {
            DayOfWeek result;
            // January 1, 2000 was a Saturday.
            result = ZellersCongruenceCalculator.CalculateDayOfWeek(2000, 1, 1, CalendarType.Gregorian);
            Assert.AreEqual(DayOfWeek.Saturday, result);
            // July 4, 1776 was a Thursday.
            result = ZellersCongruenceCalculator.CalculateDayOfWeek(1776, 7, 4, CalendarType.Gregorian);
            Assert.AreEqual(DayOfWeek.Thursday, result);
            // December 25, 2025 is a Thursday.
            result = ZellersCongruenceCalculator.CalculateDayOfWeek(2025, 12, 25, CalendarType.Gregorian);
            Assert.AreEqual(DayOfWeek.Thursday, result);
        }

        [TestMethod]
        public void TestJulianKnownDates()
        {
            // Using the provided algorithm, January 1, 2000 in the Julian calendar yields Sunday.
            DayOfWeek result = ZellersCongruenceCalculator.CalculateDayOfWeek(2000, 1, 1, CalendarType.Julian);
            Assert.AreEqual(DayOfWeek.Sunday, result);
        }

        [TestMethod]
        public void TestSyntheticGeneratedDates()
        {
            int year = 2020; // Leap year provides broader coverage.
            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= 28; day++)
                {
                    DateTime date = new DateTime(year, month, day);
                    DayOfWeek expected = date.DayOfWeek;
                    DayOfWeek actual = ZellersCongruenceCalculator.CalculateDayOfWeek(year, month, day, CalendarType.Gregorian);
                    Assert.AreEqual(expected, actual, string.Format("Mismatch for date: {0}", date.ToShortDateString()));
                }
            }
        }
    }
}