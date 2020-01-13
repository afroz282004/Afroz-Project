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
using System.Text;
using KVICClassLIB;

namespace KVICWeb
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count >= 1 && (Request.Path.IndexOf("counter.aspx") >= 0 || Request.Path.IndexOf("payment.aspx") >= 0 || Request.Path.IndexOf("paymentsummary.aspx") >= 0))
            {
                Session["username"] = Request.QueryString["username"];
                if (Request.Path.IndexOf("counter.aspx") >= 0 )
                        Response.Redirect("counter.aspx");
                else if (Request.Path.IndexOf("payment.aspx") >= 0 )
                    Response.Redirect("payment.aspx");
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
                    sb.Append("<ul id='menu'>");
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

           
        //    <ul id="menu">
        //                    <li><a href="index.html">Dashboard<span class="icon1"></span></a></li>
        //                    <li><a href="#">Plugins<span class="icon6"></span></a>
        //                        <ul>	 
        //                            <li><a href="calendar.html">Advanced calendar</a></li>
        //                            <li><a href="file_explorer.html">File explorer</a></li>
        //                            <li><a href="charts.html">Charts</a></li>
        //                            <li><a href="tables.html">Data Tables</a></li>
        //                            <li><a href="lightbox.html">LightBox Evolution</a></li>
        //                            <li><a href="alerts.html">Alert messages</a></li>
        //                            <li><a href="dialogs.html">Fallr - Dialogs, modal boxes...</a></li>
        //                        </ul>
        //                    </li>
        //                    <li><a href="#">Example forms<span class="icon2"></span></a>
        //                        <ul>
        //                            <li><a href="forms.html">Basic forms</a></li>
        //                            <li><a href="forms_validation.html">Forms validation</a></li>
        //                        </ul>
        //                    </li>
        //                    <li><a href="#">Icons<span class="icon7"></span></a>
        //                        <ul>
        //                            <li><a href="glyphish_icons.html">Glyphish icons</a></li>
        //                            <li><a href="fugue_icons.html">Fugue icons</a></li>
        //                        </ul>
        //                    </li>
        //                    <li><a href="gallery.html">Image gallery<span class="icon3"></span></a></li>
        //                    <li><a href="grid.html">Grid<span class="icon8"></span></a></li>
        //                    <li><a href="typography.html">Typography<span class="icon10"></span></a></li>
        //                </ul><!--End of #menu-->
            return "";
        }
    }
}
