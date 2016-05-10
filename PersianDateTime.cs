using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Mehrdad
{
    public class PersianDateTime
    {

        #region ctors

        public PersianDateTime(DateTime dateTime)
        {
            Era = _persianCalendar.GetEra(dateTime);
            Year = _persianCalendar.GetYear(dateTime);
            Month = _persianCalendar.GetMonth(dateTime);
            Day = _persianCalendar.GetDayOfYear(dateTime);
            DayOfMonth = _persianCalendar.GetDayOfMonth(dateTime);
            DaysInMonth = _persianCalendar.GetDaysInMonth(Year, Month);
            DaysInYear = _persianCalendar.GetDaysInYear(Year, Era);
            DayOfWeek = (int)_persianCalendar.GetDayOfWeek(dateTime);
            DayOfWeekName = DayOfWeekNames_To_DayOfWeek((DayOfWeek)DayOfWeek);
            Hour = _persianCalendar.GetHour(dateTime);
            Minute = _persianCalendar.GetMinute(dateTime);
            Second = _persianCalendar.GetSecond(dateTime);
            Milliseconds = _persianCalendar.GetMilliseconds(dateTime);
            WeekOfYear = _persianCalendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, System.DayOfWeek.Saturday);
            Ticks = dateTime.Ticks;
        }

        public PersianDateTime(long ticks)
            : this(new DateTime(ticks))
        {
        }

        public PersianDateTime(string georgianDate)
            : this(DateTime.Parse(georgianDate))
        {

        }

        public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int milliseconds)
            : this(new PersianCalendar().ToDateTime(year, month, day, hour, minute, second, milliseconds))
        {

        }

        public PersianDateTime(int year, int month, int day)
            : this(year, month, day, 0, 0, 0, 0)
        {

        }

        #endregion

        #region variables
        private PersianCalendar _persianCalendar = new PersianCalendar();
        private DateTime _dateTime = new DateTime();
        #endregion

        #region properties
        public int Era { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public int DayOfMonth { get; private set; }

        public int DaysInMonth { get; private set; }

        public int DayOfWeek { get; private set; }

        public int DaysInYear { get; private set; }

        public PersianDayOfWeek DayOfWeekName { get; private set; }

        public PersianMonthNames MonthName { get { return (PersianMonthNames)Month; } }

        public int WeekOfYear { get; private set; }

        public int Hour { get; private set; }

        public int Minute { get; private set; }

        public int Second { get; private set; }

        public double Milliseconds { get; private set; }

        public long Ticks { get; private set; }

        public static PersianDateTime Now { get { return new PersianDateTime(DateTime.Now); } }

        public static PersianDateTime Today { get { return new PersianDateTime(DateTime.Today); } }

        #endregion

        #region operators

        public static PersianDateTime operator +(PersianDateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.Add(timeSpan);
        }

        public static PersianDateTime operator -(PersianDateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.Subtract(timeSpan);
        }

        #endregion

        public PersianDateTime Subtract(TimeSpan timeSpan)
        {
            return new PersianDateTime(ToDateTime().Subtract(timeSpan));
        }

        public TimeSpan Subtract(PersianDateTime dateTime)
        {
            return ToDateTime().Subtract(dateTime.ToDateTime());
        }

        public PersianDateTime AddDays(double days)
        {
            return new PersianDateTime(ToDateTime().AddDays(days));
        }

        public PersianDateTime AddMonths(int months)
        {
            return new PersianDateTime(ToDateTime().AddMonths(months));
        }

        public PersianDateTime AddYears(int years)
        {
            return new PersianDateTime(ToDateTime().AddYears(years));
        }

        public PersianDateTime Add(TimeSpan timeSpan)
        {
            return new PersianDateTime(ToDateTime().Add(timeSpan));
        }

        public DateTime ToDateTime()
        {
            return _persianCalendar.ToDateTime(Year, Month, DayOfMonth, Hour, Minute, Second, Convert.ToInt32(Milliseconds));
        }

        public static PersianDateTime UtcNow()
        {
            return new PersianDateTime(DateTime.UtcNow);
        }

        public static bool IsLeapYear(int year)
        {
            var pc = new PersianCalendar();
            return pc.IsLeapYear(year);
        }

        public static PersianDayOfWeek DayOfWeekNames_To_DayOfWeek(DayOfWeek dayOfWeek)
        {
            return (PersianDayOfWeek)((int)dayOfWeek);
        }

        public static string PersianDaysName(PersianDayOfWeek persianDayOfWeek)
        {
            string dayName = null;
            switch (persianDayOfWeek)
            {
                case PersianDayOfWeek.Shanbeh:
                    dayName = "شنبه";
                    break;
                case PersianDayOfWeek.YekShanbeh:
                    dayName = "یک شنبه";
                    break;
                case PersianDayOfWeek.DooShanbeh:
                    dayName = "دو شنبه";
                    break;
                case PersianDayOfWeek.SehShanbeh:
                    dayName = "سه شنبه";
                    break;
                case PersianDayOfWeek.ChahaarShanbeh:
                    dayName = "چهار شنبه";
                    break;
                case PersianDayOfWeek.PanjShanbeh:
                    dayName = "پنج شنبه";
                    break;
                case PersianDayOfWeek.Jumeh:
                    dayName = "جمعه";
                    break;
            }
            return dayName;
        }

        public static string PersianMonthName(PersianMonthNames persianMonth)
        {
            var monthName = string.Empty;
            switch (persianMonth)
            {
                case PersianMonthNames.Farvardin:
                    monthName = "فروردین";
                    break;
                case PersianMonthNames.Ordibehesht:
                    monthName = "اردیبهشت";
                    break;
                case PersianMonthNames.Khordad:
                    monthName = "خرداد";
                    break;
                case PersianMonthNames.Tir:
                    monthName = "تیر";
                    break;
                case PersianMonthNames.Mordad:
                    monthName = "مرداد";
                    break;
                case PersianMonthNames.Shahrivar:
                    monthName = "شهریور";
                    break;
                case PersianMonthNames.Mehr:
                    monthName = "مهر";
                    break;
                case PersianMonthNames.Aban:
                    monthName = "آبان";
                    break;
                case PersianMonthNames.Azar:
                    monthName = "آذر";
                    break;
                case PersianMonthNames.Dey:
                    monthName = "دی";
                    break;
                case PersianMonthNames.Bahman:
                    monthName = "بهمن";
                    break;
                case PersianMonthNames.Esfand:
                    monthName = "اسفند";
                    break;
            }

            return monthName;
        }

        public bool Equals(PersianDateTime dateTime)
        {
            return ToDateTime().Equals(dateTime.ToDateTime());
        }

        public override bool Equals(object obj)
        {
            return Equals((PersianDateTime)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Year, Month, DayOfMonth);
        }

        public string ToShortDateString(char seprator = '/')
        {
            return String.Format("{0}{3}{1}{3}{2}", Year.ToString().PadLeft(4, '0'), Month.ToString().PadLeft(2, '0'),
                DayOfMonth.ToString().PadLeft(2, '0'), seprator);
        }

        public string ToShortTimeString(char dateSeprator = '/', char timeSeprator = ':')
        {
            return String.Format("{0}{3}{1}{3}{2} {4}{7}{5}{7}{6}", Year.ToString().PadLeft(4, '0'),
                Month.ToString().PadLeft(2, '0'), DayOfMonth.ToString().PadLeft(2, '0'), dateSeprator,
                Hour.ToString().PadLeft(2, '0'), Minute.ToString().PadLeft(2, '0'), Second.ToString().PadLeft(2, '0'),
                timeSeprator);
        }

        public string ToLongDateString()
        {
            return String.Format("{0}، {1} {2} {3}",
                PersianDaysName(DayOfWeekName),
                DayOfMonth,
                PersianMonthName(MonthName),
                Year);
        }

        public enum PersianDayOfWeek
        {
            Shanbeh = 1,
            YekShanbeh,
            DooShanbeh,
            SehShanbeh,
            ChahaarShanbeh,
            PanjShanbeh,
            Jumeh
        }

        public enum PersianMonthNames
        {
            Farvardin = 1,
            Ordibehesht,
            Khordad,
            Tir,
            Mordad,
            Shahrivar,
            Mehr,
            Aban,
            Azar,
            Dey,
            Bahman,
            Esfand
        }
    }
}
