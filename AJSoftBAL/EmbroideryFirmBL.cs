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
    public class EmbroideryFirmBL
    {
        #region Get Methods

        public EmbroideryFirm GetById(Guid Id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.EmbroideryFirms.Where(p => p.EmbroideryFirmId == Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.vw_EmbroideryFirms.AsQueryable();
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
                                    e.EmbroideryFirmId,
                                    e.JariCompanyId,
                                    e.IsActive,
                                    e.EmbroideryFirmName,
                                    e.ModifiedBy,
                                    e.ModifiedOn,
                                    e.JariCompanyName,
                                    e.TotalLocations,
                                    e.EmbroideryFirmLocationId,
                                    e.ContactPerson,
                                    e.Address1,
                                    e.Address2,
                                    e.City,
                                    e.FullAddress,
                                    e.Phone,
                                    e.BillingTerms,
                                    Action = e.EmbroideryFirmId
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

        #endregion

        #region CRUD Methods

        public void Create(EmbroideryFirm oEmbroideryFirm)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.EmbroideryFirms.Add(oEmbroideryFirm);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(EmbroideryFirm oEmbroideryFirm)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oEmbroideryFirm).State = EntityState.Modified;
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
                    EmbroideryFirm oEmbroideryFirm = ctx.EmbroideryFirms.Where(p => p.EmbroideryFirmId == id).FirstOrDefault();
                    ctx.EmbroideryFirms.Remove(oEmbroideryFirm);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Check Validation

        public string checkEmbroideryFirmName(string EmbroideryFirmName, Guid EmbroideryFirmId, int JariCompanyId)
        {
            string str = null;
            using (var ctx = new DBAJEntities())
            {
                var oEFirm = ctx.EmbroideryFirms.Where(p => p.EmbroideryFirmName == EmbroideryFirmName && p.EmbroideryFirmId != EmbroideryFirmId && p.JariCompanyId == JariCompanyId).FirstOrDefault();

                if (oEFirm == null)
                    return null;
                else
                {
                    if (oEFirm != null)
                    {
                        str = oEFirm.EmbroideryFirmName;
                    }
                    return str;
                }
            }
        }

        #endregion
    }
}
