
namespace Framework.Common
{
    public static class Constants
    {
        public const int WaitTimeoutShortSeconds = 60;

        public const string SHORT_DATE_US = "M/d/yyyy";
        public const string SHORT_DATE_UK = "d/M/yyyy";
        public const string DATE_US = "MM/dd/yyyy";
        public const string DATE_UK = "dd/MM/yyyy";
        public const string SHORT_TIME = "h:mm tt";
        public const string YEAR = "yyyy";
        public const string DATETIME_NO_SPECIAL_CHARACTER = "yyyyMMddHHmmss";
        public const string TIME_ZONE_NAME = "Pacific Standard Time";
        public const string DEFAULT_SGP_MIDPOINT_DATE = "1/15/yyyy";
        public const string FULL_DATETIME = "yyyyMMddhhmmss";

        public const string COLOR_RED = "rgba(255, 0, 0, 1)";
        public const string COLOR_GREEN = "rgba(0, 128, 0, 1)";

        public enum DIRECTION { RightToLeft, LeftToRight, AboveToBelow, BelowToAbove }
    }
}