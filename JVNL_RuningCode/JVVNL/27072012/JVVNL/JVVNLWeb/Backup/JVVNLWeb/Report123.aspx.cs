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
    public partial class Report123 : System.Web.UI.Page
    {
        
       protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       private void FillSubDivision()
       {

       }

        protected void btnexport_click(object sender, EventArgs e)
        {
           int reportname = int.Parse(ddlReportType.Value);
           if (Session["username"].ToString() == "")
           {




               if (reportname == 1)
               {

                   DataTable dt = new DataTable();
                   dt = exporttoexcelexport(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Payment ControlSheet");



               }
               else if (reportname == 2)
               {

                   DataTable dt = new DataTable();
                   dt = dailycheckexport(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Cheque Report Transaction Wise");

               }


               else if (reportname == 3)
               {

                   DataTable dt = new DataTable();
                   dt = DailyChequeWiseexport(hddlSubDivision.Value, hddlCounterName.Value, hddlBank.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Cheque Report Cheque Wise");

               }



               else if (reportname == 4)
               {

                   DataTable dt = new DataTable();
                   dt = DailySubDWiseSummary(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Sub Division Wise Summary");


               }
               else if (reportname == 5 || reportname == 13 || reportname == 12)
               {

               }
               else if (reportname == 6)
               {

                   DataTable dt = new DataTable();
                   dt = DailyCSUserWise(hddlCounterName.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Collection Summary User Wise");


               }

               else if (reportname == 7)
               {

                   DataTable dt = new DataTable();
                   dt = DailyLoginLogoutexport(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Daily Login Logout Report");


               }
               else if (reportname == 8)
               {

                   DataTable dt = new DataTable();
                   dt = CancelTrasactionexport(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Cancellation Transaction Report");


               }

               else if (reportname == 9)
               {
                   DataTable dt = new DataTable();
                   dt = DatewiseSPSexport(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Date wise Subdivision wise Payment Summary");



               }

               else if (reportname == 10)
               {

                   DataTable dt = new DataTable();
                   dt = DatewiseSTSexport(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Date wise Subdivision wise Transaction Summary");


               }
               else if (reportname == 11)
               {

                   DataTable dt = new DataTable();
                   dt = DatewiseSPTSexport(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   es(dt, "Date wise & Subdivision wise Payment transaction Summary");


               }
           }
           else
           {

               if (reportname == 1)
               {

                   DataTable dt = new DataTable();
                   dt = exporttoexcel(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Payment ControlSheet");



               }

               if (reportname == 14)
               {

                   DataTable dt = new DataTable();
                   dt = PaymentData(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Download Payment Data");



               }


               else if (reportname == 2)
               {

                   DataTable dt = new DataTable();
                   dt = dailycheck(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Cheque Report Transaction Wise");

               }


               else if (reportname == 3)
               {

                   DataTable dt = new DataTable();
                   dt = DailyChequeWise(hddlSubDivision.Value, hddlCounterName.Value, hddlBank.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Cheque Report Cheque Wise");

               }



               else if (reportname == 4)
               {

                   DataTable dt = new DataTable();
                   dt = DailySubDWiseSummary(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Sub Division Wise Summary");


               }
               else if (reportname == 5 || reportname == 13 || reportname == 12)
               {

               }
               else if (reportname == 6)
               {

                   DataTable dt = new DataTable();
                   dt = DailyCSUserWise(hddlCounterName.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Collection Summary User Wise");


               }

               else if (reportname == 7)
               {

                   DataTable dt = new DataTable();
                   dt = DailyLoginLogout(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Daily Login Logout Report");


               }
               else if (reportname == 8)
               {

                   DataTable dt = new DataTable();
                   dt = CancelTrasaction(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Cancellation Transaction Report");


               }

               else if (reportname == 9)
               {
                   DataTable dt = new DataTable();
                   dt = DatewiseSPS(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Date wise Subdivision wise Payment Summary");



               }

               else if (reportname == 10)
               {

                   DataTable dt = new DataTable();
                   dt = DatewiseSTS(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Date wise Subdivision wise Transaction Summary");


               }
               else if (reportname == 11)
               {

                   DataTable dt = new DataTable();
                   dt = DatewiseSPTS(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                   ex(dt, "Date wise & Subdivision wise Payment transaction Summary");


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
            report_bal reportbal = new report_bal();
            DataTable dt = reportbal.DailySubDWiseSummary(reportProp);

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




            //   Report Header
            // hw.WriteLine("<b><u><font size='5'> JVVNL " + fileName + " </font></u></b>");

            //   Get the HTML for the control.
            //dgGrid.HeaderStyle.Font.Bold = true;
            //dgGrid.DataBind();
            //dgGrid.RenderControl(hw);

            //   Write the HTML back to the browser.
            //Response.AddHeader("Content-Disposition", "attachment; filename =\"" + fileName + ".xls");
            //Response.ContentType = "application/vnd.ms-excel";
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            //Response.Write(style);


            //this.EnableViewState = false;
            //Response.Write(tw.ToString());
            //Response.End();


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
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

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
            
        protected void btnpdf_click(object sender, EventArgs e)
        {
            int reportname = int.Parse(ddlReportType.Value);

            if (reportname == 1)
            {
                
                    DataTable dt = new DataTable();
                    dt = exporttoexcel(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                    pdf(dt, "Daily Payment ControlSheet");
                
            }


            if (reportname == 14)
            {

                DataTable dt = new DataTable();
                dt = PaymentData(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Download Payment Data");

            }


            else if (reportname == 2)
            {
                
                    DataTable dt = new DataTable();
                    dt = dailycheckexport(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                    pdf(dt, "Daily Cheque Report Transaction Wise");
                
            }
            else if (reportname == 3)
            {
                DataTable dt = new DataTable();
                dt = DailyChequeWise(hddlSubDivision.Value, hddlCounterName.Value, hddlBank.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Daily Cheque Report Cheque Wise");
            }

            else if (reportname == 4)
            {
                DataTable dt = new DataTable();
                dt = DailySubDWiseSummary(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Daily Sub Division Wise Summary");
            }

            else if (reportname == 5 || reportname == 13 || reportname == 12)
            {
                
            }
            else if (reportname == 6)
            {
                DataTable dt = new DataTable();
                dt = DailyCSUserWise(hddlCounterName.Value, hddlUser.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Daily Collection Summary User Wise");
            }

            else if (reportname == 7)
            {
                DataTable dt = new DataTable();
                dt = DailyLoginLogout(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Daily Login Logout Report");

            }
            else if (reportname == 8)
            {
                DataTable dt = new DataTable();
                dt = CancelTrasaction(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Cancellation Transaction Report");

            }

            else if (reportname == 9)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSPS(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Date wise Subdivision wise Payment Summary");

            }

            else if (reportname == 10)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSTS(hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Date wise Subdivision wise Transaction Summary");

            }
            else if (reportname == 11)
            {
                DataTable dt = new DataTable();
                dt = DatewiseSPTS(hddlSubDivision.Value, hddlCounterName.Value, Request.Form["fromdate"].ToString(), Request.Form["todate"].ToString(), Session["username"].ToString());
                pdf(dt, "Date wise & Subdivision wise Payment transaction Summary");
            }
        }
        protected void pdf(DataTable dataTable, string fileName)
        {

            DataSet dsExport = new DataSet();
            DataGrid dgGrid = new DataGrid();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            dgGrid.DataSource = dataTable;
            hw.WriteLine("<b><u><font size='5'> JVVNL " + fileName + " </font></u></b>");
            dgGrid.HeaderStyle.Font.Bold = true;
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

    }
}
