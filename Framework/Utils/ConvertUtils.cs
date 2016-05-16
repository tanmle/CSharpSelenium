using System;

namespace Framework.Utils
{
    public class ConvertUtils
    {
        public static int ConvertToInt(object value)
        {
            return (value != null && CheckUtil.IsNaturalNumber(value.ToString())) ? int.Parse(value.ToString()) : 0;
        }        

        public static bool ConvertToBoolean(object value)
        {
            try
            {
                return Boolean.Parse(value.ToString());
            }
            catch
            {
                return false;
            }
        }

        public static long ConvertToLong(object value)
        {
            return (value != null && CheckUtil.IsNaturalNumber(value.ToString())) ? long.Parse(value.ToString()) : 0;
        }

        public static byte ConvertToByte(object value)
        {
            return (value != null && CheckUtil.IsNaturalNumber(value.ToString())) ? byte.Parse(value.ToString()) : (byte)0;
        }

        public static double ConvertToDouble(object value)
        {
            try
            {
                return double.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertToString(object value)
        {
            return (value != null && !string.IsNullOrEmpty(value.ToString())) ? value.ToString() : string.Empty;
        }

        public static string ConvertToString(object value, string format)
        {
            if (!string.IsNullOrEmpty(format) && value is DateTime)
            {
                if ((DateTime)value == new DateTime())
                {
                    value = null;
                }
                return (value != null && !string.IsNullOrEmpty(value.ToString())) ? ((DateTime)value).ToString(format) : string.Empty;
            }
            else
            {
                return (value != null && !string.IsNullOrEmpty(value.ToString())) ? value.ToString() : string.Empty;
            }
        }

        public static DateTime? ConvertToDatetimeNullable(object value)
        {
            DateTime? result = null;
            try
            {
                result = DateTime.Parse(value.ToString());
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static DateTime ConvertToDatetime(object value)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.Parse(value.ToString());
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string GetDefaultTimeZoneOfUser()
        {
            string defaultTimeZone = String.Empty;            
            return defaultTimeZone;
        }

        public static DateTime ConvertToServerTimeZone(DateTime date)
        {
            string defaultClientTimeZone = GetDefaultTimeZoneOfUser();
            TimeZoneInfo defaultClientTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(defaultClientTimeZone);

            if (defaultClientTimeZone == TimeZoneInfo.Local.Id)
            {
                return date;
            }
            else if (defaultClientTimeZoneInfo.IsInvalidTime(date))
            {
                return date.AddHours(TimeZoneInfo.Local.GetUtcOffset(date).Hours - defaultClientTimeZoneInfo.GetUtcOffset(date).Hours);
            }
            else
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, defaultClientTimeZone, TimeZoneInfo.Local.Id);
            }

        }

        public static DateTime? ConvertToServerTimeZone(DateTime? date)
        {
            if (!date.HasValue)
            {
                return date;
            }
            else
            {
                return ConvertToServerTimeZone((DateTime)date);
            }
        }
    }
}
