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
    public class SectionMasterController : Controller
    {
        //
        // GET: /SectionMaster/
        public ActionResult SectionMaster()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.CompanyList = companyList();
            model.DropDownViewModel.UnitList = unitList();
            model.DropDownViewModel.SubUnitList = subunitList();
            return View("~/Views/MasterViews/SectionMaster/SectionMaster.cshtml", model);
        }

        public List<SelectListItem> companyList()
        {
            List<SelectListItem> clist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_Company_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    clist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return clist;
            }
            else
            {
                read.Close();
                con.Close();
                return clist;
            }
        }

        public List<SelectListItem> unitList()
        {
            List<SelectListItem> ulist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_Unit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    ulist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return ulist;
            }
            else
            {
                read.Close();
                con.Close();
                return ulist;
            }
        }

        public List<SelectListItem> subunitList()
        {
            List<SelectListItem> sublist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_SubUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    sublist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return sublist;
            }
            else
            {
                read.Close();
                con.Close();
                return sublist;
            }
        }

        [HttpPost]
        public ActionResult SectionMaster(ParentViewModel model)
        {

            Jct_Asset_Dep_Section obj = new Jct_Asset_Dep_Section();
            obj.CompanyCode = model.Jct_Asset_Dep_Section.CompanyCode;
            obj.Created_By = "A-00251";
            obj.Created_Hostname = System.Environment.MachineName;
            obj.Created_On = System.DateTime.Now;
            obj.Effe_From = System.DateTime.Today;
            obj.Effe_To = System.DateTime.MaxValue;
            obj.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            obj.SectionCode = model.Jct_Asset_Dep_Section.SectionName.Substring(0, 3);
            obj.SectionName = model.Jct_Asset_Dep_Section.SectionName;
            obj.Sub_SectionCode = model.Jct_Asset_Dep_Section.Sub_SectionName.Substring(0, 3);
            obj.Sub_SectionName = model.Jct_Asset_Dep_Section.Sub_SectionName;
            obj.SubUnitCode = model.Jct_Asset_Dep_Section.SubUnitCode;
            obj.UnitCode = model.Jct_Asset_Dep_Section.UnitCode;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_Insert";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", obj.CompanyCode);
            cmd.Parameters.AddWithValue("@UnitCode", obj.UnitCode);
            cmd.Parameters.AddWithValue("@SubUnitCode", obj.SubUnitCode);
            cmd.Parameters.AddWithValue("@SectionCode", obj.SectionCode);
            cmd.Parameters.AddWithValue("@SectionName", obj.SectionName);
            cmd.Parameters.AddWithValue("@Sub_SectionCode", obj.Sub_SectionCode);
            cmd.Parameters.AddWithValue("@Sub_SectionName", obj.Sub_SectionName);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", obj.Created_By);
            //cmd.Parameters.AddWithValue("@11", obj.Created_On);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("SectionMaster", "SectionMaster", null);
        }
        //------------------------------Ajax Methods--------------////


        public JsonResult GetunitList(string CompanyCode)
        {
            List<SelectListItem> ulist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_GetUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    ulist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                read.Close();
                ulist = new List<SelectListItem>();
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);
            
            }
      
        }


        public JsonResult GetsubunitList(string CompanyCode, string UnitCode)
        {
            List<SelectListItem> ulist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_GetSubUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
            cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    ulist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                read.Close();
                ulist = new List<SelectListItem>();
                con.Close();
                return Json(ulist, JsonRequestBehavior.AllowGet);

            }

        }

        //-------------------------------------------------------//


        public JsonResult SectionList(string SubUnitCode, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Section> list = new List<Jct_Asset_Dep_Section>();
            string sql = "Jct_Asset_Dep_Section_Fetch";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubUnitCode", SubUnitCode);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Section { SectionCode = read["SectionCode"].ToString(), SectionName = read["SectionName"].ToString() });
                }
                var querableList = list.AsQueryable();
                int temp = querableList.Count();

                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("SubUnitName ASC"))
                {
                    querableList = querableList.OrderBy(p => p.SectionName);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SectionDelete(string SectionCode, string SubUnitCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Section> list = new List<Jct_Asset_Dep_Section>();
            string sql = "Jct_Asset_Dep_Section_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SectionCode", SectionCode);
            cmd.Parameters.AddWithValue("@SubUnitCode", SubUnitCode);
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
	}
}