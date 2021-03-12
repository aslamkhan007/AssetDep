using AssetDeprciation.Models;
using AssetDeprciation.Models.MasterModel;
using AssetDeprciation.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.ViewModel
{
    public class ParentViewModel
    {
        public ParentViewModel()
        {
            Jct_Asset_Dep_Unit = new Jct_Asset_Dep_Unit();
            Jct_Asset_Dep_Company = new Jct_Asset_Dep_Company();
            Jct_Asset_Dep_SubUnit = new Jct_Asset_Dep_SubUnit();
            Jct_Asset_Dep_Book = new Jct_Asset_Dep_Book();
            Jct_Asset_Dep_Group = new Jct_Asset_Dep_Group();
            Jct_Asset_Dep_SubGroup = new Jct_Asset_Dep_SubGroup();
            Jct_Asset_Dep_CompanyActRate = new Jct_Asset_Dep_CompanyActRate();
            Jct_Asset_Dep_Bill_Info = new Jct_Asset_Dep_Bill_Info();
            Jct_Asset_Dep_Asset = new Jct_Asset_Dep_Asset();
            Jct_Asset_Dep_Book_AccountingYear = new Jct_Asset_Dep_Book_AccountingYear();
            Jct_Asset_Dep_Section = new Jct_Asset_Dep_Section();
            FinancialYearMasterModel = new FinancialYearMasterModel();
            Jct_Asset_Dep_Method = new Jct_Asset_Dep_Method();
            Jct_Asset_Dep_Insurance = new Jct_Asset_Dep_Insurance();
            Jct_Asset_Dep_Sub_Asset = new Jct_Asset_Dep_Sub_Asset();
            Jct_Asset_Dep_Shift = new Jct_Asset_Dep_Shift();
            SLMProspectiveCalculatorModel = new SLMProspectiveCalculatorModel();
            DropDownViewModel = new DropDownViewModel();
            ReportViewModel = new ReportViewModel();
        }
        public Jct_Asset_Dep_Unit Jct_Asset_Dep_Unit { set; get; }
        public Jct_Asset_Dep_Company Jct_Asset_Dep_Company { set; get; }
        public Jct_Asset_Dep_SubUnit Jct_Asset_Dep_SubUnit { set; get; }
        public Jct_Asset_Dep_Book Jct_Asset_Dep_Book { set; get; }
        public Jct_Asset_Dep_Group Jct_Asset_Dep_Group { set; get; }
        public Jct_Asset_Dep_SubGroup Jct_Asset_Dep_SubGroup { set; get; }
        public Jct_Asset_Dep_CompanyActRate Jct_Asset_Dep_CompanyActRate { set; get; }
        public Jct_Asset_Dep_Bill_Info Jct_Asset_Dep_Bill_Info { set; get; }
        public Jct_Asset_Dep_Asset Jct_Asset_Dep_Asset { set; get; }
        public Jct_Asset_Dep_Book_AccountingYear Jct_Asset_Dep_Book_AccountingYear { set; get; }
        public FinancialYearMasterModel FinancialYearMasterModel { set; get; }
        public Jct_Asset_Dep_Section Jct_Asset_Dep_Section { set; get; }
        public Jct_Asset_Dep_Method Jct_Asset_Dep_Method { set; get; }
        public Jct_Asset_Dep_Insurance Jct_Asset_Dep_Insurance { set; get; }
        public Jct_Asset_Dep_Sub_Asset Jct_Asset_Dep_Sub_Asset { set; get; }
        public Jct_Asset_Dep_Shift Jct_Asset_Dep_Shift { set; get; }

        #region ViewModel Declarations  
        public DropDownViewModel DropDownViewModel { set; get; }
        public ReportViewModel ReportViewModel { set; get; } 

        #endregion

        public SLMProspectiveCalculatorModel SLMProspectiveCalculatorModel { set; get; }
        //extra field
        public DateTime effectiveFrom { set; get; }
        public DateTime effectiveTo { set; get; }
        public string FinYear { set; get; }
        public bool RadioValue { set; get; }
        public string BookName { set; get; }
        public string[] selectedGroupCode { set; get; }
   
    }
}