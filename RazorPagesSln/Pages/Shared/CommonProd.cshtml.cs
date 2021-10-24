using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSln.Pages.Shared
{
    public class CommonProdModel : PageModel
    {
        //public IActionResult OnGetAsync()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToPage("../Login/UserLogin");
        //}
        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("~/Login/Userlogin");
        }
    }
}
