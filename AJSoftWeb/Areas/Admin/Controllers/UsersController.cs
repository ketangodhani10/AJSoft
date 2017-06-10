using AJSoftBAL;
using AJSoftEntity;
using AJSoftEntity.Classes;
using AJSoftWeb.Areas.Admin.Models;
using AJSoftWeb.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AJSoftEntity.Classes.EnumData;

namespace AJSoftWeb.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        User oCrntUser = SiteUtility.GetCurrentUser();

        // GET: Admin/Users
        public ActionResult Index()
        {
            string _drpFilter = ":All;";
            foreach (var e in Enum.GetNames(typeof(En_User_Status)))
                _drpFilter = _drpFilter + e.ToString() + ":" + e.ToString() + ";";

            ViewBag.lstUserStatus = _drpFilter.Remove(_drpFilter.Length - 1);

            return View();
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                return new UserBL().GetGridData(grid);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public PartialViewResult Manage(Guid? id)
        {
            User oUser = new User();
            ViewBag.lstJariCompanies = new JariCompanyBL().GetAllCompanies();
            ViewBag.lstUserStatus = Enum.GetNames(typeof(En_User_Status)).Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = s.ToString()
            });
            ViewBag.lstRoles = new RoleBL().GetByRoleGroup(En_Role_Group.Company.ToString());

            oUser.Password = AJSoftEntity.Classes.CommonFunction.EncryptData("123"); //Default Password 123

            if (id != Guid.Empty)
            {
                oUser = new UserBL().GetById((Guid)id);
            }
            return PartialView("_Manage", oUser);
        }

        public ActionResult Save(User oUser)
        {
            bool Add_Flag = new CommonBL().isNewEntry(oUser.UserId);
            try
            {
                oUser.ModifiedBy = oCrntUser.Email;
                oUser.ModifiedOn = DateTime.UtcNow;

                if (Add_Flag)
                    new UserBL().Create(oUser);
                else
                    new UserBL().Update(oUser);

                return Json(new { success = true, message = CommonMsg.Success(EntityNames.User, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.User, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
        }

        public JsonResult Delete(Guid? id)
        {
            try
            {
                bool Result = new UserBL().Delete((Guid)id);
                if (Result == true)
                    return Json(new { success = true, message = CommonMsg.Success(EntityNames.User, En_CRUD.Delete) });
                else
                    return Json(new { success = false, message = CommonMsg.DependancyError() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.User, En_CRUD.Delete) });
            }
        }

        public JsonResult checkUserEmail(string Email, Guid UserId)
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
    }
}