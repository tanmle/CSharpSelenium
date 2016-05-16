using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Label : BaseControl
    {
        public Label() { }
        public Label(string xPath) : base(By.XPath(xPath)) { }
        public Label(By by) : base(by) { }
        public Label(IWebElement element) : base(element) { }
    }
}
