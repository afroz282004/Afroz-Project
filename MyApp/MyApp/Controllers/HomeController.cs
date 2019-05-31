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
    public class HomeController : Controller
    {
        db database = new db();
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetSiteMenu()
        {
           
            DataSet ds = database.GetMenu();
            List<Menu> lst = new List<Menu>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lst.Add(new Menu
                {

                    ID = Convert.ToInt32(row["ID"].ToString()),
                    Name = row["Name"].ToString(),
                    Url = row["Url"].ToString(),
                    ParentID = row["ParentID"].ToString(),

                });

            }
           // ViewBag["MethodName"] = "GetSiteMenu";
            return Json(lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Show_Date()
        {
            return View();
        }
        public JsonResult Get_Data()
        {

            DataSet ds = database.Getrecord();
            List<register> lst = new List<register>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lst.Add(new register
                {
                    Sr_no = Convert.ToInt32(row["Sr_no"].ToString()),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    City = row["City"].ToString()

                });

            }
            ViewBag.MethodName = "Get_Data";
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dropdownlist()
        {
            return View("Dropdown");
        }
        public JsonResult Get_DatabyId(int id)
        {
            DataSet ds = database.Getrecordbyid(id);
            List<register> lst = new List<register>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lst.Add(new register
                {
                    Sr_no = Convert.ToInt32(row["Sr_no"].ToString()),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    City = row["City"].ToString()

                });

            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update_Date(int id)
        {
            return View();
        }
        public JsonResult Updated_Record(register rs)
        {
            string str = string.Empty;
            str = "Update";
            return Json(str, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Deleted_Record(int id)
        {
            string str = string.Empty;
            str = "Delted";
            return Json(str, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Add_Record(register rs)
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

            database.Add(rs);
            str = "Inserted";
            return Json(str, JsonRequestBehavior.AllowGet);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}