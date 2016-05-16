using OpenQA.Selenium;
using Framework.Common;

namespace Framework.Controls
{
	public class Hyperlink : BaseControl
	{
		public Hyperlink() { }
		public Hyperlink(string xPath) : base(By.XPath(xPath)) { }
		public Hyperlink(By by) : base(by) { }
		public Hyperlink(IWebElement element) : base(element) { }
	}
}
