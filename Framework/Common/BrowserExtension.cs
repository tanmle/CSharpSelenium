using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.Common
{
    public static class BrowserExtension
    {
        private static TimeSpan timeOut = TimeSpan.FromSeconds(30);
        /// <summary>
        /// Wait for the number of browser windows reach the input number
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="count"></param>
        public static void WaitForNumberOfBrowsersAtLeast(this IWebDriver driver, int count)
        { 
            WebDriverWait wait = new WebDriverWait(driver, timeOut);
            wait.Until(d => driver.WindowHandles.Count() >= count);
        }
        /// <summary>
        /// Switch to the new opened window
        /// </summary>
        /// <param name="driver"></param>
        public static void SwitchToNewOpenedWindow(this IWebDriver driver, bool isNewUrl = true)
        {
            string oldUrl = driver.Url;
            driver.WaitForNumberOfBrowsersAtLeast(2);                        
            string[] handles = driver.WindowHandles.Where(h => h != driver.CurrentWindowHandle).ToArray();
            driver.SwitchTo().Window(handles.Last());
            if (isNewUrl && oldUrl == driver.Url)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
                wait.Until(drv => {
                    drv.SwitchTo().Window(handles.Last());
                    return drv.Url != oldUrl;
                });
            }
        }
        /// <summary>
        /// Execute java script
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="script"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object ExecuteJavaScript(this IWebDriver driver, string script, params object[] args)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        /// <summary>
        /// Closes the opened window.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public static void CloseOpenedWindow(this IWebDriver driver)
        {
            driver.Close();            
            driver.SwitchTo().Window(driver.WindowHandles.Last());            
        }

        /// <summary>
        /// Switch to the last window
        /// </summary>
        /// <param name="driver"></param>
        public static void SwitchToLastWindow(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        /// <summary>
        /// Get the page that user is being on. 
        /// Ex: With url "https://www.abc.com/Login.aspx?srcID=t", the string "Login" will be returned
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>The page controller name</returns>
        public static string GetPage(this IWebDriver driver)
        {
            driver.WaitForPageLoad();

            const string pattern1 = @"\w+://[\w.]+.com/[\w\-]+(.*\.(?=[^.]*$))";
            Match match1 = Regex.Match(driver.Url, pattern1);

            string url1 = match1.Success ? match1.Groups[1].Value.Trim('/').Trim('.') : string.Empty;

            if (string.IsNullOrEmpty(url1))
            {
                // Url: https://rprd.renlearn.com/devusuat1/Public/RPM/DetectPlugin/
                const string pattern3 = @"\w+://[\w.]+.com/[\w\-]+(/[/\w]+)";
                Match match3 = Regex.Match(driver.Url, pattern3);
                return match3.Success ? match3.Groups[1].Value.Trim('/') : url1;
            }

            const string pattern2 = @"(.*?)\?";
            Match match2 = Regex.Match(url1, pattern2);

            if (!match2.Success)
            {
                return url1;
            }
            else
            {
                return match2.Value.Replace("aspx?", "").Trim('/').Trim('?').Trim('.');
            }           
        }

        /// <summary>
        /// Waiting for the page to load
        /// </summary>
        public static void WaitForPageLoad(this IWebDriver driver)
        {
            driver.WaitForPageHasStatus("complete");
        }

        /// <summary>
        /// Waiting for the page to be loading
        /// </summary>
        public static void WaitForPageLoading(this IWebDriver driver)
        {
            driver.WaitForPageHasStatus("loading");
        }

        /// <summary>
        /// Waiting for the page to be loading
        /// </summary>
        public static void WaitForPageHasStatus(this IWebDriver driver, string status)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
            try
            {
                wait.Until(w => w.ExecuteJavaScript("return document.readyState;").Equals(status));
            }
            catch(WebDriverException e){
                Console.WriteLine(e.ToString());
            }
            
        }

        /// <summary>
        /// Getting the URL following pattern.
        /// </summary>
        /// <returns>/UK/AcceleratedMaths/AMUKStudentDemo/multiscreen.html</returns>
        public static string GetUrlFollowPattern(this IWebDriver driver)
        {
            const string pattern = @"\w+://[\w.]+(/[/\w.]+)$";
            Match match = Regex.Match(Browser.GetCurrentUrl(), pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
