using OpenQA.Selenium;
using System;

namespace Framework.Common
{
    public class PageProperty
    {
		public string Selector;
		public string SelectorType;

        public PageProperty(string selector, string selectorType)
        {
            Selector = selector;
            SelectorType = selectorType;
        }

		public By GetSelector(string value=null) 
        {
            if (value != null)
                Selector = String.Format(Selector, value);
            switch (SelectorType.ToUpper())
            {
				case "CSS":
					return By.CssSelector(Selector);
				case "XPATH":
                    return By.XPath(Selector);
				case "ID":
                    return By.Id(Selector);
				case "NAME":
                    return By.Name(Selector);
                case "LINKTEXT":
                    return By.LinkText(Selector);                
				default:
                    return By.XPath(Selector);
            }
		}

    }
}
