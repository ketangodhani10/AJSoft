using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AJSoftWeb.Models;
using System.Web.Routing;
using AJSoftEntity;
using AJSoftBAL;
using AJSoftWeb.Classes;
using static AJSoftEntity.Classes.EnumData;
using AJSoftEntity.Classes;

namespace AJSoftWeb.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            base.Initialize(requestContext);
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            //LoginViewModel oLoginViewModel = new LoginViewModel();
            //return View("Login", oLoginViewModel);
            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult LoginAsPrimaryUser(Guid PUseId)
        {
            //logout current user
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Session.Abandon();
                FormsService.SignOut();
            }

            //authenticate primary user and set session variables
            User oUser = new UserBL().GetById(PUseId);
            FormsService.SignIn(oUser.Email, false);

            SiteUtility.SetSessionVariables(oUser.Email);

            //Notification

            //if (oUser.HomePage == En_HomePage.Case_Management.ToString().Replace("_", " "))
            //    return RedirectToAction("Index", "Case", new { Area = "Clinic" });
            //else if (oUser.HomePage == En_HomePage.System_Dashboard.ToString().Replace("_", " "))
            //{
            //    return RedirectToAction("Index", "Home", new { Area = "Clinic" });
            //}
            //else if (oUser.HomePage == En_HomePage.Finance_Dashboard.ToString().Replace("_", " "))
            //{
            //    return RedirectToAction("Index", "Payments", new { Area = "Clinic" });
            //}
            //else
            return RedirectToAction("Index", "Home", new { Area = "Clinic" });
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel oLoginViewModel)
        {
            try
            {
                vw_Users oUser = new UserBL().ValidateUser(oLoginViewModel.Email, oLoginViewModel.Password);
                if (oUser != null)
                {
                    string Active = En_User_Status.Active.ToString();
                    string Inactive = En_User_Status.Inactive.ToString();

                    if (oUser.Status == Inactive)
                    {
                        ViewBag.Error = "Your Account has been suspended, please contact to site administrator.";
                        return View();
                    }
                    else if (oUser.Status == Active)
                    {
                        FormsService.SignIn(oLoginViewModel.Email, oLoginViewModel.RememberMe);
                        SiteUtility.SetSessionVariables(oLoginViewModel.Email);

                        if (oUser.RoleId == (int)En_Role.Primary)
                        {
                            if(oUser.ActivationEndDate < DateTime.UtcNow)
                            {
                                ViewBag.Error = "Your Account has been expired, please contact to site administrator.";
                                return View();
                            }
                            //SiteUtility.SetSessionVariables(oUser.Email);
                            //if (oUser.HomePage == En_HomePage.Case_Management.ToString().Replace("_", " "))
                            //    return RedirectToAction("Index", "Case", new { Area = "Clinic" });
                            //else if (oUser.HomePage == En_HomePage.System_Dashboard.ToString().Replace("_", " "))
                            //{
                            //    return RedirectToAction("Index", "Home", new { Area = "Clinic" });
                            //}
                            //else if (oUser.HomePage == En_HomePage.Finance_Dashboard.ToString().Replace("_", " "))
                            //{
                            //    return RedirectToAction("Index", "Payments", new { Area = "Clinic" });
                            //}
                            //else
                            return RedirectToAction("Index", "ShadeCards");
                        }
                        else
                        {
                            //if (oUser.HomePage == En_HomePage.Case_Management.ToString().Replace("_", " "))
                            //    return RedirectToAction("Index", "Case");
                            //else if (oUser.HomePage == En_HomePage.Finance_Dashboard.ToString().Replace("_", " "))
                            //{
                            //    if (SiteUtility.GetCurrentUser().RoleId == (int)En_Role.Specialist)
                            //    {
                            //        return RedirectToAction("Index", "Case");
                            //    }
                            //    return RedirectToAction("Index", "FinanceDashboard");
                            //}
                            //else if (oUser.HomePage == En_HomePage.System_Dashboard.ToString().Replace("_", " "))
                            //{
                            //    return RedirectToAction("Index", "Home");
                            //}
                            //else
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }
                    }
                }
                else
                    ViewBag.Error = "Invalid Email or Password.";

                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    Session.Abandon();
                    FormsService.SignOut();
                }
                return Redirect("~/Account/Login");
            }
            catch (Exception ex)
            {
                return View(new { error = ex.Message });
            }
        }

        #region Forgot Password Link

        public ActionResult ForgotPassword(string Forgot = "")
        {
            ViewBag.Forgot = Forgot;
            return View();
        }

        [HttpPost]
        public ActionResult SendForgotPasswordLink(ForgotPasswordViewModel oForgotPasswordModel)
        {
            try
            {
                //ForgotType
                if (Request.Form["ForgotType"] != null && (!string.IsNullOrEmpty(Convert.ToString(Request.Form["ForgotType"])) && Convert.ToString(Request.Form["ForgotType"]) == "Email"))
                {
                    User oUser = null;
                    if (!string.IsNullOrEmpty(oForgotPasswordModel.EmailAddress))
                    {
                        oUser = new UserBL().GetByEmail(oForgotPasswordModel.EmailAddress);
                    }
                    if (oUser != null)
                    {
                        #region Send  Mail
                        try
                        {
                            if (!string.IsNullOrEmpty(oUser.Email))
                            {
                                //EmailTemplate oEmailTemplate = new EmailTemplateBL().GetEmailTemplateByType("ForgotEmail");
                                //string body = oEmailTemplate.Body;
                                //body = body.Replace("#name#", oUser.FirstName + " " + oUser.LastName);
                                //body = body.Replace("#companyname#", "AmarJari");
                                //body = body.Replace("#email#", oUser.Email);
                                //body = body.Replace("#LogoPath#", AJSoftEntity.Classes.Util.GetSite_URL() + "/Content/images/logo.png");
                                //body = body.Replace("#SiteUrl#", AJSoftEntity.Classes.Util.GetSite_URL());

                                //MailMessage message = new MailMessage();
                                //message.To.Add(new MailAddress(oUser.Email, "", Encoding.UTF8));
                                //message.Bcc.Add(System.Configuration.ConfigurationManager.AppSettings["CommanBCC"]);
                                //message.Subject = oEmailTemplate.Subject;
                                //message.IsBodyHtml = true;
                                //message.Body = body;

                                //SmtpClient smtpClient = new SmtpClient();
                                //smtpClient.Timeout = 1000000;
                                //smtpClient.Send(message);
                            }
                        }
                        catch (Exception) { }
                        #endregion
                    }

                    TempData["successmsg"] = "If that email address is assigned to a email in our system, then it will be emailed to you at that address. Please contact our office with any additional support questions.";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    User oUser = new UserBL().GetByEmail(oForgotPasswordModel.EmailAddress);
                    if (oUser != null)
                    {

                        #region Send Regestraion Mail
                        try
                        {
                            if (!string.IsNullOrEmpty(oUser.Email))
                            {
                                Guid Token = Guid.NewGuid();
                                //oUser.VerificationCode = Token;
                                //new UserBL().Update(oUser);

                                //EmailTemplate oEmailTemplate = new EmailTemplateBL().GetEmailTemplateByType("ForgotPassword");
                                //string body = oEmailTemplate.Body;
                                //body = body.Replace("#name#", oUser.FirstName + " " + oUser.LastName);
                                //body = body.Replace("#companyname#", "AmarJari");

                                //string link = AJSoftEntity.Classes.Util.GetSite_URL() + "/Account/ResetPasswordFromLink?UserId=" + oUser.UserId + "&Token=" + Token;
                                //body = body.Replace("#link#", link);
                                //body = body.Replace("#LogoPath#", AJSoftEntity.Classes.Util.GetSite_URL() + "/Content/images/logo.png");
                                //body = body.Replace("#SiteUrl#", AJSoftEntity.Classes.Util.GetSite_URL());

                                //MailMessage message = new MailMessage();
                                //message.To.Add(new MailAddress(oUser.Email, "", Encoding.UTF8));
                                //message.Bcc.Add(System.Configuration.ConfigurationManager.AppSettings["CommanBCC"]);
                                //message.Subject = oEmailTemplate.Subject;
                                //message.IsBodyHtml = true;
                                //message.Body = body;

                                //SmtpClient smtpClient = new SmtpClient();
                                //smtpClient.Timeout = 1000000;
                                //smtpClient.Send(message);
                            }
                        }
                        catch (Exception) { }
                        #endregion

                        TempData["successmsg"] = "A mail with reset password link has been sent. Please check your Inbox.";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        TempData["errormsg"] = "Invalid Email.";
                        return View("ForgotPassword");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errormsg"] = ex.Message;
                return View("ForgotPassword");
            }
        }

        [HttpGet]
        public ActionResult ResetPasswordFromLink(Guid userId, Guid token) //reset password from link validation
        {
            ResetPasswordModel oResetPasswordModel = new ResetPasswordModel();
            try
            {
                User oUser = new UserBL().GetById(userId);

                //if (oUser != null && oUser.VerificationCode != null && oUser.VerificationCode == token)
                //{
                //    oResetPasswordModel.Email = oUser.Email;
                //    ViewBag.IsAccountSetup = false;
                //    return View("ResetPassword", oResetPasswordModel);

                //}
                //else
                //{
                //    TempData["errormsg"] = "Reset password link is not valid.";
                //    ViewBag.error = TempData["errormsg"];
                    return View("Verify");
                //}
            }
            catch (Exception ex)
            {
                TempData["errormsg"] = ex.Message;
                ViewBag.error = TempData["errormsg"];
                return View("Verify", oResetPasswordModel);
            }
        }

        #endregion

        #region Reset Password
        //[AllowAnonymous]
        //public ActionResult Verify(Guid UserId, Guid Token)
        //{
        //    try
        //    {
        //        User oUser = new UserBL().GetById(UserId);

        //        //if (oUser.Status == En_User_Status.Active.ToString())
        //        //{
        //        //    ViewBag.error = "Your account is already verified, Please click <a href='/Account/Login'>here</a> to login.";
        //        //    return View();
        //        //}
        //        //else if (oUser.VerificationCode != Token)
        //        //{
        //        //    ViewBag.error = "URL is not properly formatted.";
        //        //    return View();
        //        //}

        //        //if no error confirm user email
        //        //new UserBL().ConfirmUser(oUser.Email);

        //        TempData["successmsg"] = "Your account has been verified successfully, Please set your User name and  password for login.";
        //        ViewBag.IsAccountSetup = true;
        //        ResetPasswordModel oRegistermodel = new ResetPasswordModel();
        //        oRegistermodel.UserId = oUser.UserId;
        //        oRegistermodel.Email = oUser.Email;

        //        if (oUser.CompanyId != null)
        //        {
        //            Company oCompany = new CompanyBL().GetById((Guid)oUser.CompanyId);
        //            oUser.Email = Convert.ToString(oUser.UserName).Replace(oCompany.ShortName, "");
        //            ViewBag.IsAdminUser = false;
        //        }
        //        else
        //        {
        //            ViewBag.IsAdminUser = true;
        //        }

        //        oRegistermodel.UserName = Convert.ToString(oUser.UserName);
        //        return View("AccountSetup", oRegistermodel);
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.error = "Your account could not confirmed due to some problem, Please contact Site Administrator.";
        //        return View();
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel oResetPasswordModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(oResetPasswordModel.Email))
                {
                    //new UserBL().ConfirmUser(oResetPasswordModel.Email, oResetPasswordModel.UserName, oResetPasswordModel.ConfirmPassword);
                }
                TempData["successmsg"] = "Your Password has been successfully Reset.";
                return RedirectToAction("Index", "Account");
            }
            catch (Exception)
            {
                TempData["errormsg"] = CommonMsg.Error("");
                ViewBag.error = TempData["errormsg"];
                return View("ResetPassword", new ResetPasswordModel());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AccountSetup(ResetPasswordModel oResetPasswordModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(oResetPasswordModel.Email))
                {
                    //new UserBL().ConfirmUser(oResetPasswordModel.Email, oResetPasswordModel.UserName, oResetPasswordModel.ConfirmPassword);
                }
                TempData["successmsg"] = "Your account has been successfully Setup.";
                return RedirectToAction("Index", "Account");
            }
            catch (Exception)
            {
                TempData["errormsg"] = CommonMsg.Error("");
                ViewBag.error = TempData["errormsg"];
                return View("AccountSetup", new ResetPasswordModel());
            }
        }

        #endregion

        #region ChangePassword

        //Render view for Change a Password.
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        //Change password for User who requesting for this function.
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel oChangePasswordModel)
        {
            try
            {
                if (new UserBL().IsCurrentPasswordMatch(oChangePasswordModel.OldPassword, (User)Session[En_UserSession.User.ToString()]))
                {
                    new UserBL().ChangePassword(oChangePasswordModel.NewPassword, (User)Session[En_UserSession.User.ToString()]);
                    TempData["successmsg"] = "Your Password has been changed successfully.";
                }
                else
                    TempData["errormsg"] = "Your Current Password does not matched.";

            }
            catch (Exception ex)
            {
                TempData["errormsg"] = CommonMsg.Error("");
            }
            return View();
        }

        #endregion

        public JsonResult checkEmail(string Email, Guid UserId)
        {
            try
            {
                var objUser = new UserBL().CheckEmail(Email, UserId);
                if (objUser == null)
                    return Json(true);
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public ActionResult NoAccess()
        {
            return View("~/Views/Shared/NoAccess.cshtml");
        }


    }
}