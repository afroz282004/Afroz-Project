using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class payment_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Payment Data Methods"

        public string BillCancel(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Payment_Cancel", new object[] { paymentprop.PaymentID , paymentprop.UserName }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string PaymentInsert(payment_Prop paymentprop)
        {
            try
            {

               return SqlHelper.ExecuteDataset(com.con, "usp_payment_insert", new object[] { paymentprop.BillID, paymentprop.Mode, paymentprop.Amount, paymentprop.Bankid, paymentprop.ChequeDate, paymentprop.ChequeNo, paymentprop.PhoneNo, paymentprop.SubdivisionCode, paymentprop.UserName, paymentprop.Emailid,paymentprop.status }).Tables[0].Rows[0][0].ToString();
               // return SqlHelper.ExecuteDataset(com.con, "usp_payment_insert", new object[] { paymentprop.BillID, paymentprop.Mode, paymentprop.Amount, paymentprop.Bankid, paymentprop.ChequeDate, paymentprop.ChequeNo, paymentprop.PhoneNo,  paymentprop.UserName, paymentprop.Emailid, paymentprop.status }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public string PaymentInsert_single(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_insert_single", new object[] { paymentprop.BillID, paymentprop.Mode, paymentprop.Amount, paymentprop.Bankid, paymentprop.ChequeDate, paymentprop.ChequeNo, paymentprop.PhoneNo, paymentprop.SubdivisionCode, paymentprop.UserName, paymentprop.Emailid, paymentprop.status }).Tables[0].Rows[0][0].ToString();
                // return SqlHelper.ExecuteDataset(com.con, "usp_payment_insert", new object[] { paymentprop.BillID, paymentprop.Mode, paymentprop.Amount, paymentprop.Bankid, paymentprop.ChequeDate, paymentprop.ChequeNo, paymentprop.PhoneNo,  paymentprop.UserName, paymentprop.Emailid, paymentprop.status }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string checkclosedtrasction(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "ups_checkClosdTrasaction ", new object[] { paymentprop.UserName  }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string PaymentDelete(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_delete", new object[] { paymentprop.PaymentID }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable  BillingSelect(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_billing_select", new object[] { paymentprop.SubdivisionCode, paymentprop.AccountNo}).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentSelect(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_select", new object[] { paymentprop.PaymentID}).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentSelectByBillID(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Payment_SelectByBillID", new object[] { paymentprop.BillIDs }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentSelectChangeDuedate(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_billing_select_chgduedate", new object[] { paymentprop.AccountNo  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string PaymentChangeDuedate(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_ChangeDuedate", new object[] { paymentprop.BillID, paymentprop.ChequeDate, paymentprop.UserName }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentSelectCancel(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_selectCancel", new object[] { paymentprop.SubdivisionCode,paymentprop.AccountNo,paymentprop.Receiptno  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable ConsumerSelect(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_consumer_select", new object[] {paymentprop.SubdivisionCode ,  paymentprop.Name, paymentprop.AccountNo }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentChequeSelect(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_select_paymentcheque", new object[] { paymentprop.Bankid, paymentprop.ChequeNo,paymentprop.ChequeDate}).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public DataTable PaymentChequeSelect_new(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_select_paymentcheque_new", new object[] { paymentprop.Bankid, paymentprop.ChequeNo, paymentprop.ChequeDate }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public DataTable PaymentUpdateMultiple(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Update_paymentmultiple", new object[] { paymentprop.Bankid, paymentprop.ChequeNo, paymentprop.ChequeDate }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentDeleteMultiple()
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Delete_paymentmultiple", new object[] {  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public DataTable PaymentUserSummary(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_usersummary", new object[] { paymentprop.Fromdate, paymentprop.Todate,paymentprop.UserName  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentSummary(payment_Prop paymentprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_summary", new object[] {paymentprop.UserName}).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable PaymentCloseTransaction(payment_Prop paymentprop )
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_payment_closetransaction", new object[] {paymentprop.Fromdate  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        #endregion
    }
}
