using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using AssetDeprciation.ViewModel;
using AssetDeprciation.DatabaseConnection;
using System.Data;
using AssetDeprciation.Models;
using Security.Controllers;

namespace AssetDeprciation.Controllers
{
    public class DepreciationCalculationController : BaseController
    {
        // GET: DepreciationCalculation
        public ActionResult DepreciationCalculation()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.GroupList = Grouplist();

            model.DropDownViewModel.AccontingYearlist = AccontingYearlist();

            return View(model);
        }

        [HttpPost]
        [ActionName("DepreciationCalculation")]
        public ActionResult DepCalculation(ParentViewModel obj)
        {
            string msg = null;
            string group_code = null;
            if (obj.selectedGroupCode != null)
            {
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Slm_Cal_Proc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BookCode",HttpContext.User.)
                cmd.Parameters.AddWithValue("@BookCode", User.book);
                if (obj.selectedGroupCode != null)
                {
                    List<string> li = obj.selectedGroupCode.Cast<string>().ToList();
                    group_code = string.Join(",", li.ToArray());
                    cmd.Parameters.AddWithValue("@GroupCodeCal", group_code);

                }
                SqlDataReader read = cmd.ExecuteReader();
                List<Jct_Asset_Dep_Asset_Slm_Cal> list = new List<Jct_Asset_Dep_Asset_Slm_Cal>();
                if (read.HasRows)
                {
                    while (read.Read())
                    {

                        Jct_Asset_Dep_Asset_Slm_Cal model = new Jct_Asset_Dep_Asset_Slm_Cal();
                        model.Asset_Life = read["Asset Life (In Months)"].ToString();
                        model.AssetCode = read["AssetCode"].ToString();
                        model.Cost = float.Parse(read["Cost"].ToString());
                        model.Depriciation_Method = read["Depriciation Method"].ToString();
                        model.GroupCode = read["Group"].ToString();
                        model.Life_In_Days = Convert.ToInt32(read["Life In Days"].ToString());
                        //model.SalvageValue = Convert.ToInt32(Math.Round(Convert.ToDecimal(read["SalvageValue"].ToString()), 0));
                        model.SalvageValue = float.Parse(read["SalvageValue"].ToString());
                        //model.SlmValue = Convert.ToInt32(Math.Round(Convert.ToDecimal(read["SlmValue"].ToString()), 0));
                        model.SlmValue = float.Parse(read["SlmValue"].ToString());
                        model.Put_In_Use_Date = Convert.ToDateTime(read["Put_In_Use_Date"].ToString());
                        model.AccClDt = Convert.ToDateTime(read["AccClDt"].ToString());


                        model.AccDays = Convert.ToInt32(read["AccDays"].ToString());
                        model.DepPeriod = float.Parse(read["DepPeriod"].ToString());
                        model.DepAmount = float.Parse(read["DepAmount"].ToString());

                        model.AccYear = read["AccYear"].ToString();
                        model.AccDep = float.Parse(read["AccDep"].ToString());

                        model.Depriciation_Rate_Com = float.Parse(read["DepRate"].ToString());
                        //model.AccYear = read["AccYear"].ToString();
                        list.Add(model);
                    }
                    read.Close();
                    con.Close();
                    TempData["DepList"] = list;
                }


            }
            else
            {
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Slm_Cal_Proc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookCode", User.book);
                cmd.Parameters.AddWithValue("@GroupCodeCal", "");
                SqlDataReader read = cmd.ExecuteReader();
                List<Jct_Asset_Dep_Asset_Slm_Cal> list = new List<Jct_Asset_Dep_Asset_Slm_Cal>();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        Jct_Asset_Dep_Asset_Slm_Cal model = new Jct_Asset_Dep_Asset_Slm_Cal();
                        model.Asset_Life = read["Asset Life (In Months)"].ToString();
                        model.AssetCode = read["AssetCode"].ToString();
                        model.Cost = float.Parse(read["Cost"].ToString());
                        model.Depriciation_Method = read["Depriciation Method"].ToString();
                        model.GroupCode = read["Group"].ToString();
                        model.Life_In_Days = Convert.ToInt32(read["Life In Days"].ToString());
                        //model.SalvageValue = Convert.ToInt32(Math.Round(Convert.ToDecimal(read["SalvageValue"].ToString()), 0));
                        model.SalvageValue = float.Parse(read["SalvageValue"].ToString());
                        model.SlmValue = (float)(read["SlmValue"] is DBNull ? 0.00 : float.Parse(read["SlmValue"].ToString()));
                        model.Put_In_Use_Date = Convert.ToDateTime(read["Put_In_Use_Date"].ToString());
                        //model.AccYear = read["AccYear"].ToString();


                        model.AccClDt = Convert.ToDateTime(read["AccClDt"].ToString());


                        model.AccDays = Convert.ToInt32(read["AccDays"].ToString());
                        model.DepPeriod = float.Parse(read["DepPeriod"].ToString());
                        model.DepAmount = float.Parse(read["DepAmount"].ToString());
                        model.AccYear = read["AccYear"].ToString();
                        model.AccDep = float.Parse(read["AccDep"].ToString());
                        model.Depriciation_Rate_Com = float.Parse(read["DepRate"].ToString());
                        list.Add(model);
                    }
                    read.Close();
                    con.Close();
                    TempData["DepList"] = list;
                }

            }


            return Json(msg = "Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DepCalculationList()
        {
            TempData.Keep("DepList");
            List<Jct_Asset_Dep_Asset_Slm_Cal> list = (List<Jct_Asset_Dep_Asset_Slm_Cal>)TempData["DepList"];

            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }
        // GET: /SubGroupMaster/
        public List<SelectListItem> Grouplist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_Group_Fetch";
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
            read.Close();
            con.Close();

            return list;
        }

        // GET: /AccountingYear/
        public List<SelectListItem> AccontingYearlist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_AccYear_Fetch";
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
            read.Close();
            con.Close();

            return list;
        }

        // GET: /AccountingYear/
        public JsonResult GetAccountingYearlist(string Act)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_cal_Acc_Year_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookCode", Act);
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
        public ActionResult FreezeCalculations(ParentViewModel obj)
        {
            string msg = null;
            string group_code = null;
            if (obj.selectedGroupCode != null)
            {
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Slm_Cal_Freeze_Proc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BookCode",HttpContext.User.)
                cmd.Parameters.AddWithValue("@BookCode", User.book);
                if (obj.selectedGroupCode != null)
                {
                    List<string> li = obj.selectedGroupCode.Cast<string>().ToList();
                    group_code = string.Join(",", li.ToArray());
                    cmd.Parameters.AddWithValue("@GroupCodeCal", group_code);

                }
                
                SqlDataReader read = cmd.ExecuteReader();
                read.Close();
                con.Close();
            }
            else
            {
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Slm_Cal_Freeze_Proc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookCode", User.book);
                cmd.Parameters.AddWithValue("@GroupCodeCal", "");
                SqlDataReader read = cmd.ExecuteReader();

                read.Close();
                con.Close();
            }

            return RedirectToAction("DepreciationCalculation", "DepreciationCalculation", null);
        }
    }
}