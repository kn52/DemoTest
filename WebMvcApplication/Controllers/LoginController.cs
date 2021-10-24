using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using WebMvcApplication.Models.Login;
using System.IO;
using WebMvcApplication.Encrypt_Decrypt;
using WebMvcApplication.Credentials;
using System;
using WebMvcApplication.LoggerModel;
using Newtonsoft.Json;

namespace WebMvcApplication.Controllers
{ 
    public class LoginController : Controller
    {
        public LoginController(Credentials.Credentials credentials,LogInfo logInfo)
        {
            Credentials = credentials;
            LogInfo = logInfo;
        }

        private Credentials.Credentials Credentials { get; set; }
        private LogInfo LogInfo { get; set; }
        public IActionResult Index()
        {
            string name = HttpContext.Session.GetString(Credentials.SessionNameKey);
            string password = HttpContext.Session.GetString(Credentials.SessionPasswordKey);

            if (name != null)
            {
                return this.VerifyCredentials(name, password);
            }

            LogInfo.InfoLogger("", "Login", "Index");
            return View();
        }

        public IActionResult Login(Login login)
        {
            return this.VerifyCredentials(login.Name, login.Password);
        }

        public IActionResult Logout()
        {
            try
            {
                string name = HttpContext.Session.GetString(Credentials.SessionNameKey);
                HttpContext.Session.Clear();
                ViewBag.NameMessage = "Hey " + name + "!";
                ViewBag.LogoutMessage = "You Logout Successfully...";
                ViewBag.SessionMessage = "All the sessions has been cleared";
                ViewBag.LoginMessage = "to login again click on login button to continue...";
            }
            catch(Exception exp)
            {
                LogInfo.ErrorLogger(exp);
                return Content(exp.Message.ToString());
            }

            LogInfo.InfoLogger("", "Login", "Logout");
            return View();
        }

        private IActionResult VerifyCredentials(string name,string password)
        {
            if (name != null && password != null)
            {
                try
                {
                    string EncryptedName = AESEncryption.EnryptString(name);
                    string EncryptedPassword = AESEncryption.EnryptString(password);
                    string DecryptedName = AESDecryption.Decrypt(Credentials.Name);
                    string DecryptedPassword = AESDecryption.Decrypt(Credentials.Password);

                    if (EncryptedName != DecryptedName)
                    {
                        ViewBag.Message = "Invalid Name";
                        return View("Index");
                    }
                    if (EncryptedPassword != DecryptedPassword)
                    {
                        ViewBag.Message = "Invalid Password";
                        return View("Index");
                    }

                    HttpContext.Session.SetString(Credentials.SessionNameKey, name);
                    HttpContext.Session.SetString(Credentials.SessionPasswordKey, password);
                }
                catch (Exception exp)
                {
                    LogInfo.ErrorLogger(exp);
                    return Content(exp.Message.ToString());
                }

                LogInfo.InfoLogger(JsonConvert.SerializeObject(new { username = Credentials.Name, userpassword = Credentials.Password }), "File", "Index");
                return RedirectToAction("Index", "File");
            }

            LogInfo.InfoLogger(JsonConvert.SerializeObject(new { username = Credentials.Name, userpassword = Credentials.Password }), "Login", "Index");
            ViewBag.Message = "Invalid Credentials";
            return View("Index");
        }
    }
}
