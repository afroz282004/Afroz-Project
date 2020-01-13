using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using JVVNLClassLIB;
using System.Threading;
using System.Net;
using System.IO;
namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Report : IHttpHandler
    {
        string strmsg = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string type = context.Request["type"].ToString();
            string userid = "";
            if (context.Request["UserId"] == null)
            {
                userid = "";
            }
            else
            {
                userid = context.Request["UserId"].ToString();
            }

            switch (type)
            {
                case "populate":
                    if (context.Request["id"].ToString() == "ALL")
                    {
                        int CounterId = 0;
                        strmsg = filluser(CounterId);
                    }
                    else
                    {
                        int CounterId = Convert.ToInt32(context.Request["id"].ToString());
                        strmsg = filluser(CounterId);
                    }

                    break;
                case "DailyPCSheet":  //Daily Payment Control Sheet
                    string SubDivision = context.Request["ddlSubDivision"].ToString();
                    string CounterName = context.Request["ddlCounterName"].ToString();
                    string fromdate = context.Request["fromdate"].ToString();
                    string todate = context.Request["todate"].ToString();
                    strmsg = DailyPCSheet(SubDivision, CounterName, fromdate, todate, userid);
                    break;

                case "PaymentData":  //Daily Payment Control Sheet
                    string SubDivisionnew = context.Request["ddlSubDivision"].ToString();
                    string CounterNamenew = context.Request["ddlCounterName"].ToString();
                    string fromdatenew = context.Request["fromdate"].ToString();
                    string todatenew = context.Request["todate"].ToString();
                    strmsg = PaymentData(SubDivisionnew, CounterNamenew, fromdatenew, todatenew, userid);
                    break;

                case "repReceiptno":
                    string rSubDivision = context.Request["ddlSubDivision"].ToString();
                    string rCounterName = context.Request["ddlCounterName"].ToString();
                    string rfromdate = context.Request["fromdate"].ToString();
                    string rtodate = context.Request["todate"].ToString();
                    string Receiptno = context.Request["Receiptno"].ToString();
                    strmsg = repReceiptno(rSubDivision, rCounterName, rfromdate, rtodate, userid, Receiptno);
                    break;
                case "DailyCRTransactionWise":
                    string SubDivision1 = context.Request["ddlSubDivision"].ToString();
                    string CounterName1 = context.Request["ddlCounterName"].ToString();
                    string fromdate1 = context.Request["fromdate"].ToString();
                    string todate1 = context.Request["todate"].ToString();
                    strmsg = DailyCRTransactionWise(SubDivision1, CounterName1, fromdate1, todate1, userid);
                    break;
                case "DailyChequeWise":
                    string SubDivisiond = context.Request["ddlSubDivision"].ToString();
                    string CounterNamed = context.Request["ddlCounterName"].ToString();
                    string Bank = context.Request["Bank"].ToString();
                    string User = context.Request["User"].ToString();
                    string fromdated = context.Request["fromdate"].ToString();
                    string todated = context.Request["todate"].ToString();
                    strmsg = DailyChequeWise(SubDivisiond, CounterNamed, Bank, User, fromdated, todated, userid);
                    break;
                case "DailySubDWiseSummary":
                    string SubDivision4 = context.Request["ddlSubDivision"].ToString();
                    string CounterName4 = context.Request["ddlCounterName"].ToString();
                    string fromdate4 = context.Request["fromdate"].ToString();
                    string todate4 = context.Request["todate"].ToString();
                    strmsg = DailySubDWiseSummary(SubDivision4, CounterName4, fromdate4, todate4, userid);
                    break;

                case "DailyCSUserWise":

                    string CounterNameu = context.Request["ddlCounterName"].ToString();
                    string Useru = context.Request["User"].ToString();
                    string fromdateu = context.Request["fromdate"].ToString();
                    string todateu = context.Request["todate"].ToString();
                    strmsg = DailyCSUserWise(CounterNameu, Useru, fromdateu, todateu, userid);
                    break;

                case "DailyLoginLogout":

                    string CounterName7 = context.Request["ddlCounterName"].ToString();
                    string fromdate7 = context.Request["fromdate"].ToString();
                    string todate7 = context.Request["todate"].ToString();
                    strmsg = DailyLoginLogout(CounterName7, fromdate7, todate7, userid);
                    break;

                case "DatewiseSPS":

                    string CounterNameDS = context.Request["ddlCounterName"].ToString();
                    string fromdateDS = context.Request["fromdate"].ToString();
                    string todateDS = context.Request["todate"].ToString();
                    strmsg = DatewiseSPS(CounterNameDS, fromdateDS, todateDS, userid);
                    break;

                case "DatewiseSTS":

                    string CounterNameDST = context.Request["ddlCounterName"].ToString();
                    string fromdateDST = context.Request["fromdate"].ToString();
                    string todateDST = context.Request["todate"].ToString();
                    strmsg = DatewiseSTS(CounterNameDST, fromdateDST, todateDST, userid);
                    break;
                case "DatewiseSPTS":

                    string SDO = context.Request["SubDivision"].ToString();
                    string CounterNameSPTS = context.Request["ddlCounterName"].ToString();
                    string fromdateSPTS = context.Request["fromdate"].ToString();
                    string todateSPTS = context.Request["todate"].ToString();
                    strmsg = DatewiseSPTS(SDO, CounterNameSPTS, fromdateSPTS, todateSPTS, userid);
                    break;

                case "CancelTrasaction":
                    string CounterNameCT = context.Request["ddlCounterName"].ToString();
                    string fromdateCT = context.Request["fromdate"].ToString();
                    string todateCT = context.Request["todate"].ToString();
                    strmsg = CancelTrasaction(CounterNameCT, fromdateCT, todateCT, userid);
                    break;
                case "Populatecouter":
                    strmsg = PopulateCounterUserwise(userid);
                    break;

            }
            context.Response.Write(strmsg);
        }
        private string PopulateCounterUserwise(string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.UserName = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.PopulateCounterUserwise(reportProp);
            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(dt.Rows[i]["counterid"].ToString() + "#" + dt.Rows[i]["countername"].ToString() + "$");
                    }
                    return sb.ToString();
                }
                else
                    return "0#No Data Found$";
            }
        }
        private string CancelTrasaction(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.CancelTrasaction(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=11>" + "Cancellation Transaction Report" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

                }
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {

                    sb.Append("$");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {

                            sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

                        }
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td colspan=11>No Record Found</td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DatewiseSPTS(string sdo, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = sdo;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPTS(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=7>" + "Date wise & Subdivision wise Payment transaction Summary" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

                }
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {

                    sb.Append("$");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        sb.Append("<tr>");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {

                            sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

                        }
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }
        private string DatewiseSTS(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSTS(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan='11'>" + "Date wise Subdivision wise Transaction Summary" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

                }
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        sb.Append("$");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            sb.Append("<tr>");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {

                                sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

                            }
                            sb.Append("</tr>");
                        }
                    }
                    else
                        sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DatewiseSPS(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPS(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan='11'>" + "Date wise Subdivision wise Payment Summary" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

                }
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        sb.Append("$");


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            sb.Append("<tr>");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {

                                sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

                            }
                            sb.Append("</tr>");
                        }
                    }
                    else
                    {
                        sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }
        private string DailyLoginLogout(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyLoginLogout(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=5>" + "Daily Login Logout Report" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "Sr No" + "</th>");
                sb.Append("<th>" + "COUNTERNAME" + "</th>");
                sb.Append("<th>" + "USERNAME" + "</th>");

                sb.Append("<th>" + "LOGINDATE" + "</th>");
                sb.Append("<th>" + "LOGOUTDATE" + "</th>");
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["COUNTERNAME"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["USERNAME"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["LOGINDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["LOGOUTDATE"].ToString() + "</td>");


                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DailySubDWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailySubDWiseSummary(reportProp);
            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=7>" + "Daily Sub Division Wise Summary" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");


                sb.Append("<th>" + "SDO Name" + "</th>");
                sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
                sb.Append("<th>" + "TOTALCASHS Amount" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ Amount" + "</th>");
                sb.Append("<th>" + "Total Stub" + "</th>");
                sb.Append("<th>" + "TotalAmount" + "</th>");

                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCASHSAMount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCHQAmount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DailyCRTransactionWise(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyCRTransactionWise(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=12>" + "Daily Cheque Report Transaction Wise" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "Sr No" + "</th>");
                sb.Append("<th>" + "SDO" + "</th>");
                sb.Append("<th>" + "SDO Name" + "</th>");

                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "PAYDATE" + "</th>");
                sb.Append("<th>" + "CNT NO" + "</th>");
                sb.Append("<th>" + "REC NO" + "</th>");
                sb.Append("<th>" + "AMOUNT" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th>" + "CHQ DATE" + "</th>");
                sb.Append("<th>" + "CHEQUENO" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CNT NO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["REC NO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DailyChequeWise(string SubDivision, string CounterName, string Bank, string User, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.Bank = Bank;
            reportProp.UserName = User;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyChequeWise(reportProp);

            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=9>" + "Daily Cheque Report Cheque Wise" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "Sr No" + "</th>");
                sb.Append("<th>" + "SDO" + "</th>");
                sb.Append("<th>" + "SDO Name" + "</th>");

                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "CHEQUENO" + "</th>");
                sb.Append("<th>" + "CHQ DATE" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th>" + "UNDER A/C" + "</th>");

                sb.Append("<th>" + "AMOUNT" + "</th>");

                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK NAME"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["UNDER A/C"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string repReceiptno(string SubDivision, string CounterName, string fromdate, string todate, string userid, string Receiptno)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.Receiptno = Receiptno;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.Receiptno(reportProp);



            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=10>" + "Cashier location allocation report" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "Sr No" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "PAYDATE" + "</th>");
                sb.Append("<th>" + "COUNTERNO" + "</th>");
                sb.Append("<th>" + "ReceiptNo" + "</th>");
                sb.Append("<th>" + "AMOUNT" + "</th>");
                sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
                sb.Append("<th>" + "CHEQUENO" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("<th>" + "Print" + "</th>");
                sb.Append("</tr>");

                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/print.jpeg' alt='delete' onclick=PrintReceipt('");
                        sb.Append(dt.Rows[i]["paymentid"].ToString().Trim() + "') /></td>");

                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string DailyCSUserWise(string CounterName, string user, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.UserName = user;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyCSUserWise(reportProp);
            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=7>" + "Daily Collection Summary User Wise" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");


                sb.Append("<th>" + "User Name" + "</th>");
                sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
                sb.Append("<th>" + "TOTALCASHS Amount" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ Amount" + "</th>");
                sb.Append("<th>" + "Total Stub" + "</th>");
                sb.Append("<th>" + "TotalAmount" + "</th>");

                sb.Append("</tr>");


                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>" + dt.Rows[i]["username"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCASHSAMount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCHQAmount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }
        private string filluser(int CounterId)
        {
            counter_Prop counterProp = new counter_Prop();
            counterProp.Counterid = CounterId;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.fillUser(counterProp);



            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {

                if (dt.Rows.Count > 0)
                {

                    StringBuilder sbtxt = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbtxt.Append(dt.Rows[i]["UserID"].ToString() + "#" + dt.Rows[i]["UserName"].ToString() + "$");
                    }
                    return sbtxt.ToString();
                }
                else
                    return "0#No Data Found$";
            }


            return sb.ToString();
        }
        private string DailyPCSheet(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;

            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.ReportDailyPCSheet(reportProp);



            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=9>" + "Daily Payment ControlSheet" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "Sr No" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "PAYDATE" + "</th>");
                sb.Append("<th>" + "COUNTERNO" + "</th>");
                sb.Append("<th>" + "ReceiptNo" + "</th>");
                sb.Append("<th>" + "AMOUNT" + "</th>");
                sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
                sb.Append("<th>" + "CHEQUENO" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("</tr>");

                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string PaymentData(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;

            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.PaymentData(reportProp);



            StringBuilder sb = new StringBuilder();

            if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
            {
                return "0#" + reportbal.ErrMessage + "$";
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<th colspan=15>" + "Daily Payment Data" + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>" + "SDO" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "Counter No" + "</th>");
                sb.Append("<th>" + "ReceiptNo" + "</th>");
                sb.Append("<th>" + "AMOUNT" + "</th>");
                sb.Append("<th>" + "RDATE" + "</th>");
                sb.Append("<th>" + "CHEQUENO" + "</th>");
                sb.Append("<th>" + "CHQ DATE" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th>" + "Mode Of Payment" + "</th>");
                sb.Append("<th>" + "Status" + "</th>");
                sb.Append("<th>" + "CounterName" + "</th>");
                sb.Append("<th>" + "Entered Time" + "</th>");
                sb.Append("<th>" + "Consumer Name" + "</th>");
                sb.Append("</tr>");

                if (dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Counter No"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Mode Of Payment"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Status"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CounterName"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Entered Time"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Consumer Name"].ToString() + "</td>");


                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }


        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}