using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class payment_bal
    {
        #region GENERAL DECLARATION
        payment_dal paymentdal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public string PaymentInsert(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string strMsg = paymentdal.PaymentInsert(paymentprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }


        public string PaymentInsert_single(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string strMsg = paymentdal.PaymentInsert_single(paymentprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }


        public string BillCancel(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string strMsg = paymentdal.BillCancel(paymentprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string checkclosedtrasction(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string strMsg = paymentdal.checkclosedtrasction(paymentprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string PaymentDelete(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string strMsg = paymentdal.PaymentDelete(paymentprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable  BillingSelect(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.BillingSelect(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentSelect(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentSelect(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentSelectCancel(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentSelectCancel(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentSelectChangeDuedate(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentSelectChangeDuedate(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public string PaymentChangeDuedate(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                string dt = paymentdal.PaymentChangeDuedate(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable ConsumerSelect(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.ConsumerSelect(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentChequeSelect(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentChequeSelect(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }


        public DataTable PaymentChequeSelect_new(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentChequeSelect_new(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }


        public DataTable PaymentUpdateMultiple(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentUpdateMultiple (paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentDeleteMultiple()
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentDeleteMultiple();
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentUserSummary(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentUserSummary (paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentSummary(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentSummary(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentClosedTransaction(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentCloseTransaction(paymentprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable PaymentSelectByBillID(payment_Prop paymentprop)
        {
            try
            {
                paymentdal = new payment_dal();
                DataTable dt = paymentdal.PaymentSelectByBillID(paymentprop);
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
