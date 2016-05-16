namespace Framework.Utils
{
    using System.Text.RegularExpressions;

    public class CheckUtil
    {
        public static bool IsNaturalNumber(string strNumber)
        {
            var objNotNaturalPattern = new Regex("[^0-9]");
            var objNaturalPattern = new Regex("0*[1-9][0-9]*");

            return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
        }
    }
}
