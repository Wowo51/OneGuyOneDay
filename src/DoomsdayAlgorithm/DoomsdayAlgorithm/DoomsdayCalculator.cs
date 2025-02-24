using System;

namespace DoomsdayAlgorithm
{
    public static class DoomsdayCalculator
    {
        public static DayOfWeek GetDayOfWeek(int year, int month, int day)
        {
            if (year < 1 || year > 9999)
            {
                return DayOfWeek.Sunday;
            }

            if (month < 1 || month > 12)
            {
                return DayOfWeek.Sunday;
            }

            int maxDay = DateTime.DaysInMonth(year, month);
            if (day < 1 || day > maxDay)
            {
                return DayOfWeek.Sunday;
            }

            int centuryAnchor = GetCenturyAnchor(year);
            int yy = year % 100;
            int a = yy / 12;
            int b = yy % 12;
            int c = b / 4;
            int sum = a + b + c;
            int doomsday = (centuryAnchor + (sum % 7)) % 7;
            int baseDay = GetMonthDoomsday(month, DateTime.IsLeapYear(year));
            int diff = day - baseDay;
            int result = ((doomsday + diff) % 7 + 7) % 7;
            return (DayOfWeek)result;
        }

        public static DayOfWeek GetDayOfWeek(System.DateTime date)
        {
            return GetDayOfWeek(date.Year, date.Month, date.Day);
        }

        private static int GetCenturyAnchor(int year)
        {
            int century = year / 100;
            int anchor = ((5 * (century % 4)) + 2) % 7;
            return anchor;
        }

        private static int GetMonthDoomsday(int month, bool isLeapYear)
        {
            switch (month)
            {
                case 1:
                    return isLeapYear ? 4 : 3;
                case 2:
                    return isLeapYear ? 29 : 28;
                case 3:
                    return 14;
                case 4:
                    return 4;
                case 5:
                    return 9;
                case 6:
                    return 6;
                case 7:
                    return 11;
                case 8:
                    return 8;
                case 9:
                    return 5;
                case 10:
                    return 10;
                case 11:
                    return 7;
                case 12:
                    return 12;
                default:
                    return 0;
            }
        }
    }
}