using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Framework.Common
{
    public class PageMapping
    {
        private static PageMapping instance;
        public static PageMapping Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PageMapping();
                }
                return instance;
            }
        }

        /// <summary>
        /// Get page mapping from xml file
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetPageMapping()
        {
            Dictionary<string, string> pageMapping = new Dictionary<string, string>();
            string pagesPropertiesPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PageObjects");
            DirectoryInfo dirInfo = new DirectoryInfo(pagesPropertiesPath);
            foreach (var file in dirInfo.GetFiles())
            {
                if (file.Name.Contains("PageMapping.xml"))
                {
                    XDocument xdoc = XDocument.Load(file.FullName);
                    var newMapping = xdoc.Root.Descendants().ToDictionary(
                        p => p.Attribute("page").Value,
                        p => p.Attribute("xml").Value);//.AsEnumerable<KeyValuePair<string, string>>();
                    foreach (var item in newMapping)
                    {
                        if (!pageMapping.Keys.Contains(item.Key))
                        {
                            pageMapping.Add(item.Key, item.Value);
                        }
                    }
                }
            }

            return pageMapping;
        }
    }
}
