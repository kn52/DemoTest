using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.CustomerMngt;

namespace RazorPagesSln.Pages.Customers
{
    public class CustomerProjectSkillEditModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectSkillEditModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public ProjectSkill ProjectSkill { get; set; }
        [BindProperty]
        public IList<SDescription> SDescription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            ProjectSkill = await _context.ProjectSkills
                .Include(p => p.Project)
                .ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ProjectSkillId == id);

            Project = ProjectSkill.Project;
            Customer = ProjectSkill.Project.Customer;
            SDescription = await _context.SDescriptions.ToListAsync();

            if (ProjectSkill == null)
            {
                return NotFound();
            }

            ViewData["SCode"] = new SelectList(SDescription, "SDescriptionId", "SCode");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            
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

            _context.Attach(ProjectSkill).State = EntityState.Modified;

            try
            {
                ProjectSkill.ProjectId = Project.ProjectId;
                ProjectSkill.SD = SDescriptions.SDescriptionId;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectSkillExists(ProjectSkill.ProjectSkillId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CustomerProjectSkills", new { id = Project.ProjectId });
        }

        private bool ProjectSkillExists(int id)
        {
            return _context.ProjectSkills.Any(e => e.ProjectSkillId == id);
        }
    }
}
