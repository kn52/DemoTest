using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.Home
{
    [Table("RedirectUrls")]
    public class RedirectUrls
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string UrlDescription { get; set; }
    }


    public class HomeMessage
    {
        public static string GetHomeMessage()
        {
            return "Choose page to which you want to redirect";
        }
    }
}
