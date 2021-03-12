using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models;
using AssetDeprciation.Models.ReportModel;
using AssetDeprciation.ViewModel;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace AssetDeprciation.Controllers.ReportController.CompanyActDepreciationReportController
{
    public class YearlyReportController : Controller
    {
        //
        // GET: /YearlyReport/
        public ActionResult YearlyReport()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.AccontingYearlist = getAccYearList();
            model.ReportViewModel.CompanyActYearlyDepReportModel.GridNeeded = "False";
            return View("~/Views/Report/CompanyActDepreciationReport/YearlyReport/YearlyReport.cshtml",model);
        }


       

        #region JSON Results
        public JsonResult getCompanyActYearlyReport(string AccYear)
        {
            List<Jct_Asset_Dep_Asset_Slm_Cal> list = new List<Jct_Asset_Dep_Asset_Slm_Cal>();
            if (!AccYear.Equals("null", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SqlConnection con = DBConnection.getConnection())
                {
                    string sql = "Jct_Asset_Dep_Slm_Yearly_Dep_Summary";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@AccYear", AccYear);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {

                            Jct_Asset_Dep_Asset_Slm_Cal model = new Jct_Asset_Dep_Asset_Slm_Cal();
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
          var jsonData=new{
        records=list.Count,
        rows=list
        };
          return Json(jsonData, JsonRequestBehavior.AllowGet); 
        }


        #endregion



        #region Excel Exporting
        public ActionResult ExportToExcel()
        {
            List<CompanyActYearlyDepReportModel> list = new List<CompanyActYearlyDepReportModel>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Slm_Yearly_Dep_Summary";
            DataTable dt = new DataTable();
            dt.TableName = "CompanyActYearlyDepReport";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            con.Close();   
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= CompanyActYearlyDepReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }   

             return RedirectToAction("CompanyActYearlyDepreciationReport", "CompanyActYearlyDepreciationReport", null);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        } 

        #endregion




        #region SelectList Items
        public List<SelectListItem> getAccYearList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using(SqlConnection con=DBConnection.getConnection())
            {
                string sql = "Jct_Dep_Asset_AccYear_List_Fetch";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader read = cmd.ExecuteReader();
                if(read.HasRows)
                {
                    while(read.Read())
                    {
                        list.Add(new SelectListItem { Text = read["AccYear"].ToString(), Value = read["AccYear"].ToString() });
                    }
                    return list;
                }
            }
            return list;
        }
        #endregion
    }
}