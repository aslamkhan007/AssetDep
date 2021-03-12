using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Method
    {
        public int TransNo { set; get; }
        public string Method_Code { set; get; }
        public string Method_Name { set; get; }
        public string Method_Formula { set; get; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_Hostname { set; get; }
        public string Created_By { set; get; }
        public string Ip_Address { set; get; }

    }
}