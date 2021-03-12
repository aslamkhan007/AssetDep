using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models
{
    public class Jct_Asset_Dep_Bill_Info
    {
        public int TransNo { get; set; }
        public string AssetCode { get; set; }
        public string Insurance_Name { get; set; }
        public Nullable<System.DateTime> Insurance_Date { get; set; }
        public string Insurance_Type { get; set; }
        public string Invoice_NO { get; set; }
        public string Invoice_Attachment { get; set; }
        public string PO_No { get; set; }
        public Nullable<System.DateTime> PO_Date { get; set; }
        public string Bill_No { get; set; }
        public Nullable<System.DateTime> Bill_Date { get; set; }
        public string Manufacture { get; set; }
        public string Modal { get; set; }
        public string Supplier { get; set; }
        public string Imported_Indegeneous { get; set; }

        public string Custom_Duty { get; set; }
        public string Cenvat{ get; set; }
       
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Empcode { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}