using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
  public  class CompanyPriceSettingBL
    {
        //User oUser = new CommonBL().CurrentUser;

        //#region Get Methods

        ////Get Clinic and Price Difference Grid Data
        ////public string GetClinicPricingGridData(GridSettings grid)
        ////{
        ////    try
        ////    {
        ////        using (var ctx = new DBAJEntities())
        ////        {
        ////            var query = ctx.vw_ClinicPricing.AsQueryable();
        ////            int count;
        ////            var data = query.GridCommonSettings(grid, out count);

        ////            var result = new
        ////            {
        ////                total = (int)Math.Ceiling((double)count / grid.PageSize),
        ////                page = grid.PageIndex,
        ////                records = count,
        ////                rows = (from c in data
        ////                        select new
        ////                        {
        ////                            ClinicId = c.ClinicId,
        ////                            ClinicName = c.ClinicName,
        ////                            Default = c.DefaultPrice,
        ////                            Different = c.Different,
        ////                            Action = c.ClinicId
        ////                        }).ToArray()
        ////            };

        ////            return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        ////Get Clinic Price Details for Clinic

        ////Get Clinic Price Details for VR

        ////public string GetClinicPriceSettingsGridData(GridSettings grid, Guid ClinicId)
        ////{
        ////    try
        ////    {

        ////        using (var ctx = new DBAJEntities())
        ////        {
        ////            var query = ctx.vw_ClinicPriceDetails.Where(c => c.ClinicId == ClinicId && c.CaseTypeParentId != null).AsQueryable();
        ////            int count;
        ////            var data = query.GridCommonSettings(grid, out count);

        ////            var result = new
        ////            {
        ////                total = (int)Math.Ceiling((double)count / grid.PageSize),
        ////                page = grid.PageIndex,
        ////                records = count,
        ////                rows = (from e in data
        ////                        select new
        ////                        {
        ////                            RowNumber = e.RowNumber,
        ////                            ClinicPriceSettingsId = e.ClinicPriceSettingsId,
        ////                            ClinicId = e.ClinicId,
        ////                            CaseTypeId = e.CaseTypeId,
        ////                            CaseTypeName = e.CaseTypeName,
        ////                            CaseTypeAbbr = e.CaseTypeAbbr,
        ////                            Price = e.Price.Value.ToString("C"),
        ////                            Description = e.Description,
        ////                            StartDate = e.StartDate,
        ////                            Mode = e.Mode == null ? "" : Enum.GetName(typeof(En_Price_Mode), e.Mode).Replace("_", " ").Replace("Percent", "%"),
        ////                            NoOfCases = e.NoOfCases,
        ////                            PercentOff = e.PercentOff,
        ////                            IsDefaultPrice = e.IsDefaultPrice,
        ////                            DisplayOrder = e.DisplayOrder,
        ////                            Action = e.ClinicPriceSettingsId
        ////                        }).ToArray()
        ////            };

        ////            return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //public CompanyPriceSetting GetById(int id)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.CompanyPriceSettings.Where(p => p.CompanyPriceSettingsId == id).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public vw_CompanyPriceDetails GetByTypeAndCCompanyId(Guid CompanyId, int YarnTypeId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.vw_CompanyPriceDetails.Where(c => c.CompanyId == CompanyId && c.YarnTypeId == YarnTypeId).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<vw_CompanyPriceDetails> GetVwByClinicId(Guid CompanyId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.vw_CompanyPriceDetails.Where(c => c.CompanyId == CompanyId).OrderBy(c => c.DisplayOrder).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //#endregion

        //#region CRUD Methods

        //public void Create(CompanyPriceSetting oCompanyPriceSetting)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            oCompanyPriceSetting.ModifiedBy = oUser.Email;
        //            oCompanyPriceSetting.ModifiedOn = DateTime.Now;

        //            ctx.CompanyPriceSettings.Add(oCompanyPriceSetting);
        //            ctx.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Update(CompanyPriceSetting oCompanyPriceSetting)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            oCompanyPriceSetting.ModifiedBy = oUser.Email;
        //            oCompanyPriceSetting.ModifiedOn = DateTime.UtcNow;
        //            ctx.Entry(oCompanyPriceSetting).State = EntityState.Modified;
        //            ctx.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            CompanyPriceSetting oCompanyPriceSetting = ctx.CompanyPriceSettings.Where(p => p.CompanyPriceSettingsId == id).FirstOrDefault();
        //            ctx.CompanyPriceSettings.Remove(oCompanyPriceSetting);
        //            ctx.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //#endregion
    }
}
