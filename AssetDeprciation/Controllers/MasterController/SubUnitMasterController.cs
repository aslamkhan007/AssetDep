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
    public class SubUnitMasterController : Controller
    {
        //
        // GET: /SubUnitMaster/
        public ActionResult SubUnitMaster()
        {
            List<SelectListItem> clist = new List<SelectListItem>();
             SqlConnection con = DBConnection.getConnection();
             string sql = "Jct_Asset_Dep_Sub_Unit_GetSubUnitList";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    clist .Add( new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() } );
                }
                ParentViewModel model = new ParentViewModel();
                model.DropDownViewModel.CompanyList = clist;
                con.Close();
                return View("~/Views/MasterViews/SubUnitMaster/SubUnitMaster.cshtml", model);
            }
            else
            {
                con.Close();
                return RedirectToAction("Index","Home",null);
            }
        }

        public JsonResult getUnitList(string CompanyCode)
        {
            List<SelectListItem> ulist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            //string sql = "Select UnitCode,UnitName from unit where CompanyCode=@1";
            string sql = "Jct_Asset_Dep_Sub_Unit_GetUnitList";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    ulist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() } );
                }
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ulist=new List<SelectListItem>();
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SubUnitMaster(ParentViewModel model)
        {
            ParentViewModel obj = new ParentViewModel();
            obj.Jct_Asset_Dep_SubUnit.CompanyCode = model.Jct_Asset_Dep_Company.CompanyCode;
            obj.Jct_Asset_Dep_SubUnit.UnitCode = model.Jct_Asset_Dep_Unit.UnitCode;
            obj.Jct_Asset_Dep_SubUnit.SubUnitCode = model.Jct_Asset_Dep_SubUnit.SubUnitName.Substring(0, 3);
            obj.Jct_Asset_Dep_SubUnit.SubUnitName = model.Jct_Asset_Dep_SubUnit.SubUnitName;
            obj.Jct_Asset_Dep_SubUnit.Effe_From = System.DateTime.Today;
            obj.Jct_Asset_Dep_SubUnit.Effe_To = System.DateTime.MaxValue;
            obj.Jct_Asset_Dep_SubUnit.Created_By = "A-00251";
            obj.Jct_Asset_Dep_SubUnit.Created_On = System.DateTime.Now;
            obj.Jct_Asset_Dep_SubUnit.Created_Hostname = System.Environment.MachineName;
            obj.Jct_Asset_Dep_SubUnit.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Sub_Unit_Insert";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", obj.Jct_Asset_Dep_SubUnit.CompanyCode);
            cmd.Parameters.AddWithValue("@UnitCode", obj.Jct_Asset_Dep_SubUnit.UnitCode);
            cmd.Parameters.AddWithValue("@SubUnitCode", obj.Jct_Asset_Dep_SubUnit.SubUnitCode);
            cmd.Parameters.AddWithValue("@SubUnitName", obj.Jct_Asset_Dep_SubUnit.SubUnitName);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Jct_Asset_Dep_SubUnit.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Jct_Asset_Dep_SubUnit.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", obj.Jct_Asset_Dep_SubUnit.Created_By);
            //cmd.Parameters.AddWithValue("@8", obj.SubUnitMasterModel.Created_On);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Jct_Asset_Dep_SubUnit.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Jct_Asset_Dep_SubUnit.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("SubUnitMaster","SubUnitMaster",null);
        }


        public JsonResult SubUnitList(string UnitCode, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_SubUnit> list = new List<Jct_Asset_Dep_SubUnit>();
            string sql = "Jct_Asset_Dep_Sub_Unit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new Jct_Asset_Dep_SubUnit { SubUnitCode = read["SubUnitCode"].ToString(), SubUnitName = read["SubUnitName"].ToString() });
                }
                var querableList = list.AsQueryable();
                int temp = querableList.Count();

                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("SubUnitName ASC"))
                {
                    querableList = querableList.OrderBy(p => p.SubUnitName);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubUnitDelete(string SubUnitCode, string UnitCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_SubUnit> list = new List<Jct_Asset_Dep_SubUnit>();
            string sql = "Jct_Asset_Dep_Sub_Unit_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
            cmd.Parameters.AddWithValue("@SubUnitCode", SubUnitCode);              
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK"}, JsonRequestBehavior.AllowGet);
        }
	}
}