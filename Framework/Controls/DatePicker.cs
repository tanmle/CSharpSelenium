using OpenQA.Selenium;
using Framework.Common;
using System;
using System.Linq;

namespace Framework.Controls
{
    public class DatePicker: BaseControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public DatePicker() { }
        public DatePicker(string xPath) : base(By.XPath(xPath)) { }
        public DatePicker(IWebElement element) : base(element) { }
        public DatePicker(By by) : base(by) { }

        /// <summary>
        /// Set date for date picker
        /// </summary>
        /// <param name="date">date: MM/dd/yyyy</param>
        public void SetDate(string date, string shortDateFormat)
        {
            try
            {
                //parse date to year, month, day. Date with format(MM/dd/yyyy or dd/MM/yyyy)
                int month, day, year;
                string[] arrDates = date.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (shortDateFormat.Equals(Constants.SHORT_DATE_US))
                {
                    month = int.Parse(arrDates[0]) - 1;
                    day = int.Parse(arrDates[1]);                
                }
                else
                {
                    month = int.Parse(arrDates[1]) - 1;
                    day = int.Parse(arrDates[0]); 
                }
                year = int.Parse(arrDates[2]);
                
                //click at datepicker button 
                Click();
                ComboBox comboxYear = new ComboBox("//select[@class='ui-datepicker-year']");
                ComboBox comboxMonth = new ComboBox("//select[@class='ui-datepicker-month']");
                while (!comboxYear.Options.Any(p => p.Text.Equals(year.ToString())))
                {
                    string currentYear = comboxYear.GetSelectedText();
                    if (int.Parse(currentYear) > year)
                    {
                        comboxYear.SelectByValue(comboxYear.Options.First().Text);
                    }
                    else
                    {
                        comboxYear.SelectByValue(comboxYear.Options.Last().Text);
                    }
                }
                comboxYear.SelectByValue(year.ToString());
                comboxMonth.SelectByValue(month.ToString());
                Hyperlink lnkDay = new Hyperlink(String.Format("//a[contains(@class,'ui-state-default') and text()={0}]", day));
                lnkDay.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
