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
    public class ShadeCardBL
    {
        #region Get Methods

        public string GetGridData(GridSettings grid, int JariCompanyId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.vw_ShadeCards.Where(c => c.JariCompanyId == JariCompanyId).OrderBy(c => c.DisplayOrder).ThenBy(x => x.ShadeName).AsQueryable();
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
                                    e.ShadeId,
                                    e.YarnTypeId,
                                    e.JariCompanyId,
                                    e.ShadeName,
                                    ShadeImage = (e.ShadeImage == null) ? "" : "data:image/png;base64, " + Convert.ToBase64String(e.ShadeImage, 0, e.ShadeImage.Length),
                                    Price = Convert.ToDecimal(e.Price).ToString("C"),
                                    e.Description,
                                    e.StartDate,
                                    e.ModifiedBy,
                                    e.ModifiedOn,
                                    e.DisplayOrder,
                                    e.YarnTypeName,
                                    e.YarnColorCode,
                                    e.JariCompanyName,
                                    Action = e.ShadeId
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

        public ShadeCard GetById(int ShadeId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.ShadeCards.Where(p => p.ShadeId == ShadeId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<vw_ShadeCards> GetAllShadeCardsFromView()
        {
            using (var ctx = new DBAJEntities())
            {
                return ctx.vw_ShadeCards.OrderBy(c => c.DisplayOrder).ToList();
            }
        }

        public vw_ShadeCards GetShadeCardsFromVWByShadeId(int ShadeId)
        {
            using (var ctx = new DBAJEntities())
            {
                return ctx.vw_ShadeCards.Where(c => c.ShadeId == ShadeId).FirstOrDefault();
            }
        }

        public List<ShadeCard> GetAllShadeCards()
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.ShadeCards.OrderBy(c => c.DisplayOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CRUD Methods

        public void Create(ShadeCard oShadeCard)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    if (oShadeCard.DisplayOrder == null)
                        oShadeCard.DisplayOrder = (ctx.ShadeCards.OrderByDescending(c => c.ShadeId).FirstOrDefault().ShadeId) + 1;

                    ctx.ShadeCards.Add(oShadeCard);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(ShadeCard oShadeCard)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    if (oShadeCard.DisplayOrder == null)
                        oShadeCard.DisplayOrder = (ctx.ShadeCards.OrderByDescending(c => c.ShadeId).FirstOrDefault().ShadeId) + 1;

                    ctx.Entry(oShadeCard).State = EntityState.Modified;
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
                    ShadeCard oShadeCard = ctx.ShadeCards.Where(p => p.ShadeId == id).FirstOrDefault();
                    ctx.ShadeCards.Remove(oShadeCard);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MoveShades(ShadeCard oSelectedShade, ShadeCard oAffectedShade)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    int? SelectedOrderId = oSelectedShade.DisplayOrder;
                    int? affectOrderId = oAffectedShade.DisplayOrder;

                    //selected shade
                    oSelectedShade.DisplayOrder = affectOrderId;
                    ctx.Entry(oSelectedShade).State = EntityState.Modified;

                    //affected shade
                    oAffectedShade.DisplayOrder = SelectedOrderId;
                    ctx.Entry(oAffectedShade).State = EntityState.Modified;

                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
