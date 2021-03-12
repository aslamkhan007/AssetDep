using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Asset
    {
        public string AcquiredAs { set; get; }
        public string AcquiredType { set; get; }
        public string date { get; set; }
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string UnitCode { get; set; }
        public string SubUnitCode { get; set; }
        public string SectionCode { get; set; }
        public string SubSectionCode { get; set; }
        public string GroupCode { get; set; }
        public string SubGroup_Code { set; get; }
        public string SubGroup_ShiftCode { set; get; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }

        //public float totalPrice { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<System.DateTime> Put_In_Use_Date { get; set; }
        public Nullable<float> Amount { get; set; }
        public string SubArea { set; get; }
        public Nullable<float> SalvageValue { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Asset_Life { get; set; }       
        public string Remarks { get; set; }
        public Nullable<float> Depriciation_Rate_Com { get; set; }
        public Nullable<float> Depriciation_Rate_IT { get; set; }
        public string Method_Code { set; get; }
        public Nullable<int> Life_In_Days { set; get; }
        public string ITBook { set; get; }
        public string COMBook { set; get; }

        public string Tangiable { get; set; }

        public string Policy_No { set; get; }
        public string Insurer_Name { set; get; }
        public Nullable<System.DateTime> Effe_From { get; set; }
        public Nullable<System.DateTime> Effe_To { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public string Created_Hostname { get; set; }
        public string Ip_Address { get; set; }



        //Extra fields
        public Nullable<float> GroupLife { set; get; }
        public Nullable<float> EstimatedLife { set; get; }
        public Nullable<float> Asset_Life_Year { set; get; }
        public Nullable<float> NetBook_Value { set; get; }
    }
}