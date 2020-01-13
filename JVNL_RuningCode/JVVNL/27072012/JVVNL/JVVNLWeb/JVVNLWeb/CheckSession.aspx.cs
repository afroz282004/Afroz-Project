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

namespace JVVNLWeb
{
    public partial class CheckSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    if (Request.QueryString.Count == 1 && Request.QueryString["hgUserName"].ToString() != null)
                    {
                        if (Request.QueryString["hgUserName"].ToString() != null)
                        {
                            Common com = new Common();
                            com.TimeOut(Request.QueryString["hgUserName"].ToString());
                            Response.Redirect("default.aspx");

                        }
                    }
                }
                else if (Session["username"] != null &&  Session["username"].ToString() != Request.QueryString["hgUserName"].ToString())
                {
                    if (Request.QueryString.Count == 1 && Request.QueryString["hgUserName"].ToString() != null)
                    {
                        if (Request.QueryString["hgUserName"].ToString() != null)
                        {
                            Common com = new Common();
                            com.TimeOut(Request.QueryString["hgUserName"].ToString());
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else if (Session["username"] != null && Session["username"].ToString() == Request.QueryString["hgUserName"].ToString())
                {
                    if (Request.QueryString.Count == 1 && Request.QueryString["hgUserName"].ToString() != null)
                    {
                        if (Request.QueryString["hgUserName"].ToString() != null)
                        {
                            Common com = new Common();
                            com.TimeOut(Request.QueryString["hgUserName"].ToString());
                            Response.Redirect("default.aspx");
                        }
                    }
                }
            }
        }
    }
}
