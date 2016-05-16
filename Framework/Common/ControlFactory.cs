using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Common
{
    public class ControlFactory
    {
        private static Dictionary<String, PageProperty> properties = new Dictionary<string,PageProperty>();
        private static List<BaseControl> controls;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <param name="pageName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public T Control<T>(string controlName, string pageName = null, params string[] values) where T : class
        {
            try
            {
                var stackTrace = new StackTrace();
                MethodBase method = stackTrace.GetFrame(1).GetMethod();
                pageName = method.ReflectedType.Name.Replace("Page", "");

                By sel = GetSelector(pageName, controlName);

                if (controls == null)
                {
                    controls = new List<BaseControl>();
                }

                var control = controls.FirstOrDefault(c => typeof(T) == c.GetType() && c.by != null && c.by.Equals(sel));

                if (control == null)
                {
                    control = Activator.CreateInstance(typeof(T), new object[] { sel.MapValues(values) }) as BaseControl;
                    controls.Add(control);
                }
                return control as T;
            }
            catch (KeyNotFoundException)
            {
                string msg = string.Format("Can't find control '{0}' on page '{1}'", controlName, pageName);
                throw new KeyNotFoundException(msg);
            }
        }

        /// <summary>
        /// Find a list of elements base on its name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName">Name of element that defined in XML file</param>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="values">The values.</param>
        /// <returns>
        /// List of IWebElement
        /// </returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public List<T> Controls<T>(string controlName, string pageName = null, params string[] values) where T : class
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            pageName = method.ReflectedType.Name.Replace("Page", "");

            List<T> controlList = new List<T>();
            string errorMessage = string.Format("Can't find control '{0}' on page '{1}'", controlName, pageName);
            try
            {
                By sel = GetSelector(pageName, controlName);
                sel = sel.MapValues(values);

                List<IWebElement> elements = Browser.FindElements(sel).ToList();
                if (elements.Count == 0)
                {
                    throw new KeyNotFoundException(errorMessage);
                }

                foreach (var elem in elements)
                {
                    var control = Activator.CreateInstance(typeof(T), new object[] { elem });
                    controlList.Add(control as T);
                }
                return controlList;
            }
            catch (WebDriverException ex)
            {
                throw new WebDriverException(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private By GetSelector(string pageName, string controlName)
        {
            string controlKey = String.Format("{0}-{1}", pageName, controlName);

            // Get pageproperty and selector of control                
            if (properties == null || !properties.ContainsKey(controlKey))
            {
                String control;
                String controlType;
                String controlValue;
                
                string pagesPropertiesPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Interfaces", pageName + ".json");
                var json = File.ReadAllText(pagesPropertiesPath);
                JObject controls = JObject.Parse(json);

                foreach (KeyValuePair<String, JToken> app in controls)
                {
                    control = app.Key;
                    controlType = (String)app.Value["controlType"];
                    controlValue = (String)app.Value["controlValue"];

                    if (control.Equals(controlName))

                    {
                        properties.Add(String.Format("{0}-{1}", pageName, control), new PageProperty(controlValue, controlType));
                        break;
                    }
                }
            }
            PageProperty pageProperty = properties[controlKey];
            return pageProperty.GetSelector();
        }

    }
}
