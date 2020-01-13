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
    public class CollectionBoyDetails : IHttpHandler
    {

        string strMsg;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strmsg = "";
            string type = context.Request["type"].ToString();
            switch (type)
            {
                case "insert":
                    int counterID = Convert.ToInt16(context.Request["counterid"]);
                    string boyname = context.Request["boyname"].ToString();
                    DateTime cdate = Convert.ToDateTime(context.Request["cdate"]);
                    strmsg = CashCollectionBoyInsert(counterID, boyname, cdate);
                    break;
                
                default:
                    break;
            }
            context.Response.Write(strmsg);
        }

        private string CashCollectionBoyInsert(int counterID, string boyname, DateTime cdate)
        {
            cashcollection_Prop cashcollectionprop = new cashcollection_Prop();
            cashcollectionprop.CounterID = counterID;
            cashcollectionprop.BoyName = boyname;
            cashcollectionprop.cDate = cdate;

            cashcollection_bal cashcollectionbal = new cashcollection_bal();
            string strMsg = cashcollectionbal.CashCollectionInsert (cashcollectionprop);
            if (cashcollectionbal.ErrMessage != "" && cashcollectionbal.ErrMessage != null)
                return cashcollectionbal.ErrMessage;
            else
                return strMsg;
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
