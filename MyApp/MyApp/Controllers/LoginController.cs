using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MyApp.Models;


namespace MyApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoginAuthentication(register loginData)
        {
            string str = string.Empty;

            //string url = string.Empty;
            //string mResponseData = string.Empty;
            //url = "http://localhost:32185/Admin/";//ConfigurationManager.AppSettings["AuthWebApiURL"].ToString();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string json = serializer.Serialize(rs);

            //clscommon_fun clsfu = new clscommon_fun();
            //mResponseData = clsfu.WebRequest("POST", url + "VerifyLogin", json);
            //DataTable dtj = JsonStringToDataTable(mResponseData);

            //database.Add(rs);
            str = "Success fully login";
            return Json(str, JsonRequestBehavior.AllowGet);

        }
    }
}