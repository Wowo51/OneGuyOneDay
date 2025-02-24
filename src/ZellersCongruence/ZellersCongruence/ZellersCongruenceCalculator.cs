using System;

namespace ZellersCongruence
{
    public enum CalendarType
    {
        Gregorian,
        Julian
    }

    public class ZellersCongruenceCalculator
    {
        public static DayOfWeek CalculateDayOfWeek(int year, int month, int day, CalendarType calendarType)
        {
            if (month < 3)
            {
                month += 12;
                year -= 1;
            }

            int K = year % 100;
            int J = year / 100;
            int h = 0;
            if (calendarType == CalendarType.Gregorian)
            {
                h = (day + (13 * (month + 1)) / 5 + K + (K / 4) + (J / 4) + 5 * J) % 7;
            }
            else
            {
                h = (day + (13 * (month + 1)) / 5 + K + (K / 4) + 5 - 2 * J) % 7;
            }

            if (h < 0)
            {
                h = (h % 7 + 7) % 7;
            }

            int dayOfWeekIndex = (h + 6) % 7;
            return (DayOfWeek)dayOfWeekIndex;
        }
    }
}