using AJSoftBAL;
using AJSoftEntity;
using AJSoftEntity.Classes;
using AJSoftWeb.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static AJSoftEntity.Classes.EnumData;

namespace AJSoftWeb.Controllers
{
    //[Authorize]
    public class ShadeCardsController : BaseController
    {
        User oUser = SiteUtility.GetCurrentUser();

        // GET: Admin/ShadeCards
        public ActionResult Index()
        {
            string _drpFilter = ":All;";
            foreach (var e in new YarnTypeBL().GetAllYarnTypes())
                _drpFilter = _drpFilter + e.YarnTypeName.ToString() + ":" + e.YarnTypeName.ToString() + ";";

            ViewBag.lstYarnTypes = _drpFilter.Remove(_drpFilter.Length - 1);

            return View();
        }

        //Load Shade cards in Grid Format for Display in Grid.
        public string GetGridData(GridSettings grid)
        {
            try
            {
                JariCompany oJariCompany = SiteUtility.GetCurrentJariCompany(oUser);

                return new ShadeCardBL().GetGridData(grid, oJariCompany.JariCompanyId);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Load Selected Shade Cards in Form
        public PartialViewResult ManageShadeCard(int id = 0)
        {
            ShadeCard oShadeCard = new ShadeCard();
            ViewBag.lstYarnTypes = new YarnTypeBL().GetAllYarnTypes();
            var _JariCompany = SiteUtility.GetCurrentJariCompany(oUser);

            if (id > 0)
            {
                oShadeCard = new ShadeCardBL().GetById(id);
            }
            oShadeCard.JariCompanyId = _JariCompany.JariCompanyId;

            return PartialView("_ManageShadeCard", oShadeCard);
        }

        //Create or Update Shade cards 
        [HttpPost]
        public ActionResult SaveShadeCard()
        {
            ShadeCard oShadeCard = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ShadeCard>(Request["objShadeCard"]);

            bool Add_Flag = new CommonBL().isNewEntry(oShadeCard.ShadeId);
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                        oShadeCard.ShadeImage = SiteUtility.ResizeImage(binaryReader.ReadBytes(Request.Files[0].ContentLength), 200, 50);
                }
                else if (oShadeCard.ShadeId != 0)
                    oShadeCard.ShadeImage = new ShadeCardBL().GetById(oShadeCard.ShadeId).ShadeImage;


                oShadeCard.ModifiedBy = oUser.Email;
                oShadeCard.ModifiedOn = DateTime.UtcNow;

                if (Add_Flag)
                    new ShadeCardBL().Create(oShadeCard);
                else
                    new ShadeCardBL().Update(oShadeCard);

                return Json(new { success = true, message = CommonMsg.Success(EntityNames.Shade, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.Shade, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
        }

        //Delete Selected Shade
        public JsonResult DeleteShade(int id = 0)
        {
            try
            {
                bool Result = new ShadeCardBL().Delete(id);
                if (Result == true)
                    return Json(new { success = true, message = CommonMsg.Success(EntityNames.Shade, En_CRUD.Delete) });
                else
                    return Json(new { success = false, message = CommonMsg.DependancyError() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.Shade, En_CRUD.Delete) });
            }
        }

        //Move Selected Shade
        public JsonResult MoveShade(int selectedId, int affectedId)
        {
            try
            {
                //selected shade
                ShadeCard oSelectedShade = new ShadeCardBL().GetById(selectedId);
                oSelectedShade.ModifiedBy = oUser.Email;
                oSelectedShade.ModifiedOn = DateTime.UtcNow;
                
                //affected shade
                ShadeCard oAffectedShade = new ShadeCardBL().GetById(affectedId);
                oAffectedShade.ModifiedBy = oUser.Email;
                oAffectedShade.ModifiedOn = DateTime.UtcNow;

                bool Result = new ShadeCardBL().MoveShades(oSelectedShade, oAffectedShade);
                if (Result == true)
                    return Json(new { success = true, message = "Shade have been Re-Ordered successfully." });
                else
                    return Json(new { success = false, message = CommonMsg.Error() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Error() });
            }
        }
    }
}