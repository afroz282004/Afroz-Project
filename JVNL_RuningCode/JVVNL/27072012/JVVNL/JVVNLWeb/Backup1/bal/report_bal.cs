using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
   public class report_bal
    {
       #region GENERAL DECLARATION
       report_dal reportdal = null;
       string _ErrMessage;
       public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
       #endregion
       public DataTable PopulateCounterUserwise(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.PopulateCounterUserwise(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }

       public DataTable fillUser(counter_Prop counterProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.fillUser(counterProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }

       public DataTable ReportDailyPCSheet(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.ReportDailyPCSheet(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }


       public DataTable PaymentData(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.PaymentData(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }


       public DataTable ReportDailyPCSheetexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.ReportDailyPCSheetexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }




       public DataTable Receiptno(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.Receiptno(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyCRTransactionWise(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyCRTransactionWise(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyCRTransactionWiseexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyCRTransactionWiseexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       







       public DataTable DailyChequeWise(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyChequeWise(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyChequeWiseexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyChequeWiseexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }






       public DataTable DailySubDWiseSummary(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailySubDWiseSummary(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }

       public DataTable DailyDateSubDivisionWiseSummary(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyDateSubDivisionWiseSummary(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyCSUserWise(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyCSUserWise(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyLoginLogout(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyLoginLogout(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DailyLoginLogoutexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DailyLoginLogoutexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }






       public DataTable DatewiseSPS(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSPS(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DatewiseSPSexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSPSexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }






       public DataTable DatewiseSTS(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSTS(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable DatewiseSTSexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSTSexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }






       public DataTable DatewiseSPTS(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSPTS(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }


       public DataTable DatewiseSPTSexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.DatewiseSPTSexport(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable CancelTrasaction(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.CancelTrasaction(reportProp);
               return dt;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return null;
           }
       }
       public DataTable CancelTrasactionexport(report_Prop reportProp)
       {
           try
           {
               reportdal = new report_dal();
               DataTable dt = reportdal.CancelTrasactionexport(reportProp);
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
