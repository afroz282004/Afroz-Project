using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KVICClassLIB;
 namespace KVICWeb
{
    public partial class MonthlyRFDReport : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCluster();
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            //MonthlyRFDReport_bal MonthlyRFDReportbal = new MonthlyRFDReport_bal();

            //DataTable dt = MonthlyRFDReportbal.MonthlyReport_bal();
            int clusterid =Convert.ToInt32( cluster.SelectedValue);
            string date = txtdate.Text;
            Response.Redirect("MonthllyRFDReportRPT.aspx?id=" + clusterid + "&date=" + date);
            
        }

        private void FillCluster()
        {
            
            cluster_bal clusterbal = new cluster_bal();
            DataTable dt = clusterbal.ClusterFill_bal();
            cluster.DataSource = dt;
            cluster.DataValueField = "clusterid";
            cluster.DataTextField = "clustername";
            cluster.DataBind();
        }
        
       
    }
}
