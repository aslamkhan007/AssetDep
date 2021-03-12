using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_SubGroup
    {

        public int TransNo { get; set; }
        public string GroupCode { get; set; }
        public string SubGroup_Code { get; set; }
        public string SubGroup_Name { get; set; }
        public Nullable<decimal> SubGroup_Life { get; set; }
        public Nullable<decimal> SubGroup_EstimatedLife { get; set; }
        public string SubGroup_MethodCode { get; set; }
        public string SubGroup_ShiftCode { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }




        //Extra field
        public string SubGroup_ShiftName { get; set; }
    }
}