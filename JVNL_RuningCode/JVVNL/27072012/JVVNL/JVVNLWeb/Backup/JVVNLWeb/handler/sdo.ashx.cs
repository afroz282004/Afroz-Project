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
    public class sdo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string sdocode="";
            string sdoname="";
            string strmsg = "";
            switch (type)
            {
                case "insert":
                     sdocode= context.Request["sdocode"].ToString();
                     sdoname= context.Request["sdoname"].ToString();
                     strmsg = SDOInsert(sdocode, sdoname);
                     break;
                case "search":
                     sdocode = context.Request["sdocode"].ToString();
                     sdoname = context.Request["sdoname"].ToString();
                     strmsg = SDOSearch(sdocode, sdoname);
                     break;
                case "populate":
                     strmsg = SDOPopulate();
                     break;
                case "delete":
                     sdocode = context.Request["sdocode"].ToString();
                     strmsg = SDODelete(sdocode);
                     break;
                default:
                    break;
            }
            context.Response.Write(strmsg);
        }
        private string SDOInsert(string sdocode, string sdoname)
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = sdocode;
            sdoprop.SDOName = sdoname;
            sdo_bal sdobal = new sdo_bal();
            string strMsg = sdobal.SDOInsert(sdoprop );
            if (sdobal.ErrMessage != "" && sdobal.ErrMessage != null)
                return sdobal.ErrMessage;
            else
                return strMsg;

        }
        private string SDODelete(string sdocode)
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = sdocode;
            sdo_bal sdobal = new sdo_bal();
            string strMsg = sdobal.SDODelete(sdoprop);
            if (sdobal.ErrMessage != "" && sdobal.ErrMessage != null)
                return sdobal.ErrMessage;
            else
                return strMsg;

        }
        private string SDOPopulate()
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = "";
            sdoprop.SDOName = "";
            sdo_bal sdobal = new sdo_bal();
            DataTable dt = sdobal.SDOSearch(sdoprop);
            StringBuilder sb = new StringBuilder();

            if (sdobal.ErrMessage != "" && sdobal.ErrMessage != null)
            {
                return "0#" + sdobal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sbtxt = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbtxt.Append( dt.Rows[i]["sdocode"].ToString().Trim() + "#" + dt.Rows[i]["sdoname"].ToString() + "$");
                    }
                    return  sbtxt.ToString();
                }
                else
                    return "0#No Data Found$";
            }

            return sb.ToString();
        }

        private string SDOSearch(string sdocode, string sdoname)
        {
            sdo_Prop sdoprop = new sdo_Prop();
            sdoprop.SDOCode = sdocode;
            sdoprop.SDOName = sdoname;
            sdo_bal sdobal = new sdo_bal();
            DataTable dt = sdobal.SDOSearch(sdoprop );
            StringBuilder sb = new StringBuilder();

            if (sdobal.ErrMessage != "" && sdobal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + sdobal.ErrMessage + "</td><td></td><td></td></tr>");
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
                        sb.Append("<td>" + dt.Rows[i]["sdocode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["sdoname"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/icon_delete.png' alt='delete' onclick=DeleteSDO('");
                        sb.Append(dt.Rows[i]["sdocode"].ToString().Trim());
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
