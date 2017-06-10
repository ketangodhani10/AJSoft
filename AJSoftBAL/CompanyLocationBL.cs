using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class CompanyLocationBL
    {
        //User oUser = new CommonBL().CurrentUser;

        //#region Get Methods

        //public List<CompanyLocation> GetAllLocations()
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.CompanyLocations.ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public CompanyLocation GetById(Guid LocationId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.CompanyLocations.Where(c => c.CompanyLocationId == LocationId).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        ////public string GetGridData(GridSettings grid, Guid ClinicId)
        ////{
        ////    try
        ////    {
        ////        using (var ctx = new DBAJEntities())
        ////        {
        ////            var query = ctx.vw_ClinicLocations.Where(l => l.ClinicId == ClinicId).AsQueryable();
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
        ////                            ClinicLocationId = c.ClinicLocationId,
        ////                            ClinicName = c.ClinicName,
        ////                            LocationName = c.LocationName,
        ////                            Address1 = c.Address1,
        ////                            Address2 = c.Address2,
        ////                            LocationCity = c.City,
        ////                            LocationState = c.State,
        ////                            LocationCountry = c.Country,
        ////                            LocationZip = c.Zip,
        ////                            IsPrimaryLocation = c.IsPrimaryLocation,
        ////                            Phone = c.Phone,
        ////                            Fax = c.Fax,
        ////                            Email = c.Email,
        ////                            Action = c.ClinicLocationId
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

        //public List<CompanyLocation> GetByClinicId(Guid CompanyId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.CompanyLocations.Where(c => c.CompanyId == CompanyId).OrderBy(c => c.LocationName).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public bool IsPrimaryLocation(Guid Id)
        //{
        //    using (var ctx = new DBAJEntities())
        //    {
        //        CompanyLocation oCompanyLocation = ctx.CompanyLocations.Where(c => c.CompanyLocationId == Id && c.IsPrimaryLocation == true).FirstOrDefault();

        //        if (oCompanyLocation != null)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        //#endregion

        //#region CRUD Operations

        //public CompanyLocation Create(CompanyLocation oCompanyLocation)
        //{
        //    try
        //    {
        //        if (oCompanyLocation.IsPrimaryLocation == true)
        //        {
        //            ResetPrimaryLocation(oCompanyLocation.CompanyLocationId);
        //        }
        //        using (var ctx = new DBAJEntities())
        //        {
        //            oCompanyLocation.CompanyLocationId = Guid.NewGuid();
        //            ctx.CompanyLocations.Add(oCompanyLocation);
        //            ctx.SaveChanges();
        //        }
        //        return oCompanyLocation;
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Update(CompanyLocation oCompanyLocation)
        //{
        //    try
        //    {
        //        if (oCompanyLocation.IsPrimaryLocation == true)
        //        {
        //            ResetPrimaryLocation(oCompanyLocation.CompanyId);
        //        }
        //        using (var ctx = new DBAJEntities())
        //        {
        //            oCompanyLocation.ModifiedBy = oUser.Email;
        //            oCompanyLocation.ModifiedOn = DateTime.Now;
        //            ctx.Entry(oCompanyLocation).State = EntityState.Modified;
        //            ctx.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(Guid id)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            CompanyLocation oLocation = ctx.CompanyLocations.Where(p => p.CompanyLocationId == id).FirstOrDefault();
        //            ctx.CompanyLocations.Remove(oLocation);
        //            ctx.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public void ResetPrimaryLocation(Guid ComanyId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            List<CompanyLocation> lstLocations = ctx.CompanyLocations.Where(t => t.IsPrimaryLocation == true && t.CompanyId == ComanyId).ToList();
        //            if (lstLocations.Count > 0 && lstLocations != null)
        //            {
        //                lstLocations.ForEach(a => a.IsPrimaryLocation = false);
        //                ctx.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}
