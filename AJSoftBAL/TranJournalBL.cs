using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class TranJournalBL
    {
        #region Get Methods

        //public string GetGridDataByClinicId(GridSettings grid, Guid ClinicId)
        //{
        //    try
        //    {
        //        using (var ctx = new DBAJEntities())
        //        {
        //            List<TranJournalModel> lstTranModel = new List<TranJournalModel>();
        //            decimal Balance = 0;
        //            foreach (var item in ctx.vw_TranJournal.Where(c => c.ClinicId == ClinicId).ToList())
        //            {
        //                TranJournalModel oTranJournalModel = new TranJournalModel();
        //                oTranJournalModel.RowNumber = item.RowNumber;
        //                oTranJournalModel.TranJournalId = item.TranJournalId;
        //                oTranJournalModel.TranHeaderId = item.TranHeaderId;
        //                oTranJournalModel.ClinicId = item.ClinicId;
        //                oTranJournalModel.ClinicLocationId = item.ClinicLocationId;
        //                oTranJournalModel.Reference = item.Reference;
        //                oTranJournalModel.TranJournalNotes = item.TranJournalNotes;
        //                oTranJournalModel.CR = item.CR;
        //                oTranJournalModel.DR = item.DR;
        //                oTranJournalModel.CreatedOn = item.CreatedOn;
        //                oTranJournalModel.TranType = item.TranType;
        //                oTranJournalModel.ClinicName = item.ClinicName;
        //                oTranJournalModel.LocationName = item.LocationName;
        //                oTranJournalModel.Amount = Convert.ToDecimal(item.CR - item.DR);
        //                Balance += oTranJournalModel.Amount;
        //                oTranJournalModel.Balance = Balance;
        //                lstTranModel.Add(oTranJournalModel);
        //            }

        //            var query = lstTranModel.AsQueryable();
        //            int count;
        //            var data = query.GridCommonSettings(grid, out count);
        //            Decimal dec = new Decimal(-1234.4321);
        //            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        //            culture.NumberFormat.CurrencyNegativePattern = 1;
        //            String str = String.Format(culture, "{0:C}", dec);

        //            var result = new
        //            {
        //                total = (int)Math.Ceiling((double)count / grid.PageSize),
        //                page = grid.PageIndex,
        //                records = count,
        //                rows = (from e in data
        //                        select new
        //                        {
        //                            RowNumber = e.RowNumber,
        //                            TranJournalId = e.TranJournalId,
        //                            TranHeaderId = e.TranHeaderId,
        //                            ClinicId = e.ClinicId,
        //                            ClinicName = e.ClinicName,
        //                            ClinicLocationId = e.ClinicLocationId,
        //                            LocationName = e.LocationName,
        //                            Reference = e.Reference,
        //                            TranJournalNotes = e.TranJournalNotes,
        //                            CR = e.CR,
        //                            DR = e.DR,
        //                            CreatedOn = Util.ConvertUTCtoCST(e.CreatedOn.Value).ToString("MM/dd/yyyy"),
        //                            TranType = e.TranType,
        //                            Amount = String.Format(culture, "{0:C}", e.Amount),
        //                            Balance = String.Format(culture, "{0:C}", e.Balance)
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

        public TranJournal GetByTranHeaderId(Guid id)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.TranJournals.Where(c => c.TranHeaderId == id).FirstOrDefault();
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
