using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
   public class YarnTypeBL
    {
        #region Get Methods

        public YarnType GetById(int YarnTypeId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.YarnTypes.Where(p => p.YarnTypeId == YarnTypeId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<YarnType> GetAllYarnTypes()
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.YarnTypes.OrderBy(c => c.YarnTypeName).ToList();
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
