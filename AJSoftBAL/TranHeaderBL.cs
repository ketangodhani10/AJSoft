using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static AJSoftEntity.Classes.EnumData;

namespace AJSoftBAL
{
    public class TranHeaderBL
    {
        //User oUser = new CommonBL().CurrentUser;

        //#region Get Methods

        ////public string GetGridData(GridSettings grid, Guid? CompanyId = null, bool IsTranTypeInvoice = false)
        ////{
        ////    try
        ////    {
        ////        using (var ctx = new DBAJEntities())
        ////        {
        ////            string Invoice = En_Tran_Type.Invoice.ToString();
        ////            var query = new List<vw_TranHeader>().AsQueryable();

        ////            if (IsTranTypeInvoice)
        ////            {
        ////                if (CompanyId != null)
        ////                    query = ctx.vw_TranHeader.Where(c => c.TranType == Invoice && c.CompanyId == CompanyId).AsQueryable();
        ////                else
        ////                    query = ctx.vw_TranHeader.Where(c => c.TranType == Invoice).AsQueryable();
        ////            }
        ////            else
        ////            {
        ////                if (CompanyId != null)
        ////                    query = ctx.vw_TranHeader.Where(c => c.TranType != Invoice && c.CompanyId == CompanyId).AsQueryable();
        ////                else
        ////                    query = ctx.vw_TranHeader.Where(c => c.TranType != Invoice).AsQueryable();
        ////            }

        ////            int count;
        ////            var data = query.GridCommonSettings(grid, out count);

        ////            var result = new
        ////            {
        ////                total = (int)Math.Ceiling((double)count / grid.PageSize),
        ////                page = grid.PageIndex,
        ////                records = count,
        ////                rows = (from e in data
        ////                        select new
        ////                        {
        ////                            TranHeaderId = e.TranHeaderId,
        ////                            CompanyId = e.CompanyId,
        ////                            ClinicName = e.ClinicName,
        ////                            CompanyLocationId = e.CompanyLocationId,
        ////                            LocationName = e.LocationName,
        ////                            TranType = e.TranType,
        ////                            TranSubType = e.TranSubType,
        ////                            TranStatus = e.TranStatus,
        ////                            TransactionId = e.TransactionId,
        ////                            InvoiceNumber = e.InvoiceNumber,
        ////                            PaidBy = e.PaidBy,
        ////                            CheckDate = e.CheckDate,
        ////                            CheckNo = e.CheckNo,
        ////                            TransDate = Util.ConvertUTCtoCST(e.TransDate.Value).ToString("MM/dd/yyyy"),
        ////                            FromDate = (e.FromDate == null ? "" : Util.ConvertUTCtoCST(e.FromDate.Value).ToString("MM/dd/yyyy")),
        ////                            ToDate = (e.ToDate == null ? "" : Util.ConvertUTCtoCST(e.ToDate.Value).ToString("MM/dd/yyyy")),
        ////                            BillingTerms = e.BillingTerms != null ? Convert.ToString((En_Billing_Terms)Convert.ToInt32(e.BillingTerms)).Replace("_", " ") : "",
        ////                            DueDate = (e.DueDate == null ? "" : Util.ConvertUTCtoCST(e.DueDate.Value).ToString("MM/dd/yyyy")),
        ////                            Notes = e.Notes,
        ////                            Amount = e.Amount.Value.ToString("C"),
        ////                            Balance = e.Balance.Value.ToString("C"),
        ////                            CreatedBy = e.CreatedBy,
        ////                            CreatedOn = (e.CreatedOn == null ? "" : Util.ConvertUTCtoCST(e.CreatedOn.Value).ToString("MM/dd/yyyy HH:mm")),
        ////                            ModifiedBy = e.ModifiedBy,
        ////                            ModifiedOn = e.ModifiedOn,
        ////                            Aging = e.Aging,
        ////                            Action = e.TranHeaderId
        ////                        }).ToArray()
        ////            };

