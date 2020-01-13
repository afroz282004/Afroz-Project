using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class bank_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "bank Data Methods"
        public string BankInsert(bank_Prop  bankprop)
        {
            try
            {
                
                return SqlHelper.ExecuteDataset(com.con, "usp_bank_insert", new object[] {bankprop.BankCode,bankprop.BankName  }).Tables[0].Rows[0][0].ToString ();
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
