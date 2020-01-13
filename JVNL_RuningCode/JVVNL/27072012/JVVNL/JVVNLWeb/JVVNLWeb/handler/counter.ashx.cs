using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using JVVNLClassLIB ;
using System.Text;

namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class counter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strmsg = "";
            string type = context.Request["type"].ToString();
            switch (type)
            {
                case "insert":
                    string CounterName = context.Request["txtCounterName"].ToString();
                    string Address = context.Request["txtAddress"].ToString();
                    string Contactperson = context.Request["txtContactperson"].ToString();
                    string ContactNumber = context.Request["txtContactNumber"].ToString();
                    strmsg = CounterInsert(CounterName, Address, Contactperson, ContactNumber);
                    break;
                default:
                    break;
                case "search":
                    string scountername = context.Request["countername"].ToString();
                    strmsg = CounterSearch(scountername);
                    break;
                case "Edit":
                    string ids = context.Request["id"].ToString();
                    strmsg = CounterEdit(ids);
                    break;

                case "EditSave":
                    string idCounter = context.Request["id"].ToString();
                    string CounterNameU = context.Request["txtCounterName"].ToString();
                    string AddressU = context.Request["txtAddress"].ToString();
                    string ContactpersonU = context.Request["txtContactperson"].ToString();
                    string ContactNumberU = context.Request["txtContactNumber"].ToString();
                    strmsg = CounterUpdate(idCounter, CounterNameU, AddressU, ContactpersonU, ContactNumberU);
                    break;
                case "Delete":
                    string Delid = context.Request["id"].ToString();
                    strmsg = CounterDelete(Delid);
                    break;

                case "populate":
                    strmsg = PopulateCounter();
                    break;

            }
            context.Response.Write(strmsg);
        }

        private string CounterDelete(string id)
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.Counterid = Convert.ToInt32(id);
            counter_bal counterbal = new counter_bal();
            string strMsg = counterbal.CounterDelete(counterprop);
            return strMsg;
        }

        private string CounterEdit(string id)
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.Counterid = Convert.ToInt32(id);
            counter_bal counterbal = new counter_bal();
            DataTable dt = counterbal.CounterSelect(counterprop);
            string strMsg = "";
            if (dt.Rows.Count > 0)
            {
                strMsg = dt.Rows[0]["CounterName"].ToString() + "#" + dt.Rows[0]["Address"].ToString() + "#" + dt.Rows[0]["ContactPerson"].ToString() + "#" + dt.Rows[0]["ContactNo"].ToString();
            }

            return strMsg;
        }

        private string CounterUpdate(string idCounter, string CounterName, string Address, string Contactperson, string ContactNumber)
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.Counterid =Convert.ToInt16( idCounter);
            counterprop.CounterName = CounterName;
            counterprop.Address = Address;
            counterprop.ContactPerson = Contactperson;
            counterprop.ContactNo = ContactNumber;

            counter_bal counterbal = new counter_bal();
            string strMsg = counterbal.CounterUpdate(counterprop);
            return strMsg;
        }

        private string CounterInsert(string CounterName, string Address, string Contactperson, string ContactNumber)
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.CounterName = CounterName;
            counterprop.Address = Address;
            counterprop.ContactPerson = Contactperson;
            counterprop.ContactNo = ContactNumber;
            counter_bal counterbal = new counter_bal();
            string strMsg = counterbal.CounterInsert(counterprop);
            return strMsg;
        }


        private string CounterSearch( string countername)
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.CounterName = countername;

            counter_bal CounterBal = new counter_bal();
            DataTable dt = CounterBal.CounterSearch(counterprop );


            StringBuilder sb = new StringBuilder();

            if (CounterBal.ErrMessage != "" && CounterBal.ErrMessage != null)
            {
                { sb.Append("<tr><td>" + CounterBal.ErrMessage + "</td><td></td><td></td><td></td><td></td><td></td></tr>"); }
            }
            
            else
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["CounterID"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["CounterName"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Address"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ContactPerson"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ContactNo"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/editrec.png' alt='block' onclick='EditRecord(" + dt.Rows[i]["CounterID"].ToString() + ")'>");
                        sb.Append("<img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + dt.Rows[i]["CounterID"].ToString() + ")'></td>");
                        sb.Append("</tr>");
                    }
                }
                else
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td></tr>");
            }


            return sb.ToString();
        }


        private string PopulateCounter()
        {
            counter_Prop counterprop = new counter_Prop();
            counterprop.CounterName = "";
            counter_bal counterbal = new counter_bal();
            DataTable dt = counterbal.CounterSearch(counterprop);
            if (counterbal.ErrMessage != "" && counterbal.ErrMessage != null)
            {
                return "0#" + counterbal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(dt.Rows[i]["counterid"].ToString() + "#" + dt.Rows[i]["countername"].ToString() + "$");
                    }
                    return sb.ToString();
                }
                else
                    return "0#No Data Found$";
            }
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