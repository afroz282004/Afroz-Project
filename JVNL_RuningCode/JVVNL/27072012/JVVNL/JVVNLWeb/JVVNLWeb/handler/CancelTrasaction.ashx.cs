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
using System.Threading;
using System.Net;
using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.xml;
using System.Net.Mail;

namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CancelTrasaction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string strmsg = "";
            switch (type)
            {

                case "CancelBill":
                    int billid = Convert.ToInt32(context.Request["billid"].ToString());
                    string fusername = context.Request["fusername"].ToString();
                    string PhoneNo = context.Request["phoneno"].ToString();
                    string emailid = context.Request["email"].ToString();
                    string Amount = context.Request["PayAmount"].ToString();
                    strmsg = CancelBill(billid, fusername);
                    if (strmsg.IndexOf("success") >= 0 && PhoneNo != "")
                    {
                        strmsg += "#" + SMSThread(PhoneNo, "your payment of Rs." + Amount + "Cancel Successfully");
                    }
                    if (strmsg.IndexOf("success") >= 0 && emailid != "")
                    {
                        strmsg += "#" + EmailThread(emailid, "your payment of Rs." + Amount + "Cancel Successfully");
                    }
                    break;
                case "consumerselect":
                    string SDOCode = context.Request["subdivision"].ToString();
                    if (SDOCode == "select")
                    {
                        SDOCode = "0";
                    }
                    string binderno = context.Request["binderno"].ToString();
                    string accountno = context.Request["accountno"].ToString();
                    string receiptno = context.Request["receiptno"].ToString();
                    strmsg = BillingSelect(SDOCode, binderno + accountno, receiptno);
                    break;

                default:
                    break;
            }
            context.Response.Write(strmsg.ToString());
        }
        private string SMSThread(string MobileNo, string strMessage)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://203.122.58.168/prepaidgetbroadcast/PrepaidGetBroadcast?userid=trimax12&pwd=trimax12&msgtype=s&ctype=1&sender=DEMO&pno=91" + MobileNo + "&msgtxt=" + strMessage + "&alert=0");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                if (results == "-21")
                    return MobileNo + " is registered with DND";
                else
                    return "Message sent successfully!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string EmailThread(string EmailId, string strMessage)
        {
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://203.122.58.168/prepaidgetbroadcast/PrepaidGetBroadcast?userid=trimax12&pwd=trimax12&msgtype=s&ctype=1&sender=DEMO&pno=91" + MobileNo + "&msgtxt=" + strMessage + "&alert=0");
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    StreamReader sr = new StreamReader(response.GetResponseStream());
            //    string results = sr.ReadToEnd();
            //    sr.Close();
            //    if (results == "-21")
            //        return MobileNo + " is registered with DND";
            //    else
            //        return "Message sent successfully!!!";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}

            String frmgmail = "anilb@trimax.in";     //Replace your gmail id
            String frmpwd = "Trimax@123";            //Replace your mail pwd
            String s = EmailId;

            //Mulitple Recipient id stored in array only for mail from gmail
            String[] toId = s.Split(',');

            String msgsubject = strMessage;


            try
            {
                MailMessage msg = new MailMessage();

                //Below loop add multiple Recipient mail id example
                for (int i = 0; i <= toId.Length - 1; i++)
                {
                    msg.To.Add(toId[i].ToString());
                }

                MailAddress frmAdd = new MailAddress(frmgmail);
                msg.From = frmAdd;


                msg.Subject = "Payment To JVVNL";
                msg.Body = msgsubject;
                //Check for attachment is there
                //if (FileUpload1.HasFile)
                //{
                //    msg.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.FileName));
                //}
                msg.IsBodyHtml = true;


                SmtpClient mailClient = new SmtpClient("smtp.trimax.in", 25);
                NetworkCredential NetCrd = new NetworkCredential(frmgmail, frmpwd);

                mailClient.Credentials = NetCrd;

                mailClient.Send(msg);


                return "Mail sent successfully!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string CancelBill(int sdocode, string fusername)
        {
            payment_Prop lpayment_Prop = new payment_Prop();
            lpayment_Prop.PaymentID = sdocode;
            lpayment_Prop.UserName = fusername;
            payment_bal lpayment_bal = new payment_bal();
            string strMsg = lpayment_bal.BillCancel(lpayment_Prop);
            if (lpayment_bal.ErrMessage != "" && lpayment_bal.ErrMessage != null)
                return lpayment_bal.ErrMessage;
            else
                return strMsg;

        }
        private string BillingSelect(string SDOCode, string AccountNo,string receiptno)
        {
            StringBuilder sb = new StringBuilder();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.SubdivisionCode = SDOCode;
            paymentprop.AccountNo = AccountNo;
            paymentprop.Receiptno = receiptno;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelectCancel(paymentprop);
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
                        sb.Append("<td>" + dt.Rows[i]["Name"].ToString() + "</td>");

                        sb.Append("<td>" + dt.Rows[i]["billdate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["duedate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["BillAmount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["PayAmount"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/icon_delete.png' alt='delete' onclick=DeleteSDO('");
                        sb.Append(dt.Rows[i]["paymentid"].ToString().Trim() + '$' + dt.Rows[i]["Phoneno"].ToString().Trim() + '$' + dt.Rows[i]["EmailId"].ToString().Trim() + '$' + dt.Rows[i]["PayAmount"].ToString().Trim());
                        sb.Append("') /></td>");
                        sb.Append("</tr>");
                    }
                }
                return sb.ToString();
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