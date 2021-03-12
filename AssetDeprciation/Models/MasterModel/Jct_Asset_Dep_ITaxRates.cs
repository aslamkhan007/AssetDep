using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_ITaxRates
    {
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string GroupCode { get; set; }
        public string SubGroup_Code { get; set; }

        public Nullable<decimal> Rate { get; set; }
        public string FinYear { get; set; }
    }
}