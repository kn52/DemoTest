using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.DBContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesSln.Entity.CustomerMngt;
using Microsoft.AspNetCore.Http;
using RazorPagesSln.Entity;

namespace RazorPagesSln.Pages.Customers
{
    public class CustomerProjectEditModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectEditModel(RazorDBContext context, VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public IList<PDescription> PDescription { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(c => c.Customer)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (Project == null)
            {
                return NotFound();
            }

            Customer = Project.Customer;
            PDescription = await _context.PDescriptions.ToListAsync();

            ViewData["PCode"] = new SelectList(PDescription, "PDescriptionId", "PCode");

            return Page();
        }

        [BindProperty]
        public PDescription PDescriptions { get; set; }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var projectToUpdate = await _context.Projects.FindAsync(id);

            if (projectToUpdate == null)
            {
                return NotFound();
            }

            projectToUpdate.CustomerId = Customer.CustomerId;

            if (await TryUpdateModelAsync<Project>(
                projectToUpdate,
                "project",
                p => p.ProjectName))
            {
                projectToUpdate.PD = PDescriptions.PDescriptionId;
                await _context.SaveChangesAsync();
                return RedirectToPage("./CustomerProjects", new { id = Customer.CustomerId });
            }

            return Page();
        }
    }
}

