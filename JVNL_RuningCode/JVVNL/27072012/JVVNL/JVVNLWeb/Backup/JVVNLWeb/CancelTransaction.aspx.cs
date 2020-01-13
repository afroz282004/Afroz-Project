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
using JVVNLClassLIB;
using System.Text;

namespace JVVNLWeb
{
    public partial class CancelTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSDO();
                btnsearch_Click(sender, e); 
            }
        }

        private void PopulateSDO()
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = "";
            sdoprop.SDOName = "";
            sdo_bal sdobal = new sdo_bal();
            DataTable dt = sdobal.SDOSearch(sdoprop);
            subdivision.DataSource = dt;
            subdivision.DataValueField = "sdocode";
            subdivision.DataTextField= "sdoname";
            subdivision.DataBind();
            subdivision.Items.Add("------Select------------");
            subdivision.SelectedValue = "------Select------------";

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.SubdivisionCode = subdivision.SelectedValue   ;
            paymentprop.AccountNo = binderno.Value + accountno.Value  ;
            paymentprop.Receiptno = receiptno.Value ;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelectCancel(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                sb.Append("<tr><td >" + paymentbal.ErrMessage +"</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td >No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["Name"].ToString() + "</td>");

                        sb.Append("<td>" + dt.Rows[i]["Subdivisioncode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Consumeraccountno"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["receiptno"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CounterName"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PaymentDate"].ToString() + "</td>");
                        sb.Append("<td>" + Math.Round( Convert.ToDouble( dt.Rows[i]["Amount"].ToString()),2).ToString(".00") + "</td>");
                        sb.Append("<td ><img src='images/icon_delete.png' alt='delete' onclick=DeleteSDO('");
                        sb.Append(dt.Rows[i]["paymentid"].ToString().Trim() + '$' + dt.Rows[i]["Phoneno"].ToString().Trim() + '$' + dt.Rows[i]["EmailId"].ToString().Trim() + '$' + dt.Rows[i]["PayAmount"].ToString().Trim());
                        sb.Append("') /></td>");
                        sb.Append("</tr>");
                    }
                }
                tbody.InnerHtml= sb.ToString();
            }
        }
    }
}