        ////            return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //public List<TranHeader> GetTransactionHeaders()
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.TranHeaders.ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public TranHeader GetById(Guid TranHeaderId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.TranHeaders.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<vw_TranHeader> GetTranHeaderVWByCompanyId(Guid CompanyId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.vw_TranHeader.Where(c => c.CompanyId == CompanyId).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<TranHeader> GetTranHeaderByCompanyId(Guid CompanyId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.TranHeaders.Where(c => c.CompanyId == CompanyId).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public vw_TranHeader GetTranHeaderVwById(Guid TranHeaderId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.vw_TranHeader.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<TranHeader> GetBalanceByCompanyId(Guid CompanyId)
        //{
        //    try
        //    {
        //        string Invoice = En_Tran_Type.Invoice.ToString();
        //        string Paid = En_Tran_Status.Paid.ToString();
        //        using (var ctx = new DBAJEntities())
        //        {
        //            return ctx.TranHeaders.Where(c => c.CompanyId == CompanyId && c.TranType != Invoice && c.TranStatus == Paid && c.Balance > 0).OrderBy(c => c.TransDate).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<TranHeader> GetTranHeadersByFromToDate(DateTime _FromDate, DateTime _ToDate)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            string Invoice = En_Tran_Type.Invoice.ToString();
        //            string Draft = En_Tran_Status.Draft.ToString();
        //            string Standard = En_Tran_SubType.Standard.ToString();
        //            string Paid = En_Tran_Status.Paid.ToString();

        //            return ctx.TranHeaders.Where(c => c.TranType == Invoice && c.TranSubType == Standard && c.TranStatus != Paid && DbFunctions.TruncateTime(c.FromDate) == DbFunctions.TruncateTime(_FromDate) && DbFunctions.TruncateTime(c.ToDate) == DbFunctions.TruncateTime(_ToDate)).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //#endregion

        //#region CRUD Operation
        //public KeyValuePair<bool, string> VOIDPayment(Guid TranHeaderId, string ApiLoginID, string ApiTransactionKey)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            TranHeader oTranHeader = ctx.TranHeaders.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //            List<TranDetail> lstPaymentDetails = new TranDetailBL().GetTranDetailsByTranHeaderId(TranHeaderId);

        //            string InvNumbers = "";
        //            foreach (var item in lstPaymentDetails)
        //            {
        //                TranHeader oTHInvoices = ctx.TranHeaders.Where(c => c.TranHeaderId == item.InvoiceTranHeaderId).FirstOrDefault();
        //                oTHInvoices.Balance = (oTHInvoices.Balance + item.TranDetailAmt);
        //                if (oTHInvoices.Amount == oTHInvoices.Balance)
        //                {
        //                    oTHInvoices.TranStatus = En_Tran_Status.Draft.ToString();
        //                }
        //                else
        //                {
        //                    oTHInvoices.TranStatus = En_Tran_Status.Partial.ToString();
        //                }
        //                ctx.Entry(oTHInvoices).State = EntityState.Modified;

        //                InvNumbers += " " + oTHInvoices.InvoiceNumber + ",";
        //            }
        //            oTranHeader.Balance = oTranHeader.Amount;
        //            oTranHeader.TranStatus = En_Tran_Status.Void.ToString();
        //            oTranHeader.ModifiedBy = oUser.Email;
        //            oTranHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.Entry(oTranHeader).State = EntityState.Modified;

        //            TranJournal oTJMainPayment = new TranJournal();
        //            oTJMainPayment.TranJournalId = Guid.NewGuid();
        //            oTJMainPayment.TranHeaderId = oTranHeader.TranHeaderId;
        //            oTJMainPayment.CompanyId = oTranHeader.CompanyId;
        //            oTJMainPayment.CompanyLocationId = oTranHeader.CompanyLocationId;
        //            oTJMainPayment.Reference = "VOID - Check # " + oTranHeader.CheckNo;
        //            oTJMainPayment.TranJournalNotes = "Invoice " + InvNumbers.TrimEnd(',');
        //            oTJMainPayment.CR = oTranHeader.Amount;
        //            oTJMainPayment.DR = 0;
        //            oTJMainPayment.CreatedOn = DateTime.UtcNow.AddMilliseconds(+2);
        //            ctx.TranJournals.Add(oTJMainPayment);

        //            ctx.SaveChanges();

        //            //Invoice/Payment Settlement and Update Balance
        //            InvoicePaymentSettlement(oTranHeader.CompanyId, null);

        //            return new KeyValuePair<bool, string>(true, "Payment has been voided");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public KeyValuePair<bool, string> RefundPayment(Guid TranHeaderId, string ApiLoginID, string ApiTransactionKey)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            TranHeader oTranHeader = ctx.TranHeaders.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //            List<TranDetail> lstPaymentDetails = new TranDetailBL().GetTranDetailsByTranHeaderId(TranHeaderId);

        //            string InvNumbers = "";
        //            foreach (var item in lstPaymentDetails)
        //            {
        //                TranHeader oTHInvoices = ctx.TranHeaders.Where(c => c.TranHeaderId == item.InvoiceTranHeaderId).FirstOrDefault();
        //                oTHInvoices.Balance = (oTHInvoices.Balance + item.TranDetailAmt);
        //                if (oTHInvoices.Amount == oTHInvoices.Balance)
        //                {
        //                    oTHInvoices.TranStatus = En_Tran_Status.Draft.ToString();
        //                }
        //                else
        //                {
        //                    oTHInvoices.TranStatus = En_Tran_Status.Partial.ToString();
        //                }
        //                ctx.Entry(oTHInvoices).State = EntityState.Modified;

        //                InvNumbers += " " + oTHInvoices.InvoiceNumber + ",";
        //            }
        //            oTranHeader.Balance = oTranHeader.Amount;
        //            oTranHeader.TranStatus = En_Tran_Status.Refund.ToString();
        //            oTranHeader.ModifiedBy = oUser.Email;
        //            oTranHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.Entry(oTranHeader).State = EntityState.Modified;

        //            TranJournal oTJMainPayment = new TranJournal();
        //            oTJMainPayment.TranJournalId = Guid.NewGuid();
        //            oTJMainPayment.TranHeaderId = oTranHeader.TranHeaderId;
        //            oTJMainPayment.CompanyId = oTranHeader.CompanyId;
        //            oTJMainPayment.CompanyLocationId = oTranHeader.CompanyLocationId;
        //            oTJMainPayment.Reference = "Refund - Check # " + oTranHeader.CheckNo;
        //            oTJMainPayment.TranJournalNotes = "Invoice " + InvNumbers.TrimEnd(',');
        //            oTJMainPayment.CR = oTranHeader.Amount;
        //            oTJMainPayment.DR = 0;
        //            oTJMainPayment.CreatedOn = DateTime.UtcNow.AddMilliseconds(+2);
        //            ctx.TranJournals.Add(oTJMainPayment);

        //            ctx.SaveChanges();

        //            //Invoice/Payment Settlement and Update Balance
        //            InvoicePaymentSettlement(oTranHeader.CompanyId, null);

        //            return new KeyValuePair<bool, string>(true, "Refunded");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public void SaveCreditMemo(TranHeader oTranHeader)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            //Save Credit Memo first to Tran Header
        //            oTranHeader.TranHeaderId = Guid.NewGuid();
        //            oTranHeader.Balance = oTranHeader.Amount;
        //            oTranHeader.TranType = En_Tran_Type.Credit.ToString();
        //            oTranHeader.TranStatus = En_Tran_Status.Paid.ToString();
        //            oTranHeader.PaidBy = En_Tran_Type.Credit.ToString();
        //            oTranHeader.TransDate = DateTime.UtcNow;
        //            oTranHeader.CreatedBy = oUser.Email;
        //            oTranHeader.CreatedOn = DateTime.UtcNow;
        //            oTranHeader.ModifiedBy = oUser.Email;
        //            oTranHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.TranHeaders.Add(oTranHeader);
        //            ctx.SaveChanges();

        //            //Invoice/Payment Settlement and Update Balance
        //            InvoicePaymentSettlement(oTranHeader.CompanyId, null);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public bool DeleteCreditMemo(Guid TranHeaderId)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            using (var ctx = new DBAJEntities())
        //            {
        //                //Reset Invoice Balance against CreditMemo
        //                foreach (var CreditItem in ctx.TranDetails.Where(c => c.TranHeaderId == TranHeaderId).ToList())
        //                {
        //                    TranHeader oInvForBalanceUpdate = ctx.TranHeaders.Where(c => c.TranHeaderId == CreditItem.InvoiceTranHeaderId).FirstOrDefault();
        //                    oInvForBalanceUpdate.Balance = (oInvForBalanceUpdate.Balance + CreditItem.TranDetailAmt);
        //                    if (oInvForBalanceUpdate.Balance == oInvForBalanceUpdate.Amount)
        //                        oInvForBalanceUpdate.TranStatus = En_Tran_Status.Draft.ToString();
        //                    else
        //                        oInvForBalanceUpdate.TranStatus = En_Tran_Status.Partial.ToString();

        //                    oInvForBalanceUpdate.ModifiedBy = oUser.Email;
        //                    oInvForBalanceUpdate.ModifiedOn = DateTime.UtcNow;

        //                    ctx.Entry(oInvForBalanceUpdate).State = EntityState.Modified;
        //                }

        //                //Tran Detail
        //                ctx.TranDetails.Where(c => c.TranHeaderId == TranHeaderId).ToList().ForEach(m => ctx.Entry(m).State = EntityState.Deleted);

        //                //Tran Journals
        //                TranJournal oTranJournal = ctx.TranJournals.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //                if (oTranJournal != null)
        //                    ctx.TranJournals.Remove(oTranJournal);

        //                //Tran Header
        //                TranHeader oTranHeader = ctx.TranHeaders.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //                ctx.TranHeaders.Remove(oTranHeader);

        //                ctx.SaveChanges();

        //                scope.Complete();

        //                return true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            scope.Dispose();
        //            return false;
        //        }
        //    }
        //}

        //public void SaveMiscInvoice(TranHeader oTranHeader)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            oTranHeader.TranHeaderId = Guid.NewGuid();
        //            oTranHeader.Balance = oTranHeader.Amount;
        //            oTranHeader.InvoiceNumber = new SiteConfigBL().GetByName(En_Site_Config.InvoiceNumber.ToString()).Value;
        //            oTranHeader.TranType = En_Tran_Type.Invoice.ToString();
        //            oTranHeader.TranSubType = En_Tran_SubType.Miscellaneous.ToString();
        //            oTranHeader.TranStatus = En_Tran_Status.Draft.ToString();
        //            oTranHeader.TransDate = oTranHeader.TransDate.Value.AddHours(+5);
        //            oTranHeader.DueDate = oTranHeader.DueDate.Value.AddHours(+5);
        //            oTranHeader.CreatedBy = oUser.Email;
        //            oTranHeader.CreatedOn = DateTime.UtcNow;
        //            oTranHeader.ModifiedBy = oUser.Email;
        //            oTranHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.TranHeaders.Add(oTranHeader);

        //            TranDetail oTranDetail = new TranDetail();
        //            oTranDetail.TranDetailId = Guid.NewGuid();
        //            oTranDetail.TranHeaderId = oTranHeader.TranHeaderId;
        //            oTranDetail.ShadeId = null;
        //            oTranDetail.InvoiceTranHeaderId = null;
        //            oTranDetail.TranDetailAmt = oTranHeader.Amount;
        //            oTranDetail.TranDetailNotes = oTranHeader.Notes;
        //            ctx.TranDetails.Add(oTranDetail);

        //            TranJournal oTranJournal = new TranJournal();
        //            oTranJournal.TranJournalId = Guid.NewGuid();
        //            oTranJournal.TranHeaderId = oTranHeader.TranHeaderId;
        //            oTranJournal.CompanyId = oTranHeader.CompanyId;
        //            oTranJournal.CompanyLocationId = oTranHeader.CompanyLocationId;
        //            oTranJournal.Reference = oTranHeader.InvoiceNumber;
        //            oTranJournal.TranJournalNotes = oTranHeader.Notes;
        //            oTranJournal.CR = oTranHeader.Amount;
        //            oTranJournal.DR = 0;
        //            oTranJournal.CreatedOn = DateTime.UtcNow;
        //            ctx.TranJournals.Add(oTranJournal);

        //            string LastInvoiceNumber = oTranHeader.InvoiceNumber;
        //            string[] arr = LastInvoiceNumber.Split(new string[] { "VR" }, StringSplitOptions.None);
        //            int InvoiceDigit = Convert.ToInt32(arr[1]);
        //            InvoiceDigit += 1;
        //            string LatestInvoiceNumber = "VR" + InvoiceDigit.ToString();
        //            SiteConfig oSiteConfig = new SiteConfigBL().GetByName(En_Site_Config.InvoiceNumber.ToString());
        //            oSiteConfig.Value = LatestInvoiceNumber;
        //            ctx.Entry(oSiteConfig).State = EntityState.Modified;

        //            ctx.SaveChanges();

        //            //Invoice/Payment Settlement and Update Balance
        //            InvoicePaymentSettlement(oTranHeader.CompanyId, null);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public void UpdateMiscInvoice(TranHeader oTranHeader)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            TranHeader oTempHeader = GetById(oTranHeader.TranHeaderId);
        //            oTempHeader.CompanyId = oTranHeader.CompanyId;
        //            oTempHeader.CompanyLocationId = oTranHeader.CompanyLocationId;
        //            oTempHeader.Amount = oTranHeader.Amount;
        //            oTempHeader.Balance = oTranHeader.Amount;
        //            oTempHeader.TransDate = oTranHeader.TransDate.Value.AddHours(+5);
        //            oTempHeader.BillingTerms = oTranHeader.BillingTerms;
        //            oTempHeader.DueDate = oTranHeader.DueDate.Value.AddHours(+5);
        //            oTempHeader.Notes = oTranHeader.Notes;
        //            oTempHeader.ModifiedBy = oUser.Email;
        //            oTempHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.Entry(oTempHeader).State = EntityState.Modified;

        //            TranDetail oTranDetail = new TranDetailBL().GetByTranHeaderId(oTempHeader.TranHeaderId);
        //            oTranDetail.TranDetailAmt = oTempHeader.Amount;
        //            oTranDetail.TranDetailNotes = oTempHeader.Notes;
        //            ctx.Entry(oTranDetail).State = EntityState.Modified;

        //            TranJournal oTranJournal = new TranJournalBL().GetByTranHeaderId(oTempHeader.TranHeaderId);
        //            oTranJournal.TranHeaderId = oTempHeader.TranHeaderId;
        //            oTranJournal.CompanyId = oTempHeader.CompanyId;
        //            oTranJournal.CompanyLocationId = oTempHeader.CompanyLocationId;
        //            oTranJournal.Reference = oTranHeader.InvoiceNumber;
        //            oTranJournal.TranJournalNotes = oTempHeader.Notes;
        //            oTranJournal.CR = oTempHeader.Amount;
        //            oTranJournal.DR = 0;
        //            oTranJournal.CreatedOn = DateTime.UtcNow;
        //            ctx.Entry(oTranJournal).State = EntityState.Modified;

        //            ctx.SaveChanges();

        //            //Invoice/Payment Settlement and Update Balance
        //            InvoicePaymentSettlement(oTempHeader.CompanyId, null);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public bool DeleteMiscInvoice(Guid TranHeaderId)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            using (var ctx = new DBAJEntities())
        //            {
        //                //Tran Detail
        //                ctx.TranDetails.Where(c => c.TranHeaderId == TranHeaderId).ToList().ForEach(m => ctx.Entry(m).State = EntityState.Deleted);

        //                //Tran Journals
        //                TranJournal oTranJournal = ctx.TranJournals.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //                ctx.TranJournals.Remove(oTranJournal);

        //                //Tran Header
        //                TranHeader oTranHeader = ctx.TranHeaders.Where(c => c.TranHeaderId == TranHeaderId).FirstOrDefault();
        //                ctx.TranHeaders.Remove(oTranHeader);

        //                ctx.SaveChanges();

        //                scope.Complete();

        //                return true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            scope.Dispose();
        //            return false;
        //        }
        //    }
        //}

        //public void PaymentByCheck(TranHeader oTranHeader, List<TranHeader> lstInvoicesForSettlement)
        //{
        //    try
        //    {
        //        Guid CompanyId = oTranHeader.CompanyId;
        //        using (var ctx = new DBAJEntities())
        //        {
        //            //Save Tran Header for Payment first
        //            oTranHeader.TransDate = DateTime.UtcNow;
        //            oTranHeader.CreatedBy = oUser.Email;
        //            oTranHeader.CreatedOn = DateTime.UtcNow;
        //            oTranHeader.ModifiedBy = oUser.Email;
        //            oTranHeader.ModifiedOn = DateTime.UtcNow;
        //            ctx.TranHeaders.Add(oTranHeader);
        //            ctx.SaveChanges();

        //            //1. First Settle Selected Invoices
        //            InvoicePaymentSettlement(CompanyId, lstInvoicesForSettlement);

        //            //2. Then settle invoices is balance is available
        //            InvoicePaymentSettlement(CompanyId, null);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public void InvoicePaymentSettlement(Guid CompanyId, List<TranHeader> lstInvoicesSettlement)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            using (var ctx = new DBAJEntities())
        //            {
        //                string Invoice = En_Tran_Type.Invoice.ToString();
        //                string Paid = En_Tran_Status.Paid.ToString();

        //                if (lstInvoicesSettlement == null)
        //                {
        //                    lstInvoicesSettlement = GetTransactionHeaders().Where(c => c.TranType == Invoice && c.TranStatus != Paid && c.CompanyId == CompanyId).OrderBy(c => c.TransDate).ThenBy(c => c.InvoiceNumber).ToList();
        //                }

        //                foreach (var itemInv in lstInvoicesSettlement)
        //                {
        //                    foreach (var itemBalance in GetBalanceByCompanyId(CompanyId))
        //                    {
        //                        //Tran Details
        //                        TranDetail oTranDetail = new TranDetail();
        //                        oTranDetail.TranDetailId = Guid.NewGuid();
        //                        oTranDetail.TranHeaderId = itemBalance.TranHeaderId;
        //                        oTranDetail.ShadeId = null;
        //                        oTranDetail.InvoiceTranHeaderId = itemInv.TranHeaderId;
        //                        //if credit is more than invoice balance then use only required credit for particular invoice
        //                        if (Convert.ToDecimal(itemBalance.Balance) > Convert.ToDecimal(itemInv.Balance))
        //                        {
        //                            oTranDetail.TranDetailAmt = ctx.TranHeaders.Where(c => c.TranHeaderId == itemInv.TranHeaderId).FirstOrDefault().Balance;
        //                        }
        //                        else
        //                        {
        //                            oTranDetail.TranDetailAmt = itemBalance.Balance;
        //                        }
        //                        oTranDetail.TranDetailNotes = "Invoice " + itemInv.InvoiceNumber;
        //                        ctx.TranDetails.Add(oTranDetail);

        //                        //Update TranHeader for Invoice Balance
        //                        TranHeader oTranInvBalanceUpdate = ctx.TranHeaders.Where(c => c.TranHeaderId == itemInv.TranHeaderId).FirstOrDefault();
        //                        oTranInvBalanceUpdate.Balance = (oTranInvBalanceUpdate.Balance - oTranDetail.TranDetailAmt);
        //                        oTranInvBalanceUpdate.TranStatus = En_Tran_Status.Partial.ToString();
        //                        if (oTranInvBalanceUpdate.Balance == 0)
        //                            oTranInvBalanceUpdate.TranStatus = En_Tran_Status.Paid.ToString();
        //                        oTranInvBalanceUpdate.ModifiedBy = oUser.Email;
        //                        oTranInvBalanceUpdate.ModifiedOn = DateTime.UtcNow;
        //                        ctx.Entry(oTranInvBalanceUpdate).State = EntityState.Modified;

        //                        //Journal Entry for Payment
        //                        if (itemBalance.Balance == itemBalance.Amount)
        //                        {
        //                            //if balance and amount are same then full balance entry to journals 
        //                            //otherwise it was already exist in journal
        //                            TranJournal oTranJPayment = new TranJournal();
        //                            oTranJPayment.TranJournalId = Guid.NewGuid();
        //                            oTranJPayment.TranHeaderId = itemBalance.TranHeaderId;
        //                            oTranJPayment.CompanyId = itemBalance.CompanyId;
        //                            oTranJPayment.CompanyLocationId = itemBalance.CompanyLocationId;
        //                            if (itemBalance.TranType == En_Tran_Type.Credit.ToString())
        //                                oTranJPayment.Reference = En_Tran_Type.Credit.ToString();
        //                            else
        //                            {
        //                                if (itemBalance.TranSubType == En_Tran_SubType.Check.ToString())
        //                                    oTranJPayment.Reference = En_Tran_SubType.Check.ToString() + " # " + itemBalance.CheckNo;
        //                                else if (itemBalance.TranSubType == En_Tran_SubType.Cash.ToString())
        //                                    oTranJPayment.Reference = En_Tran_SubType.Cash.ToString();
        //                            }
        //                            oTranJPayment.TranJournalNotes = "Invoice " + itemInv.InvoiceNumber;
        //                            oTranJPayment.CR = 0;
        //                            oTranJPayment.DR = itemBalance.Balance;
        //                            oTranJPayment.CreatedOn = DateTime.UtcNow.AddMilliseconds(+2);
        //                            ctx.TranJournals.Add(oTranJPayment);
        //                        }
        //                        else
        //                        {
        //                            TranJournal oTranJPayment = ctx.TranJournals.Where(c => c.TranHeaderId == itemBalance.TranHeaderId).FirstOrDefault();
        //                            oTranJPayment.TranJournalNotes = oTranJPayment.TranJournalNotes + ", " + itemInv.InvoiceNumber;
        //                            ctx.Entry(oTranJPayment).State = EntityState.Modified;
        //                        }

        //                        //Update TranHeader for Payment/Credit Balance
        //                        TranHeader oTranBalanceUpdate = ctx.TranHeaders.Where(c => c.TranHeaderId == itemBalance.TranHeaderId).FirstOrDefault();
        //                        oTranBalanceUpdate.Balance = (oTranBalanceUpdate.Balance - oTranDetail.TranDetailAmt);
        //                        oTranBalanceUpdate.ModifiedBy = oUser.Email;
        //                        oTranBalanceUpdate.ModifiedOn = DateTime.UtcNow;
        //                        ctx.Entry(oTranBalanceUpdate).State = EntityState.Modified;

        //                        ctx.SaveChanges();
        //                    }
        //                }
        //                scope.Complete();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            scope.Dispose();
        //            throw;
        //        }
        //    }
        //}

        //#endregion
    }
}
