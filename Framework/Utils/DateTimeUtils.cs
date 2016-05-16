namespace Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class DateTimeUtils
    {
        static GregorianCalendar _gc = new GregorianCalendar();

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            var defaultCultureInfo = new CultureInfo("en-US");
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date 
        /// is in. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            var firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            var firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static int GetWeekOfMonth(DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return GetWeekOfYear(time) - GetWeekOfYear(first) + 1;
        }

        public static int GetWeekOfYear(DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static int GetWeekOfMonths(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }


        public static List<DateTime> GetFirstDaysOfWeeksOfMonth(DateTime dateTime)
        {
            DateTime date = DateTime.Today;
            // first generate all dates in the month of 'date'
            var dates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).Select(n => new DateTime(date.Year, date.Month, n));
            // then filter the only the start of weeks
            var weekends = (from d in dates
                            where d.DayOfWeek == DayOfWeek.Monday
                            select d).ToList();
            return weekends;
        }

        public static List<DateTime> GetLastWeeksFromToday(DateTime dateTime)
        {
            var firstDayOfWeek = DateTimeUtils.GetFirstDayOfWeek(dateTime);
            List<DateTime> dateTimes = new List<DateTime>();
            int period = 7;
            for (int i = 0; i < 3; i++)
            {
                dateTimes.Add(firstDayOfWeek.AddDays(-(period)));
                period += 7;
            }
            dateTimes.Add(firstDayOfWeek);
            dateTimes.Sort();
            return dateTimes;
        }

        /// <summary>
        /// Convert Date string to exactly DateTime format
        /// Ex:
        /// input: "15/1/2014", "dd/MM/yyyy"
        /// output: 15/01/2014 00:00:00
        /// </summary>
        /// <param name="inputString">string of Date</param>
        /// <param name="inputFormat">format of input Date string</param>
        /// <returns>Date/Time after converting</returns>
        public static DateTime ConvertStringToDateTime(string inputString, string inputFormat)
        {
            inputString = ConvertStringToDateTimeFormat(inputString, inputFormat, inputFormat);
            DateTime date = DateTime.ParseExact(inputString, inputFormat, null);

            return date;
        }

        /// <summary>
        /// Convert to right date/time format for string (MM/dd/yyyy or dd/MM/yyyy)
        /// Ex:
        /// input: "15/1/2014", "dd/MM/yyyy", "MM/dd/yyyy"
        /// output: "01/15/2014"
        /// </summary>
        /// <param name="stringConvert">string need to convert</param>
        /// <returns>string after converting</returns>
        private static String ConvertStringToDateTimeFormat(string stringConvert, string oldFormat, string newFormat)
        {
            string[] strArr = null;
            string date = null;
            string month = null;
            string year = null;

            strArr = stringConvert.Split('/');

            if (oldFormat.StartsWith("M"))
            {
                date = strArr[1];
                month = strArr[0];
                year = strArr[2];
            }
            if (oldFormat.StartsWith("d"))
            {
                date = strArr[0];
                month = strArr[1];
                year = strArr[2];
            }

            if (ConvertUtils.ConvertToInt(date) < 10 && !date.StartsWith("0"))
            {
                date = "0" + date;
            }
            if (ConvertUtils.ConvertToInt(month) < 10 && !month.StartsWith("0"))
            {
                month = "0" + month;
            }

            if (newFormat.StartsWith("M"))
            {
                stringConvert = String.Format("{0}/{1}/{2}", month, date, year);
            }
            else if (newFormat.StartsWith("d"))
            {
                stringConvert = String.Format("{0}/{1}/{2}", date, month, year);
            }

            return stringConvert;
        }

        /// <summary>
        /// Random date/time from start date to end date
        /// </summary>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns>random date/time</returns>
        public static DateTime RandomDateTime(DateTime startDate, DateTime endDate)
        {
            Random rd = new Random();

            int range = (endDate - startDate).Days;
            DateTime rdDate = startDate.AddDays(rd.NextDouble() * range);

            return rdDate;
        }

    }

}
