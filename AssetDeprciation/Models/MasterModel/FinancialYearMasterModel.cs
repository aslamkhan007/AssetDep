using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class FinancialYearMasterModel
    {
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string BookCode { get; set; }
        public Nullable<System.DateTime> FinOpDt { get; set; }
        public Nullable<System.DateTime> FinClDt { get; set; }
        public int FinClMonths { set; get; }
        public int FinDays { set; get; }
        public string FinYear { get; set; }
        public bool Closed { get; set; }
        public bool Manual { get; set; }
        public string asstyear { get; set; }
    }
}