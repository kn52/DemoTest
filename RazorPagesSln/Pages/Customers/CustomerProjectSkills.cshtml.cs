using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.AppConfig;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.CustomerMngt;
using RazorPagesSln.Pagination;

namespace RazorPagesSln.Pages.Customers
{
    public class CustomerProjectSkillsModel : PageModel
    {

        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        private readonly AppSettingConfig _appSettingConfig;

        public CustomerProjectSkillsModel(RazorDBContext context,VerifyLogin verifyLogin, AppSettingConfig appSettingConfig)
        {
            _verifyLogin = verifyLogin;
            _context = context;
            _appSettingConfig = appSettingConfig;
        }

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public PaginatedList<ProjectSkill> ProjectSkills { get; set; }

        [BindProperty]
        public IList<SDescription> SDescription { get; set; }

        public async Task<IActionResult> OnGet(int? id, int? pageIndex)
        {
            int pageSize = _appSettingConfig._pageSubSize;
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

            var queryableList = _context.ProjectSkills.Where(ps=>ps.ProjectId == Project.ProjectId);
            ProjectSkills = await PaginatedList<ProjectSkill>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (Project == null)
            {
                return NotFound();
            }

            Customer = Project.Customer;
            SDescription = await _context.SDescriptions.ToListAsync();

            HttpContext.Session.SetString("viewname", "Project Skills");
            return Page();
        }
    }
}