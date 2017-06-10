using AJSoftEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AJSoftEntity.Classes.EnumData;

namespace AJSoftBAL
{
    public class JariCompanyBL
    {
        #region Get Methods

        public JariCompany GetById(int CompanyId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.JariCompanies.Where(c => c.JariCompanyId == CompanyId).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<JariCompany> GetAllCompanies()
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.JariCompanies.OrderBy(c => c.JariCompanyName).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.JariCompanies.AsQueryable();
                    int count;
                    var data = query.GridCommonSettings(grid, out count);

                    var result = new
                    {
                        total = (int)Math.Ceiling((double)count / grid.PageSize),
                        page = grid.PageIndex,
                        records = count,
                        rows = (from c in data
                                select new
                                {
                                    c.JariCompanyId,
                                    c.JariCompanyName,
                                    c.CreateDate,
                                    c.ActivationEndDate,
                                    Action = c.JariCompanyId
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

        public void Create(JariCompany oJariCompany)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    oJariCompany.CreateDate = DateTime.UtcNow;

                    ctx.JariCompanies.Add(oJariCompany);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(JariCompany oJariCompany)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oJariCompany).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    JariCompany oJariCompany = ctx.JariCompanies.Where(p => p.JariCompanyId == id).FirstOrDefault();
                    ctx.JariCompanies.Remove(oJariCompany);
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
    }
}
