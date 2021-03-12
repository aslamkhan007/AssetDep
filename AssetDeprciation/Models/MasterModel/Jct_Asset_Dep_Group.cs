using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Group
    {
        public int TransNo { get; set; }
        public string GroupCode { get; set; }
        public string Group_Name { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}