using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Tr : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tr"/> class.
        /// </summary>
        public Tr() { }
        public Tr(string xPath) : base(By.XPath(xPath)) { }
        public Tr(IWebElement element) : base(element) { }
        public Tr(By by) : base(by) { }
    }
}
