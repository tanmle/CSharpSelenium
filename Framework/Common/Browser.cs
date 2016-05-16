using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;


namespace Framework.Common
{
    public class Browser
    {
        public static Dictionary<string, IWebDriver> Context = new Dictionary<string, IWebDriver>();
        static string currentUrl = ConfigurationManager.AppSettings["URL"];
        static string currentBrowser = ConfigurationManager.AppSettings["BROWSER"];
        protected static IWebDriver Current
        {
            get
            {
                var browser = currentBrowser;
                switch (browser.ToUpper())
                {
                    case "FIREFOX":
                        {
                            if (Context.Count != 0)
                            {
                                return Context["Driver"];
                            }
                            Context.Add("Driver", (IWebDriver)new FirefoxDriver());
                            return Context["Driver"];
                        }
                    case "CHROME":
                        {
                            if (Context.Count != 0)
                            {
                                return Context["Driver"];
                            }
                            Context.Add("Driver", (IWebDriver)new ChromeDriver());
                            return Context["Driver"];
                        }
                    default:
                        {
                            if (Context.Count != 0)
                            {
                                return Context["Driver"];
                            }
                            Context.Add("Driver", (IWebDriver)new InternetExplorerDriver());
                            return Context["Driver"];
                        }
                }

            }
        }

        /// <summary>
        /// Gets the xpath.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        public static string GetXpath(By by)
        {
            string[] words = by.ToString().Split(':');

            string xtype;
            switch (words[0])
            {
                case "By.Name":
                    xtype = "@name";
                    break;
                case "By.Id":
                    xtype = "@id";
                    break;
                case "By.LinkText":
                    xtype = "text()";
                    break;
                case "By.XPath":
                    xtype = "xpath";
                    break;
                case "By.CssSelector":
                    return null;
                default:
                    xtype = "xpath";
                    break;
            }

            return xtype == "xpath" ? words[1].Trim() : string.Format("//[{0}='{1}']", xtype, words[1]);
        }

        /// <summary>
        /// check element is arranged left to right or right to left, below or above other elements
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="lstElementsXPath">The list of elements (xpath).</param>
        /// <param name="direction">The direction left to right or right to left</param>
        /// <returns></returns>
        public static bool IsElementsArranged(List<string> lstElementsXPath, Constants.DIRECTION direction)
        {
            bool eResult = false;
            if (direction == Constants.DIRECTION.AboveToBelow | direction == Constants.DIRECTION.BelowToAbove)
            {
                if (direction == Constants.DIRECTION.BelowToAbove)
                {
                    lstElementsXPath.Reverse();
                }
                List<IWebElement> elements = new List<IWebElement>();
                List<System.Drawing.Point> positions = new List<System.Drawing.Point>();
                for (int i = 0; i < lstElementsXPath.Count; i++)
                {
                    elements.Add(Browser.Current.FindElement(By.XPath(lstElementsXPath[i])));
                    positions.Add(elements[i].Location);
                }
                for (int i = lstElementsXPath.Count - 1; i > 0; i--)
                {
                    if (positions[i].Y > positions[i - 1].Y)
                    {
                        eResult = true;
                    }
                }
            }
            else
            {
                string ArrangedElement = "//";
                string keyDirection = "/following::";

                if (direction == Constants.DIRECTION.RightToLeft)
                {
                    keyDirection = "/preceding::";
                }

                for (int i = 0; i < lstElementsXPath.Count; i++)
                {
                    ArrangedElement = ArrangedElement + lstElementsXPath[i].Substring(2, lstElementsXPath[i].Length - 2);
                    if (i != lstElementsXPath.Count - 1) ArrangedElement = ArrangedElement + keyDirection;
                }

                try
                {
                    eResult = Browser.Current.FindElements(By.XPath(ArrangedElement)).Count > 0 ? true : false;
                }
                catch
                {
                    eResult = false;
                }
            }
            return eResult;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns></returns>
        public static string GetPage()
        {
            return Browser.Current.GetPage();
        }

        /// <summary>
        /// Refresh website.
        /// </summary>
        public static void RefreshCurrentPage()
        {
            Browser.Current.Navigate().Refresh();
        }

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <param name="by">The by.</param>
        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Browser.Current.FindElements(by);
        }

        /// <summary>
        /// Getting the current URL.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            return Browser.Current.Url;
        }

        /// <summary>
        /// Switching to new opened window.
        /// </summary>
        public static void SwitchToNewOpenedWindow()
        {
            Browser.Current.SwitchToNewOpenedWindow(true);
        }

        /// <summary>
        /// Switching to the last window.
        /// </summary>
        /// <returns></returns>
        public static void SwitchToLastWindow()
        {
            Browser.Current.SwitchToLastWindow();
        }

        /// <summary>
        /// Counting the window handles.
        /// </summary>
        /// <returns></returns>
        public static int CountWindowHandles()
        {
            return Browser.Current.WindowHandles.Count;
        }

        /// <summary>
        /// Closing the opened window.
        /// </summary>
        public static void CloseOpenedWindow()
        {
            Browser.Current.CloseOpenedWindow();
        }

