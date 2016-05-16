using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Ul : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ul"/> class.
        /// </summary>
        public Ul() { }
        public Ul(string xPath) : base(By.XPath(xPath)) { }
        public Ul(IWebElement element) : base(element) { }
        public Ul(By by) : base(by) { }
    }
}
