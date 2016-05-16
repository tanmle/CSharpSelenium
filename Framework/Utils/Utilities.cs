using Framework.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Framework.Utils
{
    public static class Utilities
    {
        public static void EndProcess(string proccess)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(proccess))
                {
                    proc.Kill();
                }
            }
            catch (RLException ex)
            {
                throw new RLException("End Process failed " + ex.Message);
            }
        }

        public static T ConvertNode<T>(XmlNode node) where T : class
        {
            T result;
            using (MemoryStream stm = new MemoryStream())
            { 
                using(StreamWriter stw = new StreamWriter(stm))
                { 
                    stw.Write(node.OuterXml);
                    stw.Flush();

                    stm.Position = 0;

                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    result = (ser.Deserialize(stm) as T);
                }
            }
            return result;
        }

        public static T ConvertNode<T>(XmlDocument node) where T : class
        {
            T result;
            using(MemoryStream stm = new MemoryStream())
            { 
                using(StreamWriter stw = new StreamWriter(stm))
                {
                    stw.Write(node.OuterXml);
                    stw.Flush();

                    stm.Position = 0;

                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    result = (ser.Deserialize(stm) as T);
                }
            }
            return result;
        }

        public static string GetRandomValue(string value)
        {
            value = String.Format("{0}_{1}", value.Replace(' ', '_'), DateTime.Now.ToString(Constants.FULL_DATETIME));
            return value;
        }

        public static string GetRandomFileName()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}
