using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Reporting;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.Security;
using KVICClassLIB;
using System.Configuration;


namespace KVICWeb
{
    
    public partial class MonthllyRFDReportRPT : System.Web.UI.Page
    {
        int clusterid;
        string date;
        protected void Page_Load(object sender, EventArgs e)
        {
             clusterid =Convert.ToInt32( Request.QueryString["id"].ToString());
             date = Request.QueryString["date"].ToString();

             SpViewReport();
        }


        private void SpViewReport()
        {
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("MonthlyRFDCrystalReport.rpt"));

            ConnectionInfo connectionInfo = new ConnectionInfo();
            //connectionInfo.ServerName = "ITISRV-R5R13U";
            //connectionInfo.DatabaseName = "KVIC";
            //connectionInfo.UserID = "sa";
            //connectionInfo.Password = "password@008";

            //connectionInfo.ServerName = "192.168.102.34";
            //connectionInfo.DatabaseName = "KVIC";
            //connectionInfo.UserID = "sa";
            //connectionInfo.Password = "pass123";

            connectionInfo.ServerName = "192.168.103.253";
            connectionInfo.DatabaseName = "KVIC";
            connectionInfo.UserID = "sa";
            connectionInfo.Password = "pass123";

            SetDBLogonForReport(connectionInfo, cr);            
            
            
            MonthlyRFDReport_bal MonthlyRFDReportbal = new MonthlyRFDReport_bal();
            DataTable dt = MonthlyRFDReportbal.MonthlyReport_bal(clusterid,date);
            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;                      
        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        
    }
}
