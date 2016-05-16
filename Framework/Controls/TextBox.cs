using Framework.Common;
using OpenQA.Selenium;

namespace Framework.Controls
{
    public class TextBox : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public TextBox() { }
        public TextBox(string xPath) : base(By.XPath(xPath)) { }
        public TextBox(IWebElement element) : base(element) { }
        public TextBox(By by) : base(by) { }

        /// <summary>
        /// Returns whether textbox is ReadOnly.
        /// </summary>
        /// <returns></returns>
        public bool ReadOnly()
        {
             return bool.Parse(element.GetAttribute("readonly"));
        }
        
    }
}
