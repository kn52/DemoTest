using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcApplication.LoggerModel
{
    public class LogInfo
    {
        public static void ErrorLogger(Exception ex, string msg = null)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", msg ?? ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", (ex.TargetSite == null ? "" : ex.TargetSite.ToString()));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            // var webRoot = _env.WebRootPath;
            string path = Path.Combine(Directory.GetCurrentDirectory(),"Logs","LogError.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestJson"></param>
        /// <param name="responseJson"></param>
        /// <param name="methosName"></param>
        public static void InfoLogger(string requestJson, string controllerJson, string methosName)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Method Name: {0}", methosName);
                message += Environment.NewLine;
                message += string.Format("Request: {0}", requestJson);
                message += Environment.NewLine;
                message += string.Format("Controller: {0}", controllerJson);
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                // var webRoot = _env.WebRootPath;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "LogInfo.txt");
                using (StreamWriter writer = new StreamWriter(path,true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch
            {
                //no need to check
            }
        }
        public static void ClearLog()
        {
            string path; 
            path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "LogInfo.txt");
            File.WriteAllText(path, String.Empty);
            path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "LogError.txt");
            File.WriteAllText(path, String.Empty);
        }


        public static string LogoutMessage(string name)
        {
            string message = "Hey " + name + "!";
            message += Environment.NewLine;
            message += "You Logout Successfully...";
            message += Environment.NewLine;
            message += "All the sessions has been cleared";
            message += Environment.NewLine;
            message += "to login again click on login button to continue...";

            return message;
        }
    }
}
