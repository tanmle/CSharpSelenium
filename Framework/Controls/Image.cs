using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class Image : BaseControl
    {
        public Image() { }
        public Image(string xPath) : base(By.XPath(xPath)) { }
        public Image(IWebElement element) : base(element) { }
        public Image(By by) : base(by) { }
    }
}
