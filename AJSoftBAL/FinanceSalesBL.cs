using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
  public  class FinanceSalesBL
    {
        #region Get Methods

        public FinanceSale GetById(Guid id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.FinanceSales.Where(c => c.FinanceSaleId == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public string GetGridData(GridSettings grid)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            var query = ctx.FinanceSales.AsQueryable();
        //            int count;
        //            var data = query.GridCommonSettings(grid, out count);

        //            var result = new
        //            {
        //                total = (int)Math.Ceiling((double)count / grid.PageSize),
        //                page = grid.PageIndex,
        //                records = count,
        //                rows = (from e in data
        //                        select new
        //                        {
        //                            FinanceSaleId = e.FinanceSaleId,
        //                            Week = e.Week,
        //                            Month = e.Month,
        //                            Year = e.Year,
        //                            FromDate = (e.FromDate == null ? "" : e.FromDate.Value.ToString("MM/dd/yyyy")),
        //                            ToDate = (e.ToDate == null ? "" : e.ToDate.Value.ToString("MM/dd/yyyy")),
        //                            TotalInvCases = e.TotalInvCases,
        //                            InvoicedAmt = e.InvoicedAmt.Value.ToString("C"),
        //                            Action = e.FinanceSaleId
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

        //public string GetFinanceCaseGridData(GridSettings grid, DateTime? FromDate = null, DateTime? ToDate = null)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            string Complete = En_Case_Status.Complete.ToString();
        //            var query = new List<vw_FinanceCases>().AsQueryable();

        //            if (FromDate != null && ToDate != null)
        //            {
        //                //ToDate = ToDate.Value.AddDays(1);
        //                int month = FromDate.Value.Date.Month;
        //                int year = FromDate.Value.Date.Year;
        //                query = ctx.vw_FinanceCases.Where(c => c.Status == Complete && c.Month == month && c.Year == year).AsQueryable();
        //                //query = ctx.vw_FinanceCases.Where(c => c.Status == Complete && (c.CompleteOn >= FromDate && c.CompleteOn <= ToDate)).AsQueryable();
        //            }
        //            else
        //            {
        //                query = ctx.vw_FinanceCases.Where(c => c.Status == Complete).AsQueryable();
        //            }
        //            int count;
        //            var data = query.GridCommonSettings(grid, out count);

        //            var result = new
        //            {
        //                total = (int)Math.Ceiling((double)count / grid.PageSize),
        //                page = grid.PageIndex,
        //                records = count,
        //                rows = (from e in data
        //                        select new
        //                        {
        //                            CaseId = e.CaseId,
        //                            CaseIdLookup = e.CaseIdLookup.ToUpper(),
        //                            ClinicId = e.ClinicId,
        //                            ClinicLocationId = e.ClinicLocationId,
        //                            PatientId = e.PatientId,
        //                            Status = e.Status,
        //                            CaseInvStatus = e.CaseInvStatus,
        //                            TranHeaderId = e.TranHeaderId,
        //                            InvoiceNumber = (e.InvoiceNumber == null ? "" : e.InvoiceNumber),
        //                            TransDate = (e.TransDate == null ? "" : Util.ConvertUTCtoCST(e.TransDate.Value).ToString("MM/dd/yyyy")),
        //                            CompleteOn = (e.CompleteOn == null) ? "" : Util.ConvertUTCtoCST(e.CompleteOn.Value).ToString("MM/dd/yyyy"),
        //                            ClinicName = e.ClinicName,
        //                            ClinicLocationName = e.ClinicLocationName,
        //                            PatientName = e.PatientName,
        //                            IsStatRequest = e.IsStatRequest,
        //                            Week = e.Week,
        //                            Month = e.Month,
        //                            Year = e.Year,
        //                            ClinicianName = e.ClinicianName,
        //                            SpecialistName = e.SpecialistName,
        //                            CaseAmount = ((e.CaseAmount == null || e.CaseAmount < 0) ? "$0.00" : Convert.ToDecimal(e.CaseAmount).ToString("C")),
        //                            Action = e.CaseId,
        //                            CaseTypeName = e.CaseTypeName
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
        
        public FinanceSale GetByFromToDate(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.FinanceSales.Where(c => c.FromDate == FromDate && c.ToDate == ToDate).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region CRUD Operations

        public void Create(FinanceSale oFinanceSale)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.FinanceSales.Add(oFinanceSale);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(FinanceSale oFinanceSale)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    ctx.Entry(oFinanceSale).State = System.Data.Entity.EntityState.Modified;
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
