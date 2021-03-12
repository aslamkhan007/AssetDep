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
    public class BookAccountingYearMasterController : Controller
    {
        //
        // GET: /AccountingYearMaster/
        public ActionResult BookAccountingYearMaster()
        {
          ParentViewModel model=new ParentViewModel();
                model.DropDownViewModel.BookList = getBookList();
                return View("~/Views/MasterViews/BookAccountingYearMaster/BookAccountingYearMaster.cshtml",model); 
        }
        public List<SelectListItem> getBookList()
        {
             SqlConnection con = DBConnection.getConnection();
            List<SelectListItem> list=new List<SelectListItem>();
            string sql = "Jct_Asset_Dep_Book_Fetch";
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

        public ActionResult getBookOpeningDate(string BookCode)
        {
           
            SqlConnection con = DBConnection.getConnection();
            if(BookCode.Equals("com-0",StringComparison.InvariantCultureIgnoreCase))
            {
                DateTime AccClDt = new DateTime();
                string sql = "Select   TOP 1 AccClDt from Jct_Asset_Dep_Book_AccountingYear where Manual =1 and Closed=1 and  BookCode='" + BookCode + "'AND TransNo=(SELECT MAX(TransNo) FROM dbo.Jct_Asset_Dep_Book_AccountingYear WHERE Manual =1 or Closed=1 and  BookCode='" + BookCode + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader read = cmd.ExecuteReader();
                if(read.HasRows)
                {
                    while(read.Read())
                    {
                        AccClDt = Convert.ToDateTime(read["AccClDt"].ToString()); 
                    }
                }
                read.Close();
                con.Close();
                return Json(AccClDt, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime FinClDt = new DateTime();
                string sql = "Select FinClDt from Jct_Asset_Dep_FinancialYear where Manual <>1 or Closed<>1 and BookCode='"+BookCode+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        FinClDt = Convert.ToDateTime(read["FinClDt"].ToString());
                    }
                }
                read.Close();
                con.Close();
                return Json(FinClDt, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult BookAccountingYearMaster(ParentViewModel model)
        {
            try
            {
                if (model.Jct_Asset_Dep_Book.BookCode.Equals("Com-0", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParentViewModel obj = new ParentViewModel();

                    DateTime dt = new DateTime();

                    var bname=model.Jct_Asset_Dep_Book.BookCode.Split('-');
                    obj.Jct_Asset_Dep_Book_AccountingYear.AccOpDt = model.Jct_Asset_Dep_Book_AccountingYear.AccOpDt;
                    
                    obj.Jct_Asset_Dep_Book_AccountingYear.AccClDt = model.Jct_Asset_Dep_Book_AccountingYear.ClosingDate;
                    if(obj.Jct_Asset_Dep_Book_AccountingYear.AccClDt.Value.Second<59)
                    {

                        dt =(DateTime) obj.Jct_Asset_Dep_Book_AccountingYear.AccClDt;
                        obj.Jct_Asset_Dep_Book_AccountingYear.AccClDt = dt.AddSeconds(1);
                    }
                    obj.Jct_Asset_Dep_Book_AccountingYear.BookCode = model.Jct_Asset_Dep_Book.BookCode;
                    obj.Jct_Asset_Dep_Book_AccountingYear.AccYear = model.FinYear;
                    obj.Jct_Asset_Dep_Book_AccountingYear.AccClMonths = model.Jct_Asset_Dep_Book_AccountingYear.AccClMonths;
                    obj.BookName=bname[0].ToString()+"/"+model.FinYear;
                    obj.Jct_Asset_Dep_Book_AccountingYear.Closed = false;
                    obj.Jct_Asset_Dep_Book_AccountingYear.Manual = false;
                    SqlConnection con = DBConnection.getConnection();
                    string sql = "Jct_Asset_Dep_Book_Account_Insert";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookCode", obj.Jct_Asset_Dep_Book_AccountingYear.BookCode);
                    cmd.Parameters.AddWithValue("@AccOpDt", obj.Jct_Asset_Dep_Book_AccountingYear.AccOpDt);
                    cmd.Parameters.AddWithValue("@AccClDt", obj.Jct_Asset_Dep_Book_AccountingYear.AccClDt);
                    cmd.Parameters.AddWithValue("@AccYear", obj.Jct_Asset_Dep_Book_AccountingYear.AccYear);
                    cmd.Parameters.AddWithValue("@AccMonths", obj.Jct_Asset_Dep_Book_AccountingYear.AccClMonths);
                    cmd.Parameters.AddWithValue("@BookName", obj.BookName);
                    cmd.Parameters.AddWithValue("@AccDays", model.Jct_Asset_Dep_Book_AccountingYear.AccDays);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("BookAccountingYearMaster", "BookAccountingYearMaster", null);
                }
                else if (model.Jct_Asset_Dep_Book.BookCode.Equals("ITB-1", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParentViewModel obj = new ParentViewModel();
                    var bname = model.Jct_Asset_Dep_Book.BookCode.Split('-');
                    obj.BookName = bname[0].ToString() + "/" + model.FinYear;
                    obj.FinancialYearMasterModel.FinOpDt = model.Jct_Asset_Dep_Book_AccountingYear.AccOpDt;
                    obj.FinancialYearMasterModel.FinClDt = model.Jct_Asset_Dep_Book_AccountingYear.ClosingDate;
                    obj.FinancialYearMasterModel.BookCode = model.Jct_Asset_Dep_Book.BookCode;
                    obj.FinancialYearMasterModel.FinClMonths = model.Jct_Asset_Dep_Book_AccountingYear.AccClMonths;
                    obj.FinancialYearMasterModel.FinYear = model.FinYear;
                    obj.FinancialYearMasterModel.Closed = false;
                    obj.FinancialYearMasterModel.Manual = false;
                    SqlConnection con = DBConnection.getConnection();
                    string sql = "Jct_Asset_Dep_Book_Financial_Year_Insert";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookCode", obj.FinancialYearMasterModel.BookCode);
                    cmd.Parameters.AddWithValue("@FinOpDt", obj.FinancialYearMasterModel.FinOpDt);
                    cmd.Parameters.AddWithValue("@FinClDt", obj.FinancialYearMasterModel.FinClDt);
                    cmd.Parameters.AddWithValue("@FinYear", obj.FinancialYearMasterModel.FinYear);
                    cmd.Parameters.AddWithValue("@FinMonths", obj.FinancialYearMasterModel.FinClMonths);
                    cmd.Parameters.AddWithValue("@BookName", obj.BookName);
                    cmd.Parameters.AddWithValue("@FinDays",model.Jct_Asset_Dep_Book_AccountingYear.AccDays);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("BookAccountingYearMaster", "BookAccountingYearMaster", null);
                }
                else
                {
                    return RedirectToAction("Index", "Home", null);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("",ex.Message.ToString());
                model.DropDownViewModel.BookList = getBookList();
                //ex.Message.ToString();
                return View("~/Views/MasterViews/BookAccountingYearMaster/BookAccountingYearMaster.cshtml",model);
            }
        }

        public JsonResult BookAccountingList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Book_AccountingYear> list = new List<Jct_Asset_Dep_Book_AccountingYear>();
            string sql = "SELECT a.BookCode,b.Book_Name FROM  dbo.Jct_Asset_Dep_Book_AccountingYear AS a INNER JOIN dbo.Jct_Asset_Dep_Book_Yr_Map AS b ON a.BookCode = b.BookCode WHERE a.Closed=0 AND a.Manual=0 AND b.Status='A' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Book_AccountingYear {Book_Name=read["Book_Name"].ToString(),BookCode=read["BookCode"].ToString()});
                }
                read.Close();
                con.Close();
                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Group_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.Book_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteBookAccountingList(string BookCode)
        {
            try
            {
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_CloseBook";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookCode", BookCode);
                cmd.ExecuteNonQuery();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            
        }
	}
}