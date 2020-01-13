using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.ReportSource;
using JVVNLClassLIB;
namespace JVVNLWeb
{
    public partial class crreport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["ID"].ToString() != null && Request["ID"].ToString() != "")
                {
                    SpViewReport(Request["ID"].ToString());
                }
            }
            
            
        }
        private void SpViewReport(string Paymentid)
        {
            
            CrystalReport1 cr = new CrystalReport1();

            cr.Load(Server.MapPath("CrystalReport1.rpt"));
     
            payment_Prop paymentprop = new payment_Prop();
            paymentprop.PaymentID =Convert.ToInt32( Paymentid);
            payment_bal paymentbal = new payment_bal();
            DataTable dt = paymentbal.PaymentSelect(paymentprop);
            cr.SetDataSource(dt);
            TextObject txt = (TextObject)cr.Section3.ReportObjects["txtAmtInWords"];
            string AmtinWords = retWord(Convert.ToInt32(Math.Round(Convert.ToDouble(dt.Rows[0]["amount"].ToString()), 0).ToString()));
            txt.Text = AmtinWords;
            //cr.PrintToPrinter(1,true,1,1);
            CrystalReportViewer1.ReportSource = cr;
            
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

    }
}
