using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class iFrame : BaseControl
    {
        public iFrame() { }
        public iFrame(string xPath) : base(By.XPath(xPath)) { }
        public iFrame(IWebElement element) : base(element) { }
        public iFrame(By by) : base(by) { }
    }
}
