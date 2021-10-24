using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.CustomerMngt
{
    public class SortCustomers
    {
        public static List<SortList> GetSortByList()
        {
            return new List<SortList>(){
                new SortList() { Key = "name", Value = "Name" },
                new SortList() { Key = "contact", Value = "Contact" },
                new SortList() { Key = "phone", Value = "Phone" },
                new SortList() { Key = "email", Value = "Email" }
            };
        }
    }

    public class SortList
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
