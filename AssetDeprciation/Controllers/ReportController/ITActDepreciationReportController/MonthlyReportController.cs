using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.ReportModel.ITActDepReportModels;
using AssetDeprciation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers.ReportController.ITActDepreciationReportController
{
    public class MonthlyReportController : Controller
    {
        //
        // GET: /MonthlyReport/
        public ActionResult MonthlyReport()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.FinancialYearList = getFinYearList();
            return View("~/Views/Report/ITActDepreciationReport/MonthlyReport/MonthlyReport.cshtml", model);
        }


        public JsonResult getMonthlyReport(string FinYear)
        {
            using (SqlConnection con = DBConnection.getConnection())
            {
                List<MonthlyReportModel> list = new List<MonthlyReportModel>();
                string sql = "Select * from Jct_Asset_Dep_IT_Monthly_Dep_Record WHERE FinYear='" + FinYear + "' and Status='A'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    read.Close();
                    SqlCommand cmd2 = new SqlCommand("Jct_Asset_Dep_ITAct_MonthWise_Dep_Fetch", con);
                    cmd2.Parameters.AddWithValue("@FinYear", FinYear);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    SqlDataReader read2 = cmd2.ExecuteReader();
                    if (read2.HasRows)
                    {
                        while (read2.Read())
                        {
                            MonthlyReportModel model = new MonthlyReportModel();
                            model.GroupCode = read2["GroupCode"].ToString();
                            model.SubGroupCode = read2["SubGroupCode"].ToString();
                            //model.AssetCode = read2["AssetCode"].ToString();
                            model.Apr = (float)Convert.ToDouble(read2["Apr"].ToString());
                            model.May = (float)Convert.ToDouble(read2["May"].ToString());
                            model.Jun = (float)Convert.ToDouble(read2["Jun"].ToString());
                            model.Jul = (float)Convert.ToDouble(read2["Jul"].ToString());
                            model.Aug = (float)Convert.ToDouble(read2["Sep"].ToString());
                            model.Sep = (float)Convert.ToDouble(read2["Sep"].ToString());
                            model.Oct = (float)Convert.ToDouble(read2["Oct"].ToString());
                            model.Nov = (float)Convert.ToDouble(read2["Nov"].ToString());
                            model.Dec = (float)Convert.ToDouble(read2["Dec"].ToString());
                            model.Jan = (float)Convert.ToDouble(read2["Jan"].ToString());
                            model.Feb = (float)Convert.ToDouble(read2["Feb"].ToString());
                            model.Mar = (float)Convert.ToDouble(read2["Mar"].ToString());
                            model.TotalDepreciation = (float)Convert.ToDouble(read2["TotalDepreciation"].ToString());


                            list.Add(model);
                        }
                        read2.Close();
                        var jsonData = new
                        {
                            records = list.Count,
                            rows = list
                        };
                        return Json(jsonData, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    read.Close();
                    SqlCommand cmd1 = new SqlCommand("Jct_Asset_Dep_ITAct_MonthWise_Dep_Cal", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    SqlDataReader read1 = cmd1.ExecuteReader();
                    if (read1.HasRows)
                    {
                        while (read1.Read())
                        {
                            MonthlyReportModel model = new MonthlyReportModel();
                            model.GroupCode = read1["GroupCode"].ToString();
                            model.SubGroupCode = read1["SubroupCode"].ToString();
                            model.AssetCode = read1["AssetCode"].ToString();
                            model.Apr = (float)Convert.ToDouble(read1["Apr"].ToString());
                            model.May = (float)Convert.ToDouble(read1["May"].ToString());
                            model.Jun = (float)Convert.ToDouble(read1["Jun"].ToString());
                            model.Jul = (float)Convert.ToDouble(read1["Jul"].ToString());
                            model.Aug = (float)Convert.ToDouble(read1["Sep"].ToString());
                            model.Sep = (float)Convert.ToDouble(read1["Sep"].ToString());
                            model.Oct = (float)Convert.ToDouble(read1["Oct"].ToString());
                            model.Nov = (float)Convert.ToDouble(read1["Nov"].ToString());
                            model.Dec = (float)Convert.ToDouble(read1["Dec"].ToString());
                            model.Jan = (float)Convert.ToDouble(read1["Jan"].ToString());
                            model.Feb = (float)Convert.ToDouble(read1["Feb"].ToString());
                            model.Mar = (float)Convert.ToDouble(read1["Mar"].ToString());
                            list.Add(model);
                        }
                        read1.Close();
                        var jsonData = new
                        {
                            records = list.Count,
                            rows = list
                        };
                        return Json(jsonData, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        #region SelectList Items
        public List<SelectListItem> getFinYearList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = DBConnection.getConnection())
            {
                string sql = "Jct_Dep_Asset_FinYear_List_Fetch";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        list.Add(new SelectListItem { Text = read["FinYear"].ToString(), Value = read["FinYear"].ToString() });
                    }
                    read.Close();
                    return list;
                }
            }
            return list;
        }
        #endregion
	}
}