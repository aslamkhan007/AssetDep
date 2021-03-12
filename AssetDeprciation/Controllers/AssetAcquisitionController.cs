using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models;
using AssetDeprciation.Models.MasterModel;
using AssetDeprciation.ViewModel;
using Security.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers
{
    public class AssetAcquisitionController : BaseController
    {
        //
        // GET: /AssetAcquisition/
        public ActionResult AssetAcquisition()
        {
            return View();
        }
            
        [HttpPost]
        public JsonResult AssetList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Acquisition_Asset_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            List<Jct_Asset_Dep_Asset> list = new List<Jct_Asset_Dep_Asset>();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    Jct_Asset_Dep_Asset model = new Jct_Asset_Dep_Asset();
                    model.TransNo = Convert.ToInt32(read["TransNo"].ToString());
                    model.AssetCode = read["AssetCode"].ToString();
                    model.AssetName = read["AssetName"].ToString();
                    model.GroupCode = read["GroupCode"].ToString();
                    model.AcquiredAs = read["AcquiredAs"].ToString();
                    model.Put_In_Use_Date = Convert.ToDateTime(read["Put_In_Use_Date"].ToString());
                    model.Quantity = Convert.ToInt32(read["Quantity"].ToString());
                    list.Add(model);
                }

                var querableList = list.AsQueryable();
                int temp = querableList.Count();


                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("AssetCode ASC"))
                {
                    querableList = querableList.OrderBy(p => p.AssetCode);
                }
                querableList = querableList.Skip(jtStartIndex).Take(jtPageSize);

                list = querableList.ToList();
                return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count }, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult AssetEditInfo(string Id)
        {
            try
            {
                ParentViewModel model = new ParentViewModel();
                SqlConnection con = DBConnection.getConnection();
                string sql = "Jct_Asset_Dep_Asset_Fetch";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        model.Jct_Asset_Dep_Asset.Amount = float.Parse(read["Amount"].ToString());
                        model.Jct_Asset_Dep_Asset.Asset_Life = read["Asset_Life"].ToString();
                        model.Jct_Asset_Dep_Asset.AssetCode = read["AssetCode"].ToString();
                        model.Jct_Asset_Dep_Asset.AssetName = read["AssetName"].ToString();
                        model.Jct_Asset_Dep_Asset.CompanyCode = read["CompanyCode"].ToString();
                        model.Jct_Asset_Dep_Asset.Method_Code = read["Method_Code"].ToString();
                        //model.Jct_Asset_Dep_Asset.Depriciation_Rate = Convert.ToInt32(read["Depriciation_Rate"].ToString());
                        model.Jct_Asset_Dep_Asset.GroupCode = read["GroupCode"].ToString();
                        model.Jct_Asset_Dep_Asset.PurchaseDate = Convert.ToDateTime(read["PurchaseDate"].ToString());
                        model.Jct_Asset_Dep_Asset.Put_In_Use_Date = Convert.ToDateTime(read["Put_In_Use_Date"].ToString());
                        model.Jct_Asset_Dep_Asset.Quantity = Convert.ToInt32(read["Quantity"].ToString());
                        model.Jct_Asset_Dep_Asset.SalvageValue = Convert.ToInt32(read["SalvageValue"].ToString());
                        model.Jct_Asset_Dep_Asset.SectionCode = read["SectionCode"].ToString();
                        model.Jct_Asset_Dep_Asset.SubArea = read["SubArea"].ToString();
                        model.Jct_Asset_Dep_Asset.SubGroup_Code = read["SubGroup_Code"].ToString();
                        model.Jct_Asset_Dep_Asset.SubSectionCode = read["SubSectionCode"].ToString();
                        model.Jct_Asset_Dep_Asset.SubUnitCode = read["SubUnitCode"].ToString();
                        model.Jct_Asset_Dep_Asset.SubGroup_ShiftCode = read["SubGroup_ShiftCode"].ToString();
                        model.Jct_Asset_Dep_Asset.UnitCode = read["UnitCode"].ToString();
                        model.Jct_Asset_Dep_Asset.AcquiredAs = read["AcquiredAs"].ToString();
                        model.Jct_Asset_Dep_Asset.AcquiredType = read["AcquiredType"].ToString();
                        model.Jct_Asset_Dep_Asset.Effe_To = Convert.ToDateTime(read["Effe_To"].ToString());
                        model.Jct_Asset_Dep_Asset.Effe_From = Convert.ToDateTime(read["Effe_From"].ToString());
                        model.Jct_Asset_Dep_Asset.Depriciation_Rate_IT =(float)(read["Depriciation_Rate_IT"] is DBNull ? Convert.ToDouble(0):Convert.ToDouble(read["Depriciation_Rate_IT"].ToString())) ;
                        model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = float.Parse(read["SubGroupRate"].ToString());

                        model.Jct_Asset_Dep_Asset.Insurer_Name = read["Insurer_Name"].ToString();
                        model.Jct_Asset_Dep_Asset.Policy_No = read["Policy_No"].ToString();
                        


                        //--------------Insurance Details for Additional Partial View-----------//
                        model.Jct_Asset_Dep_Bill_Info.Bill_Date = Convert.ToDateTime(read["Bill_Date"].ToString());
                        model.Jct_Asset_Dep_Bill_Info.Bill_No = read["Bill_No"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Effe_To = Convert.ToDateTime(read["Effe_To"].ToString());
                        model.Jct_Asset_Dep_Bill_Info.Empcode = HttpContext.User.Identity.Name;
                        model.Jct_Asset_Dep_Bill_Info.Imported_Indegeneous = read["Imported_Indegeneous"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Insurance_Date = Convert.ToDateTime(read["Insurance_Date"].ToString());
                        model.Jct_Asset_Dep_Bill_Info.Insurance_Name = read["Insurance_Name"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Insurance_Type = read["Insurance_Type"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Invoice_Attachment = read["Invoice_Attachment"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Invoice_NO = read["Invoice_NO"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Manufacture = read["Manufacture"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Modal = read["Modal"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.PO_Date = Convert.ToDateTime(read["PO_Date"].ToString());
                        model.Jct_Asset_Dep_Bill_Info.PO_No = read["PO_No"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Supplier = read["Supplier"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Custom_Duty = read["Custom_Duty"].ToString();
                        model.Jct_Asset_Dep_Bill_Info.Cenvat = read["Cenvat"].ToString();

                        //----------------------------------------------------------------------//

                    }
                    read.Close();
                    con.Close();
                    model.DropDownViewModel.CompanyList = companyList();
                    model.DropDownViewModel.InsuranceList = InsuranceList();
                    model.DropDownViewModel.UnitList = unitList();
                    model.DropDownViewModel.SubUnitList = subunitList();
                    model.DropDownViewModel.SectionList = GetSectionList();
                    model.DropDownViewModel.SubSectionList = getSubSectionList();
                    model.DropDownViewModel.GroupList = Getgrouplist();
                    model.DropDownViewModel.SubGroupList = GetSubgrouplist();
                    model.DropDownViewModel.MethodList = getMethodList();
                    model.DropDownViewModel.ShiftList = ShiftList();
                    //if (Int32.Parse(model.Jct_Asset_Dep_Asset.Asset_Life) >= 12)
                    //{
                    //    float lifeyear = float.Parse(model.Jct_Asset_Dep_Asset.Asset_Life) / 12;
                    //    float rate = (95 / lifeyear);
                    //    //model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                    //    model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com =rate;
                    //}
                    //else
                    //{
                    //    //decimal rate = Convert.ToDecimal(95 / Convert.ToDouble(model.Jct_Asset_Dep_Asset.Asset_Life));
                    //    //model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = Math.Round(rate, 2, MidpointRounding.AwayFromZero);

                    //    float rate = float.Parse(Convert.ToString(95 / float.Parse(Convert.ToString(model.Jct_Asset_Dep_Asset.Asset_Life))));
                    //    model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = rate;

                    //}



                }
                return View(model);
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
                return View();
            }
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

        public JsonResult AssetChildEditInfo(string AssetCode)
        {
            ParentViewModel model = new ParentViewModel();
            if (ModelState.IsValid)
                    {
                //try
                //{                   
                        //ParentViewModel model = new ParentViewModel();
                        //ModelState.Clear();
                        SqlConnection con = DBConnection.getConnection();
                        string sql = "Jct_Asset_Dep_Asset_Child_Fetch";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AssetCode", AssetCode);
                        SqlDataReader read = cmd.ExecuteReader();
                        if (read.HasRows)
                        {
                            ModelState.Clear();
                            while (read.Read())
                            {

                                model.Jct_Asset_Dep_Asset.Amount = Convert.ToInt32(read["Amount"].ToString());
                                //model.Jct_Asset_Dep_Asset.Asset_Life = read["Asset_Life"].ToString();
                                model.Jct_Asset_Dep_Asset.Asset_Life = "180";
                                model.Jct_Asset_Dep_Asset.AssetCode = read["AssetCode"].ToString();
                                //model.Jct_Asset_Dep_Asset.AssetName = read["AssetName"].ToString();
                                model.Jct_Asset_Dep_Asset.CompanyCode = read["CompanyCode"].ToString();
                                model.Jct_Asset_Dep_Asset.Method_Code = read["Method_Code"].ToString();
                                //model.Jct_Asset_Dep_Asset.Depriciation_Rate = Convert.ToInt32(read["Depriciation_Rate"].ToString());
                                model.Jct_Asset_Dep_Asset.GroupCode = read["GroupCode"].ToString();
                                model.Jct_Asset_Dep_Asset.PurchaseDate = Convert.ToDateTime(read["PurchaseDate"].ToString());
                                model.Jct_Asset_Dep_Asset.Put_In_Use_Date = Convert.ToDateTime(read["Put_In_Use_Date"].ToString());
                                model.Jct_Asset_Dep_Asset.Quantity = Convert.ToInt32(read["Quantity"].ToString());
                                model.Jct_Asset_Dep_Asset.SalvageValue = Convert.ToInt32(read["SalvageValue"].ToString());
                                model.Jct_Asset_Dep_Asset.SectionCode = read["SectionCode"].ToString();
                                model.Jct_Asset_Dep_Asset.SubArea = read["SubArea"].ToString();
                                model.Jct_Asset_Dep_Asset.SubGroup_Code = read["SubGroup_Code"].ToString();
                                model.Jct_Asset_Dep_Asset.SubSectionCode = read["SubSectionCode"].ToString();
                                model.Jct_Asset_Dep_Asset.SubUnitCode = read["SubUnitCode"].ToString();
                                model.Jct_Asset_Dep_Asset.UnitCode = read["UnitCode"].ToString();
                                model.Jct_Asset_Dep_Asset.AcquiredAs = read["AcquiredAs"].ToString();
                                model.Jct_Asset_Dep_Asset.AcquiredType = read["AcquiredType"].ToString();
                                model.Jct_Asset_Dep_Asset.Effe_To = Convert.ToDateTime(read["Effe_To"].ToString());
                                model.Jct_Asset_Dep_Asset.Effe_From = Convert.ToDateTime(read["Effe_From"].ToString());
                                model.Jct_Asset_Dep_Asset.Depriciation_Rate_IT = (float)(read["Depriciation_Rate_IT"] is DBNull ? Convert.ToDouble(0) : Convert.ToDouble(read["Depriciation_Rate_IT"].ToString()));


                                //--------------Insurance Details for Additional Partial View-----------//
                                model.Jct_Asset_Dep_Bill_Info.Bill_Date = Convert.ToDateTime(read["Bill_Date"].ToString());
                                model.Jct_Asset_Dep_Bill_Info.Bill_No = read["Bill_No"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Effe_To = Convert.ToDateTime(read["Effe_To"].ToString());
                                model.Jct_Asset_Dep_Bill_Info.Empcode = HttpContext.User.Identity.Name;
                                model.Jct_Asset_Dep_Bill_Info.Imported_Indegeneous = read["Imported_Indegeneous"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Insurance_Date = Convert.ToDateTime(read["Insurance_Date"].ToString());
                                model.Jct_Asset_Dep_Bill_Info.Insurance_Name = read["Insurance_Name"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Insurance_Type = read["Insurance_Type"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Invoice_Attachment = read["Invoice_Attachment"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Invoice_NO = read["Invoice_NO"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Manufacture = read["Manufacture"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Modal = read["Modal"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.PO_Date = Convert.ToDateTime(read["PO_Date"].ToString());
                                model.Jct_Asset_Dep_Bill_Info.PO_No = read["PO_No"].ToString();
                                model.Jct_Asset_Dep_Bill_Info.Supplier = read["Supplier"].ToString();
                                //----------------------------------------------------------------------//

                            }
                            read.Close();
                            con.Close();
                            model.DropDownViewModel.CompanyList = companyList();
                            model.DropDownViewModel.InsuranceList = InsuranceList();
                            model.DropDownViewModel.UnitList = unitList();
                            model.DropDownViewModel.SubUnitList = subunitList();
                            model.DropDownViewModel.SectionList = GetSectionList();
                            model.DropDownViewModel.SubSectionList = getSubSectionList();
                            model.DropDownViewModel.GroupList = Getgrouplist();
                            model.DropDownViewModel.SubGroupList = GetSubgrouplist();
                            model.DropDownViewModel.MethodList = getMethodList();
                            if (Int32.Parse(model.Jct_Asset_Dep_Asset.Asset_Life) >= 12)
                            {
                                float lifeyear = float.Parse(model.Jct_Asset_Dep_Asset.Asset_Life) / 12;
                                float rate = (95 / lifeyear);
                                //model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                                model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = rate;
                            }
                            else
                            {
                                //decimal rate = Convert.ToDecimal(95 / Convert.ToDouble(model.Jct_Asset_Dep_Asset.Asset_Life));
                                //model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = Math.Round(rate, 2, MidpointRounding.AwayFromZero);

                                float rate = float.Parse(Convert.ToString(95 / float.Parse(Convert.ToString(model.Jct_Asset_Dep_Asset.Asset_Life))));
                                model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com = rate;

                            }

                        }
                        //if (ModelState.IsValid)
                        //{
                            ModelState.Clear();
                        //}
                        //return View(model);
                    
                   
                    //return RedirectToAction("AssetChildEditInfo", "AssetAcquisition");
                    
                

                //catch (Exception ex)
                //{

                //    ex.Message.ToString();
                //    return View();
                //}
                            //return View(model);
                }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View(model);
            
        }


        public List<SelectListItem> getMethodList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Select Method_Code,Method_Name from Jct_Asset_Dep_Method where Status='A'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    list.Add(new SelectListItem { Text = read["Method_Name"].ToString(), Value = read["Method_Code"].ToString() });
                }
            }
            return list;
        }
        public List<SelectListItem> InsuranceList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "select Insurance_Code,Insurance_Name from Jct_Asset_Dep_Insurance";
            SqlCommand cmd = new SqlCommand(sql, con);
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

        [HttpPost]
        public ActionResult UpdateAssetInfo(ParentViewModel model)
        {
            try
            {
                Jct_Asset_Dep_Asset asset = new Jct_Asset_Dep_Asset();
                Jct_Asset_Dep_Bill_Info info = new Jct_Asset_Dep_Bill_Info();
                //--------------------------------------------------------//
                asset.CompanyCode = model.Jct_Asset_Dep_Asset.CompanyCode;
                asset.UnitCode = model.Jct_Asset_Dep_Asset.UnitCode;
                asset.SubUnitCode = model.Jct_Asset_Dep_Asset.SubUnitCode;
                asset.SectionCode = model.Jct_Asset_Dep_Asset.SectionCode;
                asset.SubSectionCode = model.Jct_Asset_Dep_Asset.SubSectionCode;
                asset.GroupCode = model.Jct_Asset_Dep_Asset.GroupCode;
                asset.SubGroup_Code = model.Jct_Asset_Dep_Asset.SubGroup_Code;
                asset.AcquiredAs = model.Jct_Asset_Dep_Asset.AcquiredAs;
                asset.AcquiredType = model.Jct_Asset_Dep_Asset.AcquiredType;
                asset.Quantity = model.Jct_Asset_Dep_Asset.Quantity;
                asset.SalvageValue = model.Jct_Asset_Dep_Asset.SalvageValue;
                asset.AssetCode = model.Jct_Asset_Dep_Asset.AssetCode;
                asset.AssetName = model.Jct_Asset_Dep_Asset.AssetName;
                asset.SubGroup_ShiftCode = model.Jct_Asset_Dep_Asset.SubGroup_ShiftCode;
                asset.PurchaseDate = model.Jct_Asset_Dep_Asset.PurchaseDate;
                asset.Put_In_Use_Date = model.Jct_Asset_Dep_Asset.Put_In_Use_Date;
                asset.Amount = model.Jct_Asset_Dep_Asset.Amount;
                asset.Asset_Life = model.Jct_Asset_Dep_Asset.Asset_Life;
                asset.Asset_Life_Year = model.Jct_Asset_Dep_Asset.Asset_Life_Year;                  

                asset.SubArea = model.Jct_Asset_Dep_Asset.SubArea;
                asset.Method_Code = model.Jct_Asset_Dep_Asset.Method_Code;


                asset.Insurer_Name = model.Jct_Asset_Dep_Asset.Insurer_Name;
                asset.Policy_No  = model.Jct_Asset_Dep_Asset.Policy_No;
                asset.NetBook_Value = model.Jct_Asset_Dep_Asset.Amount;

                asset.Depriciation_Rate_Com = float.Parse(Convert.ToString(model.Jct_Asset_Dep_Asset.Depriciation_Rate_Com));
                asset.Depriciation_Rate_IT = float.Parse(Convert.ToString(model.Jct_Asset_Dep_Asset.Depriciation_Rate_IT));

                asset.Life_In_Days = model.Jct_Asset_Dep_Asset.Life_In_Days;
                asset.Effe_From = System.DateTime.Today;
                asset.Effe_To = model.Jct_Asset_Dep_Asset.Effe_To;
                asset.Created_By = HttpContext.User.Identity.Name;
                asset.Created_On = System.DateTime.Now;
                asset.Created_Hostname = System.Environment.MachineName;
                asset.Tangiable = ("::1" == System.Web.HttpContext.Current.Request.UserHostAddress) ? "localhost" : System.Web.HttpContext.Current.Request.UserHostAddress;
                //--------------------------------------------------------------------------------//
                //-----------------------------------------------------------------------------//
                //info.Bill_Date=model
                info.AssetCode = model.Jct_Asset_Dep_Asset.AssetCode;
                info.Insurance_Name = model.Jct_Asset_Dep_Bill_Info.Insurance_Name;
                info.Insurance_Date = model.Jct_Asset_Dep_Bill_Info.Insurance_Date;
                info.Insurance_Type = model.Jct_Asset_Dep_Bill_Info.Insurance_Type;
                info.Invoice_NO = model.Jct_Asset_Dep_Bill_Info.Invoice_NO;
                info.Invoice_Attachment = model.Jct_Asset_Dep_Bill_Info.Invoice_Attachment;
                info.PO_No = model.Jct_Asset_Dep_Bill_Info.PO_No;
                info.PO_Date = model.Jct_Asset_Dep_Asset.PurchaseDate;
                info.Bill_No = model.Jct_Asset_Dep_Bill_Info.Bill_No;
                info.Bill_Date = model.Jct_Asset_Dep_Bill_Info.Bill_Date;
                info.Manufacture = model.Jct_Asset_Dep_Bill_Info.Manufacture;
                info.Modal = model.Jct_Asset_Dep_Bill_Info.Modal;
                info.Supplier = model.Jct_Asset_Dep_Bill_Info.Supplier;
                info.Imported_Indegeneous = model.Jct_Asset_Dep_Bill_Info.Imported_Indegeneous;
                info.Cenvat = model.Jct_Asset_Dep_Bill_Info.Cenvat;
                info.Custom_Duty  = model.Jct_Asset_Dep_Bill_Info.Custom_Duty;




                //-----------------------------------------------------------------------------//
                SqlConnection con = DBConnection.getConnection();              
                string sql = "Jct_Asset_Dep_Asset_Update";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyCode", asset.CompanyCode);
                cmd.Parameters.AddWithValue("@UnitCode", asset.UnitCode);
                cmd.Parameters.AddWithValue("@SubUnitCode", asset.SubUnitCode);
                cmd.Parameters.AddWithValue("@SectionCode", asset.SectionCode);
                cmd.Parameters.AddWithValue("@SubSectionCode", asset.SubSectionCode);
                cmd.Parameters.AddWithValue("@Group_Code", asset.GroupCode);
                cmd.Parameters.AddWithValue("@SubGroup_Code ", asset.SubGroup_Code);
                cmd.Parameters.AddWithValue("@AcquiredAs", asset.AcquiredAs);
                cmd.Parameters.AddWithValue("@AcquiredType", asset.AcquiredType);
                cmd.Parameters.AddWithValue("@Quantity", asset.Quantity);
                cmd.Parameters.AddWithValue("@SalvageValue", asset.SalvageValue);
                cmd.Parameters.AddWithValue("@AssetCode", asset.AssetCode);
                cmd.Parameters.AddWithValue("@AssetName", asset.AssetName);

                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Put_In_Use_Date", asset.Put_In_Use_Date);
                cmd.Parameters.AddWithValue("@Amount", asset.Amount);
                cmd.Parameters.AddWithValue("@Asset_Life", asset.Asset_Life);
                cmd.Parameters.AddWithValue("@Asset_Life_Year", asset.Asset_Life_Year);
                cmd.Parameters.AddWithValue("@SubArea", asset.SubArea);             
                cmd.Parameters.AddWithValue("@Method_Code", asset.Method_Code);
                cmd.Parameters.AddWithValue("@ITBook", "ITB-1");
                cmd.Parameters.AddWithValue("@COMBook", "COM-0");
                cmd.Parameters.AddWithValue("@SubGroup_ShiftCode", asset.SubGroup_ShiftCode);
                cmd.Parameters.AddWithValue("@Depriciation_Rate_IT", asset.Depriciation_Rate_IT);
                cmd.Parameters.AddWithValue("@Depriciation_Rate_Com", asset.Depriciation_Rate_Com);
                cmd.Parameters.AddWithValue("@Life_In_Days", asset.Life_In_Days);


                cmd.Parameters.AddWithValue("@Insurance_Name", info.Insurance_Name);
                cmd.Parameters.AddWithValue("@Insurance_Date", info.Insurance_Date);
                cmd.Parameters.AddWithValue("@Insurance_Type", info.Insurance_Type);
                cmd.Parameters.AddWithValue("@Invoice_NO", info.Invoice_NO);
                cmd.Parameters.AddWithValue("@Invoice_Attachment", info.Invoice_Attachment);
                cmd.Parameters.AddWithValue("@PO_No", info.PO_No);
                cmd.Parameters.AddWithValue("@PO_Date", info.PO_Date);
                cmd.Parameters.AddWithValue("@Bill_No", info.Bill_No);
                cmd.Parameters.AddWithValue("@Bill_Date", info.Bill_Date);
                cmd.Parameters.AddWithValue("@Manufacture", info.Manufacture);
                cmd.Parameters.AddWithValue("@Modal", info.Modal);
                cmd.Parameters.AddWithValue("@Supplier", info.Supplier);
                cmd.Parameters.AddWithValue("@Imported_Indegeneous", info.Imported_Indegeneous);

                cmd.Parameters.AddWithValue("@Insurer_Name", asset.Insurer_Name);
                cmd.Parameters.AddWithValue("@Policy_No", asset.Policy_No);
                cmd.Parameters.AddWithValue("@Cenvat", info.Cenvat);
                cmd.Parameters.AddWithValue("@Custom_Duty", info.Custom_Duty);

                cmd.Parameters.AddWithValue("@Effe_From", asset.Effe_From);
                cmd.Parameters.AddWithValue("@Effe_To", asset.Effe_To);
                cmd.Parameters.AddWithValue("@Created_By", asset.Created_By);
                cmd.Parameters.AddWithValue("@Created_On", asset.Created_On);
                cmd.Parameters.AddWithValue("@Created_Hostname", asset.Created_Hostname);
                cmd.Parameters.AddWithValue("@Ip_Address", asset.Tangiable);
                cmd.Parameters.AddWithValue("@NetBook_Value", asset.NetBook_Value);
                cmd.Parameters.AddWithValue("@BookCode", User.book);
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("AssetEditInfo", "AssetAcquisition", new { Id = asset.AssetCode });
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
                return View();
            }
        }

        #region Fetch Rate on Mehtod Change 

        public JsonResult fetchRateForNewMethod(string MethodCode,string GroupCode,string SubGroupCode,string ShiftCode)
        {
            float rate;
            string msg;
            SqlConnection con = DBConnection.getConnection();
            SqlCommand cmd = new SqlCommand("Jct_Asset_Dep_Asset_Fetch_Rate_On_Method_Change", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MethodCode", MethodCode);
            cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
            cmd.Parameters.AddWithValue("@SubGroupCode", SubGroupCode);
            cmd.Parameters.AddWithValue("@ShiftCode", ShiftCode);
            SqlDataReader dr =cmd.ExecuteReader();
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     rate = (float)Convert.ToDouble(dr["SubGroupRate"].ToString());
                     msg = "Success";
                     return Json(new { rate, msg }, JsonRequestBehavior.AllowGet);
                 }
             }

                 msg = "Rate Not added for this Combination of Group,SubGroup and Shift for the current selected Method.Enter rate Using Rate master!!!!!";
                 return Json(msg, JsonRequestBehavior.AllowGet);
           
        }
        #endregion
        public List<SelectListItem> Getgrouplist()
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

            return list;
        }
        public List<SelectListItem> GetSubgrouplist()
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

            return list;
        }
        public List<SelectListItem> GetSectionList()
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
            return list;
        }
        public List<SelectListItem> getSubSectionList()
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
            return list;
        }
        public List<SelectListItem> companyList()
        {
            List<SelectListItem> clist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_Company_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    clist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return clist;
            }
            else
            {
                read.Close();
                con.Close();
                return clist;
            }
        }
        public List<SelectListItem> unitList()
        {
            List<SelectListItem> ulist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_Unit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    ulist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return ulist;
            }
            else
            {
                read.Close();
                con.Close();
                return ulist;
            }
        }
        public List<SelectListItem> subunitList()
        {
            List<SelectListItem> sublist = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "Jct_Asset_Dep_Section_SubUnit_Fetch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    sublist.Add(new SelectListItem { Text = read[1].ToString(), Value = read[0].ToString() });
                }
                read.Close();
                con.Close();
                return sublist;
            }
            else
            {
                read.Close();
                con.Close();
                return sublist;
            }
        }
        //-----------------------------Ajax Calls--------------------//
        public JsonResult getDetailsForGroup(string grpCode)
        {
            ParentViewModel model = new ParentViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            string sql = "SELECT * FROM  dbo.Jct_Asset_Dep_Group INNER JOIN dbo.Jct_Asset_Dep_SubGroup ON dbo.Jct_Asset_Dep_Group.GroupCode = dbo.Jct_Asset_Dep_SubGroup.GroupCode WHERE dbo.Jct_Asset_Dep_Group.GroupCode=@1";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@1", grpCode);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {

                while (read.Read())
                {
                    //model.Jct_Asset_Dep_Group.Group_Life = read["Group_Life"].ToString();
                    //model.Jct_Asset_Dep_SubGroup.SubGroup_Code = read["SubGroup_Code"].ToString();
                    //model.Jct_Asset_Dep_SubGroup.SubGroup_Name = read["SubGroup_Name"].ToString();
                    list.Add(new SelectListItem { Text = read["SubGroup_Name"].ToString(), Value = read["SubGroup_Code"].ToString() });
                }
                read.Close();
                con.Close();
                model.DropDownViewModel.SubGroupList = list;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                read.Close();
                con.Close();
                list = new List<SelectListItem>();
                model.DropDownViewModel.SubGroupList = list;
                return Json(model, JsonRequestBehavior.AllowGet);
            }


        }
        //----------------------------------------------------------------//

    }
}