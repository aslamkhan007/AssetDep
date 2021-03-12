using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.ViewModel;

namespace AssetDeprciation.Controllers.MasterController
{
    public class ITTaxRateMasterController : Controller
    {
        //
        // GET: /ITTaxRate/
        public ActionResult ITTaxRateMaster()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.GroupList = Grouplist();
            model.DropDownViewModel.SubGroupList = Subgrouplist();
            model.DropDownViewModel.FinancialYearList = FinancialYearlist();
            model.DropDownViewModel.ShiftList = ShiftList();
            return View("~/Views/MasterViews/ITTaxRateMaster/ITTaxRateMaster.cshtml", model); 
        }


        public List<SelectListItem> Grouplist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_IT_ActRate_Group_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }

            }

            return list;
        }


        public List<SelectListItem> Subgrouplist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_IT_ActRate_SubGroup_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }

            }

            return list;
        }

        public List<SelectListItem> FinancialYearlist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_IT_ActRate_FinYear_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }

            }

            return list;
        }
        public List<SelectListItem> ShiftList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Select ShiftCode,Shift_Name from Jct_Asset_Dep_Shift where status='A'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read["Shift_Name"].ToString(), Value = read["ShiftCode"].ToString() });
                }
            }
            read.Close();
            con.Close();
            return list;
        }


        [HttpPost]
        public ActionResult ITTaxRateMaster(ParentViewModel model)
        {
            ParentViewModel obj = new ParentViewModel();

            obj.Jct_Asset_Dep_Group.GroupCode = model.Jct_Asset_Dep_Group.GroupCode;
            obj.Jct_Asset_Dep_SubGroup.SubGroup_Code = model.Jct_Asset_Dep_SubGroup.SubGroup_Code;
            obj.Jct_Asset_Dep_CompanyActRate.Rate = model.Jct_Asset_Dep_CompanyActRate.Rate;
            obj.FinancialYearMasterModel.FinYear = model.FinancialYearMasterModel.FinYear; 

            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_IT_ActRate_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", obj.Jct_Asset_Dep_Group.GroupCode);
            cmd.Parameters.AddWithValue("@SubGroup_Code ", obj.Jct_Asset_Dep_SubGroup.SubGroup_Code);
            cmd.Parameters.AddWithValue("@SubGroup_ShiftCode ", model.Jct_Asset_Dep_SubGroup.SubGroup_ShiftCode);
            cmd.Parameters.AddWithValue("@Status","A");
            cmd.Parameters.AddWithValue("@Rate", obj.Jct_Asset_Dep_CompanyActRate.Rate);
            cmd.Parameters.AddWithValue("@FinYear", obj.FinancialYearMasterModel.FinYear);

            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ITTaxRateMaster", "ITTaxRateMaster", null);
        }


        
  
//// Ajax methods//


        public JsonResult GetSubgrouplist(string GroupCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_IT_ActRate_GetSubGroup_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                 read.Close();
                con.Close();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                read.Close();
                list = new List<SelectListItem>();
                con.Close();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            
            
        }



        //public JsonResult GetSubgroupShift(string SubGroupCode)
        //{
        //    string shiftCode = null;
        //    SqlConnection con = DBConnection.getConnection();
        //    string sql = "select SubGroup_ShiftCode from Jct_Asset_Dep_SubGroup where SubGroup_Code=@1 and status='A'";
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.Parameters.AddWithValue("@1", SubGroupCode);
        //    SqlDataReader read = cmd.ExecuteReader();
        //    if (read.HasRows)
        //    {
        //        while (read.Read())
        //        {
        //            shiftCode = read["SubGroup_ShiftCode"].ToString();
        //        }
        //    }
        //    return Json(shiftCode, JsonRequestBehavior.AllowGet);
        //}



     ////////   

	
	}
}