using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models.MasterModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Company
    {
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}