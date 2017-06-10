using AJSoftBAL;
using AJSoftEntity;
using AJSoftEntity.Classes;
using AJSoftWeb.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AJSoftEntity.Classes.EnumData;

namespace AJSoftWeb.Controllers
{
    public class EmbroideryFirmController : BaseController
    {
        User oUser = SiteUtility.GetCurrentUser();
        JariCompany oJariCompany = SiteUtility.GetCurrentJariCompany(SiteUtility.GetCurrentUser());

        // GET: EmbroideryFirm
        #region Emb Firm Details
        public ActionResult Index()
        {
            return View();
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                return new EmbroideryFirmBL().GetGridData(grid);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public ActionResult ViewDetail(Guid? id = null, string tab = "")
        {
            ViewBag.tab = tab;
            ViewBag.EmbroideryFirmId = (id != null) ? id.Value.ToString().ToUpper() : "";
            ViewBag.EmbroideryFirmName = "";
            if (id != null)
                ViewBag.EmbroideryFirmName = new EmbroideryFirmBL().GetById((Guid)id).EmbroideryFirmName;

            return View();
        }

        public ActionResult Manage(Guid? id)
        {
            List<SelectListItem> lstterms = new List<SelectListItem>();
            foreach (var e in Enum.GetValues(typeof(En_Billing_Terms)))
            {
                SelectListItem obj = new SelectListItem();
                obj.Text = e.ToString().Replace("_", "");
                obj.Value = e.ToString().Replace("_", "");
                lstterms.Add(obj);
            }
            ViewBag.lstBillingTerms = lstterms;

            EmbFirmLocationVM oEmbFirmLocationVM = new EmbFirmLocationVM();
            oEmbFirmLocationVM.City = "Surat"; //Default City
            oEmbFirmLocationVM.JariCompanyId = oJariCompany.JariCompanyId;

            if (id != null)
            {
                EmbroideryFirm oEmbroideryFirm = new EmbroideryFirmBL().GetById(id.Value);
                oEmbFirmLocationVM.EmbroideryFirmId = oEmbroideryFirm.EmbroideryFirmId;
                oEmbFirmLocationVM.JariCompanyId = oEmbroideryFirm.JariCompanyId;
                oEmbFirmLocationVM.IsActive = oEmbroideryFirm.IsActive;
                oEmbFirmLocationVM.EmbroideryFirmName = oEmbroideryFirm.EmbroideryFirmName;

                //oEmbFirmLocationVM.LocationContact ="";
                //oEmbFirmLocationVM.Address1 = "";
                //oEmbFirmLocationVM.Address2 = "";
                //oEmbFirmLocationVM.City = "";
                //oEmbFirmLocationVM.IsPrimaryLocation = true;
                //oEmbFirmLocationVM.Status = true;
                //oEmbFirmLocationVM.Phone = "";
                //oEmbFirmLocationVM.Email = "";
                //oEmbFirmLocationVM.BillingTerms = 1;
                //oEmbFirmLocationVM.Latitude = 100;
                //oEmbFirmLocationVM.Longitude = 100;
            }

            return View(oEmbFirmLocationVM);
        }

        public ActionResult Save(EmbFirmLocationVM oEmbFirmLocationVM)
        {
            bool Add_Flag = new CommonBL().isNewEntry(oEmbFirmLocationVM.EmbroideryFirmId);
            try
            {
                if (Add_Flag)
                {
                    EmbroideryFirmLocation oEmbroideryFirmLocation = new EmbroideryFirmLocation();
                    oEmbroideryFirmLocation.EmbroideryFirmLocationId = Guid.NewGuid();
                    oEmbroideryFirmLocation.JariCompanyId = oJariCompany.JariCompanyId;
                    oEmbroideryFirmLocation.ContactPerson = oEmbFirmLocationVM.ContactPerson;
                    oEmbroideryFirmLocation.Address1 = oEmbFirmLocationVM.Address1;
                    oEmbroideryFirmLocation.Address2 = oEmbFirmLocationVM.Address2;
                    oEmbroideryFirmLocation.City = oEmbFirmLocationVM.City;
                    oEmbroideryFirmLocation.IsPrimaryLocation = true;
                    oEmbroideryFirmLocation.Status = true;
                    oEmbroideryFirmLocation.Phone = oEmbFirmLocationVM.Phone;
                    oEmbroideryFirmLocation.Email = oEmbFirmLocationVM.Email;
                    oEmbroideryFirmLocation.BillingTerms = oEmbFirmLocationVM.BillingTerms;
                    oEmbroideryFirmLocation.ModifiedBy = oUser.Email;
                    oEmbroideryFirmLocation.ModifiedOn = DateTime.UtcNow;

                    EmbroideryFirm oEmbroideryFirm = new EmbroideryFirm();
                    oEmbroideryFirm.EmbroideryFirmId = Guid.NewGuid();
                    oEmbroideryFirm.JariCompanyId = oJariCompany.JariCompanyId;
                    oEmbroideryFirm.IsActive = oEmbFirmLocationVM.IsActive;
                    oEmbroideryFirm.EmbroideryFirmName = oEmbFirmLocationVM.EmbroideryFirmName;
                    oEmbroideryFirm.ModifiedBy = oUser.Email;
                    oEmbroideryFirm.ModifiedOn = DateTime.UtcNow;

                    //Add location with EmbroideryFirm object
                    oEmbroideryFirm.EmbroideryFirmLocations.Add(oEmbroideryFirmLocation);

                    new EmbroideryFirmBL().Create(oEmbroideryFirm);
                }
                else
                {
                    EmbroideryFirm oEmbroideryFirm = new EmbroideryFirm();
                    oEmbroideryFirm.EmbroideryFirmId = oEmbFirmLocationVM.EmbroideryFirmId;
                    oEmbroideryFirm.JariCompanyId = oJariCompany.JariCompanyId;
                    oEmbroideryFirm.IsActive = oEmbFirmLocationVM.IsActive;
                    oEmbroideryFirm.EmbroideryFirmName = oEmbFirmLocationVM.EmbroideryFirmName;
                    oEmbroideryFirm.ModifiedBy = oUser.Email;
                    oEmbroideryFirm.ModifiedOn = DateTime.UtcNow;

                    new EmbroideryFirmBL().Update(oEmbroideryFirm);
                }

                TempData["successmsg"] = CommonMsg.Success(EntityNames.EmbroideryFirm, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update);
                //return RedirectToAction("ViewDetail", new { id = oEmbFirmLocationVM.EmbroideryFirmId.ToString().ToUpper(), tab = "EmbroideryFirmDetail" });
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.EmbroideryFirm, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
        }

        public JsonResult Delete(Guid? id)
        {
            try
            {
                bool Result = new EmbroideryFirmBL().Delete((Guid)id);
                if (Result == true)
                    return Json(new { success = true, message = CommonMsg.Success(EntityNames.EmbroideryFirm, En_CRUD.Delete) });
                else
                    return Json(new { success = false, message = CommonMsg.DependancyError() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.EmbroideryFirm, En_CRUD.Delete) });
            }
        }

        public JsonResult checkEmbroideryFirmName(string EmbroideryFirmName, Guid EmbroideryFirmId, int JariCompanyId)
        {
            try
            {
                var obj = new EmbroideryFirmBL().checkEmbroideryFirmName(EmbroideryFirmName.Trim(), EmbroideryFirmId, JariCompanyId);
                if (obj == null)
                    return Json(true);
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        #endregion

        #region Locations
        public ActionResult EmbroideryFirmLocations(Guid id)
        {
            ViewBag.EmbroideryFirmId = id;
            return PartialView("_EmbroideryFirmLocations");
        }

        public string GetEmbroideryFirmLocationGridData(GridSettings grid, Guid EmbroideryFirmId)
        {
            try
            {
                return new EmbroideryFirmLocationsBL().GetGridData(grid, EmbroideryFirmId);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public ActionResult ManageEmbroideryFirmLocation(Guid? EmbroideryFirmLocationId, Guid EmbroideryFirmId)
        {
            EmbroideryFirmLocation oEmbroideryFirmLocation = new EmbroideryFirmLocation();
            List<SelectListItem> lstterms = new List<SelectListItem>();
            foreach (var e in Enum.GetValues(typeof(En_Billing_Terms)))
            {
                SelectListItem obj = new SelectListItem();
                obj.Text = e.ToString().Replace("_", "");
                obj.Value = e.ToString().Replace("_", "");
                lstterms.Add(obj);
            }
            ViewBag.lstBillingTerms = lstterms;

            if (EmbroideryFirmLocationId != null)
                oEmbroideryFirmLocation = new EmbroideryFirmLocationsBL().GetById(EmbroideryFirmLocationId.Value);
            else
            {
                oEmbroideryFirmLocation.EmbroideryFirmId = EmbroideryFirmId;
                oEmbroideryFirmLocation.JariCompanyId = oJariCompany.JariCompanyId;
                oEmbroideryFirmLocation.City = "Surat";
            }

            return PartialView("_ManageEmbroideryFirmLocations", oEmbroideryFirmLocation);
        }

        public ActionResult SaveEmbroideryFirmLocation(EmbroideryFirmLocation oEmbroideryFirmLocation)
        {
            try
            {
                bool Add_Flag = new CommonBL().isNewEntry(oEmbroideryFirmLocation.EmbroideryFirmLocationId);

                oEmbroideryFirmLocation.ModifiedBy = oUser.Email;
                oEmbroideryFirmLocation.ModifiedOn = DateTime.UtcNow;

                if (Add_Flag)
                {
                    oEmbroideryFirmLocation.EmbroideryFirmLocationId = Guid.NewGuid();
                    new EmbroideryFirmLocationsBL().Create(oEmbroideryFirmLocation);
                }
                else
                    new EmbroideryFirmLocationsBL().Update(oEmbroideryFirmLocation);

                return Json(new { success = true, message = CommonMsg.Success(EntityNames.EmbroideryFirmLocation, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Error() });
            }
        }

        public JsonResult DeleteEmbroideryFirmLocattion(Guid id)
        {
            try
            {
                if (!new EmbroideryFirmLocationsBL().IsPrimaryLocation(id))
                {
                    bool Result = new EmbroideryFirmLocationsBL().Delete(id);
                    if (Result == true)
                        return Json(new { success = true, message = CommonMsg.Success(EntityNames.EmbroideryFirmLocation, En_CRUD.Delete) });
                    else
                        return Json(new { success = false, message = CommonMsg.DependancyError() });
                }
                else
                {
                    return Json(new { success = false, message = "you can't delete Primary Location, Please set other Location as Primary Location first." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.EmbroideryFirmLocation, En_CRUD.Delete) });
            }
        }

        public ActionResult SetDefaultPrimaryLocation(Guid id)
        {
            try
            {
                EmbroideryFirmLocation oEmbroideryFirmLocation = new EmbroideryFirmLocationsBL().GetById(id);
                oEmbroideryFirmLocation.IsPrimaryLocation = true;

                new EmbroideryFirmLocationsBL().SetDefaultPrimaryLocation(oEmbroideryFirmLocation);
                return Json(new { success = true, message = "Primary Location has been settled successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Error() });
            }
        }
        #endregion

        #region Pricing
        public ActionResult EmbroideryFirmPricing(Guid id)
        {
            ViewBag.EmbroideryFirmId = id;
            string _drpFilter = ":All;";
            foreach (var e in new YarnTypeBL().GetAllYarnTypes())
                _drpFilter = _drpFilter + e.YarnTypeName.ToString() + ":" + e.YarnTypeName.ToString() + ";";

            ViewBag.lstYarnTypes = _drpFilter.Remove(_drpFilter.Length - 1);

            return PartialView("_EmbroideryFirmPricing");
        }

        public string GetEmbroideryFirmPricingGridData(GridSettings grid, Guid EmbroideryFirmId)
        {
            try
            {
                return new EmbroideryFirmPriceSettingsBL().GetGridData(grid, EmbroideryFirmId);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public ActionResult ManageEmbroideryFirmPricing(int? EmbroideryFirmPriceSettingsId, Guid? EmbroideryFirmId, int? ShadeId)
        {
            EmbroideryFirmPriceSetting oEmbroideryFirmPriceSetting = new EmbroideryFirmPriceSetting();
            oEmbroideryFirmPriceSetting.EmbroideryFirmId = EmbroideryFirmId.Value;
            oEmbroideryFirmPriceSetting.JariCompanyId = oJariCompany.JariCompanyId;

            vw_ShadeCards oShadeCard = new vw_ShadeCards();
            ViewBag.oShadeCard = oShadeCard;

            if (EmbroideryFirmPriceSettingsId != null && EmbroideryFirmPriceSettingsId > 0)
            {
                oEmbroideryFirmPriceSetting = new EmbroideryFirmPriceSettingsBL().GetById(EmbroideryFirmPriceSettingsId.Value);
                oShadeCard = new ShadeCardBL().GetShadeCardsFromVWByShadeId(ShadeId.Value);
                ViewBag.oShadeCard = oShadeCard;
            }
            else if ((EmbroideryFirmPriceSettingsId == null || EmbroideryFirmPriceSettingsId == 0) && ShadeId.Value > 0)
            {
                oShadeCard = new ShadeCardBL().GetShadeCardsFromVWByShadeId(ShadeId.Value);
                oEmbroideryFirmPriceSetting.ShadeId = oShadeCard.ShadeId;
                oEmbroideryFirmPriceSetting.Price = oShadeCard.Price;
                oEmbroideryFirmPriceSetting.StartDate = oShadeCard.StartDate;
                ViewBag.oShadeCard = oShadeCard;
            }

            return PartialView("_ManageEmbroideryFirmPricing", oEmbroideryFirmPriceSetting);
        }

        public ActionResult SaveEmbroideryFirmPricing(EmbroideryFirmPriceSetting oEmbroideryFirmPriceSetting)
        {
            try
            {
                bool Add_Flag = new CommonBL().isNewEntry(oEmbroideryFirmPriceSetting.EmbroideryFirmPriceSettingsId);

                oEmbroideryFirmPriceSetting.ModifiedBy = oUser.Email;
                oEmbroideryFirmPriceSetting.ModifiedOn = DateTime.UtcNow;

                if (Add_Flag)
                {
                    new EmbroideryFirmPriceSettingsBL().Create(oEmbroideryFirmPriceSetting);
                }
                else
                    new EmbroideryFirmPriceSettingsBL().Update(oEmbroideryFirmPriceSetting);

                return Json(new { success = true, message = CommonMsg.Success(EntityNames.EmbroideryFirmPricing, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Error() });
            }
        }

        public JsonResult DeleteEmbroideryFirmPricing(int id)
        {
            try
            {
                bool Result = new EmbroideryFirmPriceSettingsBL().Delete(id);
                if (Result == true)
                    return Json(new { success = true, message = CommonMsg.Success(EntityNames.EmbroideryFirmPricing, En_CRUD.Delete) });
                else
                    return Json(new { success = false, message = CommonMsg.DependancyError() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.EmbroideryFirmPricing, En_CRUD.Delete) });
            }
        }

        #endregion
    }
}