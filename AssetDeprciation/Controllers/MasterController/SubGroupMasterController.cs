using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models;
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
    public class SubGroupMasterController : Controller
    {
        //
        // GET: /SubGroupMaster/
        public ActionResult SubGroupMaster()
        {
            List<SelectListItem> glist = new List<SelectListItem>();
            ParentViewModel model = new ParentViewModel();
            SqlConnection con = DBConnection.getConnection();
            string sql = "select GroupCode,Group_Name from Jct_Asset_Dep_Group";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    glist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                
                model.DropDownViewModel.GroupList = glist;
                model.DropDownViewModel.MethodList = MethodList();
                con.Close();
                return View("~/Views/MasterViews/SubGroupMaster/SubGroupMaster.cshtml", model);
            }
            else
            {
                
                con.Close();
                model.DropDownViewModel.GroupList = new List<SelectListItem>{new SelectListItem { }};
                return View("~/Views/MasterViews/SubGroupMaster/SubGroupMaster.cshtml", model);
            }
        }

        [HttpPost]
        public ActionResult SubGroupMaster(ParentViewModel model)
        {                                     
            ParentViewModel obj = new ParentViewModel();                        
            obj.Jct_Asset_Dep_SubGroup.GroupCode = model.Jct_Asset_Dep_Group.GroupCode;
            obj.Jct_Asset_Dep_SubGroup.SubGroup_Code = model.Jct_Asset_Dep_SubGroup.SubGroup_Name.Substring(0, 3);
            obj.Jct_Asset_Dep_SubGroup.SubGroup_Name = model.Jct_Asset_Dep_SubGroup.SubGroup_Name;
            obj.Jct_Asset_Dep_SubGroup.SubGroup_MethodCode = model.Jct_Asset_Dep_SubGroup.SubGroup_MethodCode;
            obj.Jct_Asset_Dep_SubGroup.Created_By = HttpContext.User.Identity.Name;
            obj.Jct_Asset_Dep_SubGroup.Created_Hostname = System.Environment.MachineName;
            obj.Jct_Asset_Dep_SubGroup.Created_On = System.DateTime.Now;
            obj.Jct_Asset_Dep_SubGroup.Effe_From = System.DateTime.Today;
            obj.Jct_Asset_Dep_SubGroup.Effe_To = System.DateTime.MaxValue;
            obj.Jct_Asset_Dep_SubGroup.Status = "A";
            obj.Jct_Asset_Dep_SubGroup.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Book_Sub_Group_Insert";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", obj.Jct_Asset_Dep_SubGroup.GroupCode);
            cmd.Parameters.AddWithValue("@SubGroup_Code", obj.Jct_Asset_Dep_SubGroup.SubGroup_Code);
            cmd.Parameters.AddWithValue("@SubGroup_Name", obj.Jct_Asset_Dep_SubGroup.SubGroup_Name);
            cmd.Parameters.AddWithValue("@SubGroup_MethodCode", obj.Jct_Asset_Dep_SubGroup.SubGroup_MethodCode);
            cmd.Parameters.AddWithValue("@Effe_From", obj.Jct_Asset_Dep_SubGroup.Effe_From);
            cmd.Parameters.AddWithValue("@Effe_To", obj.Jct_Asset_Dep_SubGroup.Effe_To);
            cmd.Parameters.AddWithValue("@Status", obj.Jct_Asset_Dep_SubGroup.Status);
            cmd.Parameters.AddWithValue("@Created_By", obj.Jct_Asset_Dep_SubGroup.Created_By);
            //cmd.Parameters.AddWithValue("@8", obj.SubGroupMasterModel.Created_On);
            cmd.Parameters.AddWithValue("@Created_Hostname", obj.Jct_Asset_Dep_SubGroup.Created_Hostname);
            cmd.Parameters.AddWithValue("@Ip_Address", obj.Jct_Asset_Dep_SubGroup.Ip_Address);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("SubGroupMaster", "SubGroupMaster",null);
        }

        public JsonResult SubGroupList(string GroupCode,int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            List<Jct_Asset_Dep_SubGroup> list = new List<Jct_Asset_Dep_SubGroup>();            
            string sql = "Jct_Asset_Dep_SubGroup_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new Jct_Asset_Dep_SubGroup { SubGroup_Code = read["SubGroup_Code"].ToString(), SubGroup_Name = read["SubGroup_Name"].ToString(), SubGroup_MethodCode = read["SubGroup_MethodCode"].ToString()});
                }
                var querableList = list.AsQueryable();
                int temp = querableList.Count();

                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("SubGroup_Name ASC"))
                {
                    querableList = querableList.OrderBy(p => p.SubGroup_Name);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();

                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SubGroupDelete(string SubGroupCode, string GroupCode)
        {
            SqlConnection con = DBConnection.getConnection();            
            string sql = "Jct_Asset_Dep_SubGroup_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
            cmd.Parameters.AddWithValue("@SubGroup_Code", SubGroupCode);
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public List<SelectListItem> MethodList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Select Method_Code,Method_Name from Jct_Asset_Dep_Method where status='A'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows)
            {
                while(read.Read())
                {
                    list.Add(new SelectListItem { Text = read["Method_Name"].ToString(), Value = read["Method_Code"].ToString()});
                }
                
            }
            read.Close();
            con.Close();
            return list;
        }

       
        [HttpPost]
        public ActionResult EditSubGroupInfo(ParentViewModel model)
        {
            SqlConnection con = DBConnection.getConnection();          
            string sql = "Jct_Asset_Dep_Book_Sub_Group_Update";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupCode", model.Jct_Asset_Dep_Group.GroupCode);
            cmd.Parameters.AddWithValue("@SubGroup_Code", model.Jct_Asset_Dep_SubGroup.SubGroup_Code);
            cmd.Parameters.AddWithValue("@SubGroup_Name", model.Jct_Asset_Dep_SubGroup.SubGroup_Name);
            cmd.Parameters.AddWithValue("@SubGroup_MethodCode", model.Jct_Asset_Dep_SubGroup.SubGroup_MethodCode);
            cmd.Parameters.AddWithValue("@Effe_From", System.DateTime.Today);
            cmd.Parameters.AddWithValue("@Effe_To", System.DateTime.MaxValue);
            cmd.Parameters.AddWithValue("@Status", "A");
            cmd.Parameters.AddWithValue("@Created_By",  HttpContext.User.Identity.Name);
            //cmd.Parameters.AddWithValue("@8", obj.SubGroupMasterModel.Created_On);
            cmd.Parameters.AddWithValue("@Created_Hostname", System.Environment.MachineName);
            cmd.Parameters.AddWithValue("@Ip_Address",("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress);
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("SubGroupMaster", "SubGroupMaster",null);
        }

	}
}