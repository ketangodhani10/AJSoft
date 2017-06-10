using AJSoftBAL;
using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AJSoftEntity.Classes.EnumData;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using AJSoftEntity.Classes;

namespace AJSoftWeb.Classes
{
    public static class SiteUtility
    {
        public static void SetSessionVariables(string Email)
        {
            User oUser = new UserBL().GetByEmail(Email);
            System.Web.HttpContext.Current.Session[En_UserSession.User.ToString()] = oUser;
            System.Web.HttpContext.Current.Session[En_UserSession.RoleId.ToString()] = oUser.RoleId;

            //if (oUser.RoleId == (int)En_Role.Primary || oUser.RoleId == (int)En_Role.User)
            //{
            //    if (new UserPermissionBL().IsPermissionExist(oUser.ClinicId.Value, oUser.UserId, En_ClinicUserPermission.ClinicAdmin.ToString()).Permission == En_Permission_ClinicAdmin.Edit_All.ToString().Replace("_", " "))
            //        System.Web.HttpContext.Current.Session[En_ClinicUserPermission.ClinicAdmin.ToString()] = true;
            //    else
            //        System.Web.HttpContext.Current.Session[En_ClinicUserPermission.ClinicAdmin.ToString()] = false;

            //    if (new UserPermissionBL().IsPermissionExist(oUser.ClinicId.Value, oUser.UserId, En_ClinicUserPermission.ClinicBilling.ToString()).Permission == En_Permission_ClinicBilling.Full_Access.ToString().Replace("_", " "))
            //        System.Web.HttpContext.Current.Session[En_ClinicUserPermission.ClinicBilling.ToString()] = true;
            //    else
            //        System.Web.HttpContext.Current.Session[En_ClinicUserPermission.ClinicBilling.ToString()] = false;
            //}
        }

        public static User GetCurrentUser()
        {
            return (User)System.Web.HttpContext.Current.Session[En_UserSession.User.ToString()];
        }

        public static int GetCurrentUserRole()
        {
            return (int)System.Web.HttpContext.Current.Session[En_UserSession.RoleId.ToString()];
        }

        public static JariCompany GetCurrentJariCompany(User oUser)
        {
            JariCompany oCompany = new JariCompanyBL().GetById(oUser.JariCompanyId.Value);
            System.Web.HttpContext.Current.Session[En_UserSession.JariCompany.ToString()] = oCompany;
            return (JariCompany)System.Web.HttpContext.Current.Session[En_UserSession.JariCompany.ToString()];
        }

        public static bool IsImage(string FileName)
        {
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileName).ToLower() != ".png"
                && Path.GetExtension(FileName).ToLower() != ".bmp"
                && Path.GetExtension(FileName).ToLower() != ".gif"
                && Path.GetExtension(FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            return true;
        }

        public static Image ResizeImage(Image Image, int Width, int Height)
        {
            int newwidth = Image.Width;
            int newheight = Image.Height;
            if (newwidth > Width || newheight > Height)
            {
                //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
                //from displaying as the thumbnail image later, in other words, we want a clean
                //resize, not a grainy one.
                Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                float ratio = 0;
                if (newwidth > newheight)
                {
                    ratio = (float)newwidth / (float)newheight;
                    newwidth = Width;
                    newheight = Convert.ToInt32(Math.Round((float)newwidth / ratio));

                }
                else
                {
                    ratio = (float)newheight / (float)newwidth;
                    newheight = Height;
                    newwidth = Convert.ToInt32(Math.Round((float)newheight / ratio));
                }
                var newImage = new Bitmap(newwidth, newheight);
                Graphics.FromImage(newImage).DrawImage(Image, 0, 0, newwidth, newheight);
                Bitmap bmp = new Bitmap(newImage);

                return bmp;
            }
            else
                return Image;
        }

        public static byte[] ResizeImage(byte[] bytesOfImage, int Width, int Height)
        {
            if (bytesOfImage != null)
            {
                MemoryStream ms = new MemoryStream(bytesOfImage);
                Image Image = System.Drawing.Image.FromStream(ms);

                int newwidth = Image.Width;
                int newheight = Image.Height;
                if (newwidth > Width || newheight > Height)
                {
                    Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                    Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                    float ratio = 0;
                    if (newwidth > newheight)
                    {
                        ratio = (float)newwidth / (float)newheight;
                        newwidth = Width;
                        newheight = Convert.ToInt32(Math.Round((float)newwidth / ratio));
                    }
                    else
                    {
                        ratio = (float)newheight / (float)newwidth;
                        newheight = Height;
                        newwidth = Convert.ToInt32(Math.Round((float)newheight / ratio));
                    }

                    var newImage = new Bitmap(newwidth, newheight);
                    Graphics.FromImage(newImage).DrawImage(Image, 0, 0, newwidth, newheight);
                    Bitmap bmp = new Bitmap(newImage);

                    ImageConverter converter = new ImageConverter();
                    return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                }
                else
                    return bytesOfImage;
            }
            else
                return bytesOfImage;
        }

        public static string GetImageUrl()
        {
            //string ImagePath = ConfigurationManager.AppSettings["ImagePath"];
            string ImagePath = Util.GetSite_URL() + "/uploads";
            //string ImagePath = HttpContext.Current.Server.MapPath("uploads");
            return ImagePath;
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static DateTime ConvertUTCtoIST(DateTime UTCDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(UTCDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }
    }
}