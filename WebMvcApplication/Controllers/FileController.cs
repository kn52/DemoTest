using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebMvcApplication.Logs;

namespace WebMvcApplication.Controllers
{
    public class FileController : Controller
    {
        public FileController(Credentials.Credentials credentials,LogInfo logInfo)
        {
            Credentials = credentials;
            LogInfo = logInfo;
        }
        private Credentials.Credentials Credentials { get; set; }
        private LogInfo LogInfo { get; set; }
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString(Credentials.SessionNameKey);
            if(name == null)
            {
                LogInfo.InfoLogger("", "Login", "Index");
                return RedirectToAction("Index", "Login");
            }

            LogInfo.InfoLogger("", "File", "Index");
            ViewBag.LoginMessage = "Hi " + name + "!";
            return View();
        }

        public IActionResult Upload()
        {
            var name = HttpContext.Session.GetString(Credentials.SessionNameKey);
            if (name == null)
            {
                LogInfo.InfoLogger("", "Login", "Index");
                return RedirectToAction("Index", "Login");
            }

            LogInfo.InfoLogger("", "File", "Upload");
            ViewBag.LoginMessage = "Hi " + name + "!";
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(List<IFormFile> files)
        {
            var name = HttpContext.Session.GetString(Credentials.SessionNameKey);
            if (name == null)
            {
                LogInfo.InfoLogger("", "Login", "Index");
                return RedirectToAction("Index", "Login");
            }

            if (files.Count == 0)
                return Content("file not selected");

            try
            {
                var directory = Directory.GetCurrentDirectory();
                foreach(var file in files)
                {
                    var path = Path.Combine(directory, "wwwroot", "images", file.FileName);
                    LogInfo.InfoLogger(JsonConvert.SerializeObject(new { fileName = file.FileName, filePath = path }), "File", "UploadFile");
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
            }
            catch (Exception exp)
            {
                LogInfo.ErrorLogger(exp);
                return Content(exp.Message.ToString());
            }

            ViewBag.LoginMessage = "Hi " + name + "!";
            ViewBag.Message = "File Uploaded Successfully";
            return View();
        }

        public IActionResult DownloadFile(string fileName)
        {
            if (fileName == null || fileName.Trim() == "")
                return Content("filename not present");

            byte[] bytes;

            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                bytes = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception exp)
            {
                LogInfo.ErrorLogger(exp);
                return Content(exp.Message.ToString());
            }

            LogInfo.InfoLogger(JsonConvert.SerializeObject(fileName), "File", "UploadFile");
            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult ViewFile()
        {
            var name = HttpContext.Session.GetString(Credentials.SessionNameKey);
            if (name == null)
            {
                LogInfo.InfoLogger("", "Login", "Index");
                return RedirectToAction("Index", "Login");
            }

            Dictionary<string, string> filelist = null;

            try
            {
                var directory = Directory.GetCurrentDirectory();
                var path = Path.Combine(directory, "wwwroot", "images");
                string[] files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    filelist = new Dictionary<string, string>();
                    foreach (var im in files)
                    {
                        string[] arr = im.Split("\\");
                        string Key = arr[arr.Length - 1];
                        string Value = im;
                        filelist.Add(Key, Value);
                    }
                }
                
            }
            catch(Exception exp)
            {
                LogInfo.ErrorLogger(exp);
                return Content(exp.Message.ToString());
            }

            LogInfo.InfoLogger("","File","ViewFile");    
            ViewBag.LoginMessage = "Hi " + name + "!";
            return View(filelist);
        }
    }
}
