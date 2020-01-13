using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class paymenttype_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Payment Type Data Methods"
        public string PaymentTypeInsert(paymenttype_Prop paymenttypeprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_paymenttype_insert", new object[] { paymenttypeprop.PaymentType}).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string PaymentTypeDelete(paymenttype_Prop paymenttypeprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Paymenttype_delete", new object[] { paymenttypeprop.Paymentid }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentTypeSearch(paymenttype_Prop paymenttypeprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Paymenttype_search", new object[] { paymenttypeprop.PaymentType}).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
