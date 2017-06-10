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
    public class EmbroideryFirmPriceSettingsBL
    {
        #region Get Methods

        public EmbroideryFirmPriceSetting GetById(int id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.EmbroideryFirmPriceSettings.Where(c => c.EmbroideryFirmPriceSettingsId == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetGridData(GridSettings grid, Guid EmbroideryFirmId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.vw_EmbroideryFirmPriceSettings.Where(c => c.EmbroideryFirmId == EmbroideryFirmId).OrderBy(c => c.DisplayOrder).ThenBy(x => x.ShadeName).AsQueryable();
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
                                    e.ShadeId,
                                    e.JariCompanyId,
                                    e.YarnTypeId,
                                    e.YarnTypeName,
                                    e.YarnColorCode,
                                    e.ShadeName,
                                    ShadeImage = (e.ShadeImage == null) ? "" : "data:image/png;base64, " + Convert.ToBase64String(e.ShadeImage, 0, e.ShadeImage.Length),
                                    Price = Convert.ToDecimal(e.Price).ToString("C"),
                                    e.Description,
                                    e.DisplayOrder,
                                    e.EmbroideryFirmPriceSettingsId,
                                    e.StartDate,
                                    e.IsDefaultPrice,
                                    e.RowNumber
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

        #region CRUD Operations
        public void Create(EmbroideryFirmPriceSetting oEmbroideryFirmPriceSetting)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.EmbroideryFirmPriceSettings.Add(oEmbroideryFirmPriceSetting);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(EmbroideryFirmPriceSetting oEmbroideryFirmPriceSetting)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oEmbroideryFirmPriceSetting).State = EntityState.Modified;
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
                    EmbroideryFirmPriceSetting oEmbroideryFirmPriceSetting = ctx.EmbroideryFirmPriceSettings.Where(p => p.EmbroideryFirmPriceSettingsId == id).FirstOrDefault();
                    ctx.EmbroideryFirmPriceSettings.Remove(oEmbroideryFirmPriceSetting);
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
