using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models;
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
    public class YearlyReportController : Controller
    {
        //
        // GET: /YearlyReport/
        public ActionResult YearlyReport()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.FinancialYearList = getFinYearList();
            return View("~/Views/Report/ITActDepreciationReport/YearlyReport/YearlyReport.cshtml", model);
        }
        #region JSON Results
        public JsonResult getITActYearlyReport(string FinYear)
        {
            List<Jct_Asset_Dep_Asset_IT_Cal> list = new List<Jct_Asset_Dep_Asset_IT_Cal>();
            if (!FinYear.Equals("null", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SqlConnection con = DBConnection.getConnection())
                {
                    string sql = "Jct_Asset_Dep_IT_Yearly_Dep_Summary";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@FinYear", FinYear);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {

                            Jct_Asset_Dep_Asset_IT_Cal model = new Jct_Asset_Dep_Asset_IT_Cal();
                            model.GroupCode = read["GroupCode"].ToString();
                            model.SubGroupCode = read["SubGroupCode"].ToString();
                            model.TotalAmount = (float)Convert.ToDouble(read["TotalAmount"].ToString());
                            model.TotalDepreciation = (float)Convert.ToDouble(read["TotalDepreciation"].ToString());
                            model.TotalOpeningBalance = (float)Convert.ToDouble(read["TotalOpeningBalance"].ToString());
                            model.TotalQuantity = (float)Convert.ToDouble(read["TotalQuantity"].ToString());

                            list.Add(model);
                        }
                    }

                }
            }
            var jsonData = new
            {
                records = list.Count,
                rows = list
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        #endregion


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
                    return list;
                }
            }
            return list;
        }
        #endregion
	}
}