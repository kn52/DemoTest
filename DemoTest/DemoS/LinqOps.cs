using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTest.DemoS
{
    public class LinqOps
    {
        public static void Operations()
        {
            List<Opts> shoppingbags_barcodes = new List<Opts>()
            {
               new Opts() { bagId = "1", barcodes = new List<string>() { "1", "2", "3", "4" } },
               new Opts() { bagId = "2", barcodes = new List<string>() { "1", "2", "3", "4" } }
            };

            List<Opts> post_shoppingbags_barcodes = new List<Opts>()
            {
               new Opts() { bagId = "1", barcodes = new List<string>() { "1", "2", "3", "4" } },
               new Opts() { bagId = "2", barcodes = new List<string>() { "1" } }
            };

            
            foreach (var shp_bags in shoppingbags_barcodes)
            {
                List<Opts> newList = new List<Opts>();
                var postlist = post_shoppingbags_barcodes.Where(y => y.bagId == shp_bags.bagId).Select(y=>y);
                //newList.Add(postlist[0]);
            }
            

        }
    }

    public class Opts
    {
        public string bagId { get; set; }
        public List<string> barcodes { get; set; }
    }
}
