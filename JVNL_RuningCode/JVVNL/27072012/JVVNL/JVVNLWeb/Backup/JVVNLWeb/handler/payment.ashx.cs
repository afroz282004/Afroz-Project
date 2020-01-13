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
using iTextSharp.text.pdf ;
using iTextSharp.text;
using iTextSharp.text.xml;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using System.Management;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing.Printing;
namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class payment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"].ToString();
            string strmsg = "";
            switch (type)
            {
                case "billing":
                    string SDOCode = context.Request["subdivision"].ToString();
                    string binderno = context.Request["binderno"].ToString();
                    string accountno = context.Request["accountno"].ToString();
                    strmsg = BillingSelect(SDOCode, binderno + accountno);
                    break;
                case "insert":
                    int billid = Convert.ToInt32(context.Request["BillID"].ToString());
                    string paymenttype = context.Request["paymenttype"].ToString();
                    string mode = context.Request["mode"].ToString();
                    string Amount = context.Request["amount"].ToString();
                    string Bankid = context.Request["bankid"].ToString();
                    string CDate = context.Request["chequedate"].ToString().Replace("_", "/");
                    string CNo = context.Request["chequeno"].ToString();
                    string PhoneNo = context.Request["phoneno"].ToString();
                    string pusername = context.Request["username"].ToString();
                    string emailid = context.Request["EmailId"].ToString();
                    string form = context.Request["form"].ToString();
                    strmsg = form;
                    if (form=="single")
                        strmsg = PaymentInsert(billid, mode, Amount, Bankid, CDate, CNo, PhoneNo, paymenttype, pusername, emailid,"0");
                    else
                        strmsg = PaymentInsert(billid, mode, Amount, Bankid, CDate, CNo, PhoneNo, paymenttype, pusername, emailid, "0");

                    if (form == "single")
                    {
                        if (strmsg.IndexOf("success") >= 0 && PhoneNo != "")
                        {
                            strmsg += "#" + SMSThread(PhoneNo, "We wish to confirm the receipt of your payment of Rs." + Amount);
                        }
                        if (strmsg.IndexOf("success") >= 0 && emailid != "")
                        {
                            strmsg += "#" + EmailThread(emailid, "We wish to confirm the receipt of your payment of Rs." + Amount);
                        }
                    }
                    break;
                case "insert_single":
                    int billid1 = Convert.ToInt32(context.Request["BillID"].ToString());
                    string paymenttype1 = context.Request["paymenttype"].ToString();
                    string mode1 = context.Request["mode"].ToString();
                    string Amount1 = context.Request["amount"].ToString();
                    string Bankid1 = context.Request["bankid"].ToString();
                    string CDate1 = context.Request["chequedate"].ToString().Replace("_", "/");
                    string CNo1 = context.Request["chequeno"].ToString();
                    string PhoneNo1 = context.Request["phoneno"].ToString();
                    string pusername1 = context.Request["username"].ToString();
                    string emailid1 = context.Request["EmailId"].ToString();
                    string form1 = context.Request["form"].ToString();
                    strmsg = form1;
                    if (form1 == "single")
                        strmsg = PaymentInsert_single(billid1, mode1, Amount1, Bankid1, CDate1, CNo1, PhoneNo1, paymenttype1, pusername1, emailid1, "0");
                   
                    if (form1 == "single")
                    {
                        if (strmsg.IndexOf("success") >= 0 && PhoneNo1 != "")
                        {
                            strmsg += "#" + SMSThread(PhoneNo1, "We wish to confirm the receipt of your payment of Rs." + Amount1);
                        }
                        if (strmsg.IndexOf("success") >= 0 && emailid1 != "")
                        {
                            strmsg += "#" + EmailThread(emailid1, "We wish to confirm the receipt of your payment of Rs." + Amount1);
                        }
                    }
                    break;
                case "consumerselect":
                    string sSDOCode = context.Request["subdivision"].ToString();
                    string Name = context.Request["name"].ToString();
                    string saccountno = context.Request["accountno"].ToString();
                    strmsg = ConsumerSelect(sSDOCode, Name, saccountno);
                    break;
                case "chequeselect":
                    int cBankid = Convert.ToInt16(context.Request["bankid"].ToString());
                    string cchequeno = context.Request["chequeno"].ToString();
                    string chequepaymentdate = context.Request["chequedate"].ToString();
                    strmsg = PaymentChequeSelect(cBankid, cchequeno,chequepaymentdate );
                    break;


                case "chequeselectnew":
                    int cBankid1 = Convert.ToInt16(context.Request["bankid"].ToString());
                    string cchequeno1 = context.Request["chequeno"].ToString();
                    string chequepaymentdate1 = context.Request["chequedate"].ToString();
                    strmsg = PaymentChequeSelect_new(cBankid1, cchequeno1, chequepaymentdate1);
                    break;

                case "delete":
                    string paymentid = context.Request["paymentid"].ToString();
                    strmsg = PaymentDelete(Convert.ToInt16(paymentid));
                    break;
                case "summary":
                    string payusername= context.Request["username"].ToString();
                    strmsg = PaymentSummary(payusername);
                    break;
                case "checkclosedtrasction":
                    string user = context.Request["user"].ToString();
                    strmsg = checkclosedtrasction(user.ToString());
                    break;
                case "pdf":
                    int ppaymentid = Convert.ToInt16(context.Request["paymentid"].ToString());
                    strmsg = GenerateReceipt(ppaymentid);
                    break;
                case "sms":
                    int spaymentid = Convert.ToInt16(context.Request["paymentid"].ToString());
                    strmsg = SendSMS(spaymentid);
                    break;
                case "usersummary":
                    string fromdate = context.Request["fromdate"].ToString();
                    string todate = context.Request["todate"].ToString();
                    string username = context.Request["username"].ToString();
                    strmsg = PaymentUserSummary(username, fromdate, todate);
                    break;
                case "closetransaction":
                    string cfromdate = context.Request["fromdate"].ToString();
                    strmsg = PaymentCloseTransaction(cfromdate);
                    break;

                //case "deletemultiple":
                //    payment_bal payment = new payment_bal();
                //    payment.PaymentDeleteMultiple();
                //    break;
                case "update":
                    int uBankid = Convert.ToInt16(context.Request["bankid"].ToString());
                    string uchequeno = context.Request["chequeno"].ToString();
                    string uhequepaymentdate = context.Request["chequedate"].ToString();
                    strmsg = PaymentUpdateMultiple(uBankid, uchequeno, uhequepaymentdate);
                    break;
                case "billids":
                    string billids = context.Request["billids"].ToString();
                    strmsg = PaymentIDs(billids);
                    break;
                default:
                    break;
            }
            context.Response.Write(strmsg.ToString());
        }
        private string PaymentIDs(string BillIds)
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.BillIDs = BillIds.Substring(1);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelectByBillID(paymentprop);
            string strData = "";
            if (dt != null)
            { 
                for (int i=0; i < dt.Rows.Count; i++)
                {
                    strData = strData + "," + dt.Rows[i][0].ToString();
                }
            }
            return strData;

        }
        private string PaymentCloseTransaction(string fromdate)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.Fromdate = cm.MMDDYYYY(fromdate);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentClosedTransaction(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymentbal.ErrMessage + "</td><td></td><td></td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["CounterName"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["username"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalTrans"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>");
                        sb.Append("<td onclick=ShowTrans('" + dt.Rows[i]["username"].ToString() + "')><img src='images/closetransaction.png' alt='close'/></td>");
                        sb.Append("</tr>");
                    }
                }
            }
            return sb.ToString();
        }
        private string PaymentUserSummary(string username, string fromdate, string todate)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.UserName = username;
            paymentprop.Fromdate = cm.MMDDYYYY(fromdate);
            paymentprop.Todate = cm.MMDDYYYY(todate);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentUserSummary(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymentbal.ErrMessage + "</td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dt.Rows[i]["Subdivisioncode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["ConsumerAccountNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["paymentdate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["mode"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["username"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
            }
            return sb.ToString();
        }

        private string SendSMS(int id)
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.PaymentID = id;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelect(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                return "Unable to send message";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string Amount = dt.Rows[0]["Amount"].ToString();
                    string PhoneNo = dt.Rows[0]["PhoneNo"].ToString();
                    return SMSThread(PhoneNo, "We wish to confirm the receipt of your payment of Rs." + Amount);
                }
                else
                {
                    return "Unable to send message";
                }
            }
        }
        private string PaymentSummary(string username)
        {
            payment_bal paymentbal = new payment_bal();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.UserName = username;
            DataTable dt = paymentbal.PaymentSummary(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("0#0#0#0#0#0#0#");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                    sb.Append("0#0#0#0#0#0#0#");
                else
                    sb.Append(dt.Rows[0]["LastReceiptNo"].ToString() + "#" + dt.Rows[0]["LastReceiptAmount"].ToString() + "#" + dt.Rows[0]["TotalCash"].ToString() + "#" + dt.Rows[0]["TotalCheque"].ToString() + "#" + dt.Rows[0]["TotalReceipt"].ToString() + "#" + dt.Rows[0]["NoofCheques"].ToString() + "#" + dt.Rows[0]["TotalAmount"].ToString() + "#");
            }
            return sb.ToString();
        }
        private string PaymentDelete(int paymentid)
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.PaymentID = paymentid;
            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.PaymentDelete(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
        }
        private string checkclosedtrasction(string user)
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.UserName = user;
            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.checkclosedtrasction(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
        } 
        private string PaymentChequeSelect(int bankid, string chequeno, string chedate)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.Bankid = bankid;
            paymentprop.ChequeNo = chequeno;
            paymentprop.ChequeDate = cm.MMDDYYYY( chedate); 
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentChequeSelect(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymentbal.ErrMessage + "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToInt16(i + 1).ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SubdivisionID"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AccountNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["paymentdate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["paymenttype"].ToString() + "</td>");
                        sb.Append("<td>" + Convert.ToDecimal( dt.Rows[i]["Amount"].ToString()).ToString("0.00") + "</td>");
                        //sb.Append("<td><img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + dt.Rows[i]["paymentid"].ToString() + ")'><img src='images/receipt.png' alt='receipt' onclick='GenerateReceipt(" + dt.Rows[i]["paymentid"].ToString() + ")'><img src='images/sms.png' alt='sms' onclick='SendSMS(" + dt.Rows[i]["paymentid"].ToString() + ")'></td>");
                        sb.Append("<td><img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + dt.Rows[i]["paymentid"].ToString() + "," + dt.Rows[i]["amount"].ToString() + ")'></td>");
                        sb.Append("</tr>");
                    }
                }
            }
            return sb.ToString();
        }

        private string PaymentChequeSelect_new(int bankid, string chequeno, string chedate)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.Bankid = bankid;
            paymentprop.ChequeNo = chequeno;
            paymentprop.ChequeDate = cm.MMDDYYYY(chedate);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentChequeSelect_new(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymentbal.ErrMessage + "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                {
                    sb.Append("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToInt16(i + 1).ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["SubdivisionID"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["AccountNo"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["paymentdate"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["paymenttype"].ToString() + "</td>");
                        sb.Append("<td>" + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()).ToString("0.00") + "</td>");
                        //sb.Append("<td><img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + dt.Rows[i]["paymentid"].ToString() + ")'><img src='images/receipt.png' alt='receipt' onclick='GenerateReceipt(" + dt.Rows[i]["paymentid"].ToString() + ")'><img src='images/sms.png' alt='sms' onclick='SendSMS(" + dt.Rows[i]["paymentid"].ToString() + ")'></td>");
                        sb.Append("<td><img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + dt.Rows[i]["paymentid"].ToString() + "," + dt.Rows[i]["amount"].ToString() + ")'></td>");
                        sb.Append("</tr>");
                    }
                }
            }
            return sb.ToString();
        }

        private string PaymentUpdateMultiple(int bankid, string chequeno, string chedate)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.Bankid = bankid;
            paymentprop.ChequeNo = chequeno;
            paymentprop.ChequeDate = cm.MMDDYYYY(chedate);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentUpdateMultiple(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append(paymentbal.ErrMessage );
            }
            else
            {
                sb.Append(dt.Rows[0][0].ToString()); 
            }
            return sb.ToString();
        }
        private string ConsumerSelect(string subdivisioncode, string name, string saccountno)
        {
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.SubdivisionCode = subdivisioncode;
            paymentprop.Name = name;
            paymentprop.AccountNo = saccountno;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.ConsumerSelect(paymentprop);
            StringBuilder sb = new StringBuilder();
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
            {
                sb.Append("<tr><td>" + paymentbal.ErrMessage + "</td><td></td><td></td></tr>");
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
                        sb.Append("<td onclick=Populate('" + dt.Rows[i]["SubDivisionID"].ToString() + "#" + dt.Rows[i]["AccountNO"].ToString() + "')>" + dt.Rows[i]["AccountNO"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Name"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["Address"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
            }
            return sb.ToString();
        }
        private string PaymentInsert(int BillID, string Mode, string Amount, string Bankid, string CDate, string CNo, string PhoneNo, string paymenttype, string username, string emailid, string status)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.BillID = BillID;
            paymentprop.SubdivisionCode = paymenttype;
            paymentprop.Mode = Mode;
            paymentprop.Amount = Convert.ToDouble(Amount);
            paymentprop.Bankid = Convert.ToInt16(Bankid.Replace("select", "0"));
            paymentprop.ChequeDate = CDate == "" ? "01/01/1900" : cm.MMDDYYYY(CDate);
            paymentprop.ChequeNo = CNo;
            paymentprop.PhoneNo = PhoneNo;
            paymentprop.UserName = username;
            paymentprop.Emailid = emailid;
            paymentprop.status = Convert.ToInt32( status);

            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.PaymentInsert(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
        }
        private string PaymentInsert_single(int BillID, string Mode, string Amount, string Bankid, string CDate, string CNo, string PhoneNo, string paymenttype, string username, string emailid, string status)
        {
            Common cm = new Common();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.BillID = BillID;
            paymentprop.SubdivisionCode = paymenttype;
            paymentprop.Mode = Mode;
            paymentprop.Amount = Convert.ToDouble(Amount);
            paymentprop.Bankid = Convert.ToInt16(Bankid.Replace("select", "0"));
            paymentprop.ChequeDate = CDate == "" ? "01/01/1900" : cm.MMDDYYYY(CDate);
            paymentprop.ChequeNo = CNo;
            paymentprop.PhoneNo = PhoneNo;
            paymentprop.UserName = username;
            paymentprop.Emailid = emailid;
            paymentprop.status = Convert.ToInt32(status);

            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.PaymentInsert_single(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
        }
        private string BillingSelect(string SDOCode, string AccountNo)
        {
            StringBuilder sb = new StringBuilder();
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.SubdivisionCode = SDOCode;
            paymentprop.AccountNo = AccountNo;
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.BillingSelect(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sb.Append(dt.Rows[0][i].ToString() + "#");
                    }
                    return sb.ToString() + SDOCode;
                }
                else
                    return "No Data Found";
            }


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
        public static string GetDefaultPrinterName()
        {
            try
            {
                var query = new ObjectQuery("SELECT * FROM Win32_Printer");
                string strComputer = "computerName";

                ManagementScope msc = new ManagementScope("\\\\192.168.102.81\\root\\cimv2");
                msc.Connect();
                var searcher = new ManagementObjectSearcher(msc, query);
                searcher.Scope = msc;
                foreach (ManagementObject mo in searcher.Get())
                {
                    if (((bool?)mo["Default"]) ?? false)
                    {
                        return mo["Name"] as string;
                    }
                   
                }

              return "Error1";
            }
            catch (ManagementException ex)
            {
                return ex.Message ;
            }
            
           
        }

        private string GenerateReceipt(int Paymentid)
        {
            try
            {
                
                CrystalReport1 cr = new CrystalReport1();
                
                //this.printDialog1.Document = this.printDocument1;
                //string PrinterName = this.printDocument1.PrinterSettings.PrinterName;
                // System.Drawing.Printing.PrinterSettings settings = new PrinterSettings();
                string Printer = GetDefaultPrinterName();
                //int i=0;
                //foreach (string Printer_loopVariable in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                //{

                //    if (i == 1)
                //    {
                //        Printer = Printer_loopVariable;
                //    }
                //    i = i + 1;
                //}
               //return Printer;
               // if (Printer == "Error1")
               // {
               //     return "Error1d";
               // }
               // else
               // {
                //cr.PrintOptions.PrinterName = @"\\192.168.102.81\Send To OneNote 2007";
                cr.PrintOptions.PrinterName = "Thermal";
                //}
                
              //  System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings();
                //cr.PrintOptions.PrinterName = oPS.PrinterName;
                payment_Prop paymentprop = new payment_Prop();
                paymentprop.PaymentID = Paymentid;
                payment_bal paymentbal = new payment_bal();
                DataTable dt = paymentbal.PaymentSelect(paymentprop);
                cr.SetDataSource(dt);
                TextObject txt = (TextObject)cr.Section3.ReportObjects["txtAmtInWords"];
                string AmtinWords = retWord(Convert.ToInt32(Math.Round(Convert.ToDouble(dt.Rows[0]["amount"].ToString()), 0).ToString()));
                txt.Text = AmtinWords;
     
                cr.PrintToPrinter(1, true, 1, 1);
                string s = cr.PrintOptions.PrinterName.ToString();
                return s;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string retWord(int number)
        {

            if (number == 0) return "Zero";

            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

            int[] num = new int[4];

            int first = 0;

            int u, h, t;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {

                sb.Append("Minus ");

                number = -number;

            }

            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };

            string[] words = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };

            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            num[0] = number % 1000; // units

            num[1] = number / 1000;

            num[2] = number / 100000;

            num[1] = num[1] - 100 * num[2]; // thousands

            num[3] = number / 10000000; // crores

            num[2] = num[2] - 100 * num[3]; // lakhs



            for (int i = 3; i > 0; i--)
            {

                if (num[i] != 0)
                {

                    first = i;

                    break;

                }

            }

            for (int i = first; i >= 0; i--)
            {

                if (num[i] == 0) continue;

                u = num[i] % 10; // ones

                t = num[i] / 10;

                h = num[i] / 100; // hundreds

                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {

                    if (h > 0 || i == 0) sb.Append("and ");

                    if (t == 0)

                        sb.Append(words0[u]);

                    else if (t == 1)

                        sb.Append(words[u]);

                    else

                        sb.Append(words2[t - 2] + words0[u]);

                }

                if (i != 0) sb.Append(words3[i - 1]);

            }

            return sb.ToString().TrimEnd();

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