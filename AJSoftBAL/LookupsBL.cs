using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class LookupsBL
    {
        #region Get Methods

        public List<Lookup> GetAllLookups()
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.Lookups.OrderBy(c => c.LookupId).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Lookup GetById(int LookupId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.Lookups.Where(c => c.LookupId == LookupId).FirstOrDefault();
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
