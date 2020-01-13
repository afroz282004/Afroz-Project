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

namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class paymenttype : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            int paymentid = 0;
            string paymenttype = "";
            string strmsg = "";
            switch (type)
            {
                case "insert":
                    paymenttype= context.Request["paymenttype"].ToString();
                    strmsg = PaymentTypeInsert(paymenttype);
                    break;
                case "search":
                    paymenttype = context.Request["paymenttype"].ToString();
                    strmsg = PaymentTypeSearch(paymenttype);
                    break;
                case "populate":
                    strmsg = PaymentTypePopulate();
                    break;
                case "delete":
                    paymentid = Convert.ToInt16( context.Request["paymentid"].ToString());
                    strmsg = PaymentTypeDelete(paymentid );
                    break;
                default:
                    break;
            }
            context.Response.Write(strmsg);
        }
        private string PaymentTypeInsert(string paymenttype)
        {
            paymenttype_Prop paymenttypeprop = new paymenttype_Prop();
            paymenttypeprop.PaymentType= paymenttype ;
            paymenttype_bal paymenttypebal = new paymenttype_bal();
            string strMsg = paymenttypebal.PaymentTypeInsert(paymenttypeprop );
            if (paymenttypebal.ErrMessage != "" && paymenttypebal.ErrMessage != null)
                return paymenttypebal.ErrMessage;
            else
                return strMsg;

        }
        private string PaymentTypeDelete(int paymentid)
        {
            paymenttype_Prop paymenttypeprop = new paymenttype_Prop();
            paymenttypeprop.Paymentid = paymentid ;
            paymenttype_bal paymenttypebal = new paymenttype_bal();
            string strMsg = paymenttypebal.PaymentTypeDelete(paymenttypeprop);
            if (paymenttypebal.ErrMessage != "" && paymenttypebal.ErrMessage != null)
                return paymenttypebal.ErrMessage;
            else
                return strMsg;

        }
        private string PaymentTypePopulate()
        {
            paymenttype_Prop paymenttypeprop = new paymenttype_Prop();
            paymenttypeprop.PaymentType  = "";
            
            paymenttype_bal paymenttypebal = new paymenttype_bal();
            DataTable dt = paymenttypebal.PaymentTypeSearch(paymenttypeprop);
            StringBuilder sb = new StringBuilder();

            if (paymenttypebal.ErrMessage != "" && paymenttypebal.ErrMessage != null)
            {
                return "0#" + paymenttypebal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sbtxt = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbtxt.Append(dt.Rows[i]["paymentid"].ToString() + "#" + dt.Rows[i]["paymenttype"].ToString() + "$");
                    }
                    return sbtxt.ToString();
                }
                else
                    return "0#No Data Found$";
            }

            return sb.ToString();
        }
        private string PaymentTypeSearch(string paymenttype)
        {
            paymenttype_Prop paymenttypeprop = new paymenttype_Prop();
            paymenttypeprop.PaymentType = paymenttype;
            paymenttype_bal paymenttypebal = new paymenttype_bal();
            DataTable dt = paymenttypebal.PaymentTypeSearch(paymenttypeprop );
            StringBuilder sb = new StringBuilder();

            if (paymenttypebal.ErrMessage != "" && paymenttypebal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymenttypebal.ErrMessage + "</td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                { sb.Append("<tr><td >No Record Found</td><td></td></tr>"); }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["paymenttype"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/icon_delete.png' alt='delete' onclick=DeletePaymentType('");
                        sb.Append(dt.Rows[i]["paymentid"].ToString() + "#" + dt.Rows[i]["paymenttype"].ToString());
                        sb.Append("') /></td>");
                        sb.Append("</tr>");
                    }
                }
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
