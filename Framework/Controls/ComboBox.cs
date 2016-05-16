using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Controls
{
    public class ComboBox : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public ComboBox() { }
        public ComboBox(string xPath) : base(By.XPath(xPath)) { }
        public ComboBox(IWebElement element) : base(element) { }
        public ComboBox(By by) : base(by) { }

        private SelectElement selectElement;

        /// <summary>
        /// Gets the select control.
        /// </summary>
        private void GetSelectControl()
        {
            LoadControl();
            selectElement = new SelectElement(element);
        }

        /// <summary>
        /// Gets list of options in combobox
        /// </summary>
        public IList<IWebElement> Options
        {
            get
            {
                GetSelectControl();
                return selectElement.Options;
            }
        }

        /// <summary>
        /// Gets list of option strings from combobox
        /// </summary>
        public IList<string> OptionStrings
        {
            get
            {
                GetSelectControl();
                IList<IWebElement> options = selectElement.Options;
                return options.Select(option => option.Text).ToList();
            }
        }

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SelectByText(string text)
        {
            GetSelectControl();
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectByIndex(int index)
        {
            GetSelectControl();
            selectElement.SelectByIndex(index);
        }

        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SelectByValue(string value)
        {
            GetSelectControl();
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Returns the selected option.
        /// </summary>
        /// <returns></returns>
        public IWebElement SelectedOption()
        {
            GetSelectControl();
            return selectElement.SelectedOption;
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedText()
        {
            GetSelectControl();
            return selectElement.SelectedOption.Text;
        }

        /// <summary>
        /// Selects the item with unexact text.
        /// </summary>
        /// <param name="text">The text that is part of an item</param>
        /// <returns></returns>
        public void SelectByUnexactText(string text)
        {
            IList<string> options = OptionStrings;
            string item = options.First(i => i.ToLower().Contains(text.ToLower()));
            SelectByIndex(options.IndexOf(item));
        }
    }
}