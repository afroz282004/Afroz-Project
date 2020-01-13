using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Protocols;
using JVVNLClassLIB;
using System.Collections.Generic;
using iTextSharp.text;

using iTextSharp.text.pdf;

using iTextSharp.text.html;

using iTextSharp.text.html.simpleparser;

namespace JVVNLWeb
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    if (Convert.ToString(Session["username"]) == "administrator"  )
                    {
                        FillReportForAdmin();
                    }
                    FillCounter();
                    if (hdncounter.Value == "")
                    {
                        // string name = Session["username"].ToString();
                        FillhdnCounterName();
                        hdncounter.Value = ddlCounterName.SelectedValue;
                        counterid.Value = ddlCounterName.SelectedValue;
                    }
                    FillSubDivision();
                    Fillbank();
                }

             
            }
        }
        private void FillReportForAdmin()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----------Select------------", "0"));
            ddlReportType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Daily Payment ControlSheet", "1"));
            ddlReportType.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Daily Cheque Report Transaction Wise", "2"));
            ddlReportType.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Daily Cheque Report Cheque Wise", "3"));
            ddlReportType.Items.Insert(4, new System.Web.UI.WebControls.ListItem("Daily Sub Division Wise Summary", "4"));
            ddlReportType.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Daily Date SubDivisionWise Summary", "5"));
            ddlReportType.Items.Insert(6, new System.Web.UI.WebControls.ListItem("Daily Collection Summary User Wise", "6"));
            ddlReportType.Items.Insert(7, new System.Web.UI.WebControls.ListItem("Daily Login Logout Report", "7"));
            ddlReportType.Items.Insert(8, new System.Web.UI.WebControls.ListItem("Cancellation Transaction Report", "8"));
            ddlReportType.Items.Insert(9, new System.Web.UI.WebControls.ListItem("Date wise Subdivision wise Payment Summary", "9"));
            ddlReportType.Items.Insert(10, new System.Web.UI.WebControls.ListItem("Date wise Subdivision wise Transaction Summary", "10"));
            ddlReportType.Items.Insert(11, new System.Web.UI.WebControls.ListItem("Date wise & Subdivision wise Payment transaction Summary", "11"));
            ddlReportType.Items.Insert(12, new System.Web.UI.WebControls.ListItem("Cashier location allocation report", "12"));
            ddlReportType.Items.Insert(13, new System.Web.UI.WebControls.ListItem("Print Receipt", "13"));
            ddlReportType.Items.Insert(14, new System.Web.UI.WebControls.ListItem("Download Payment Information", "14"));
        }
        private void FillhdnCounterName()
        {
            report_Prop reportProp = new report_Prop();
            reportProp.UserName = Session["username"].ToString();
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.PopulateCounterUserwise(reportProp);
            ddlCounterName.SelectedValue = dt.Rows[0]["counterid"].ToString();

        }
        private void FillSubDivision()
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = "";
            sdoprop.SDOName = "";
            sdo_bal sdobal = new sdo_bal();
            DataTable dt = sdobal.SDOSearch(sdoprop);
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataValueField = "sdocode";
            ddlSubDivision.DataTextField = "sdoname";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----------Select------------", "select"));
            ddlSubDivision.Items.Insert(1, new System.Web.UI.WebControls.ListItem("----------ALL------------", "ALL"));
            
        }

        private void FillCounter()
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.CounterName = "";
            counter_bal counterbal = new counter_bal();
            DataTable dt = counterbal.CounterSearch(counterprop);
            ddlCounterName.DataSource = dt;
            ddlCounterName.DataValueField = "counterid";
            ddlCounterName.DataTextField = "countername";
            // ddlCounterName.Items.Add(new ListItem("Select","0"));
            ddlCounterName.DataBind();
            ddlCounterName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----------Select------------", "select"));
            ddlCounterName.Items.Insert(1, new System.Web.UI.WebControls.ListItem("----------ALL------------", "ALL"));
        }

        private void Fillbank()
        {
            bank_Prop bankprop = new bank_Prop();
            bankprop.BankName = "";
            bankprop.BankCode = "";
            bank_bal bankbal = new bank_bal();
            DataTable dt = bankbal.BankSearch(bankprop);
            ddlBank.DataSource = dt;
            ddlBank.DataValueField = "BankCode";
            ddlBank.DataTextField = "bankname";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----------Select------------", "select"));
            ddlBank.Items.Insert(1, new System.Web.UI.WebControls.ListItem("----------ALL------------", "ALL"));
        }
        protected void ddlCounterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCounterName.SelectedValue == "" || ddlCounterName.SelectedValue == "ALL")
            //{
            //    FillUser(0);
            //    hdncounter.Value = ddlCounterName.SelectedValue;
            //}
            //else
            //{
            //    if (ddlCounterName.SelectedValue != "select")
            //    {
            //        FillUser(Convert.ToInt32(ddlCounterName.SelectedValue));
            //        hdncounter.Value = ddlCounterName.SelectedValue;
            //    }
            //}
        }
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowVisible();
        }

        private void ShowVisible()
        {
            int ddlVal = Convert.ToInt32(ddlReportType.SelectedValue);


            if (ddlVal == 0)
            {
                SubDivision.Visible = false;
                ddlSubDivision.Visible = false;
                CounterName.Visible = false;
                ddlCounterName.Visible = false;
                txtfromdate.Visible = false;
                lFromDate.Visible = false;
                lTodate.Visible = false;
                txttodate.Visible = false;
                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = false;
                ddlUser.Visible = false;

                lselectDate.Visible = false;
                txtselectDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;
            }

            if (ddlVal == 1 || ddlVal == 2 || ddlVal == 4 || ddlVal == 5 || ddlVal == 11 || ddlVal == 14)
            {
                SubDivision.Visible = true;
                ddlSubDivision.Visible = true;

                CounterName.Visible = true;

                ddlCounterName.Visible = true;
                if (counterid.Value != "0" && counterid.Value != "" && counterid.Value != "select")
                {
                    ddlCounterName.SelectedValue = counterid.Value;
                    ddlCounterName.Enabled = false;
                };
                if (Session["sdocode"] != null)
                {
                    string sdodata = Convert.ToString(Session["sdocode"]);
                    ddlSubDivision.SelectedValue = sdodata;
                    ddlSubDivision.Enabled = false;
                }
                txtfromdate.Visible = true;
                lFromDate.Visible = true;

                lTodate.Visible = true;
                txttodate.Visible = true;


                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = false;
                ddlUser.Visible = false;

                lselectDate.Visible = false;
                txtselectDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;

            }


            if (ddlVal == 3)
            {


                SubDivision.Visible = true;
                ddlSubDivision.Visible = true;
                lBank.Visible = true;
               
                ddlBank.Visible = true;
                lUser.Visible = true;
                ddlUser.Visible = true;
                CounterName.Visible = true;

                ddlCounterName.Visible = true; ;
                if (counterid.Value != "0" && counterid.Value != "" && counterid.Value != "select")
                {
                    ddlCounterName.SelectedValue = counterid.Value;
                    ddlCounterName.Enabled = false;
                };
                if (Session["sdocode"] != null)
                {
                    string sdodata = Convert.ToString(Session["sdocode"]);
                    ddlSubDivision.SelectedValue = sdodata;
                    ddlSubDivision.Enabled = false;
                }

                txtfromdate.Visible = true;
                lFromDate.Visible = true;
              
                lTodate.Visible = true;
                txttodate.Visible = true;
                txtselectDate.Visible = false;
                lselectDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;
            }
            else
            {


            }

            if (ddlVal == 6)
            {



                CounterName.Visible = true;

                ddlCounterName.Visible = true;
                if (counterid.Value != "0" && counterid.Value != "" && counterid.Value != "select")
                {
                    ddlCounterName.SelectedValue = counterid.Value;
                    ddlCounterName.Enabled = false;
                };
                txtfromdate.Visible = true;
                lFromDate.Visible = true;

                lTodate.Visible = true;
                txttodate.Visible = true;

                SubDivision.Visible = false;
                ddlSubDivision.Visible = false;
                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = true;
                ddlUser.Visible = true;


                lselectDate.Visible = false;
                txtselectDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;

            }

            if (ddlVal == 7 || ddlVal == 8 || ddlVal == 9 || ddlVal == 10)
            {
                CounterName.Visible = true;

                ddlCounterName.Visible = true;
                if (counterid.Value != "0" && counterid.Value != "" && counterid.Value != "select")
                {
                    ddlCounterName.SelectedValue = counterid.Value;
                    ddlCounterName.Enabled = false;
                };
                txtfromdate.Visible = true;
                lFromDate.Visible = true;

                lTodate.Visible = true;
                txttodate.Visible = true;

                SubDivision.Visible = false;
                ddlSubDivision.Visible = false;


                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = false;
                ddlUser.Visible = false;

                lselectDate.Visible = false;
                txtselectDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;

            }
            else
            {

            }

            if (ddlVal == 12)
            {

                lselectDate.Visible = true;
                txtselectDate.Visible = true;

                SubDivision.Visible = false;
                ddlSubDivision.Visible = false;

                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = false;
                ddlUser.Visible = false;

                lTodate.Visible = false;
                txttodate.Visible = false;

                txtfromdate.Visible = false;
                lFromDate.Visible = false;
                lReceipt.Visible = false;
                txtReceipt.Visible = false;
            }
            else
            {


            }
            if (ddlVal == 13)
            {
                SubDivision.Visible = true;
                ddlSubDivision.Visible = true;

                CounterName.Visible = true;

                ddlCounterName.Visible = true;
                if (counterid.Value != "0" && counterid.Value != "" && counterid.Value != "select")
                {
                    ddlCounterName.SelectedValue = counterid.Value;
                    ddlCounterName.Enabled = false;
                };
                if (Session["sdocode"] != null)
                {
                    string sdodata = Convert.ToString(Session["sdocode"]);
                    ddlSubDivision.SelectedValue = sdodata;
                    ddlSubDivision.Enabled = false;
                }

                txtfromdate.Visible = true;
                lFromDate.Visible = true;

                lTodate.Visible = true;
                txttodate.Visible = true;

                lReceipt.Visible = true;
                txtReceipt.Visible = true;

                lBank.Visible = false;
                ddlBank.Visible = false;
                lUser.Visible = false;
                ddlUser.Visible = false;


                lselectDate.Visible = false;
                txtselectDate.Visible = false;
            }
          

            if (Session["groupid"].ToString() == "1")
            {
                ddlCounterName.Enabled = true;
            }
        }

        //private void FillUser(int l_intCounterID)
        //{
        //    counter_Prop counterProp = new counter_Prop();
        //    counterProp.Counterid = l_intCounterID;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.fillUser(counterProp);

        //    ddlUser.DataSource = dt;
        //    ddlUser.DataValueField = "UserID";
        //    ddlUser.DataTextField = "UserName";
        //    ddlUser.DataBind();
        //}

        private void ShowDropDown()
        {
            int l_intReportID = Convert.ToInt32(ddlReportType.SelectedValue);
            if (l_intReportID == 1 || l_intReportID == 2 || l_intReportID == 4 || l_intReportID == 11 || l_intReportID == 14)
            {

            }

        }

        # region


        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string userid = "";
            if (Session["username"].ToString() != null)
            {
                userid = Session["username"].ToString();
            }

            string strmsg = "";
            thead.InnerHtml = "";
            tbody.InnerHtml = "";
            if (ddlReportType.SelectedValue == "1")
            {
                string SubDivision = ddlSubDivision.SelectedValue;
                string CounterName = ddlCounterName.SelectedValue;
                string fromdate = txtfromdate.Text;
                string todate = txttodate.Text;
                strmsg = RepDailyPCSheet(SubDivision, CounterName, fromdate, todate, userid);


            }//end of if

            if (ddlReportType.SelectedValue == "2")
            {
                string SubDivision1 = ddlSubDivision.SelectedValue;
                string CounterName1 = ddlCounterName.SelectedValue;
                string fromdate1 = txtfromdate.Text;
                string todate1 = txttodate.Text;
                strmsg = RepDailyCRTransactionWise(SubDivision1, CounterName1, fromdate1, todate1, userid);
            }//end of if

            if (ddlReportType.SelectedValue == "3")
            {
                string SubDivisiond = ddlSubDivision.SelectedValue;
                string CounterNamed = ddlCounterName.SelectedValue;
                string Bank = ddlBank.SelectedValue;
                string User = ddlUser.SelectedValue;
                string fromdated = txtfromdate.Text;
                string todated = txttodate.Text;
                strmsg = RepDailyChequeWise(SubDivisiond, CounterNamed, Bank, User, fromdated, todated, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "4")
            {
                string SubDivision4 = ddlSubDivision.SelectedValue;
                string CounterName4 = ddlCounterName.SelectedValue;
                string fromdate4 = txtfromdate.Text;
                string todate4 = txttodate.Text;
                strmsg = RepDailySubDWiseSummary(SubDivision4, CounterName4, fromdate4, todate4, userid);


            }//end of if

            if (ddlReportType.SelectedValue == "5")
            {
                string SubDivision4 = ddlSubDivision.SelectedValue;
                string CounterName4 = ddlCounterName.SelectedValue;
                string fromdate4 = txtfromdate.Text;
                string todate4 = txttodate.Text;
                strmsg = RepDailyDateSubDivisionWiseSummary(SubDivision4, CounterName4, fromdate4, todate4, userid);
                //strmsg = RepDailySubDWiseSummary(SubDivision4, CounterName4, fromdate4, todate4, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "6")
            {
                string CounterNameu = ddlCounterName.SelectedValue;
                string Useru = ddlUser.SelectedValue;
                string fromdateu = txtfromdate.Text;
                string todateu = txttodate.Text;
                strmsg = RepDailyCSUserWise(CounterNameu, Useru, fromdateu, todateu, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "7")
            {
                string CounterName7 = ddlCounterName.SelectedValue;
                string fromdate7 = txtfromdate.Text;
                string todate7 = txttodate.Text;
                strmsg = RepDailyLoginLogout(CounterName7, fromdate7, todate7, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "8")
            {
                string CounterNameCT = ddlCounterName.SelectedValue;
                string fromdateCT = txtfromdate.Text;
                string todateCT = txttodate.Text;
                strmsg = RepCancelTrasaction(CounterNameCT, fromdateCT, todateCT, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "9")
            {
                string CounterNameDS = ddlCounterName.SelectedValue;
                string fromdateDS = txtfromdate.Text;
                string todateDS = txttodate.Text;
                strmsg = RepDatewiseSPS(CounterNameDS, fromdateDS, todateDS, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "10")
            {
                string CounterNameDST = ddlCounterName.SelectedValue;
                string fromdateDST = txtfromdate.Text;
                string todateDST = txttodate.Text;
                strmsg = RepDatewiseSTS(CounterNameDST, fromdateDST, todateDST, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "11")
            {
                string SDO = ddlSubDivision.SelectedValue;
                string CounterNameSPTS = ddlCounterName.SelectedValue;
                string fromdateSPTS = txtfromdate.Text;
                string todateSPTS = txttodate.Text;
                strmsg = RepDatewiseSPTS(SDO, CounterNameSPTS, fromdateSPTS, todateSPTS, userid);


            }//end of if
            if (ddlReportType.SelectedValue == "13")
            {
                string rSubDivision = ddlSubDivision.SelectedValue;
                string rCounterName = ddlCounterName.SelectedValue;
                string rfromdate = txtfromdate.Text;
                string rtodate = txttodate.Text;
                string Receiptno = txtReceipt.Text;
                strmsg = repReceiptno(rSubDivision, rCounterName, rfromdate, rtodate, userid, Receiptno);


            }//end of if
            if (ddlReportType.SelectedValue == "14")
            {

                string SubDivisionnew = ddlSubDivision.SelectedValue;
                string CounterNamenew = ddlCounterName.SelectedValue;
                string fromdatenew = txtfromdate.Text;
                string todatenew = txttodate.Text;
                strmsg = RepPaymentData(SubDivisionnew, CounterNamenew, fromdatenew, todatenew, userid);



            }//end of if
            string[] strSplitArr = strmsg.Split('$');

            thead.InnerHtml = strSplitArr[0].ToString();
            if (strSplitArr.Length > 1)
            {
                if (strSplitArr[1].ToString() != "")
                {

                    tbody.InnerHtml = strSplitArr[1].ToString();
                }
            }

        }//end of function

        # region "Export To Excel "

        protected void btnexport_click(object sender, EventArgs e)
        {
            int reportname = int.Parse(ddlReportType.SelectedValue);
            if (Session["username"].ToString() == "")
            {

                if (reportname == 1)
                {

                    DataTable dt = new DataTable();
                    dt = exporttoexcelexport(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Payment ControlSheet");
                    }


                }
                else if (reportname == 2)
                {

                    DataTable dt = new DataTable();
                    dt = dailycheckexport(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Cheque Report Transaction Wise");
                    }

                }


                else if (reportname == 3)
                {

                    DataTable dt = new DataTable();
                    dt = DailyChequeWiseexport(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, ddlBank.SelectedValue, ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Cheque Report Cheque Wise");
                    }

                }



                else if (reportname == 4)
                {

                    DataTable dt = new DataTable();
                    dt = DailySubDWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Sub Division Wise Summary");
                    }


                }
                else if (reportname == 5)
                {

                    DataTable dt = new DataTable();
                    dt = DailyDateSubDivisionWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Date SubDivisionWise Summary");
                    }


                }
                else if ( reportname == 13 || reportname == 12)
                {



                }
                else if (reportname == 6)
                {

                    DataTable dt = new DataTable();
                    dt = DailyCSUserWise(ddlCounterName.SelectedValue, ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Collection Summary User Wise");
                    }


                }

                else if (reportname == 7)
                {

                    DataTable dt = new DataTable();
                    dt = DailyLoginLogoutexport(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Daily Login Logout Report");
                    }


                }
                else if (reportname == 8)
                {

                    DataTable dt = new DataTable();
                    dt = CancelTrasactionexport(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Cancellation Transaction Report");
                    }


                }

                else if (reportname == 9)
                {
                    DataTable dt = new DataTable();
                    dt = DatewiseSPSexport(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Date wise Subdivision wise Payment Summary");
                    }




                }

                else if (reportname == 10)
                {

                    DataTable dt = new DataTable();
                    dt = DatewiseSTSexport(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Date wise Subdivision wise Transaction Summary");
                    }


                }
                else if (reportname == 11)
                {

                    DataTable dt = new DataTable();
                    dt = DatewiseSPTSexport(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        es(dt, "Date wise & Subdivision wise Payment transaction Summary");
                    }


                }
            }
            else
            {

                if (reportname == 1)
                {

                    DataTable dt = new DataTable();
                    dt = exporttoexcel(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt!=null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Payment ControlSheet");
                    }



                }

                if (reportname == 14)
                {

                    DataTable dt = new DataTable();
                    dt = PaymentData(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Download Payment Data");
                    }



                }


                else if (reportname == 2)
                {

                    DataTable dt = new DataTable();
                    dt = dailycheck(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Cheque Report Transaction Wise");
                    }

                }


                else if (reportname == 3)
                {

                    DataTable dt = new DataTable();
                    dt = DailyChequeWise(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, ddlBank.SelectedValue, ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Cheque Report Cheque Wise");
                    }

                }



                else if (reportname == 4)
                {

                    DataTable dt = new DataTable();
                    dt = DailySubDWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Sub Division Wise Summary");
                    }


                }
                else if (reportname == 5)
                {

                    DataTable dt = new DataTable();
                    dt = DailyDateSubDivisionWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Date SubDivisionWise Summary");
                    }


                }
                else if ( reportname == 13 || reportname == 12)
                {

                    DataTable dt = new DataTable();
                    dt = printreceipt(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString(), txtReceipt.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Print Receipt Details");
                    }
                }
                else if (reportname == 6)
                {

                    DataTable dt = new DataTable();
                    dt = DailyCSUserWise(ddlCounterName.SelectedValue, ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Collection Summary User Wise");
                    }


                }

                else if (reportname == 7)
                {

                    DataTable dt = new DataTable();
                    dt = DailyLoginLogout(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Daily Login Logout Report");
                    }


                }
                else if (reportname == 8)
                {

                    DataTable dt = new DataTable();
                    dt = CancelTrasaction(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Cancellation Transaction Report");
                    }


                }

                else if (reportname == 9)
                {
                    DataTable dt = new DataTable();
                    dt = DatewiseSPS(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ex(dt, "Date wise Subdivision wise Payment Summary");
                        }

                    }

                }

                else if (reportname == 10)
                {

                    DataTable dt = new DataTable();
                    dt = DatewiseSTS(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Date wise Subdivision wise Transaction Summary");
                    }



                }
                else if (reportname == 11)
                {

                    DataTable dt = new DataTable();
                    dt = DatewiseSPTS(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ex(dt, "Date wise & Subdivision wise Payment transaction Summary");
                    }


                }

            }
        }

        private DataTable exporttoexcelexport(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;

            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.ReportDailyPCSheetexport(reportProp);


            return dt;


        }
        private DataTable exporttoexcel(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "Excel";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.ReportDailyPCSheet(reportProp);


            return dt;


        }



        private DataTable PaymentData(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;

            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.PaymentData(reportProp);


            return dt;


        }
        private DataTable DatewiseSPTS(string sdo, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = sdo;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPTS(reportProp);

            return dt;
        }

        private DataTable DatewiseSPTSexport(string sdo, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = sdo;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPTSexport(reportProp);

            return dt;
        }


        private DataTable DatewiseSTSexport(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSTSexport(reportProp);

            return dt;
        }
        private DataTable DatewiseSTS(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSTS(reportProp);

            return dt;
        }

        private DataTable DatewiseSPSexport(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPSexport(reportProp);

            return dt;
        }
        private DataTable DatewiseSPS(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DatewiseSPS(reportProp);

            return dt;
        }

        private DataTable dailycheckexport(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "0";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyCRTransactionWiseexport(reportProp);

            return dt;


        }
        private DataTable dailycheck(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "0";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyCRTransactionWise(reportProp);

            return dt;


        }


        private DataTable DailyChequeWiseexport(string SubDivision, string CounterName, string Bank, string User, string fromdate, string todate, string userid)
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
            DataTable dt = reportbal.DailyChequeWiseexport(reportProp);

            return dt;
        }
        private DataTable DailyChequeWise(string SubDivision, string CounterName, string Bank, string User, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.Bank = Bank;
            reportProp.UserName = User;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyChequeWise(reportProp);

            return dt;
        }

        private DataTable DailySubDWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "0";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailySubDWiseSummary(reportProp);

            return dt;
        }
        private DataTable DailyDateSubDivisionWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "0";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyDateSubDivisionWiseSummary(reportProp);

            return dt;
        }
        
        private DataTable printreceipt(string SubDivision, string CounterName, string fromdate, string todate, string userid, string ReceiptNo)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.Receiptno = ReceiptNo;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.Receiptno(reportProp);

            return dt;
        }


        private DataTable DailyCSUserWise(string CounterName, string user, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.UserName = user;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "Excel";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyCSUserWise(reportProp);

            return dt;
        }

        private DataTable DailyLoginLogoutexport(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyLoginLogoutexport(reportProp);

            return dt;
        }
        private DataTable DailyLoginLogout(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyLoginLogout(reportProp);

            return dt;
        }

        private DataTable CancelTrasactionexport(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.CancelTrasactionexport(reportProp);

            return dt;
        }
        private DataTable CancelTrasaction(string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.CancelTrasaction(reportProp);

            return dt;
        }
        protected void es(DataTable dataTable, string fileName)
        {
            DataSet dsExport = new DataSet();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            dgGrid.DataSource = dataTable;



            HttpContext context = HttpContext.Current;

            context.Response.Clear();
            foreach (DataColumn column in dataTable.Columns)
            {

                context.Response.Write(column.ColumnName + ",");

            }

            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in dataTable.Rows)
            {

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {

                    context.Response.Write("=\"" + row[i].ToString() + "\",");

                }

                context.Response.Write(Environment.NewLine);

            }
            context.Response.ContentType = "text/csv";
            //Response.Write("<style> .text {mso-number-format:\@; } </style>");


            context.Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + ".csv");

            context.Response.End();
        }

        protected void ex(DataTable dataTable, string fileName)
        {
            DataSet dsExport = new DataSet();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            dgGrid.DataSource = dataTable;

           
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel

            if ((fileName + "-").ToLower().Contains(".xlsx-"))
            {
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else
            {
                response.ContentType = "application/vnd.ms-excel";
            }



            response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    GridView gvExport = new GridView();
                    gvExport.DataSource = dataTable;
                    gvExport.DataBind();
                    //(start): require for date format issue
                  
                    string strBankName = "";
                    if (ddlBank.SelectedValue == "ALL")
                    {
                        strBankName = ddlBank.SelectedValue;
                    }
                    else
                    {
                        strBankName = ddlBank.SelectedItem.Text;
                    }
                    string strSubDivision = "";
                    if (ddlSubDivision.SelectedValue == "ALL")
                    {
                        strSubDivision = ddlSubDivision.SelectedValue;
                    }
                    else
                    {
                        strSubDivision = ddlSubDivision.SelectedItem.Text;
                    }

                    string strCounterName = "";
                    if (ddlCounterName.SelectedValue == "ALL")
                    {
                        strCounterName = ddlCounterName.SelectedValue;
                    }
                    else
                    {
                        strCounterName = ddlCounterName.SelectedItem.Text;
                    }
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    if (ddlReportType.SelectedValue == "1")
                    {
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> Daily Activity Report Control Sheet&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name : " + strCounterName + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SDO Code : " + strSubDivision + " </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> User ID : " + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date From : " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp; " + txttodate.Text + " </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> Payment Entry  </font></b><br/><br/>");
                    }


                    if (ddlReportType.SelectedValue == "2")
                    {


                        hw.WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> DAILY CHEQUE WISE COLLECTION REPORT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SubDivision&nbsp;&nbsp;&nbsp;&nbsp;  " + strSubDivision + " </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>Userid:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + txttodate.Text + " </font></b><br/>");

                        foreach (GridViewRow r in gvExport.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                                {
                                    r.Cells[columnIndex].Attributes.Add("class", "text");
                                }
                            }
                        }
                    }

                    if (ddlReportType.SelectedValue == "3")
                    {


                        hw.WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> Cheque Summary From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp; " + txttodate.Text + " &nbsp;&nbsp;&nbsp;&nbsp;SubDivision:&nbsp;&nbsp;&nbsp;&nbsp " + strSubDivision + "&nbsp;&nbsp;Bank Type:&nbsp;&nbsp;&nbsp;&nbsp " + strBankName + " </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> User:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + " </font></b><br/>");
                        foreach (GridViewRow r in gvExport.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                                {
                                    r.Cells[columnIndex].Attributes.Add("class", "text");
                                }
                            }
                        }
                    }
                    if (ddlReportType.SelectedValue == "4")
                    {


                        hw.WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> SDO Wise Collection Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>  From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp; Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + "  </font></b><br/>");

                        foreach (GridViewRow r in gvExport.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                                {
                                    r.Cells[columnIndex].Attributes.Add("class", "text");
                                }
                            }
                        }
                    }

                    if (ddlReportType.SelectedValue == "5")
                    {


                        hw.WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> Daily Date SubDivisionWise Summary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>  From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;" + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp; Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + "  </font></b><br/>");

                        foreach (GridViewRow r in gvExport.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                                {
                                    r.Cells[columnIndex].Attributes.Add("class", "text");
                                }
                            }
                        }
                    }

                    if (ddlReportType.SelectedValue == "6")
                    {

                        hw.WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                        hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> User Wise Collection Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter : " + strCounterName + "  </font></b><br/>");
                        hw.WriteLine("<b><font size='2'> From:&nbsp;" + txtfromdate.Text + "&nbsp;To:&nbsp;" + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DateTime.Now + " </font></b><br/><br/>");
            
                        foreach (GridViewRow r in gvExport.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                                {
                                    r.Cells[columnIndex].Attributes.Add("class", "text");
                                }
                            }
                        }
                    }
                    if (ddlReportType.SelectedValue == "7" || ddlReportType.SelectedValue == "8" || ddlReportType.SelectedValue == "9" || ddlReportType.SelectedValue == "10" || ddlReportType.SelectedValue == "11" ||  ddlReportType.SelectedValue == "13" || ddlReportType.SelectedValue == "14")
                    {
                        hw.WriteLine("<b><font size='4'>  " + fileName + " </font></b>");
                    }
                    //(end): require for date format issue
                    gvExport.RenderControl(htw);
                    //(start): require for date format issue
                    System.Text.StringBuilder style = new System.Text.StringBuilder();
                    style.Append("<style>");
                    style.Append("." + "text" + " { mso-number-format:" + "\\@;" + " }");
                    style.Append("</style>");
                    response.Clear();
                    Response.Buffer = true;
                    //response.Charset = "";
                    //response.Write(sw.ToString());
                    Response.Write(style.ToString());
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    //(end): require for date format issue
                    try
                    {
                        response.End();
                    }
                    catch (Exception err)
                    {

                    }
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
            }


        }
        //protected void ex(DataTable dataTable, string fileName)
        //{
        //    DataSet dsExport = new DataSet();
        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //    DataGrid dgGrid = new DataGrid();

        //    dgGrid.DataSource = dataTable;

        //    HttpResponse response = HttpContext.Current.Response;
        //    response.Clear();
        //    response.Charset = "";
        //    // set the response mime type for excel 

        //    if ((fileName + "-").ToLower().Contains(".xlsx-"))
        //    {
        //        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    }
        //    else
        //    {
        //        response.ContentType = "application/vnd.ms-excel";
        //    }
        //    response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

        //    // create a string writer 
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //        {
        //            // instantiate a datagrid 
        //            GridView gvExport = new GridView();
        //            gvExport.DataSource = dataTable;
        //            gvExport.DataBind();
        //            //(start): require for date format issue
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);
        //            hw.WriteLine("<b><u><font size='5'> JVVNL " + fileName + " </font></u></b>");
        //            foreach (GridViewRow r in gvExport.Rows)
        //            {
        //                if (r.RowType == DataControlRowType.DataRow)
        //                {
        //                    for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
        //                    {
        //                        r.Cells[columnIndex].Attributes.Add("class", "text");
        //                    }
        //                }
        //            }
        //            //(end): require for date format issue
        //            gvExport.RenderControl(htw);
        //            //(start): require for date format issue
        //            System.Text.StringBuilder style = new System.Text.StringBuilder();
        //            style.Append("<style>");
        //            style.Append("." + "text" + " { mso-number-format:" + "\\@;" + " }");
        //            style.Append("</style>");
        //            response.Clear();
        //            Response.Buffer = true;
        //            //response.Charset = "";
        //            //response.Write(sw.ToString());
        //            Response.Write(style.ToString());
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            //(end): require for date format issue
        //            try
        //            {
        //                response.End();
        //            }
        //            catch (Exception err)
        //            {

        //            }
        //            //HttpContext.Current.ApplicationInstance.CompleteRequest();

        //        }
        //    }


        //}

  
        protected void btnpdf_click(object sender, EventArgs e)
        {
            int reportname = int.Parse(ddlReportType.SelectedValue);

            if (reportname == 1)
            {

                DataTable dt = new DataTable();
                dt = exporttoexcel(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Payment ControlSheet");

            }


            if (reportname == 14)
            {

                DataTable dt = new DataTable();
                dt = PaymentData(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Download Payment Data");

            }


            else if (reportname == 2)
            {

                DataTable dt = new DataTable();
                dt = dailycheckexport(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Cheque Report Transaction Wise");

            }
            else if (reportname == 3)
            {
                DataTable dt = new DataTable();
                dt = DailyChequeWise(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, ddlBank.SelectedValue , ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Cheque Report Cheque Wise");
            }

            else if (reportname == 4)
            {
                DataTable dt = new DataTable();
                dt = DailySubDWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Sub Division Wise Summary");
            }

            else if (reportname == 5)
            {
                DataTable dt = new DataTable();
                dt = DailyDateSubDivisionWiseSummary(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Date SubDivisionWise Summary");
            }

            else if ( reportname == 13 || reportname == 12)
            {

            }
            else if (reportname == 6)
            {
                DataTable dt = new DataTable();
                dt = DailyCSUserWise(ddlCounterName.SelectedValue, ddlUser.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Collection Summary User Wise");
            }

            else if (reportname == 7)
            {
                DataTable dt = new DataTable();
                dt = DailyLoginLogout(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Daily Login Logout Report");

            }
            else if (reportname == 8)
            {
                DataTable dt = new DataTable();
                dt = CancelTrasaction(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Cancellation Transaction Report");

            }

            else if (reportname == 9)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSPS(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Date wise Subdivision wise Payment Summary");

            }

            else if (reportname == 10)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSTS(ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Date wise Subdivision wise Transaction Summary");

            }
            else if (reportname == 11)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSPTS(ddlSubDivision.SelectedValue, ddlCounterName.SelectedValue, txtfromdate.Text, txttodate.Text, Session["username"].ToString());
                pdf(dt, "Date wise & Subdivision wise Payment transaction Summary");
            }
        }
        //protected void pdf(DataTable dataTable, string fileName)
        //{

        //    DataSet dsExport = new DataSet();
        //    DataGrid dgGrid = new DataGrid();
        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //    dgGrid.DataSource = dataTable;
        //    hw.WriteLine("<b><u><font size='5'> JVVNL " + fileName + " </font></u></b>");
        //    dgGrid.HeaderStyle.Font.Bold = true;
        //    dgGrid.DataBind();
        //    dgGrid.RenderControl(hw);

        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("Content-Disposition", "attachment; filename =\"" + fileName + ".pdf");

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);


        //    Document document = new Document();

        //    PdfWriter.GetInstance(document, Response.OutputStream);
        //    document.Open();
        //    StringReader str = new StringReader(tw.ToString());
        //    HTMLWorker htmlworker = new HTMLWorker(document);
        //    htmlworker.Parse(str);

        //    document.Close();

        //    Response.Write(document);

        //    Response.End();


        //}


        protected void pdf(DataTable dataTable, string fileName)
        {

            DataSet dsExport = new DataSet();
            DataGrid dgGrid = new DataGrid();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            dgGrid.DataSource = dataTable;
            //hw.WriteLine("<b><u><font size='5'> JVVNL " + fileName + " </font></u></b>");
            string strBankName = "";
            if (ddlBank.SelectedValue == "ALL")
            {
                strBankName = ddlBank.SelectedValue;
            }
            else
            {
                strBankName = ddlBank.SelectedItem.Text;
            }
            string strSubDivision = "";
            if (ddlSubDivision.SelectedValue == "ALL")
            {
                strSubDivision = ddlSubDivision.SelectedValue;
            }
            else
            {
                strSubDivision = ddlSubDivision.SelectedItem.Text;
            }

            string strCounterName= "";
            if (ddlCounterName.SelectedValue == "ALL")
            {
                strCounterName = ddlCounterName.SelectedValue;
            }
            else
            {
                strCounterName = ddlCounterName.SelectedItem.Text;
            }

            if (ddlReportType.SelectedValue == "1")
            {
                
                
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                hw.WriteLine("<b><font size='2'> Daily Activity Report Control Sheet&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name : " + strCounterName + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SDO Code : " + strSubDivision + " </font></b><br/>");
                hw.WriteLine("<b><font size='2'> User ID : " + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date From : " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp; " + txttodate.Text + " </font></b><br/>");
                hw.WriteLine("<b><font size='2'> Payment Entry  </font></b><br/><br/>");
            }

            if (ddlReportType.SelectedValue == "2")
            {

                // HtmlTextWriter hw = new HtmlTextWriter(sw);
                hw.WriteLine("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   <b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                hw.WriteLine("<b><font size='2'> DAILY CHEQUE WISE COLLECTION REPORT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SubDivision&nbsp;&nbsp;&nbsp;&nbsp;  " + strSubDivision + " </font></b><br/><br/>");
                hw.WriteLine("<b><font size='2'>Userid:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + txttodate.Text + " </font></b><br/><br/>");


            }
            if (ddlReportType.SelectedValue == "3")
            {
              
                hw.WriteLine("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   <b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
               hw.WriteLine("<b><font size='2'> Cheque Summary From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp; " + txttodate.Text + " &nbsp;&nbsp;&nbsp;&nbsp;SubDivision:&nbsp;&nbsp;&nbsp;&nbsp; " + strSubDivision + "&nbsp;&nbsp;Bank Type:&nbsp;&nbsp;&nbsp;&nbsp; " + strBankName + " </font></b><br/>");
                hw.WriteLine("<b><font size='2'> User:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ddlUser.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + " </font></b><br/><br/>");

            }

            if (ddlReportType.SelectedValue == "4")
            {

                hw.WriteLine("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   <b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                hw.WriteLine("<b><font size='2'> SDO Wise Collection Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font></b><br/>");
                hw.WriteLine("<b><font size='2'>  From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp; Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + "  </font></b><br/>");

            }

            if (ddlReportType.SelectedValue == "5")
            {

                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                hw.WriteLine("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   <b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                hw.WriteLine("<b><font size='2'> Daily Date SubDivisionWise Summary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + strCounterName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font></b><br/>");
                hw.WriteLine("<b><font size='2'>  From &nbsp;&nbsp;&nbsp;&nbsp; " + txtfromdate.Text + "&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp; Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + "  </font></b><br/><br/>");

            }



            if (ddlReportType.SelectedValue == "6")
            {
                //hw.WriteLine("<b><font size='2'>  " + fileName + " </font></b>");
                //  HtmlTextWriter hw = new HtmlTextWriter(sw);
                hw.WriteLine("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   <b><font size='2'> TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
                hw.WriteLine("<b><font size='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
                hw.WriteLine("<b><font size='2'> User Wise Collection Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter : " + strCounterName + "  </font></b><br/>");
                hw.WriteLine("<b><font size='2'> From:&nbsp;" + txtfromdate.Text + "&nbsp;&nbsp;To:&nbsp;" + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + " </font></b><br/><br/>");
            }
            if (ddlReportType.SelectedValue == "7" || ddlReportType.SelectedValue == "8" || ddlReportType.SelectedValue == "9" || ddlReportType.SelectedValue == "10" || ddlReportType.SelectedValue == "11" ||  ddlReportType.SelectedValue == "13" || ddlReportType.SelectedValue == "14")
            {
                hw.WriteLine("<b><font size='2'>  " + fileName + " </font></b>");
            }
            dgGrid.HeaderStyle.Font.Bold = true;
            dgGrid.Font.Size = 8;
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);

            Response.ContentType = "application/pdf";

            Response.AddHeader("Content-Disposition", "attachment; filename =\"" + fileName + ".pdf");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            Document document = new Document();

            PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();
            StringReader str = new StringReader(tw.ToString());
            HTMLWorker htmlworker = new HTMLWorker(document);
            htmlworker.Parse(str);

            document.Close();

            Response.Write(document);

            Response.End();


        }

        //protected void pdf6(DataTable dataTable, string fileName)
        //{

        //    DataSet dsExport = new DataSet();
        //    DataGrid dgGrid = new DataGrid();
        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //    dgGrid.DataSource = dataTable;
        //    hw.WriteLine("<b><font size='1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TRIMAX IT INFRASTRUCTURE & SERVICES LTD. </font></b><br/>");
        //    hw.WriteLine("<b><font size='1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(An Association with JVVNL, Jaipur) </font></b><br/>");
        //    hw.WriteLine("<b><font size='2'> User Wise Collection Report &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Counter :" + ddlCounterName.SelectedItem + "  </font></b><br/>");
        //    hw.WriteLine("<b><font size='2'> From: " + txtfromdate.Text + "&nbsp;&nbsp; To: " + txttodate.Text + "&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;Generation Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + " </font></b><br/>");
        //    //  hw.WriteLine("<b><font size='5'> User:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ddlUser.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Counter Name:&nbsp;&nbsp;&nbsp;&nbsp;" + ddlCounterName.Text + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generation  Time:&nbsp;&nbsp;&nbsp;&nbsp;  " + DateTime.Now + " </font></b><br/>");


        //    //dgGrid.HeaderStyle.Font.Bold = true;
        //    dgGrid.DataBind();
        //    dgGrid.RenderControl(hw);

        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("Content-Disposition", "attachment; filename =\"" + fileName + ".pdf");

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);


        //    Document document = new Document();

        //    PdfWriter.GetInstance(document, Response.OutputStream);
        //    document.Open();
        //    StringReader str = new StringReader(tw.ToString());
        //    HTMLWorker htmlworker = new HTMLWorker(document);
        //    htmlworker.Parse(str);

        //    document.Close();

        //    Response.Write(document);

        //    Response.End();


        //}

        # endregion
  

        # endregion






     

     
        # region  "Report Grid"

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
                if (dt != null && dt.Rows.Count > 0)
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
        private string RepCancelTrasaction(string CounterName, string fromdate, string todate, string userid)
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


                if (dt != null && dt.Rows.Count > 0)
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

        private string RepDatewiseSPTS(string sdo, string CounterName, string fromdate, string todate, string userid)
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


                if (dt != null && dt.Rows.Count > 0)
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
        private string RepDatewiseSTS(string CounterName, string fromdate, string todate, string userid)
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


                if (dt != null && dt.Rows.Count > 0)
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

        private string RepDatewiseSPS(string CounterName, string fromdate, string todate, string userid)
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


                if (dt != null && dt.Rows.Count > 0)
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
        private string RepDailyLoginLogout(string CounterName, string fromdate, string todate, string userid)
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


                if (dt != null && dt.Rows.Count > 0)
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

        private string RepDailySubDWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "1";
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


                //sb.Append("<th>" + "SDO Name" + "</th>");
                sb.Append("<th>" + "SDO CODE" + "</th>");
                sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
                sb.Append("<th>" + "TOTALCASHS AMT" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ AMT" + "</th>");
                sb.Append("<th>" + "Total Stub" + "</th>");
                sb.Append("<th>" + "TotalAMT" + "</th>");

                sb.Append("</tr>");


                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>" + dt.Rows[i]["SDOCODE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCASHSAMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTALCHQAMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalAMT"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }
        private string RepDailyDateSubDivisionWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "1";
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailyDateSubDivisionWiseSummary(reportProp);
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


                sb.Append("<th>" + "SrNo" + "</th>");
                sb.Append("<th>" + "SDOCode" + "</th>");
                sb.Append("<th>" + "Payment Date" + "</th>");
                sb.Append("<th>" + "TotalStub" + "</th>");
                sb.Append("<th>" + "TotalAMT" + "</th>");
                //sb.Append("<th>" + "TOTAL CHQ AMT" + "</th>");
                //sb.Append("<th>" + "Total Stub" + "</th>");
                //sb.Append("<th>" + "TotalAMT" + "</th>");

                sb.Append("</tr>");


                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDOCode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Payment Date"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalAMT"].ToString() + "</td>");
                        
                        //sb.Append("<td>" + dt.Rows[i]["TOTALCHQAMT"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["TotalAMT"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
            }


            return sb.ToString();
        }

        private string RepDailyCRTransactionWise(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "1";
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
                //sb.Append("<th>" + "SDO" + "</th>");
                //sb.Append("<th>" + "SDO Name" + "</th>");
                sb.Append("<th>" + "SDO CODE" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "PAYDATE" + "</th>");
                //sb.Append("<th>" + "CNT NO" + "</th>");
                //sb.Append("<th>" + "REC NO" + "</th>");
                sb.Append("<th>" + "AMT" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th>" + "RECP NO" + "</th>");
                sb.Append("<th>" + "CHQ DATE" + "</th>");
                sb.Append("<th>" + "CHQNO" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("</tr>");


                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDOCODE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["CNT NO"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["REC NO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RECPNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQNO"].ToString() + "</td>");
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

        private string RepDailyChequeWise(string SubDivision, string CounterName, string Bank, string User, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.Bank = Bank;
            reportProp.UserName = User;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "ForGrid";
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
                sb.Append("<th>" + "SDO CODE" + "</th>");
                //sb.Append("<th>" + "SDO Name" + "</th>");

                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "CHQNO" + "</th>");
                sb.Append("<th>" + "CHQ DATE" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th>" + "UNDER A/C" + "</th>");

                sb.Append("<th>" + "AMT" + "</th>");

                sb.Append("</tr>");


                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO CODE"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK NAME"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["UNDER A/C"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMT"].ToString() + "</td>");
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
                sb.Append("<th>" + "SDO CODE" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "Payment Date" + "</th>");
                sb.Append("<th>" + "COUNTERNO" + "</th>");
                sb.Append("<th>" + "RECP NO" + "</th>");
                sb.Append("<th>" + "AMT" + "</th>");
                sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
                sb.Append("<th>" + "CHQ" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("<th>" + "Print" + "</th>");
                sb.Append("</tr>");

                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO CODE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Payment Date"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RECP NO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQNO"].ToString() + "</td>");
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

        private string RepDailyCSUserWise(string CounterName, string user, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();

            reportProp.CounterName = CounterName;
            reportProp.UserName = user;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "";
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


                sb.Append("<th>" + "USER ID" + "</th>");
                sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
                sb.Append("<th>" + "TOTAL CASH AMT" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
                sb.Append("<th>" + "TOTAL CHQ AMTS" + "</th>");
                sb.Append("<th>" + "TOTAL STUBS" + "</th>");
                sb.Append("<th>" + "TOTAL AMOUNT" + "</th>");

                sb.Append("</tr>");


                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>" + dt.Rows[i]["USER ID"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CASH AMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ AMTS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL STUBS"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TOTAL AMOUNT"].ToString() + "</td>");

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

                if (dt != null && dt.Rows.Count > 0)
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
        private string RepDailyPCSheet(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        {
            report_Prop reportProp = new report_Prop();
            reportProp.SubDivision = SubDivision;
            reportProp.CounterName = CounterName;
            reportProp.fromdate = fromdate;
            reportProp.todate = todate;
            reportProp.UserId = userid;
            reportProp.ForGrid = "";
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
                sb.Append("<th>" + "SDO CODE" + "</th>");
                sb.Append("<th>" + "ACCNo" + "</th>");
                sb.Append("<th>" + "PAYDATE" + "</th>");
                sb.Append("<th>" + "COUNTERNO" + "</th>");
                sb.Append("<th>" + "RCPTNo" + "</th>");
                sb.Append("<th>" + "AMT" + "</th>");
                sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
                sb.Append("<th>" + "CHQNO" + "</th>");
                sb.Append("<th>" + "USER" + "</th>");
                sb.Append("</tr>");

                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SDO CODE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RCPTNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQNO"].ToString() + "</td>");
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

        private string RepPaymentData(string SubDivision, string CounterName, string fromdate, string todate, string userid)
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
                //sb.Append("<th >" + "CNT&nbsp;No" + "</th>");
                sb.Append("<th>" + "RECP&nbsp;NO" + "</th>");
                sb.Append("<th>" + "AMT" + "</th>");
                sb.Append("<th>" + "RDATE" + "</th>");
                sb.Append("<th>" + "CHQNO" + "</th>");
                sb.Append("<th>" + "CHQDATE" + "</th>");
                sb.Append("<th>" + "BANK" + "</th>");
                sb.Append("<th '>" + "Mode&nbsp;Of&nbsp;PYT" + "</th>");
                //sb.Append("<th>" + "Status" + "</th>");
                sb.Append("<th>" + "CNTRName" + "</th>");
                sb.Append("<th>" + "ENTTime" + "</th>");
                sb.Append("<th>" + "Consumer&nbsp;Name" + "</th>");
                sb.Append("</tr>");

                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("$");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["Counter No"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RECPNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AMT"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["RDATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Mode Of Payment"].ToString() + "</td>");
                        //sb.Append("<td>" + dt.Rows[i]["Status"].ToString() + "</td>");
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


        # endregion
        //private string DailyPCSheet(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;

        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.ReportDailyPCSheet(reportProp);



        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=9>" + "Daily Payment ControlSheet" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "Sr No" + "</th>");
        //        sb.Append("<th>" + "ACCNo" + "</th>");
        //        sb.Append("<th>" + "PAYDATE" + "</th>");
        //        sb.Append("<th>" + "COUNTERNO" + "</th>");
        //        sb.Append("<th>" + "ReceiptNo" + "</th>");
        //        sb.Append("<th>" + "AMOUNT" + "</th>");
        //        sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
        //        sb.Append("<th>" + "CHEQUENO" + "</th>");
        //        sb.Append("<th>" + "USER" + "</th>");
        //        sb.Append("</tr>");

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}
        //private string PaymentData(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;

        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.PaymentData(reportProp);



        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=15>" + "Daily Payment Data" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "SDO" + "</th>");
        //        sb.Append("<th>" + "ACCNo" + "</th>");
        //        sb.Append("<th>" + "Counter No" + "</th>");
        //        sb.Append("<th>" + "ReceiptNo" + "</th>");
        //        sb.Append("<th>" + "AMOUNT" + "</th>");
        //        sb.Append("<th>" + "RDATE" + "</th>");
        //        sb.Append("<th>" + "CHEQUENO" + "</th>");
        //        sb.Append("<th>" + "CHQ DATE" + "</th>");
        //        sb.Append("<th>" + "BANK" + "</th>");
        //        sb.Append("<th>" + "Mode Of Payment" + "</th>");
        //        sb.Append("<th>" + "Status" + "</th>");
        //        sb.Append("<th>" + "CounterName" + "</th>");
        //        sb.Append("<th>" + "Entered Time" + "</th>");
        //        sb.Append("<th>" + "Consumer Name" + "</th>");
        //        sb.Append("</tr>");

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["Counter No"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["RDATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["Mode Of Payment"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["Status"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CounterName"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["Entered Time"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["Consumer Name"].ToString() + "</td>");


        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}



        //private string PopulateCounterUserwise(string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.UserName = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.PopulateCounterUserwise(reportProp);
        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append(dt.Rows[i]["counterid"].ToString() + "#" + dt.Rows[i]["countername"].ToString() + "$");
        //            }
        //            return sb.ToString();
        //        }
        //        else
        //            return "0#No Data Found$";
        //    }
        //}
        //private string CancelTrasaction(string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();

        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.CancelTrasaction(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=11>" + "Cancellation Transaction Report" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

        //        }
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {

        //            sb.Append("$");

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {

        //                    sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

        //                }
        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td colspan=11>No Record Found</td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DatewiseSPTS(string sdo, string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = sdo;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DatewiseSPTS(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=7>" + "Date wise & Subdivision wise Payment transaction Summary" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

        //        }
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {

        //            sb.Append("$");

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {

        //                sb.Append("<tr>");
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {

        //                    sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

        //                }
        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}
        //private string DatewiseSTS(string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();

        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DatewiseSTS(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan='11'>" + "Date wise Subdivision wise Transaction Summary" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

        //        }
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            if (dt.Rows[0][0].ToString() != "")
        //            {
        //                sb.Append("$");

        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {

        //                    sb.Append("<tr>");
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {

        //                        sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

        //                    }
        //                    sb.Append("</tr>");
        //                }
        //            }
        //            else
        //                sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DatewiseSPS(string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();

        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DatewiseSPS(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan='11'>" + "Date wise Subdivision wise Payment Summary" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            sb.Append("<th>" + dt.Columns[i].ColumnName.ToString() + "</th>");

        //        }
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            if (dt.Rows[0][0].ToString() != "")
        //            {
        //                sb.Append("$");


        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {

        //                    sb.Append("<tr>");
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {

        //                        sb.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");

        //                    }
        //                    sb.Append("</tr>");
        //                }
        //            }
        //            else
        //            {
        //                sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}
        //private string DailyLoginLogout(string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();

        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DailyLoginLogout(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=5>" + "Daily Login Logout Report" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "Sr No" + "</th>");
        //        sb.Append("<th>" + "COUNTERNAME" + "</th>");
        //        sb.Append("<th>" + "USERNAME" + "</th>");

        //        sb.Append("<th>" + "LOGINDATE" + "</th>");
        //        sb.Append("<th>" + "LOGOUTDATE" + "</th>");
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["COUNTERNAME"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["USERNAME"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["LOGINDATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["LOGOUTDATE"].ToString() + "</td>");


        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DailySubDWiseSummary(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DailySubDWiseSummary(reportProp);
        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=7>" + "Daily Sub Division Wise Summary" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");


        //        sb.Append("<th>" + "SDO Name" + "</th>");
        //        sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
        //        sb.Append("<th>" + "TOTALCASHS Amount" + "</th>");
        //        sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
        //        sb.Append("<th>" + "TOTAL CHQ Amount" + "</th>");
        //        sb.Append("<th>" + "Total Stub" + "</th>");
        //        sb.Append("<th>" + "TotalAmount" + "</th>");

        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");

        //                sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTALCASHSAMount"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTALCHQAmount"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>");

        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DailyCRTransactionWise(string SubDivision, string CounterName, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DailyCRTransactionWise(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=12>" + "Daily Cheque Report Transaction Wise" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "Sr No" + "</th>");
        //        sb.Append("<th>" + "SDO" + "</th>");
        //        sb.Append("<th>" + "SDO Name" + "</th>");

        //        sb.Append("<th>" + "ACCNo" + "</th>");
        //        sb.Append("<th>" + "PAYDATE" + "</th>");
        //        sb.Append("<th>" + "CNT NO" + "</th>");
        //        sb.Append("<th>" + "REC NO" + "</th>");
        //        sb.Append("<th>" + "AMOUNT" + "</th>");
        //        sb.Append("<th>" + "BANK" + "</th>");
        //        sb.Append("<th>" + "CHQ DATE" + "</th>");
        //        sb.Append("<th>" + "CHEQUENO" + "</th>");
        //        sb.Append("<th>" + "USER" + "</th>");
        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CNT NO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["REC NO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["BANK"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DailyChequeWise(string SubDivision, string CounterName, string Bank, string User, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.Bank = Bank;
        //    reportProp.UserName = User;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DailyChequeWise(reportProp);

        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=9>" + "Daily Cheque Report Cheque Wise" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "Sr No" + "</th>");
        //        sb.Append("<th>" + "SDO" + "</th>");
        //        sb.Append("<th>" + "SDO Name" + "</th>");

        //        sb.Append("<th>" + "ACCNo" + "</th>");
        //        sb.Append("<th>" + "CHEQUENO" + "</th>");
        //        sb.Append("<th>" + "CHQ DATE" + "</th>");
        //        sb.Append("<th>" + "BANK" + "</th>");
        //        sb.Append("<th>" + "UNDER A/C" + "</th>");

        //        sb.Append("<th>" + "AMOUNT" + "</th>");

        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["SDO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["SDO Name"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHQ DATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["BANK NAME"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["UNDER A/C"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string repReceiptno(string SubDivision, string CounterName, string fromdate, string todate, string userid, string Receiptno)
        //{
        //    report_Prop reportProp = new report_Prop();
        //    reportProp.SubDivision = SubDivision;
        //    reportProp.CounterName = CounterName;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    reportProp.Receiptno = Receiptno;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.Receiptno(reportProp);



        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=10>" + "Cashier location allocation report" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");
        //        sb.Append("<th>" + "Sr No" + "</th>");
        //        sb.Append("<th>" + "ACCNo" + "</th>");
        //        sb.Append("<th>" + "PAYDATE" + "</th>");
        //        sb.Append("<th>" + "COUNTERNO" + "</th>");
        //        sb.Append("<th>" + "ReceiptNo" + "</th>");
        //        sb.Append("<th>" + "AMOUNT" + "</th>");
        //        sb.Append("<th>" + "CASH/CHEQUE" + "</th>");
        //        sb.Append("<th>" + "CHEQUENO" + "</th>");
        //        sb.Append("<th>" + "USER" + "</th>");
        //        sb.Append("<th>" + "Print" + "</th>");
        //        sb.Append("</tr>");

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");
        //                sb.Append("<td>" + dt.Rows[i]["SrNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ACCNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["PAYDATE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["COUNTERNO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["ReceiptNo"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CASH/CHEQUE"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["CHEQUENO"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["USER"].ToString() + "</td>");
        //                sb.Append("<td ><img src='images/print.jpeg' alt='delete' onclick=PrintReceipt('");
        //                sb.Append(dt.Rows[i]["paymentid"].ToString().Trim() + "') /></td>");

        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='9'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}

        //private string DailyCSUserWise(string CounterName, string user, string fromdate, string todate, string userid)
        //{
        //    report_Prop reportProp = new report_Prop();

        //    reportProp.CounterName = CounterName;
        //    reportProp.UserName = user;
        //    reportProp.fromdate = fromdate;
        //    reportProp.todate = todate;
        //    reportProp.UserId = userid;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.DailyCSUserWise(reportProp);
        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<th colspan=7>" + "Daily Collection Summary User Wise" + "</th>");
        //        sb.Append("</tr>");
        //        sb.Append("<tr>");


        //        sb.Append("<th>" + "User Name" + "</th>");
        //        sb.Append("<th>" + "TOTAL CASHSTUBS" + "</th>");
        //        sb.Append("<th>" + "TOTALCASHS Amount" + "</th>");
        //        sb.Append("<th>" + "TOTAL CHQ STUBS" + "</th>");
        //        sb.Append("<th>" + "TOTAL CHQ Amount" + "</th>");
        //        sb.Append("<th>" + "Total Stub" + "</th>");
        //        sb.Append("<th>" + "TotalAmount" + "</th>");

        //        sb.Append("</tr>");


        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            sb.Append("$");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sb.Append("<tr>");

        //                sb.Append("<td>" + dt.Rows[i]["username"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTAL CASHSTUBS"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTALCASHSAMount"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTAL CHQ STUBS"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TOTALCHQAmount"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TotalStub"].ToString() + "</td>");
        //                sb.Append("<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>");

        //                sb.Append("</tr>");
        //            }
        //        }
        //        else
        //            sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        //        //sb.Append("<tr><td colspan='13'>No Record Found</td></tr>");
        //    }


        //    return sb.ToString();
        //}
        //private string filluser(int CounterId)
        //{
        //    counter_Prop counterProp = new counter_Prop();
        //    counterProp.Counterid = CounterId;
        //    report_bal reportbal = new report_bal();
        //    DataTable dt = reportbal.fillUser(counterProp);



        //    StringBuilder sb = new StringBuilder();

        //    if (reportbal.ErrMessage != "" && reportbal.ErrMessage != null)
        //    {
        //        return "0#" + reportbal.ErrMessage + "$";
        //    }
        //    else
        //    {

        //        if (dt != null && dt.Rows.Count > 0)
        //        {

        //            StringBuilder sbtxt = new StringBuilder();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sbtxt.Append(dt.Rows[i]["UserID"].ToString() + "#" + dt.Rows[i]["UserName"].ToString() + "$");
        //            }
        //            return sbtxt.ToString();
        //        }
        //        else
        //            return "0#No Data Found$";
        //    }


        //    return sb.ToString();
        //}

    }//namespace
}
