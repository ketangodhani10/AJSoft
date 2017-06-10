using AJSoftEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class EmbroideryFirmLocationsBL
    {
        #region Get Methods

        public EmbroideryFirmLocation GetById(Guid Id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.EmbroideryFirmLocations.Where(p => p.EmbroideryFirmLocationId == Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetGridData(GridSettings grid, Guid EmbroideryFirmId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.EmbroideryFirmLocations.Where(c => c.EmbroideryFirmId == EmbroideryFirmId).AsQueryable();
                    int count;
                    var data = query.GridCommonSettings(grid, out count);

                    var result = new
                    {
                        total = (int)Math.Ceiling((double)count / grid.PageSize),
                        page = grid.PageIndex,
                        records = count,
                        rows = (from e in data
                                select new
                                {
                                    e.EmbroideryFirmLocationId,
                                    e.EmbroideryFirmId,
                                    e.JariCompanyId,
                                    e.ContactPerson,
                                    e.Address1,
                                    e.Address2,
                                    e.City,
                                    FullAddress = (string.IsNullOrEmpty(e.Address1) ? "" : e.Address1 + " ") + (string.IsNullOrEmpty(e.Address2) ? "" : e.Address2 + ", ") + (string.IsNullOrEmpty(e.City) ? "" : e.City),
                                    e.IsPrimaryLocation,
                                    e.Status,
                                    e.Phone,
                                    e.Email,
                                    e.BillingTerms,
                                    e.Latitude,
                                    e.Longitude,
                                    e.ModifiedBy,
                                    e.ModifiedOn,
                                    Action = e.EmbroideryFirmLocationId
                                }).ToArray()
                    };

                    return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsPrimaryLocation(Guid Id)
        {
            using (var ctx = new DBAJEntities())
            {
                EmbroideryFirmLocation oEmbroideryFirmLocation = ctx.EmbroideryFirmLocations.Where(c => c.EmbroideryFirmLocationId == Id && c.IsPrimaryLocation == true).FirstOrDefault();

                if (oEmbroideryFirmLocation != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region CRUD Methods

        public void Create(EmbroideryFirmLocation oEmbroideryFirmLocation)
        {
            try
            {
                if (oEmbroideryFirmLocation.IsPrimaryLocation == true)
                {
                    ResetPrimaryLocation(oEmbroideryFirmLocation.EmbroideryFirmId);
                }
                using (var ctx = new DBAJEntities())
                {
                    ctx.EmbroideryFirmLocations.Add(oEmbroideryFirmLocation);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(EmbroideryFirmLocation oEmbroideryFirmLocation)
        {
            try
            {
                if (oEmbroideryFirmLocation.IsPrimaryLocation == true)
                {
                    ResetPrimaryLocation(oEmbroideryFirmLocation.EmbroideryFirmId);
                }
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oEmbroideryFirmLocation).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    EmbroideryFirmLocation oEmbroideryFirmLocation = ctx.EmbroideryFirmLocations.Where(p => p.EmbroideryFirmLocationId == id).FirstOrDefault();
                    ctx.EmbroideryFirmLocations.Remove(oEmbroideryFirmLocation);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ResetPrimaryLocation(Guid EmbroideryFirmId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    List<EmbroideryFirmLocation> lstLocations = ctx.EmbroideryFirmLocations.Where(t => t.IsPrimaryLocation == true && t.EmbroideryFirmId == EmbroideryFirmId).ToList();
                    if (lstLocations.Count > 0 && lstLocations != null)
                    {
                        lstLocations.ForEach(a => a.IsPrimaryLocation = false);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetDefaultPrimaryLocation(EmbroideryFirmLocation oEmbroideryFirmLocation)
        {
            try
            {
                if (oEmbroideryFirmLocation.IsPrimaryLocation == true)
                {
                    ResetPrimaryLocation(oEmbroideryFirmLocation.EmbroideryFirmId);
                }
                using (var ctx = new DBAJEntities())
                {

                    ctx.Entry(oEmbroideryFirmLocation).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
