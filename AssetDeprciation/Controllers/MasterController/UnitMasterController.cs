using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.MasterModel;
using AssetDeprciation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers.MasterController
{
    public class UnitMasterController : Controller
    {
        //
        // GET: /UnitMaster/
        public ActionResult UnitMaster()
        {
            List<SelectListItem> clist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Company_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    clist.Add (new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() } );
                }
                ParentViewModel model = new ParentViewModel();
                model.DropDownViewModel.CompanyList = clist;
                con.Close();
                return View("~/Views/MasterViews/UnitMaster/UnitMaster.cshtml", model);
            }
            else
            {
                con.Close();
                return RedirectToAction("Index", "Home", null);
            }
            
        }

        [HttpPost]
        public ActionResult UnitMaster(ParentViewModel model)
        {
            ParentViewModel obj = new ParentViewModel();

            obj.Jct_Asset_Dep_Unit.CompanyCode = model.Jct_Asset_Dep_Company.CompanyCode;
            obj.Jct_Asset_Dep_Unit.UnitCode = model.Jct_Asset_Dep_Unit.UnitName.Substring(0, 3);
            obj.Jct_Asset_Dep_Unit.UnitName = model.Jct_Asset_Dep_Unit.UnitName;
            obj.Jct_Asset_Dep_Unit.Effe_From = System.DateTime.Today;
            obj.Jct_Asset_Dep_Unit.Effe_To = System.DateTime.MaxValue;
            obj.Jct_Asset_Dep_Unit.Created_By = "a-00251";
            obj.Jct_Asset_Dep_Unit.Created_On = System.DateTime.Now;
            obj.Jct_Asset_Dep_Unit.Created_Hostname = System.Environment.MachineName;
            obj.Jct_Asset_Dep_Unit.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Unit_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", obj.Jct_Asset_Dep_Unit.CompanyCode);
            cmd.Parameters.AddWithValue("@UnitCode", obj.Jct_Asset_Dep_Unit.UnitCode);
            cmd.Parameters.AddWithValue("@UnitName", obj.Jct_Asset_Dep_Unit.UnitName);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Jct_Asset_Dep_Unit.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Jct_Asset_Dep_Unit.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", obj.Jct_Asset_Dep_Unit.Created_By);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Jct_Asset_Dep_Unit.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Jct_Asset_Dep_Unit.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("UnitMaster","UnitMaster",null);
        }

        public JsonResult UnitList(string CompanyCode,int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Unit> list = new List<Jct_Asset_Dep_Unit>();
            string sql = "Jct_Asset_Dep_Unit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Unit { UnitCode = read["UnitCode"].ToString(), UnitName = read["UnitName"].ToString() });
                }
                var querableList=list.AsQueryable();
                int temp=querableList.Count();
                
                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("UnitName ASC"))
                {
                    querableList = querableList.OrderBy(p => p.UnitName);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp },JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnitDelete(string UnitCode,string CompanyCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Unit> list = new List<Jct_Asset_Dep_Unit>();
            string sql = "Jct_Asset_Dep_Unit_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
	}

}