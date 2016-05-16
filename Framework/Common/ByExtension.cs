using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Framework.Common
{
    public static class ByExtension
    {
        /// <summary>
        /// Map arguments to selector value
        /// </summary>
        /// <param name="by"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static By MapValues(this By by, params string[] values)
        {
            string sel = by.ToString();
            const string pattern = @"By\.(\w+): (.*)";
            Match match = Regex.Match(sel, pattern);
            if (match.Success)
            {
                string byKey = match.Groups[1].Value;
                string byValue = match.Groups[2].Value;
                string mappedValue = string.Format(byValue, values);
                switch (byKey.ToUpper())
                {
                    case "XPATH":
                        return By.XPath(mappedValue);
                    case "CSSSELECTOR":
                        return By.CssSelector(mappedValue);
                    case "ID":
                        return By.Id(mappedValue);
                    case "NAME":
                        return By.Name(mappedValue);
                    default:
                        return By.XPath(mappedValue);
                }
            }
            return null;
        }
    }
}
