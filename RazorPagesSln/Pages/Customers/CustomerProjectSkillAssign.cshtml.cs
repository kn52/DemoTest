using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.CustomerMngt;

namespace RazorPagesSln.Pages.Customers
{
    public class CustomerProjectSkillAssignModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectSkillAssignModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public IList<SDescription> SDescription { get; set; }

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
                .Include(p => p.Customer)
                .Include(p => p.ProjectSkills)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            SDescription = await _context.SDescriptions.ToListAsync();

            if (Project == null)
            {
                return NotFound();
            }

            Customer = Project.Customer;

            ViewData["SCode"] = new SelectList(SDescription, "SDescriptionId", "SCode");

            return Page();
        }

        [BindProperty]
        public SDescription SDescriptions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProjectSkill projectSkill = new ProjectSkill
            {
                ProjectId = Project.ProjectId,
                SD = SDescriptions.SDescriptionId
            };

            _context.ProjectSkills.Add(projectSkill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CustomerProjectSkills", new { id = Project.ProjectId });
        }
    }
} 