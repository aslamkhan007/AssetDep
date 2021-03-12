using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.MasterModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers.MasterController
{
    public class GroupMasterController : Controller
    {
        //
        // GET: /GroupMaster/
        public ActionResult GroupMaster()
        {
            return View("~/Views/MasterViews/GroupMaster/GroupMaster.cshtml");
        }



        [HttpPost]
        public ActionResult GroupMaster(Jct_Asset_Dep_Group model)
        {
            Jct_Asset_Dep_Group obj = new Jct_Asset_Dep_Group();
            obj.GroupCode = model.Group_Name.Substring(0, 3);
            obj.Group_Name = model.Group_Name;
            obj.Created_By = HttpContext.User.Identity.Name;
            obj.Created_Hostname = System.Environment.MachineName;
            obj.Created_On = System.DateTime.Now;
            obj.Effe_From = System.DateTime.Today;
            obj.Effe_To = System.DateTime.MaxValue;
            obj.Status = "A";
            obj.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Book_Group_Insert";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", obj.GroupCode);
            cmd.Parameters.AddWithValue("@Group_Name", obj.Group_Name);
            cmd.Parameters.AddWithValue("@Created_By", obj.Created_By);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Created_Hostname);
            //cmd.Parameters.AddWithValue("@Effe_From", obj.Created_On);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Effe_To);
            cmd.Parameters.AddWithValue("@Status", obj.Status);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("GroupMaster", "GroupMaster", null);
        }
     
        public JsonResult GroupList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Group> list = new List<Jct_Asset_Dep_Group>();
            string sql = "Jct_Asset_Dep_Group_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new Jct_Asset_Dep_Group { GroupCode = read["GroupCode"].ToString(), Group_Name = read["Group_Name"].ToString() });
                }
                read.Close();
                con.Close();
                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Group_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.Group_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult DeleteGroupList(string GroupCode)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_Group> list = new List<Jct_Asset_Dep_Group>();
            string sql = "Jct_Asset_Dep_Group_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
            cmd.ExecuteNonQuery();

            string SQL = "Jct_Asset_Dep_Group_Fetch";
            SqlCommand CMD = new SqlCommand(SQL, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = CMD.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    list.Add(new Jct_Asset_Dep_Group { GroupCode = dr[0].ToString(), Group_Name = dr[1].ToString() });
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
