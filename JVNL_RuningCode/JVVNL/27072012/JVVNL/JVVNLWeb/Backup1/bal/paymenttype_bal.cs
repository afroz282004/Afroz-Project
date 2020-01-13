using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class paymenttype_bal
    {
        #region GENERAL DECLARATION
        paymenttype_dal paymenttypedal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string PaymentTypeInsert(paymenttype_Prop paymenttypeprop)
        {
            try
            { 
                paymenttypedal= new paymenttype_dal();
                string strMsg = paymenttypedal.PaymentTypeInsert(paymenttypeprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        public string PaymentTypeDelete(paymenttype_Prop paymenttypeprop)
        {
            try
            {
                paymenttypedal = new paymenttype_dal();
                string strMsg = paymenttypedal.PaymentTypeDelete(paymenttypeprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable PaymentTypeSearch(paymenttype_Prop paymenttypeprop)
        {
            try
            {
                paymenttypedal = new paymenttype_dal();
                DataTable dt = paymenttypedal.PaymentTypeSearch(paymenttypeprop);
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
