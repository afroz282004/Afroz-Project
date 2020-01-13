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
using System.Threading;
using System.Net;
using System.IO;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;
using System.ComponentModel;
using JVVNLClassLIB;

namespace JVVNLWeb
{
    public partial class multiple : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void complete_Click(object sender, EventArgs e)
        {
            string strpaymentdata = paymentdata.Value;
            string[] paydatarow = strpaymentdata.Split('$');
            string[] data = null;
            for (int i = 0; i < paydatarow.Length-1; i++)
            {
                data = paydatarow[i].Split('#');
                string billid = data[0];
                string mode = data[1];
                string amt = data[2];
                string bankid = data[3];
                string chequedate = data[4];
                string chequeno = data[5];
                string phoneno = data[6];
                string paymenttype = data[7];
                string username = data[8];
                string emailid = data[9];
                string strmsg = PaymentInsert(Convert.ToInt32 (billid), mode, amt, bankid, chequedate, chequeno, phoneno, paymenttype, username, emailid, "1");
                string []strdata = strmsg.Split('#');
                ClientScript.RegisterStartupScript(this.GetType(), "MyScript" + i, "<script>GenerateReceipt(" + strdata[1] + ");</script>");
                //print.InnerHtml  = "<iframe src='crreport.aspx?ID=" + strdata[1] + "'  onload='this.contentWindow.print();'></iframe>";
                strmsg = SendSMS(Convert.ToInt32( strdata[1]));
            }
            lblError.Text = "Payment Added Successfully!!!";
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
            paymentprop.status = Convert.ToInt32(status);

            payment_bal paymentbal = new payment_bal();
            string strmsg = paymentbal.PaymentInsert(paymentprop);
            if (paymentbal.ErrMessage != "" && paymentbal.ErrMessage != null)
                return paymentbal.ErrMessage;
            else
                return strmsg;
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

    }
}
