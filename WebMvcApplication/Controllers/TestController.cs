using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcApplication.DbC;
using WebMvcApplication.Models;
using WebMvcApplication.Models.Mgmt;

namespace WebMvcApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly DBC _db;
        public TestController(DBC db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var emp = _db.Customers.ToList();
            return View(emp);
        }

        // GET: HomeController1/Details/5
        
        public bool Details(string age = "")
        {            
            return true;
            //return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        [HttpPost]
        public ActionResult Edit(string id = "")
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteEmp([FromBody] string id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChangesAsync();
            }
            return Json("Success");
        }

        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            PersonModel person = new PersonModel
            {
                Name = name,
                DateTime = DateTime.Now.ToString()
            };
            return Json(person);
        }
    }
}
