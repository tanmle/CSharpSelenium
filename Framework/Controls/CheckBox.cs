using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class CheckBox : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        public CheckBox() { }
        public CheckBox(string xPath) : base(By.XPath(xPath)) { }
        public CheckBox(IWebElement element) : base(element) { }
        public CheckBox(By by) : base(by) { }

        /// <summary>
        /// Checks a checkbox.
        /// </summary>
        public void Check()
        {
            if (Selected == false)
            {
                Click();
            }
        }

        /// <summary>
        /// Unchecks a checkbox
        /// </summary>
        public void Uncheck()
        {
            if (Selected == true)
            {
                Click();
            }
        }
    }
}