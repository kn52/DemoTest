using DemoTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace DemoTest.DemoS
{
    public class HtmlWithXmlFile
    {
        public void FromHtmlWithXmlFile()
        {
            HtmlWithXmlDocument xmldoc = new HtmlWithXmlDocument();
            
            var directory = Directory.GetCurrentDirectory();
            XDocument doc = XDocument.Load(Path.Combine(directory.Substring(0, directory.IndexOf("bin") - 1), "Files", "XMLs", "EmailTemplate.xml"));

            foreach(XElement element in doc.Descendants("EmailContainers").Descendants("EmailContainer"))
            {
                xmldoc.HeaderTemplate = element.Element("HeaderTemplate") != null ? element.Element("HeaderTemplate").Value : string.Empty;
                xmldoc.BodyTemplate = element.Element("BodyTemplate") != null ? element.Element("BodyTemplate").Value : string.Empty;
                xmldoc.FooterTemplate = element.Element("FooterTemplate") != null ? element.Element("FooterTemplate").Value : string.Empty;
            }

            StringBuilder mainHtm = new StringBuilder();

            mainHtm.Append(xmldoc.HeaderTemplate);
            mainHtm.Append(xmldoc.BodyTemplate.Replace("@@num", "pp"));
            mainHtm.Append(xmldoc.BodyTemplate.Replace("@@num", "xx"));
            mainHtm.Append(xmldoc.FooterTemplate);

            Console.WriteLine(mainHtm);
        }
    }
}
