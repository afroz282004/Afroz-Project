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
    public class dailycollection : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string strMsg = "";
            switch (type)
            {
                case "insert":
                    double OneThousand = Convert.ToDouble(context.Request["txtOneThosand"].ToString());
                    double FiveHundred = Convert.ToDouble(context.Request["txtFiveHundred"].ToString());
                    double ConHundred = Convert.ToDouble(context.Request["txtHundred"].ToString());
                    double fifty = Convert.ToDouble(context.Request["txtFifty"].ToString());
                    double twenty = Convert.ToDouble(context.Request["txtTwenty"].ToString());
                    double Ten = Convert.ToDouble(context.Request["txtTen"].ToString());
                    double Five = Convert.ToDouble(context.Request["txtFive"].ToString());
                    double Two = Convert.ToDouble(context.Request["txtTwo"].ToString());
                    double One = Convert.ToDouble(context.Request["txtOne"].ToString());
                    string iusername = context.Request["username"].ToString();
                    string coldate = context.Request["cdate"].ToString();
                    double cashAmt =Convert.ToDouble(context.Request["cashAmt"].ToString());
                    double chequeAmt =Convert.ToDouble( context.Request["chequeAmt"].ToString());

                    strMsg = DailyCollectionInsert(OneThousand, FiveHundred, ConHundred, fifty, twenty, Ten, Five, Two, One, iusername, coldate, cashAmt, chequeAmt);
                    break;
                case "cashcollection":
                    string username= context.Request["username"].ToString();
                    string cdate = context.Request["date"].ToString();
                    if (cdate != "")
                    {
                        cdate = DateTime.Today.ToString("dd/MM/yyyy");
                    }
                    strMsg = CollectionSelect(username, cdate);
                    break;
                case "cashamount":
                    string cusername = context.Request["username"].ToString(); 
                    strMsg = CashAmountSelect(cusername);
                    break;
                case "GetCollectionDetails":
                    string cdateDailyCollection = context.Request["cdate"].ToString();
                    string cgusername = context.Request["username"].ToString();
                    strMsg = DailyCollectionDetails(cdateDailyCollection, cgusername);
                    break;

            }
            context.Response.Write(strMsg);
        }

        private string DailyCollectionDetails(string cdateDailyCollection, string username)
        {
            dailycollection_Prop dailycollectionprop = new dailycollection_Prop();           
            dailycollectionprop.CDate = cdateDailyCollection;
            dailycollectionprop.UserName = username;

            dailycollection_bal dailycollectionbal = new dailycollection_bal();

            DataSet ds = dailycollectionbal.DailyCollectionDetails(dailycollectionprop);
            string strMsg = "";

            if (dailycollectionbal.ErrMessage != "" && dailycollectionbal.ErrMessage != null)
                return dailycollectionbal.ErrMessage;
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        strMsg += ds.Tables[0].Rows[i][0].ToString() + "#" + ds.Tables[0].Rows[i][1].ToString() + "#";
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            strMsg += ds.Tables[1].Rows[i]["ThosandNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["FiveHundredNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["HundredNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["FiftyNote"].ToString() + "#" + ds.Tables[1].Rows[i]["TwentyNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["TenNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["FiveNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["TwoNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["OneNotes"].ToString() + "#" + ds.Tables[1].Rows[i]["Amount"].ToString();
                        }
                    }
                }
                else
                    return "No Record Found";
            }
            return strMsg;
        }

        private string CashAmountSelect(string username)
            {
                
                dailycollection_Prop dailycollectionprop = new dailycollection_Prop();
                dailycollectionprop.UserName = username;
                
                dailycollection_bal dailycollectionbal = new dailycollection_bal();

                DataTable dt = dailycollectionbal.DailyCashSelect(dailycollectionprop);
                string strMsg = "";

                if (dailycollectionbal.ErrMessage != "" && dailycollectionbal.ErrMessage != null)
                    return dailycollectionbal.ErrMessage;
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            strMsg += dt.Rows[i]["Mode"].ToString() + "#" + dt.Rows[i]["Amount"].ToString() + "~";
                        }
                        
                    }
                    else
                        return "No Record Found";
                }
                return strMsg;
            }
        private string CollectionSelect(string username, string strdate)
        {
            Common cm = new Common();
            dailycollection_Prop dailycollectionprop = new dailycollection_Prop();
            dailycollectionprop.UserName = username;
            dailycollectionprop.CDate = cm.MMDDYYYY(strdate); 
            dailycollection_bal  dailycollectionbal = new dailycollection_bal ();
            DataTable dt = dailycollectionbal.DailyCollectionSelect(dailycollectionprop );
            string strMsg = "";
            if (dailycollectionbal.ErrMessage != "" && dailycollectionbal.ErrMessage != null)
                return dailycollectionbal.ErrMessage;
            else
            {
                if (dt.Rows.Count > 0)
                {
                    strMsg = dt.Rows[0]["ThosandNotes"].ToString() + "#" + dt.Rows[0]["FiveHundredNotes"].ToString() + "#" + dt.Rows[0]["HundredNotes"].ToString() + "#" + dt.Rows[0]["FiftyNote"].ToString() + "#" + dt.Rows[0]["TwentyNotes"].ToString() + "#" + dt.Rows[0]["TenNotes"].ToString() + "#" + dt.Rows[0]["FiveNotes"].ToString() + "#" + dt.Rows[0]["TwoNotes"].ToString() + "#" + dt.Rows[0]["OneNotes"].ToString();
                }
                else
                    return "No Record Found";
            }
            return strMsg;
        }
        private string DailyCollectionInsert(double OneThousand, double FiveHundred, double ConHundred, double fifty, double twenty, double Ten, double Five, double Two, double One, string username, string cdate, double cashAmt, double chequeAmt)
        {
            dailycollection_Prop dailycollectionprop = new dailycollection_Prop();
            Common cm = new Common();
            dailycollectionprop.ThosandNotes = OneThousand;
            dailycollectionprop.FiveHundredNotes = FiveHundred;
            dailycollectionprop.HundredNotes = ConHundred;
            dailycollectionprop.FiftyNote = fifty;
            dailycollectionprop.TwentyNotes = twenty;
            dailycollectionprop.TenNotes = Ten;
            dailycollectionprop.FiveNotes = Five;
            dailycollectionprop.TwoNotes = Two;
            dailycollectionprop.OneNotes = One;
            dailycollectionprop.UserName = username ;
            dailycollectionprop.CDate = cdate;
            dailycollectionprop.CashAmt = cashAmt;
            dailycollectionprop.ChequeAmt = chequeAmt;

            string strMsg = "";
            dailycollection_bal dailycollectionbal = new dailycollection_bal();
            strMsg = dailycollectionbal.DailyCollectionInsert (dailycollectionprop);
            if (dailycollectionbal.ErrMessage != "" && dailycollectionbal.ErrMessage != null)
                return dailycollectionbal.ErrMessage;
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
