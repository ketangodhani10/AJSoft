using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftEntity.Classes
{
    public class EnumData
    {
        public enum En_UserSession
        {
            User,
            JariCompany,
            RoleId
        }

        public enum En_CRUD
        {
            Delete,
            Insert,
            Update
        }

        public enum En_User_Status
        {
            Active,
            Inactive
        }

        public enum En_Role
        {
            Admin = 1,
            Primary = 2
        }

        public enum En_Role_Group
        {
            AJ,
            Company
        }

        public enum En_Tran_Type
        {
            Invoice,
            Payment,
            Credit,
            AutoCredit
        }

        public enum En_Tran_Status
        {
            Draft,
            Partial,
            Paid,
            Void,
            Refund,
        }

        public enum En_Tran_SubType
        {
            Cash,
            Check,
            Standard,
            Miscellaneous
        }

        public enum En_Payment_Type
        {
            Cash,
            Check,
            Credit
        }

        public enum En_Invoice_Type
        {
            Standard,
            Miscellaneous
        }

        public enum En_Site_Config
        {
            InvoiceNumber,
            AppDate
        }

        public enum En_Billing_Terms
        {
            _15,
            _30,
            _60,
            _90,
            _120
        }
    }
}
