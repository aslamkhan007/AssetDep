using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.ViewModel;
using AssetDeprciation.Models;
using AssetDeprciation.Models.MasterModel;

namespace AssetDeprciation.Controllers
{
    public class AssetController : Controller
    {
        //
        // GET: /Asset/
        [HttpGet]
        public ActionResult AssetMaster()
        {
            ParentViewModel model = new ParentViewModel();
            model.DropDownViewModel.GroupList = grouplist();
            model.DropDownViewModel.SubGroupList = Subgrouplist();
            model.DropDownViewModel.CompanyList = Companylist();
            model.DropDownViewModel.UnitList = Unitlist();
            model.DropDownViewModel.SubUnitList = SubUnitlist();
            model.DropDownViewModel.SectionList = SectionList();
            model.DropDownViewModel.SubSectionList = SubSectionList();
            model.DropDownViewModel.MethodList = Methodlist();
            model.DropDownViewModel.ShiftList = ShiftList();
            return View(model);
        }


        [HttpPost]
        public ActionResult AssetMaster(ParentViewModel model)
        {
            Jct_Asset_Dep_Asset obj = new Jct_Asset_Dep_Asset();
            Jct_Asset_Dep_Sub_Asset obj1 = new Jct_Asset_Dep_Sub_Asset();
            if (model.Jct_Asset_Dep_Asset.AcquiredType == "Dependent-Child" || model.Jct_Asset_Dep_Asset.AcquiredType == "Independent-Child")
            {
                obj1.CompanyCode = model.Jct_Asset_Dep_Asset.CompanyCode;
                obj1.UnitCode = model.Jct_Asset_Dep_Asset.UnitCode;
                obj1.SubUnitCode = model.Jct_Asset_Dep_Asset.SubUnitCode;
                obj1.SectionCode = model.Jct_Asset_Dep_Asset.SectionCode;
                obj1.SubSectionCode = model.Jct_Asset_Dep_Asset.SubSectionCode;
                obj1.GroupCode = model.Jct_Asset_Dep_Asset.SubGroup_Code;
                obj1.SubGroup_Code = model.Jct_Asset_Dep_Asset.SubGroup_Code;
                obj1.AcquiredAs = model.Jct_Asset_Dep_Asset.AcquiredAs;
                obj1.AcquiredType = model.Jct_Asset_Dep_Asset.AcquiredType;

                obj1.Quantity = model.Jct_Asset_Dep_Asset.Quantity;
                obj1.SalvageValue = model.Jct_Asset_Dep_Asset.SalvageValue;
                obj1.SubArea = model.Jct_Asset_Dep_Asset.SubArea;
                obj1.ITBook = model.Jct_Asset_Dep_Asset.ITBook;
                obj1.COMBook = model.Jct_Asset_Dep_Asset.COMBook;
                obj1.Tangiable = model.Jct_Asset_Dep_Asset.Tangiable;
              
                obj1.AssetCode = model.Jct_Asset_Dep_Asset.AssetCode;
                obj1.Sub_AssetCode = model.Jct_Asset_Dep_Sub_Asset.Sub_AssetCode;
                obj1.Sub_AssetName = model.Jct_Asset_Dep_Sub_Asset.Sub_AssetName;
                obj1.PurchaseDate = model.Jct_Asset_Dep_Asset.PurchaseDate;
                obj1.Put_In_Use_Date = model.Jct_Asset_Dep_Asset.Put_In_Use_Date;
                obj1.Amount = model.Jct_Asset_Dep_Asset.Amount;
                obj1.Asset_Life = model.Jct_Asset_Dep_Asset.Asset_Life;
                obj.SubGroup_ShiftCode = model.Jct_Asset_Dep_Asset.SubGroup_ShiftCode;
                obj1.Method_Code = model.Jct_Asset_Dep_Asset.Method_Code;
                obj1.Effe_From = model.Jct_Asset_Dep_Asset.Effe_From;
                obj1.Effe_To = model.Jct_Asset_Dep_Asset.Effe_To;
                obj1.Created_By = model.Jct_Asset_Dep_Asset.Created_By;
                obj1.Created_On = model.Jct_Asset_Dep_Asset.Created_On;
                obj1.Created_Hostname = model.Jct_Asset_Dep_Asset.Created_Hostname;
                obj1.Ip_Address = model.Jct_Asset_Dep_Asset.Tangiable;

                obj1.Effe_From = System.DateTime.Today;
                obj1.Effe_To = model.Jct_Asset_Dep_Asset.Effe_To;
                obj1.Created_By =HttpContext.User.Identity.Name;
                obj1.Created_On = System.DateTime.Now;
                obj1.Created_Hostname = System.Environment.MachineName;
                obj1.Ip_Address = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;

                SqlConnection con1 = DBConnection.getConnection();
                string sql1 = "Jct_Asset_Dep_SubAsset_Insert";
                SqlCommand cmd1 = new SqlCommand(sql1, con1);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@CompanyCode", obj1.CompanyCode);
                cmd1.Parameters.AddWithValue("@UnitCode", obj1.UnitCode);
                cmd1.Parameters.AddWithValue("@SubUnitCode", obj1.SubUnitCode);
                cmd1.Parameters.AddWithValue("@SectionCode", obj1.SectionCode);
                cmd1.Parameters.AddWithValue("@SubSectionCode", obj1.SubSectionCode);
                cmd1.Parameters.AddWithValue("@Group_Code", obj1.GroupCode);
                cmd1.Parameters.AddWithValue("@SubGroup_Code ", obj1.SubGroup_Code);
                cmd1.Parameters.AddWithValue("@AcquiredAs", obj1.AcquiredAs);
                cmd1.Parameters.AddWithValue("@AcquiredType", obj1.AcquiredType);
                cmd1.Parameters.AddWithValue("@Quantity", obj1.Quantity);
                cmd1.Parameters.AddWithValue("@SalvageValue", obj1.SalvageValue);
                cmd1.Parameters.AddWithValue("@SubArea", obj1.SubArea);                
                cmd1.Parameters.AddWithValue("@ITBook", "ITB-1");
                cmd1.Parameters.AddWithValue("@COMBook", "COM-0");               
                cmd1.Parameters.AddWithValue("@AssetCode", obj1.AssetCode);
                cmd1.Parameters.AddWithValue("@Sub_AssetCode", obj1.AssetCode);
                cmd1.Parameters.AddWithValue("@Sub_AssetName", obj1.AssetCode);
                cmd1.Parameters.AddWithValue("@PurchaseDate", obj1.PurchaseDate);
                cmd1.Parameters.AddWithValue("@Put_In_Use_Date", obj1.Put_In_Use_Date);
                cmd1.Parameters.AddWithValue("@Amount", obj1.Amount);
                cmd1.Parameters.AddWithValue("@Asset_Life", obj1.Asset_Life);
                cmd1.Parameters.AddWithValue("@Method_Code", obj1.Method_Code);
                cmd1.Parameters.AddWithValue("@SubGroup_ShiftCode", obj.SubGroup_ShiftCode);
                cmd1.Parameters.AddWithValue("@Effe_From", obj1.Effe_From);
                cmd1.Parameters.AddWithValue("@Effe_To", obj1.Effe_To);
                cmd1.Parameters.AddWithValue("@Created_By", obj1.Created_By);
                cmd1.Parameters.AddWithValue("@Created_On", obj1.Created_On);
                cmd1.Parameters.AddWithValue("@Created_Hostname", obj1.Created_Hostname);
                cmd1.Parameters.AddWithValue("@Ip_Address", obj1.Ip_Address);
                cmd1.ExecuteNonQuery();
                con1.Close();
            }

            if (model.Jct_Asset_Dep_Asset.AcquiredType == "Independent")
            {
                obj.CompanyCode = model.Jct_Asset_Dep_Asset.CompanyCode;
                obj.UnitCode = model.Jct_Asset_Dep_Asset.UnitCode;
                obj.AcquiredAs = model.Jct_Asset_Dep_Asset.AcquiredAs;
                obj.AcquiredType = model.Jct_Asset_Dep_Asset.AcquiredType;
                obj.SubUnitCode = model.Jct_Asset_Dep_Asset.SubUnitCode;
                obj.SectionCode = model.Jct_Asset_Dep_Asset.SectionCode;
                obj.SubSectionCode = model.Jct_Asset_Dep_Asset.SubSectionCode;
                obj.GroupCode = model.Jct_Asset_Dep_Asset.GroupCode;
                obj.SubGroup_Code = model.Jct_Asset_Dep_Asset.SubGroup_Code;
                obj.AssetCode = model.Jct_Asset_Dep_Asset.AssetCode;
                obj.AssetName = model.Jct_Asset_Dep_Asset.AssetName;
                obj.SubArea = model.Jct_Asset_Dep_Asset.SubArea;
                obj.PurchaseDate = model.Jct_Asset_Dep_Asset.PurchaseDate;
                obj.Put_In_Use_Date = model.Jct_Asset_Dep_Asset.Put_In_Use_Date;
                obj.Quantity = model.Jct_Asset_Dep_Asset.Quantity;
                obj.SalvageValue = model.Jct_Asset_Dep_Asset.SalvageValue;
                obj.Amount = model.Jct_Asset_Dep_Asset.Amount;
                obj.Asset_Life = model.Jct_Asset_Dep_Asset.Asset_Life;
                obj.Method_Code = model.Jct_Asset_Dep_Asset.Method_Code;
                obj.SubGroup_ShiftCode = model.Jct_Asset_Dep_Asset.SubGroup_ShiftCode;
                obj.Depriciation_Rate_Com = model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com;

                obj.Tangiable = model.Jct_Asset_Dep_Asset.Tangiable;
                obj.Insurer_Name = model.Jct_Asset_Dep_Asset.Insurer_Name;
                obj.Policy_No = model.Jct_Asset_Dep_Asset.Policy_No;
                

                obj.Effe_From = System.DateTime.Today;
                obj.Effe_To = model.Jct_Asset_Dep_Asset.Effe_To;
                obj.Created_By = User.Identity.Name;
                obj.Created_On = System.DateTime.Now;
                obj.Created_Hostname = System.Environment.MachineName;
                //obj.Tangiable = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;

                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Insert";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyCode", obj.CompanyCode);
                cmd.Parameters.AddWithValue("@UnitCode", obj.UnitCode);
                cmd.Parameters.AddWithValue("@SubUnitCode", obj.SubUnitCode);
                cmd.Parameters.AddWithValue("@SectionCode", obj.SectionCode);
                cmd.Parameters.AddWithValue("@SubSectionCode", obj.SubSectionCode);
                cmd.Parameters.AddWithValue("@Group_Code", obj.GroupCode);
                cmd.Parameters.AddWithValue("@SubGroup_Code ", obj.SubGroup_Code);
                cmd.Parameters.AddWithValue("@AcquiredAs", obj.AcquiredAs);
                cmd.Parameters.AddWithValue("@AcquiredType", obj.AcquiredType);
                cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
                cmd.Parameters.AddWithValue("@SalvageValue", obj.SalvageValue);
                cmd.Parameters.AddWithValue("@AssetCode", obj.AssetCode);
                cmd.Parameters.AddWithValue("@AssetName", obj.AssetName);
                cmd.Parameters.AddWithValue("@PurchaseDate", obj.PurchaseDate);
                cmd.Parameters.AddWithValue("@Put_In_Use_Date", obj.Put_In_Use_Date);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@Asset_Life", obj.Asset_Life);
                cmd.Parameters.AddWithValue("@SubArea", obj.SubArea);
                cmd.Parameters.AddWithValue("@Method_Code", obj.Method_Code);
                cmd.Parameters.AddWithValue("@SubGroup_ShiftCode", obj.SubGroup_ShiftCode);
                cmd.Parameters.AddWithValue("@ITBook", "ITB-1");
                cmd.Parameters.AddWithValue("@COMBook", "COM-0");

                cmd.Parameters.AddWithValue("@Policy_No", obj.Policy_No);
                cmd.Parameters.AddWithValue("@Insurer_Name", obj.Insurer_Name);
                cmd.Parameters.AddWithValue("@Tangiable", obj.Tangiable);
                
                cmd.Parameters.AddWithValue("@Effe_From", obj.Effe_From);
                cmd.Parameters.AddWithValue("@Effe_To", obj.Effe_To);
                cmd.Parameters.AddWithValue("@Created_By", obj.Created_By);
                cmd.Parameters.AddWithValue("@Created_On", obj.Created_On);
                cmd.Parameters.AddWithValue("@Created_Hostname", obj.Created_Hostname);
                cmd.Parameters.AddWithValue("@Ip_Address", obj.Tangiable);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("AssetMaster", "Asset", null);
        }



