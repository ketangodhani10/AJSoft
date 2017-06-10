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
    public class JariCompaniesController : AdminBaseController
    {
        User oUser = SiteUtility.GetCurrentUser();

        // GET: Admin/JariCompanies
        public ActionResult Index()
        {
            return View();
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                return new JariCompanyBL().GetGridData(grid);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public PartialViewResult Manage(int id = 0)
        {
            JariCompany oJariCompany = new JariCompany();
            if (id > 0)
            {
                oJariCompany = new JariCompanyBL().GetById(id);
            }

            return PartialView("_Manage", oJariCompany);
        }

        public ActionResult Save(JariCompany oJariCompany)
        {
            bool Add_Flag = new CommonBL().isNewEntry(oJariCompany.JariCompanyId);
            try
            {
                if (Add_Flag)
                    new JariCompanyBL().Create(oJariCompany);
                else
                    new JariCompanyBL().Update(oJariCompany);

                return Json(new { success = true, message = CommonMsg.Success(EntityNames.Company, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.Company, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
        }

        public JsonResult Delete(int id = 0)
        {
            try
            {
                bool Result = new JariCompanyBL().Delete(id);
                if (Result == true)
                    return Json(new { success = true, message = CommonMsg.Success(EntityNames.Company, En_CRUD.Delete) });
                else
                    return Json(new { success = false, message = CommonMsg.DependancyError() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.Company, En_CRUD.Delete) });
            }
        }
    }
}