using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using JVVNLClassLIB;
using System.Text;

namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Changeduedate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string strmsg = "";
            switch (type)
            {

                case "ChangeBillDuedate":
                    int billid = Convert.ToInt32 (context.Request["billid"].ToString());
                    string username = context.Request["username"].ToString();
                    string duedate = context.Request["duedate"].ToString();
                    strmsg = ChangeBillDuedate(billid, username, duedate);
               
                    break;
                case "consumerselect":

                    string binderno = context.Request["binderno"].ToString();
                   
                    strmsg = BillingSelect( binderno);
                    break;

                default:
                    break;
            }
            context.Response.Write(strmsg.ToString());
        }
        private string BillingSelect( string AccountNo)
        {
            StringBuilder sb = new StringBuilder();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.AccountNo = AccountNo;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelectChangeDuedate(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td >No Record Found</td><td></td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["BillID"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Name"].ToString() + "</td>");

                        sb.Append("<td>" + dt.Rows[i]["billdate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["duedate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Amount"].ToString() + "</td>");

                        sb.Append("<td ><input type='checkbox' id='chkBoxChild' /></td> ");
                       
                        sb.Append("</tr>");
                    }
                }
                return sb.ToString();
            }

        }
        private string ChangeBillDuedate(int billid,string  username, string duedate )
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.BillID = billid;
            paymentprop.UserName = username;
            paymentprop.ChequeDate = duedate;
            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.PaymentChangeDuedate(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
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
