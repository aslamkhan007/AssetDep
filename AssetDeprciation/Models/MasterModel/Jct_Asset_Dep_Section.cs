﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Section
    {
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string UnitCode { get; set; }
        public string SubUnitCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Sub_SectionCode { get; set; }
        public string Sub_SectionName { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
    }
}