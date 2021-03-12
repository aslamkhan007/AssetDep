using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models
{
    public class SubAssetDetailsModel
    {
        public int TransNo { get; set; }
        public string AssetCode { get; set; }
        public string Sub_AssetCode { get; set; }
        public string Sub_AssetName { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<System.DateTime> Put_In_Use_Date { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Manufacture_Code { get; set; }
        public string Asset_Life { get; set; }
        public string Depriciation_Method { get; set; }
        public Nullable<int> Depriciation_Rate { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}