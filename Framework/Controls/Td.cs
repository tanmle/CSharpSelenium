
using OpenQA.Selenium;

using Framework.Common;

namespace Framework.Controls
{
    public class Td : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Td"/> class.
        /// </summary>
        public Td() { }
        public Td(string xPath) : base(By.XPath(xPath)) { }
        public Td(IWebElement element) : base(element) { }
        public Td(By by) : base(by) { }
    }
}
