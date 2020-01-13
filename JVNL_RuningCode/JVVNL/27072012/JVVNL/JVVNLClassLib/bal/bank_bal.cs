using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class bank_bal
    {
        #region GENERAL DECLARATION
        bank_dal bankdal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string BankInsert(bank_Prop bankprop)
        {
            try
            { 
                bankdal= new bank_dal();
                string strMsg = bankdal.BankInsert(bankprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        public string BankDelete(bank_Prop bankprop)
        {
            try
            {
                bankdal = new bank_dal();
                string strMsg = bankdal.BankDelete(bankprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable BankSearch(bank_Prop bankprop)
        {
            try
            {
                bankdal = new bank_dal();
                DataTable dt = bankdal.BankSearch(bankprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        
        
    }
}
