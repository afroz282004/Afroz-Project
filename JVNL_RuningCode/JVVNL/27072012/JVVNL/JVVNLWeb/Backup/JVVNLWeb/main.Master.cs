using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using JVVNLClassLIB ;
using System.Text;
namespace JVVNLWeb
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString.Count >= 1 && (Request.Path.IndexOf("counter.aspx") >= 0 || Request.Path.IndexOf("payment.aspx") >= 0 || Request.Path.IndexOf("paymentsummary.aspx") >= 0 || Request.Path.IndexOf("report.aspx") >= 0))
            {
                Session["username"] = Request.QueryString["username"];
                Session["groupid"] = Request.QueryString["groupid"];
                

                if (Request.Path.IndexOf("counter.aspx") >= 0)
                    Response.Redirect("counter.aspx");
                else if (Request.Path.IndexOf("payment.aspx") >= 0)
                    Response.Redirect("payment.aspx");
                else if (Session["username"].ToString() == "importer")
                    Response.Redirect("masterdataimport.aspx");
                else if (Session["groupid"].ToString() == "5")
                {
                    Session["sdocode"] = Request.QueryString["sdocode"] + " ";
                    Response.Redirect("report.aspx");
                }
                else
                    Response.Redirect("paymentsummary.aspx");
            }
            else
            {
                if (Session["username"] == null) { Response.Redirect("default.aspx"); }
              
            }
            if (!IsPostBack)
            {
                lblusername.InnerHtml = Session["username"].ToString() ;
                showmenu.InnerHtml = ShowMenu(Session["username"].ToString());
            }
        }
        private string ShowMenu(string username)
        {
            menu_Prop menuprop = new menu_Prop ();
            menuprop.UserName=username;
            menu_bal menubal = new menu_bal ();
            DataTable dt = menubal.MenuSelect(menuprop);
            StringBuilder sb = new StringBuilder();

            if (dt != null)
            {
                DataRow [] dr = dt.Select("parentid=0");
                if (dr.Length > 0)
                {
                    
                    sb.Append("<ul  id='menu' >");
                    for (int i = 0; i < dr.Length; i++)
                    {
                        sb.Append(" <li><a href='" + dr[i][2].ToString() + "'>" + dr[i][1].ToString() + "<span class='" + dr[i][1].ToString().ToLower ()+ "'></span></a>");
                        DataRow []drsub = dt.Select("parentid=" + Convert.ToInt16(dr[i][0].ToString()));
                        if (drsub != null)
                        {
                            if (drsub.Length > 0)
                            {
                                sb.Append("<ul>");
                                for (int j = 0; j < drsub.Length; j++)
                                {
                                    sb.Append(" <li><a href='" + drsub[j][2].ToString() + "'>" + drsub[j][1].ToString() + "</a></li>");
                                }
                                sb.Append("</ul>");
                            }
                            sb.Append("</li>"); 
                        }
                    }
                    sb.Append ("</ul>");
                    

                }
                return sb.ToString();
            }
            return "";

           
        }

       

    }
}
