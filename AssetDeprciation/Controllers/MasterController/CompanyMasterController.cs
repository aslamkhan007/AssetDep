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
    public class CompanyMasterController : Controller
    {
        //
        // GET: /CompanyMaster/
        public ActionResult CompanyMaster()
        {
            return View("~/Views/MasterViews/CompanyMaster/CompanyMaster.cshtml");
        }
        [HttpPost]
        public ActionResult CompanyMaster(Jct_Asset_Dep_Company model)
        {
            Jct_Asset_Dep_Company obj = new Jct_Asset_Dep_Company();
            obj.CompanyCode = model.CompanyName.Substring(0, 3);
            obj.CompanyName = model.CompanyName;
            obj.Created_By = "A-00251";
            obj.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            obj.Created_On = System.DateTime.Now;
            obj.Created_Hostname = Environment.MachineName;
            obj.Effe_From = System.DateTime.Today;
            obj.Effe_To = System.DateTime.MaxValue;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Company_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@CompanyCode", obj.CompanyCode);
            cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", obj.Created_By);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return View("~/Views/MasterViews/CompanyMaster/CompanyMaster.cshtml");
        }
        public JsonResult CompanyList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Company> list = new List<Jct_Asset_Dep_Company>();
            string sql = "Jct_Asset_Dep_CompanyList_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Company { CompanyCode=read["CompanyCode"].ToString(),CompanyName=read["CompanyName"].ToString()});
                }
                read.Close();
                con.Close();
                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("CompanyName ASC"))
                {
                    querableList = querableList.OrderBy(p => p.CompanyName);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp },JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count },JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyDelete(string CompanyCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Company> list = new List<Jct_Asset_Dep_Company>();
            string sql = "Jct_Asset_Dep_Company_Delete";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
	}
}