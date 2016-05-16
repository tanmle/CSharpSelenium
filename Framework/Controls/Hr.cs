using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Hr : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hr"/> class.
        /// </summary>
        public Hr() { }
        public Hr(string xPath) : base(By.XPath(xPath)) { }
        public Hr(IWebElement element) : base(element) { }
        public Hr(By by) : base(by) { }
    }
}
