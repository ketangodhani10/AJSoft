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
    public class UserBL
    {
        #region Get Methods

        public User GetByEmail(string EmaiId)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.Users.Where(p => p.Email == EmaiId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GetGridData(GridSettings grid, int JariCompanyId = 0)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            var query = ctx.vw_Users.AsQueryable();
        //            if (JariCompanyId != 0)
        //            {
        //                query = query.Where(k => k.JariCompanyId == JariCompanyId);
        //            }
        //            else
        //            {
        //                string RoleGroupName = Convert.ToString(En_Role_Group.AJ);
        //                List<int> lstRole = ctx.Roles.Where(r => r.RoleGroup == RoleGroupName).Select(r => r.RoleId).ToList();
        //                query = query.Where(u => lstRole.Contains(u.RoleId));
        //            }

        //            int count;
        //            var data = query.GridCommonSettings(grid, out count);

        //            var result = new
        //            {
        //                total = (int)Math.Ceiling((double)count / grid.PageSize),
        //                page = grid.PageIndex,
        //                records = count,
        //                rows = (from u in data
        //                        select new
        //                        {
        //                            u.UserId,
        //                            u.RoleId,
        //                            u.JariCompanyId,
        //                            u.FirstName,
        //                            u.LastName,
        //                            u.Email,
        //                            u.Mobile,
        //                            u.Phone,
        //                            u.Password,
        //                            u.Status,
        //                            u.ModifiedBy,
        //                            u.ModifiedOn,
        //                            u.JariCompanyName,
        //                            u.CreateDate,
        //                            u.ActivationEndDate,
        //                            u.RoleGroup,
        //                            u.RoleName,
        //                            Action = u.UserId
        //                        }).ToArray()
        //            };

        //            return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string GetGridData(GridSettings grid)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    var query = ctx.vw_Users.AsQueryable();

                    string RoleGroupName = Convert.ToString(En_Role_Group.Company);
                    List<int> lstRole = ctx.Roles.Where(r => r.RoleGroup == RoleGroupName).Select(r => r.RoleId).ToList();
                    query = query.Where(u => lstRole.Contains(u.RoleId));

                    int count;
                    var data = query.GridCommonSettings(grid, out count);

                    var result = new
                    {
                        total = (int)Math.Ceiling((double)count / grid.PageSize),
                        page = grid.PageIndex,
                        records = count,
                        rows = (from u in data
                                select new
                                {
                                    u.UserId,
                                    u.RoleId,
                                    u.JariCompanyId,
                                    u.FirstName,
                                    u.LastName,
                                    u.Email,
                                    u.Mobile,
                                    u.Phone,
                                    u.Password,
                                    u.Status,
                                    u.ModifiedBy,
                                    u.ModifiedOn,
                                    u.JariCompanyName,
                                    u.CreateDate,
                                    u.ActivationEndDate,
                                    u.RoleGroup,
                                    u.RoleName,
                                    Action = u.UserId
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

        public User GetById(Guid id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    User oUser = ctx.Users.Where(p => p.UserId == id).FirstOrDefault();
                    return oUser;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var ctx = new DBAJEntities())
            {
                return ctx.Users.ToList();
            }
        }

        public List<vw_Users> GetAllVWUsers()
        {
            using (var ctx = new DBAJEntities())
            {
                return ctx.vw_Users.ToList();
            }
        }

        #endregion

        #region Check Validation

        //Check for Validate a User.
        public vw_Users ValidateUser(string Email, string password)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    password = AJSoftEntity.Classes.CommonFunction.EncryptData(password);
                    vw_Users oUser = ctx.vw_Users.Where(p => p.Email == Email && p.Password == password).FirstOrDefault();

                    if (oUser != null)
                        return oUser;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CheckEmail(string email, Guid userId)
        {
            string strEmail = null;
            using (var ctx = new DBAJEntities())
            {
                var varUser = ctx.Users.Where(p => p.Email == email && p.UserId != userId).FirstOrDefault();

                if (varUser == null)
                    return null;
                else
                {
                    if (varUser != null)
                    {
                        strEmail = varUser.Email;
                    }
                    return strEmail;
                }
            }
        }

        //Check Current Password (if Matched)
        public bool IsCurrentPasswordMatch(string OldPassword, User oUser)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    string OldPasswordHash = AJSoftEntity.Classes.CommonFunction.EncryptData(OldPassword);
                    if (ctx.Users.Where(c => c.Password == OldPasswordHash && c.Email == oUser.Email).FirstOrDefault() != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangePassword(string password, User oUser)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Database.ExecuteSqlCommand("update Users set password='" + AJSoftEntity.Classes.CommonFunction.EncryptData(password) + "' where UserId='" + oUser.UserId + "'");
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CRUD Operations

        public User Create(User oUser)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    oUser.UserId = Guid.NewGuid();
                    ctx.Users.Add(oUser);
                    ctx.SaveChanges();
                    return oUser;

                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(User oUser)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oUser).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Guid id)
        {

            using (var ctx = new DBAJEntities())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        User oUser = ctx.Users.Where(p => p.UserId == id).FirstOrDefault();
                        ctx.Users.Remove(oUser);
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        #endregion
    }
}
