using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSln.Pages.Common
{
    public class CommonOpsModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Login/UserLogin");
        }
        public IActionResult OnPostAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Login/Userlogin");
        }
        public IActionResult OnPostLogoutEmpAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Login/Userlogin");
        }
    }
}
