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
    public class bank : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string bankcode = "";
            string bankname = "";
            string strmsg = "";
            switch (type)
            {
                case "insert":
                    bankcode = context.Request["bankcode"].ToString();
                    bankname = context.Request["bankname"].ToString();
                    strmsg = BankInsert(bankcode, bankname);
                    break;
                case "search":
                    bankcode = context.Request["bankcode"].ToString();
                    bankname = context.Request["bankname"].ToString();
                    strmsg = BankSearch(bankcode, bankname);
                    break;
                case "populate":
                    strmsg = BankPopulate();
                    break;
                case "delete":
                    bankcode = context.Request["bankcode"].ToString();
                    strmsg = BankDelete(bankcode);
                    break;
                default:
                    break;
            }
            context.Response.Write(strmsg);
        }
        private string BankInsert(string bankcode, string bankname)
        {
            bank_Prop bankprop = new bank_Prop();
            bankprop.BankCode = bankcode;
            bankprop.BankName = bankname;
            bank_bal bankbal = new bank_bal();
            string strMsg = bankbal.BankInsert(bankprop);
            if (bankbal.ErrMessage != "" && bankbal.ErrMessage != null)
                return bankbal.ErrMessage;
            else
                return strMsg;

        }
        private string BankDelete(string bankcode)
        {
            bank_Prop bankprop = new bank_Prop();
            bankprop.BankCode = bankcode;
            bank_bal bankbal = new bank_bal();
            string strMsg = bankbal.BankDelete(bankprop);
            if (bankbal.ErrMessage != "" && bankbal.ErrMessage != null)
                return bankbal.ErrMessage;
            else
                return strMsg;

        }
        private string BankPopulate()
        {
            bank_Prop bankprop = new bank_Prop();
            bankprop.BankName = "";
            bankprop.BankCode = ""; 
            bank_bal bankbal = new bank_bal();
            DataTable dt = bankbal.BankSearch(bankprop);
            StringBuilder sb = new StringBuilder();

            if (bankbal.ErrMessage != "" && bankbal.ErrMessage != null)
            {
                return "0#" + bankbal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sbtxt = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbtxt.Append(dt.Rows[i]["bankid"].ToString() + "#" + dt.Rows[i]["bankname"].ToString() + "$");
                    }
                    return sbtxt.ToString();
                }
                else
                    return "0#No Data Found$";
            }

            return sb.ToString();
        }
        private string BankSearch(string bankcode, string bankname)
        {
            bank_Prop bankprop = new bank_Prop();
            bankprop.BankCode = bankcode;
            bankprop.BankName = bankname;
            bank_bal bankbal = new bank_bal();
            DataTable dt = bankbal.BankSearch(bankprop);
            StringBuilder sb = new StringBuilder();

            if (bankbal.ErrMessage != "" && bankbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + bankbal.ErrMessage + "</td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                { sb.Append("<tr><td >No Record Found</td><td></td><td></td></tr>"); }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["bankcode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["bankname"].ToString() + "</td>");
                        sb.Append("<td><img src='images/icon_delete.png'  alt='delete' onclick=DeleteBank('" + dt.Rows[i]["bankid"].ToString() +"') /> </td>");
                        //sb.Append("<td ><img src='images/icon_delete.png' alt='delete' onclick=DeleteBank('");
                        //sb.Append(dt.Rows[i]["bankcode"].ToString());
                        //sb.Append("') /></td>");
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