        /// <summary>
        /// Switching to the default content.
        /// </summary>
        public static void SwitchToDefaultContent()
        {
            Browser.Current.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Switching to frame.
        /// </summary>
        /// <param name="strFrame">The string frame.</param>
        public static void SwitchToFrame(string strFrame)
        {
            Browser.Current.SwitchTo().Frame(strFrame);
        }

        /// <summary>
        /// Switching to frame.
        /// </summary>
        /// <param name="strFrame">The string frame.</param>
        public static void SwitchToFrameByXpath(string xPath)
        {
            Browser.Current.SwitchTo().Frame(Current.FindElement(By.XPath(xPath)));
        }

        /// <summary>
        /// Getting the current title.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTitle()
        {
            return Browser.Current.Title;
        }

        /// <summary>
        /// Waiting for page load.
        /// </summary>
        public static void WaitForPageLoad()
        {
            Browser.Current.WaitForPageLoad();
        }

        /// <summary>
        /// Scrolling to top.
        /// </summary>
        /// <param name="strControlXPath">Xpath of the control</param>
        public static void ScrollToTop(BaseControl control)
        {
            Actions action = new Actions(Browser.Current);
            action.MoveToElement(Browser.Current.FindElement(By.XPath(control.XPath))).Perform();
            action.SendKeys(Keys.Home).Perform();
        }

        /// <summary>
        /// Gets the file name from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetFileNameFromUrl(string url)
        {
            const string pattern = @"/([-&%\w]+.\w+)$";
            Match match = Regex.Match(url, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        /// <summary>
        /// Goes to default URL.
        /// </summary>
        public static void GoToDefaultUrl()
        {
            Browser.Current.Navigate().GoToUrl(currentUrl);
            MaximizeWindow();
        }

        /// <summary>
        /// Goes to URL.
        /// </summary>
        /// <param name="URL">The URL.</param>
        public static void GoToUrl(string Url)
        {
            Browser.Current.Navigate().GoToUrl(Url);
            MaximizeWindow();
        }

        /// <summary>
        /// Waiting the URL change.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="seconds">The seconds.</param>
        public static void WaitingUrlChange(string url, int seconds)
        {
            double miliSeconds = seconds * 1000;

            while (Browser.GetCurrentUrl().Contains(url))
            {
                //short time sleep to wait Url changing
                System.Threading.Thread.Sleep(300);
                miliSeconds = miliSeconds - 300;
                if (miliSeconds < 0)
                {
                    Logger.Error(String.Format("Browser doesn't change after {0} seconds", seconds));
                    break;
                }
            }
        }

        /// <summary>
        /// Getting the URL following pattern.
        /// </summary>
        /// <returns>/UK/AcceleratedMaths/AMUKStudentDemo/multiscreen.html</returns>
        public static string GetUrlFollowPattern()
        {
            return Browser.Current.GetUrlFollowPattern();
        }

        /// <summary>
        /// Check if alert presents
        /// </summary>
        /// <returns>true/false</returns>
        public static Boolean IsAlertPresent()
        {
            try
            {
                return (Browser.Current.SwitchTo().Alert().Text != null) ? true : false;
            }
            catch (NoAlertPresentException e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Gets the alert text.
        /// </summary>
        /// <returns>Text on alert window</returns>
        public static string GetAlertText()
        {
            return Browser.Current.SwitchTo().Alert().Text;
        }

        /// <summary>
        /// Waits for confirm dialog display in a specific time in seconds.
        /// </summary>
        /// <param name="timeInSeconds">The time in seconds.</param>
        public static void WaitForConfirmDialogDisplay(int timeInSeconds)
        {
            double timeInMiliSeconds = timeInSeconds * 1000;
            while (!IsAlertPresent())
            {
                // Short time sleep to wait for confirmation dialog display
                System.Threading.Thread.Sleep(100);
                timeInMiliSeconds -= 100;
                if (timeInMiliSeconds < 0)
                {
                    Logger.Info(string.Format("Confimation dialog is not displayed after {0} seconds", timeInSeconds));
                    break;
                }
            }
        }

        /// <summary>
        /// Waiting for control display event. 
        /// The method should be used when you try to use WaitControlDisplay of a control (Control<BaseControl>(controlName).WaitForControlDisplay) that doesn't affect
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        public static void WaitForControlDisplayEvent(string controlName, int timeoutInSeconds)
        {
            double miliSeconds = timeoutInSeconds * 1000;
            ControlFactory conFactory = new ControlFactory();
            while (conFactory.Control<BaseControl>(controlName).Displayed == false)
            {
                //short time sleep to wait control display
                System.Threading.Thread.Sleep(100);
                miliSeconds -= 100;
                if (miliSeconds < 0)
                {
                    Logger.Info(String.Format("Control {0} doesn't display after {1} seconds", controlName, timeoutInSeconds));
                    break;
                }
            }
        }

        /// <summary>
        /// Confirms the dialog based on the button name.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        public static void ConfirmDialog(string buttonName)
        {
            switch (buttonName.ToUpper())
            {
                case "OK":
                case "YES":
                    Browser.Current.SwitchTo().Alert().Accept();
                    break;
                case "NO":
                case "CANCEL":
                    Browser.Current.SwitchTo().Alert().Dismiss();
                    break;
            }
        }

        /// <summary>
        /// Wait for the number of browser windows reach the input number
        /// </summary>
        public static void WaitForNumberOfBrowsersAtLeast(int count)
        {
            Browser.Current.WaitForNumberOfBrowsersAtLeast(count);
        }

        /// <summary>
        /// Sets the implicitly wait.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        public static void SetImplicitlyWait(TimeSpan timeToWait)
        {
            Browser.Current.Manage().Timeouts().ImplicitlyWait(timeToWait);
        }

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        public static void MaximizeWindow()
        {
            Browser.Current.Manage().Window.Maximize();
        }

        /// <summary>
        /// Deletes all cookies.
        /// </summary>
        public static void DeleteAllCookies()
        {
            Browser.Current.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Switch to Frame
        /// </summary>
        public static void SwitchToFrame()
        {
            throw new NotImplementedException();
        }

        public static void Close()
        {
            Browser.Current.Quit();
        }
    }
}
