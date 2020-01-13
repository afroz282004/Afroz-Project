using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class cashcollection_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Cash Collection Data Methods"
        public string CashCollectionInsert(cashcollection_Prop cashcollectionprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_CashCollectionBoy_INSERT", new object[] { cashcollectionprop.CounterID, cashcollectionprop.BoyName, cashcollectionprop.cDate }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string BankDelete(bank_Prop bankprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Bank_delete", new object[] { bankprop.BankCode}).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable BankSearch(bank_Prop bankprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Bank_search", new object[] { bankprop.BankCode,bankprop.BankName  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
