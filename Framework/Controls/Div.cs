using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Div : BaseControl
    {
        public Div() { }
        public Div(string xPath) : this(By.XPath(xPath)) { }
        public Div(By by) : base(by) { }
		public Div(IWebElement element) : base(element) { }
    }
}
