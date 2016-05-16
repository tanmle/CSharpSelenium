using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Tab : BaseControl
    {
        public Tab() { }
        public Tab(string xPath) : base(By.XPath(xPath)) { }
        public Tab(IWebElement element) : base(element) { }
        public Tab(By by) : base(by) { }
    }
}
