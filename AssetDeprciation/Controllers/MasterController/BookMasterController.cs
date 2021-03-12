using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.MasterModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers.MasterController
{
    public class BookMasterController : Controller
    {
        //
        // GET: /BookMaster/
        public ActionResult BookMaster()
        {
            return View("~/Views/MasterViews/BookMaster/BookMaster.cshtml");
        }

        [HttpPost]
        public ActionResult BookMaster(Jct_Asset_Dep_Book model)
        {
            Jct_Asset_Dep_Book obj = new Jct_Asset_Dep_Book();
            obj.BookCode = model.Book_Name.Substring(0, 3);
            obj.Book_Name = model.Book_Name;
            obj.Created_By = "A-00251";
            obj.Created_On = System.DateTime.Now;
            obj.Effe_From = System.DateTime.Today;
            obj.Effe_To = System.DateTime.MaxValue;
            obj.Created_Hostname = System.Environment.MachineName;
            obj.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Book_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookCode", obj.BookCode);
            cmd.Parameters.AddWithValue("@Book_Name", obj.Book_Name);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", obj.Created_By);
            //cmd.Parameters.AddWithValue("@6", obj.Created_On);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("BookMaster", "BookMaster", null);
        }
	}
}