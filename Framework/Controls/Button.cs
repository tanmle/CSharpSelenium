using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Button : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button() { }
        public Button(string xPath) : base(By.XPath(xPath)) { }
        public Button(IWebElement element) : base(element) { }
        public Button(By by) : base(by) { }
    }
}
