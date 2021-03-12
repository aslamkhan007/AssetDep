using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.ViewModel;
using AssetDeprciation.Models.MasterModel;

namespace AssetDeprciation.Controllers.MasterController
{
    public class DepMethodMasterController : Controller
    {
        // GET: DepMethodMaster
        public ActionResult DepMethodMaster()
        {
           
            return View("~/Views/MasterViews/DepMethodMaster/DepMethodMaster.cshtml"); 
        }

        [HttpPost]
       public ActionResult DepMethodMaster(ParentViewModel model)
        {

            SqlConnection con = DBConnection.getConnection();
            SqlCommand cmd = new SqlCommand("Jct_Asset_Dep_Asset_Detail_Method_Insert", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Method_Code", model.Jct_Asset_Dep_Method.Method_Name.Substring(0,3));
            cmd.Parameters.AddWithValue("@Method_Name", model.Jct_Asset_Dep_Method.Method_Name);
            cmd.Parameters.AddWithValue("@Effe_From", System.DateTime.Today);
            cmd.Parameters.AddWithValue("@Effe_To", System.DateTime.MaxValue);
            cmd.Parameters.AddWithValue("@Created_By", HttpContext.User.Identity.Name);
            cmd.Parameters.AddWithValue("@Created_Hostname", System.Environment.MachineName);
            cmd.Parameters.AddWithValue("@Ip_Address", ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress);
            cmd.ExecuteNonQuery();
            con.Close();
            return View("~/Views/MasterViews/DepMethodMaster/DepMethodMaster.cshtml");
        }
        public JsonResult DepMethodList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Method> list = new List<Jct_Asset_Dep_Method>();
            string sql = "Select Method_Name,Method_Code from Jct_Asset_Dep_Method where Status='A'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Method { Method_Code = read["Method_Code"].ToString(), Method_Name = read["Method_Name"].ToString() });
                }
                  var querableList = list.AsQueryable();
                int temp = querableList.Count();

                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Method_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.Method_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult DepMethodDelete(string Method_Code)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Method> list = new List<Jct_Asset_Dep_Method>();
            string sql = "Update Jct_Asset_Dep_Method set Status='D' where Method_Code='" + Method_Code + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
    }
}