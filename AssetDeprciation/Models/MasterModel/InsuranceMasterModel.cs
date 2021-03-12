using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class InsuranceMasterModel
    {
        public int TransNo { get; set; }
        public string Asset_Code { get; set; }
        public string Insurance_Id { get; set; }
        public string Insurance_Name { get; set; }
        public string Insurance_Type { get; set; }
        public string Contact_Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip_Code { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Status { get; set; }
        public string Empcode { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}