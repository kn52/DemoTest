using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Entity.CustomerMngt;

namespace RazorPages.Pages.Customers
{
    public class CustomerProjectSkillDeleteModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectSkillDeleteModel(RazorDBContext context, VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        public Project Project { get; set; }
        [BindProperty]
        public ProjectSkill ProjectSkill { get; set; }
        [BindProperty]
        public SDescription SDescription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            ProjectSkill = await _context.ProjectSkills
                .Include(ps => ps.Project)
                    .ThenInclude(p => p.Customer)
                    .FirstOrDefaultAsync(ps => ps.ProjectSkillId == id);

            if (ProjectSkill == null)
            {
                return NotFound();
            }

            Project = ProjectSkill.Project;
            SDescription = await _context.SDescriptions.FirstAsync(x=>x.SDescriptionId == ProjectSkill.SD);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectSkill = await _context.ProjectSkills
                .FirstOrDefaultAsync(ps => ps.ProjectSkillId == id);

            if (ProjectSkill != null)
            {
                _context.ProjectSkills.Remove(ProjectSkill);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./CustomerProjectSkills", new { id = ProjectSkill.ProjectId });
        }
    }
}