using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace AJSoftEntity.Classes
{
   public static class Util
    {
        public static string GetLoginEmail()
        {
            try
            {
                return HttpContext.Current.User.Identity.Name;
            }
            catch (Exception)
            {
                return "Anonymous";
            }

        }

        public static void EnsureFilePath(string filePath)
        {
            if (String.IsNullOrEmpty(filePath)) return;

            if (!System.IO.Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public static string GetClientIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        public static string GetDateFromDateTime(DateTime? date)
        {
            if (date == null || date == DateTime.MinValue || date == DateTime.MaxValue)
                return "";
            else
                return date.Value.ToShortDateString();
        }

        public static string GetDateFromDateTimeDDMMYYY(DateTime? date)
        {
            if (date == null || date == DateTime.MinValue || date == DateTime.MaxValue)
                return "";
            else
                return date.Value.ToString("dd/MM/yyyy");
        }

        public static string GetUniqueFileName(string extention)
        {
            return System.DateTime.UtcNow.Ticks.ToString() + "." + extention.Replace(".", "");
        }

        public static string GetSite_URL()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "";
            }
            catch (Exception)
            {
                //return "Anonymous";
                return "";
            }
        }

        public static string viewtostring(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static byte[] GeneratePDF(string HTML)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = HttpContext.Current.Server.MapPath(Constants.TempUploadPath + "\\wkhtmltopdf.exe");

            //string CaseStr = RenderRazorViewToString("ShareCaseView", lstModel);
            string FileName = Guid.NewGuid().ToString();

            p.StartInfo.Arguments = "-q -n - " + HttpContext.Current.Server.MapPath(Constants.TempUploadPath + "\\" + FileName + ".pdf");
            p.Start();

            p.StandardInput.AutoFlush = true;
            p.StandardInput.Write(HTML);
            p.StandardInput.Close();

            p.WaitForExit();
            p.Close();

            var buffer = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(Constants.TempUploadPath + "\\" + FileName + ".pdf"));
            System.IO.File.Delete(HttpContext.Current.Server.MapPath(Constants.TempUploadPath + "\\" + FileName + ".pdf"));

            return buffer;
        }

        public static DateTime ConvertUTCtoIST(DateTime UTCDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(UTCDateTime, TimeZoneInfo.FindSystemTimeZoneById("Indian Standard Time"));
        }
    }
}
