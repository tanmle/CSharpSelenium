using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Framework.Common
{
    public class BaseControl : Browser
    {
        protected IWebElement element;
        protected Actions action;
        protected IMouse mouse;
        protected WebDriverWait wait;
        protected ICoordinates coordinates;

        public string XPath { get; set; }
        public By by { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseControl"/> class.
        /// </summary>
        public BaseControl() { }

        /// <summary>
        /// Create new selenium control with By elemenent
        /// </summary>
        /// <param name="by">By element (By.ID or By.xPath)</param>
        public BaseControl(By by)
        {
            this.by = by;
            XPath = Browser.GetXpath(by);
            action = new Actions(Browser.Current);
            mouse = ((IHasInputDevices)Browser.Current).Mouse;
        }


        /// <summary>
        /// Create new selenium control with xPath
        /// </summary>
        /// <param name="xPath">The xpath of the element.</param>
        public BaseControl(string xPath)
        {
            XPath = xPath;
            by = By.XPath(xPath);
            action = new Actions(Browser.Current);
            mouse = ((IHasInputDevices)Browser.Current).Mouse;
        }

        public BaseControl(IWebElement element)
        {
            this.element = element;
            action = new Actions(Browser.Current);
            mouse = ((IHasInputDevices)Browser.Current).Mouse;
        }

        /// <summary>
        /// Double- clicks the mouse on this  element
        /// </summary>
        public void DoubleClick()
        {
            LoadControl();
            action.DoubleClick(element).Build().Perform();
        }

        /// <summary>
        /// Raise javascript event on this element
        /// </summary>
        /// <param name="eventName"></param>
        public void FireEvent(string eventName)
        {
            //JavaScriptLibrary.ExecuteScript(this.driver, "arguments[0].fireEvent('" + eventName + "')", this.element);
        }

        /// <summary>
        /// Waits for control exists.
        /// </summary>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        public void WaitForControlExists(int timeoutInSeconds)
        {
            try
            {
                if (element == null)
                {
                    wait = new WebDriverWait(Browser.Current, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.Until(drv => drv.FindElement(by));
                }
            }
            catch
            {
                throw new RLException(string.Format("No element '{0}' have been found.", by));
            }

        }

        /// <summary>
        /// Waiting for control display.
        /// </summary>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        public void WaitForControlDisplay(int timeoutInSeconds)
        {
            try
            {
                wait = new WebDriverWait(Browser.Current, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(drv => drv.FindElement(by).Displayed == true);
            }
            catch
            {
                throw new RLException(string.Format("No element '{0}' have been found.", by));
            }
        }

        /// <summary>
        /// Loads the control.
        /// </summary>
        public void LoadControl()
        {
            //WaitForControlDisplay(30);
            if (by != null)
            {
                WaitForControlExists(Constants.WaitTimeoutShortSeconds);
                element = Browser.Current.FindElement(by);
            }
            //}
        }

        public List<string> CssClasses
        {
            get
            {
                LoadControl();
                return GetAttribute("class").Split(' ').ToList();
            }
        }

        public bool HasClass(string cssClass)
        {
            return CssClasses.Contains(cssClass);
        }

        #region IWebElement Members
        /// <summary>
        /// Clear the content of this element
        /// </summary>
        public void Clear()
        {
            LoadControl();
            element.SendKeys(Keys.End);
            element.SendKeys(Keys.Shift + Keys.Home);
            element.SendKeys(Keys.Delete);
            element.Clear();
        }
        /// <summary>
        /// Click this element
        /// </summary>
        public void Click()
        {
            LoadControl();
            element.Click();  
        }

        /// <summary>
        /// Click element by javascript
        /// </summary>
        public void ClickByJS()
        {
            if (by != null)
            {
                LoadControl();
            }
            ((IJavaScriptExecutor)Current).ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Gets the value indicating whether or not this element is displayed
        /// </summary>
        public bool Displayed
        {
            get
            {
                if (Exists)
                {
                    LoadControl();
                    return element.Displayed;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Gets the value indicating whether or not this element is enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                LoadControl();
                return element.Enabled;
            }
        }
        /// <summary>
        /// Gets the value of the specified attribute of this element
        /// </summary>
        public string GetAttribute(string attributeName)
        {
            LoadControl();
            return element.GetAttribute(attributeName);
        }
        /// <summary>
        /// Gets the value of a CSS property of this element
        /// </summary>
        /// <param name="propertyName">The name of the CSS property to get the value of</param>
        /// <returns></returns>
        public string GetCssValue(string propertyName)
        {
            LoadControl();
            return element.GetCssValue(propertyName);
        }
        /// <summary>
        /// Gets a System.Drawing.Point object containgin the coordinates of the upper-left corner of this element relative to
        /// the upper-left corner of the page
        /// </summary>
        public System.Drawing.Point Location
        {
            get
            {
                LoadControl();
                return element.Location;
            }
        }
        /// <summary>
        /// Gets the value indicating whether or not this element is selected
        /// </summary>
        public bool Selected
        {
            get
            {
                LoadControl();
                return element.Selected;
            }
        }
        /// <summary>
        /// Simulates typing text into the element
        /// </summary>
        /// <param name="text">The text to type into element</param>
        public void SendKeys(string text)
        {
            LoadControl();
            element.SendKeys(text);
        }

        /// <summary>
        /// Simulates typing special key or combination key into the element
        /// </summary>
        /// <param name="specialKey"></param>
        /// <param name="normalKey"></param>
        public void SendSpecialKeys(string specialKey, string normalKey)
        {
            LoadControl();
            
            switch (specialKey)
            {
                case "Control":
                    if (!string.IsNullOrEmpty(normalKey))
                    {
                        element.SendKeys(Keys.Control + normalKey);
                    }
                    else
                    {
                        element.SendKeys(Keys.Control);
                    }
                    break;
                case "Enter":
                    element.SendKeys(Keys.Enter);
                    break;
                default:
                    break;
            }
            
        }
        /// <summary>
        /// Gets a <see cref="Size"/> object containing the height and width of this element.
        /// </summary>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public System.Drawing.Size Size
        {
            get
            {
                LoadControl();
                return element.Size;
            }
        }
        /// <summary>
        /// Submits this element to the web server.
        /// </summary>
        /// <remarks>If this current element is a form, or an element within a form, 
        /// then this will be submitted to the web server. If this causes the current 
        /// page to change, then this method will block until the new page is loaded.</remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public void Submit()
        {
            LoadControl();
            element.Submit();
        }
        /// <summary>
        /// Gets the tag name of this element.
        /// </summary>
        /// <remarks>
        /// The <see cref="TagName"/> property returns the tag name of the
        /// element, not the value of the name attribute. For example, it will return
        /// "input" for an element specifiedby the HTML markup &lt;input name="foo" /&gt;. 
        /// </remarks>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public string TagName
        {
            get
            {
                LoadControl();
                return element.TagName;
            }
        }
        /// <summary>
        /// Gets the innerText of this element, without any leading or trailing whitespace,
        /// and with other whitespace collapsed.
        /// </summary>
        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public string Text
        {
            get
            {
                LoadControl();
                return element.Text;
            }
        }

        /// <summary>
        /// Gets the value of controls such as textbox, combobox...
        /// </summary>
        public string Value
        {
            get
            {
                LoadControl();
                return element.GetAttribute("value");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="BaseControl"/> is exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if exists; otherwise, <c>false</c>.
        /// </value>
        public bool Exists
        {
            get
            {
                try
                {
                    if (by != null)
                    {
                        Browser.Current.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                        int count = Browser.Current.FindElements(by).Count;
                        return count > 0 ? true : false;
                    }
                    return element != null ? true : false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("The {0} control doesn't exist in {1} with {2}", by, Browser.Current.Title, ex.Message));
                    return false;
                }
                finally
                {
                    Browser.Current.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
                }
            }
        }

        #endregion

        #region ISearchContext Members
        /// <summary>
        /// Finds the first <see cref="IWebElement"/> using the given method. 
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindElement(By by)
        {
            IWebElement eResult;
            LoadControl();
            try
            {
                eResult = element.FindElement(by);
            }
            catch
            {
                throw new RLException(string.Format("No element '{0}' have been found.", by));
            }
            return eResult;
        }
        /// <summary>
        /// Finds all <see cref="IWebElement">IWebElements</see> within the current context 
        /// using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>A <see cref="System.Collections.ObjectModel.ReadOnlyCollection{T}"/> of all <see cref="IWebElement">WebElements</see>
        /// matching the current criteria, or an empty list if nothing matches.</returns>
        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LoadControl();
            try
            {
                element.FindElements(by);
            }
            catch
            {
                throw new RLException(string.Format("No element '{0}' have been found.", by));
            }
            return element.FindElements(by);
        }

        /// <summary>
        /// Move to element
        /// </summary>
        public void MoveToElement()
        {
            LoadControl();
            action.MoveToElement(element);
            action.Perform();
        }

        #endregion
    }
}
