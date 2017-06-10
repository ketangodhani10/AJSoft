using AJSoftEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class YarnTypesBL
    {
        #region Get Methods
        public string GetGridData(GridSettings grid)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.YarnTypes.AsQueryable();
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
                                    e.YarnTypeId,
                                    e.YarnTypeName,
                                    e.YarnColorCode,
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

    }
}
