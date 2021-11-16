using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoTest.DemoS
{
    class HtmlWIthTextFile
    {
        public void FormHtmlWithFile()
        {
            StringBuilder str = new StringBuilder();
            
            var directory = Directory.GetCurrentDirectory();
            string mainhtml = string.Join("", Path.Combine(directory.Substring(0, directory.IndexOf("bin") - 1), "Files", "TEXTs", "TextFile.txt"));
            string dychtml = string.Join("", Path.Combine(directory.Substring(0, directory.IndexOf("bin") - 1), "Files", "TEXTs", "TextFile1.txt"));

            for (int i = 0; i < 2; i++)
            {
                string count = (i + 1).ToString();
                var body = dychtml.Replace("@@count", count);
                str.Append(body);
            }
            var finalhtml = mainhtml.Replace("@@bodycontent", str.ToString());

            Console.WriteLine(finalhtml);
        }

            
    }
}
