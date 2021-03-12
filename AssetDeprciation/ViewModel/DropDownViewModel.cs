using AssetDeprciation.Models;
using AssetDeprciation.Models.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.ViewModel
{
    public class DropDownViewModel
    {
        
        //DropdownList//
        public List<SelectListItem> BookList { set; get; }
        public List<SelectListItem> GroupList { set; get; }
        public List<SelectListItem> SubGroupList { set; get; }
        public List<SelectListItem> FinancialYearList { set; get; }
        public List<SelectListItem> CompanyList { set; get; }
        public List<SelectListItem> UnitList { set; get; }
        public List<SelectListItem> SubUnitList { set; get; }
        public List<SelectListItem> SectionList { set; get; }
        public List<SelectListItem> SubSectionList { set; get; }
        public List<SelectListItem> MethodList { set; get; }
        public List<SelectListItem> AccontingYearlist { set; get; }
        public List<SelectListItem> ShiftList { set; get; }

        public List<SelectListItem> InsuranceList { set; get; }
        //------------------------//

        ////extra field
        //public DateTime effectiveFrom { set; get; }
        //public DateTime effectiveTo { set; get; }
        //public string FinYear { set; get; }
        //public bool RadioValue { set; get; }
        //public string book { set; get; }
    }
}