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
    public class ShiftMasterController : Controller
    {
        // GET: ShiftMaster
        public ActionResult ShiftMaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShiftMaster(ParentViewModel model)
        {
            Jct_Asset_Dep_Shift shift = new Jct_Asset_Dep_Shift();
            shift.Shift_Name = model.Jct_Asset_Dep_Shift.Shift_Name;
            shift.ShiftCode = model.Jct_Asset_Dep_Shift.Shift_Name.Substring(0, 3);
            shift.Effe_From = System.DateTime.Today;
            shift.Created_On = System.DateTime.Now;
            shift.Effe_To = System.DateTime.MaxValue;
            shift.Status = "A";
            shift.Created_By = HttpContext.User.Identity.Name;
            shift.Created_Hostname = System.Environment.MachineName;
            shift.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Book_Shift_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShiftCode", shift.ShiftCode);
            cmd.Parameters.AddWithValue("@Shift_Name", shift.Shift_Name);
            cmd.Parameters.AddWithValue("@Effe_From", shift.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", shift.Effe_To);
            cmd.Parameters.AddWithValue("@Created_By", shift.Created_By);
            cmd.Parameters.AddWithValue("@Status", shift.Status);
            cmd.Parameters.AddWithValue("@Created_Hostname", shift.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", shift.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ShiftMaster","ShiftMaster",null);
        }


        public JsonResult ShiftList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Shift> list = new List<Jct_Asset_Dep_Shift>();
            string sql = "Jct_Asset_Dep_Shift_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Shift { ShiftCode = read["ShiftCode"].ToString(), Shift_Name = read["Shift_Name"].ToString() });
                }
               read.Close();
                con.Close();
                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Group_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.Shift_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
            }

        public JsonResult DeleteShiftList(string ShiftCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Shift> list = new List<Jct_Asset_Dep_Shift>();
            string sql = "Jct_Asset_Dep_Shift_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShiftCode", ShiftCode);
            cmd.ExecuteNonQuery();

            string SQL = "Jct_Asset_Dep_Shift_Fetch";
            SqlCommand CMD = new SqlCommand(SQL, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = CMD.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    list.Add(new Jct_Asset_Dep_Shift { ShiftCode = dr[0].ToString(), Shift_Name = dr[1].ToString() });
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