        #region SelectList Items
        public List<SelectListItem> SectionList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "select SectionCode,SectionName from Jct_Asset_Dep_Section";
            SqlCommand cmd = new SqlCommand(sql, con);
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

        public List<SelectListItem> SubSectionList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "select Sub_SectionCode,Sub_SectionName from Jct_Asset_Dep_Section";
            SqlCommand cmd = new SqlCommand(sql, con);
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

        public List<SelectListItem> Companylist()
        {

            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_Company_Fetch";
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

        public List<SelectListItem> grouplist()
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

        public List<SelectListItem> Subgrouplist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_SubGroup_Fetch";
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

        public List<SelectListItem> Unitlist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_Unit_Fetch";
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

        public JsonResult GetShiftDetails(string SubGroupCode, string SubGroup_ShiftCode, string SubGroupMethod)
        {
            SqlConnection con = DBConnection.getConnection();
            Jct_Asset_Dep_Asset model = new Jct_Asset_Dep_Asset();
            string sql = "Jct_Asset_Dep_CompanyActRate_Join_Subgroup";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubGroupCode", SubGroupCode);
            cmd.Parameters.AddWithValue("@SubGroup_ShiftCode", SubGroup_ShiftCode);
            cmd.Parameters.AddWithValue("@SubGroupMethod", SubGroupMethod);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    model.GroupLife = float.Parse(read["Group_life"].ToString());
                    model.EstimatedLife = float.Parse(read["SubGroup_EstimatedLife"].ToString());
                }
            }
            read.Close();
            con.Close();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public List<SelectListItem> SubUnitlist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_SubUnit_Fetch";
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

        public List<SelectListItem> Methodlist()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_Method_Fetch";
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

        #endregion

        #region Ajax methods

        public JsonResult GetSubgrouplist(string GroupCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_GetSubGroup_Fetch";
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

        public JsonResult GetUnitlist(string CompanyCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_GetUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode);
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

        public JsonResult GetSubUnitlist(string UnitCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_GetSubUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
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

        public JsonResult GetSectionList(string SubUnitCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_GetSection_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubUnitCode", SubUnitCode);
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

        public JsonResult GetSubSectionList(string SectionCode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Asset_Detail_GetSubSection_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SectionCode", SectionCode);
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

        #endregion



    }
}