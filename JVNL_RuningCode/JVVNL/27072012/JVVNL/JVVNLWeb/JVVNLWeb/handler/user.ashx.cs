using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using JVVNLClassLIB;
using System.Text ;

namespace JVVNLWeb.handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class user : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strmsg = "";
            string type = context.Request["type"].ToString();
            switch ( type)
            {
                case "insert":
                    string firstname = context.Request["firstname"].ToString();
                    string lastname = context.Request["lastname"].ToString();
                    string username = context.Request["username"].ToString();
                    string password = context.Request["password"].ToString();
                    string countername = context.Request["countername"].ToString();
                    string groupname = context.Request["groupname"].ToString();
                    string subdivisioncode = context.Request["subdivisioncode"].ToString();
                    string accountno = context.Request["accountno"].ToString();
                    string phoneno = context.Request["phoneno"].ToString();
                    string emailid = context.Request["emailid"].ToString();
                    string IPAddress = context.Request["ipaddress"].ToString();
                    strmsg = UserInsert(firstname, lastname, username , password, countername, groupname,subdivisioncode,accountno,phoneno,emailid,IPAddress  );
                    break;
                case "update":
                    string dfirstname = context.Request["firstname"].ToString();
                    string dlastname = context.Request["lastname"].ToString();
                    string dusername = context.Request["username"].ToString();
                    string dcountername = context.Request["countername"].ToString();
                    string dgroupname = context.Request["groupname"].ToString();
                    strmsg = UserUpdate(dfirstname, dlastname, dusername, dcountername, dgroupname);
                    break;
                case "search":
                    string susername = context.Request["username"].ToString();
                    string sgroupname = context.Request["grpname"].ToString();
                    string scountername = context.Request["countername"].ToString();
                    strmsg = UserSearch(susername, sgroupname, scountername);
                    break;
                case "block":
                    string busername = context.Request["username"].ToString();
                    strmsg = UserBlock(busername);
                    break;
                case "reset":
                    string rusername = context.Request["username"].ToString();
                    strmsg = UserUpdatePwd(rusername, "pass123");
                    break;
                case "updatepwd":
                    string uusername = context.Request["username"].ToString();
                    string upassword = context.Request["password"].ToString();
                    strmsg = UserUpdatePwd(uusername, upassword );
                    break;
                case "edit":
                    string eusername = context.Request["username"].ToString();
                    strmsg = UserEdit(eusername); 
                    break;
                case "login":
                    string lusername = context.Request["username"].ToString();
                    string lpassword = context.Request["password"].ToString();
                    string MacAddress = GetClientIPAddress(context.Request); 
                    strmsg = CheckLogin(lusername, lpassword,MacAddress );
                    
                    break;
                case "logout":
                    string lousername = context.Request["username"].ToString();
                    strmsg = Logout(lousername);
                    break;
                case "selectgroup":
                    strmsg = SelectGroup(); 
                    break;
                default :
                    break;
            }
            context.Response.Write(strmsg);
        }
        private string Logout(string username)
        {
            users_Prop userprop = new users_Prop ();
            userprop.UserName = username;
            users_bal userbal= new users_bal();
            string strmsg = userbal.LogOut(userprop );
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                return userbal.ErrMessage;
            }
            else
                return strmsg; 
        }
        private string SelectGroup()
        {
            group_bal groupbal = new group_bal();
            DataTable dt = groupbal.GroupSelect();
            if (groupbal.ErrMessage != "" && groupbal.ErrMessage != null)
            {
                return "0#" + groupbal.ErrMessage + "$";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder(); 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(dt.Rows[i]["groupid"].ToString() + "#" + dt.Rows[i]["groupname"].ToString() + "$");
                    }
                    return sb.ToString();
                }
                else
                    return "0#No Data Found$";
            }
        }
        
        private string CheckLogin(string username, string Password, string IPAddress)
        {
            users_Prop userprop = new users_Prop();
            userprop.UserName = username;
            userprop.Password = Common.EncryptText(Password);
            userprop.IPAddress = IPAddress;
            users_bal userbal = new users_bal();
            string strmsg=userbal.UserCheckLogin(userprop);
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                return userbal.ErrMessage;
            }
            else
            {
                return strmsg;
            }
        }


        private string UserEdit(string username)
        {
            users_Prop userprop = new users_Prop();
            userprop.UserName = username;
            users_bal userbal = new users_bal();
            DataTable dt = userbal.UserSelect(userprop );
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                return userbal.ErrMessage;
            }
            else
            {
                return dt.Rows[0]["username"].ToString() + "#" + dt.Rows[0]["firstname"].ToString() + "#" + dt.Rows[0]["lastname"].ToString() + "#" + dt.Rows[0]["counterid"].ToString() + "#" + dt.Rows[0]["groupid"].ToString();
            }
        }
        private string UserBlock(string username)
        {
            users_Prop userprop = new users_Prop();
            userprop.UserName = username;
            users_bal userbal = new users_bal();
            string strMsg = userbal.UserBlock(userprop);
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                return userbal.ErrMessage;
            }
            else
                return strMsg;
  
        }
        private string UserUpdatePwd(string username, string password)
        {
            users_Prop userprop = new users_Prop();
            userprop.UserName = username;
            userprop.Password = Common.EncryptText(password );  
            users_bal userbal = new users_bal();
            string strMsg = userbal.UserUpdatePassword(userprop);
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                return userbal.ErrMessage;
            }
            else
                return strMsg;

        }
        private string UserSearch(string username, string groupname, string countername)
        {
            users_Prop userprop = new users_Prop();
            userprop.UserName = username;
            userprop.GroupName = groupname;
            userprop.CounterName = countername;
            users_bal userbal = new users_bal();
            DataTable dt = userbal.UserSearch(userprop);
            StringBuilder sb = new StringBuilder ();
            
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
            {
                sb.Append("<tr><td >" + userbal.ErrMessage + "</td><td></td><td></td><td></td><td></td></tr>");
            }
            else
            {
                if (dt.Rows.Count <= 0)
                { sb.Append("<tr><td >No Record Found</td><td></td><td></td><td></td><td></td></tr>"); }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>"+ dt.Rows[i]["username"].ToString()  +"</td>");
                        sb.Append("<td>" + dt.Rows[i]["firstname"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["countername"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["groupname"].ToString() + "</td>");
                        sb.Append("<td ><img src='images/user_block.png' alt='block' onclick=BlockUser('");
                        sb.Append( dt.Rows[i]["Username"].ToString() );
                        sb.Append("') /><img src='images/resetpwd.png' alt='reset' onclick=ResetPassword('" + dt.Rows[i]["Username"].ToString() + "') /><img src='images/editrec.png' alt='edit' onclick=EditRecord('" + dt.Rows[i]["username"].ToString() + "') ></td>");
                        sb.Append("</tr>");
                    }
                }
            }
            
            return sb.ToString();
        }
        private string UserInsert(string firstname, string Lastname, string username, string password, string countername, string groupname,string subdivisioncode, string accountno,string phoneno, string emailid,string IPAddress)
        {
            users_Prop userprop = new users_Prop() ;
            userprop.FirstName =firstname;
            userprop.LastName=Lastname ;
            userprop.UserName=username;
            userprop.Password=Common.EncryptText(password) ;
            userprop.CounterID =Convert.ToInt16(countername) ;
            userprop.GroupID =Convert.ToInt16(groupname);
            userprop.SubdivisionCode = subdivisioncode;
            userprop.AccountNo= accountno;
            userprop.PhoneNo= phoneno;
            userprop.EmailID= emailid;
            userprop.IPAddress = IPAddress;
            users_bal userbal = new users_bal();
            string strMsg = userbal.UserInsert(userprop);
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
                return userbal.ErrMessage;
            else
                return strMsg;
  
        }
        private string UserUpdate(string firstname, string Lastname, string username,  string countername, string groupname)
        {
            users_Prop userprop = new users_Prop();
            userprop.FirstName = firstname;
            userprop.LastName = Lastname;
            userprop.UserName = username;
            userprop.CounterID = Convert.ToInt16(countername);
            userprop.GroupID = Convert.ToInt16(groupname);

            users_bal userbal = new users_bal();
            string strMsg = userbal.UserUpdate(userprop);
            if (userbal.ErrMessage != "" && userbal.ErrMessage != null)
                return userbal.ErrMessage;
            else
                return strMsg;

        }
        private static string GetClientIPAddress(System.Web.HttpRequest httprequest)
        {
            string OriginalIP = string.Empty;
            string RemoteIP = string.Empty;
            RemoteIP = httprequest.UserHostAddress;//httprequest.ServerVariables["REMOTE_ADDR"];
            return RemoteIP;  
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
