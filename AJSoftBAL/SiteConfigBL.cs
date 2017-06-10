using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
   public class SiteConfigBL
    {
        #region Get Methods

        public SiteConfig GetByName(string Name)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.SiteConfigs.Where(c => c.Name == Name).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SiteConfig GetById(int SiteConfigId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.SiteConfigs.Where(c => c.SiteConfigId == SiteConfigId).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        public void Create(SiteConfig oSiteConfig)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.SiteConfigs.Add(oSiteConfig);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(SiteConfig oSiteConfig)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oSiteConfig).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
