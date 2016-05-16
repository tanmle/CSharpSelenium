using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
    public class RadioButton : BaseControl
    {
        public RadioButton() { }
        public RadioButton(string xPath) : base(By.XPath(xPath)) { }
        public RadioButton(IWebElement element) : base(element) { }
        public RadioButton(By by) : base(by) { }
    }
}
