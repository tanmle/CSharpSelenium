using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class ListBox : BaseControl
    {
        public ListBox() { }
        public ListBox(string xPath) : base(By.XPath(xPath)) { }
        public ListBox(IWebElement element) : base(element) { }
        public ListBox(By by) : base(by) { }
    }
}
