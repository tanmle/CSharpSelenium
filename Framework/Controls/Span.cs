using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Span : BaseControl
    {
        public Span() { }
        public Span(string xPath) : base(By.XPath(xPath)) { }
        public Span(IWebElement element) : base(element) { }
        public Span(By by) : base(by) { }
    }
}
