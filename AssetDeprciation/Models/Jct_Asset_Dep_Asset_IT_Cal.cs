using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models
{
    public class Jct_Asset_Dep_Asset_IT_Cal
    {
        public int TransNo { get; set; }
        public string GroupCode { get; set; }
        public string SubGroupCode { get; set; }
        public string Depriciation_Method { get; set; }
        public string AssetCode { get; set; }
        public Nullable<int> Life_In_Days { get; set; }
        public string Asset_Life { get; set; }
        public Nullable<float> Cost { get; set; }
        public Nullable<float> AccDep { get; set; }
        public Nullable<float> SalvageValue { get; set; }
        public string AccYear { get; set; }
        public Nullable<float> SlmValue { get; set; }
        public Nullable<float> Depriciation_Rate_Com { get; set; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }
        public DateTime Put_In_Use_Date { get; set; }
        public Nullable<System.DateTime> AccClDt { get; set; }




        public Nullable<int> AccDays { get; set; }
        public Nullable<float> DepPeriod { get; set; }
        public Nullable<float> DepAmount { get; set; }

        //Exrtra Fields For Testing
        public float TotalOpeningBalance { set; get; }
        public float TotalQuantity { set; get; }
        public float TotalDepreciation { set; get; }
        public float TotalAmount { set; get; }


    }
}