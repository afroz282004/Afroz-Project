using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace JVVNLClassLIB
{
   public class report_dal
    {

       #region "General Declaration"
       Common com = new Common();
       #endregion

       public DataTable PopulateCounterUserwise(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_Counter_Search_user", new object[] { reportProp.UserName }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable fillUser(counter_Prop counterprop)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_User_Select", new object[] { counterprop.Counterid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public DataTable ReportDailyPCSheetexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyPaymentControlSheet", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable ReportDailyPCSheet(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyPaymentControlSheet", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId, reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public DataTable PaymentData(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_reppaymentdata", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public DataTable Receiptno(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repReceiptno", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId, reportProp.Receiptno }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public DataTable DailyCRTransactionWiseexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyChequeTransactionWise", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId,reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DailyCRTransactionWise(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyChequeTransactionWise", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId, reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public DataTable DailyChequeWiseexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyChequeWise_export", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.UserName, reportProp.Bank, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DailyChequeWise(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyChequeWise", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.UserName, reportProp.Bank, reportProp.fromdate, reportProp.todate, reportProp.UserId,reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }




       public DataTable DailyCSUserWise(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyCollectionSummaryUserwise", new object[] { reportProp.CounterName, reportProp.UserName, reportProp.fromdate, reportProp.todate, reportProp.UserId, reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DailySubDWiseSummary(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailySubDivisionWiseSummary", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId,reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DailyDateSubDivisionWiseSummary(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repDailyDateSubDivisionWiseSummary", new object[] { reportProp.SubDivision, reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId, reportProp.ForGrid }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public DataTable DailyLoginLogoutexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repUserLoginLogout_export", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DailyLoginLogout(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repUserLoginLogout", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }




       public DataTable DatewiseSPSexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repPaymentSummaryDatewiseSubDivisonwise_export", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DatewiseSPS(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repPaymentSummaryDatewiseSubDivisonwise", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }




       public DataTable DatewiseSTSexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repTrasactionSummaryDatewiseSubDivisonwise_export", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable DatewiseSTS(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repTrasactionSummaryDatewiseSubDivisonwise", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }




       public DataTable DatewiseSPTS(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repPayTransSummaryDatewiseSubDivision", new object[] { reportProp.CounterName, reportProp.SubDivision, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public DataTable DatewiseSPTSexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repPayTransSummaryDatewiseSubDivision_export", new object[] { reportProp.CounterName, reportProp.SubDivision, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable CancelTrasactionexport(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repCancelTrasaction_export", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public DataTable CancelTrasaction(report_Prop reportProp)
       {
           try
           {

               return SqlHelper.ExecuteDataset(com.con, "usp_repCancelTrasaction", new object[] { reportProp.CounterName, reportProp.fromdate, reportProp.todate, reportProp.UserId }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
