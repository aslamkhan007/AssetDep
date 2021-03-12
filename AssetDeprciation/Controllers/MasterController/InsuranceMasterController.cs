using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.MasterModel;
using AssetDeprciation.ViewModel;

namespace AssetDeprciation.Controllers.MasterController
{
    public class InsuranceMasterController : Controller
    {
        // GET: InsuranceMaster
        public ActionResult InsuranceMaster()
        {
            ParentViewModel model = new ParentViewModel();
            return View("~/Views/MasterViews/InsuranceMaster/InsuranceMaster.cshtml", model);
        }

        [HttpPost]
        public ActionResult InsuranceMaster(ParentViewModel model)
        {
            ParentViewModel obj = new ParentViewModel();

            obj.Jct_Asset_Dep_Insurance.Insurance_Code = model.Jct_Asset_Dep_Insurance.Insurance_Code;
            obj.Jct_Asset_Dep_Insurance.Insurance_Name = model.Jct_Asset_Dep_Insurance.Insurance_Code;
            obj.Jct_Asset_Dep_SubGroup.Created_By = "A-00251";
            obj.Jct_Asset_Dep_SubGroup.Created_Hostname = System.Environment.MachineName;
            obj.Jct_Asset_Dep_SubGroup.Created_On = System.DateTime.Now;
            obj.Jct_Asset_Dep_SubGroup.Effe_From = System.DateTime.Today;
            obj.Jct_Asset_Dep_SubGroup.Effe_To = System.DateTime.MaxValue;
            obj.Jct_Asset_Dep_SubGroup.Status = "A";
            obj.Jct_Asset_Dep_SubGroup.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Insurance_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Insurance_Code", obj.Jct_Asset_Dep_Insurance.Insurance_Code);
            cmd.Parameters.AddWithValue("@Insurance_Name", obj.Jct_Asset_Dep_Insurance.Insurance_Name);            
            cmd.Parameters.AddWithValue("@Effe_From", obj.Jct_Asset_Dep_SubGroup.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Jct_Asset_Dep_SubGroup.Effe_To);            
            cmd.Parameters.AddWithValue("@Created_By", obj.Jct_Asset_Dep_SubGroup.Created_By);           
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Jct_Asset_Dep_SubGroup.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Jct_Asset_Dep_SubGroup.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("InsuranceMaster", "InsuranceMaster", null);
        }


        public JsonResult InsuranceList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)             
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Insurance> list = new List<Jct_Asset_Dep_Insurance>();
            string sql = "Jct_Asset_Dep_Insurance_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Insurance { Insurance_Code = read["Insurance_Code"].ToString(), Insurance_Name = read["Insurance_Name"].ToString() });
                }
                read.Close();
                con.Close();
                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Insurance_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.Insurance_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteInsurance(string Insurance_Code)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Insurance> list = new List<Jct_Asset_Dep_Insurance>();
            string sql = "Jct_Asset_Dep_Insurance_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Insurance_Code", Insurance_Code);
            cmd.ExecuteNonQuery();

            string SQL = "Jct_Asset_Dep_Insurance_Fetch";
            SqlCommand CMD = new SqlCommand(SQL, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = CMD.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    list.Add(new Jct_Asset_Dep_Insurance { Insurance_Code = dr[0].ToString(), Insurance_Name = dr[1].ToString() });
                }
            }
            dr.Close();
            con.Close();

            var querableList = list.AsQueryable();
            int temp = querableList.Count();
            return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);

        }

    }
}