using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.MasterModel
{
    public class Jct_Asset_Dep_Book_AccountingYear
    {
        public int TransNo { get; set; }
        public string CompanyCode { get; set; }
        public string BookCode { get; set; }
        public string AccYear { get; set; }
        public Nullable<System.DateTime> AccOpDt { get; set; }
        public Nullable<System.DateTime> AccClDt { get; set; }
        public int AccClMonths { set; get; }
        public int AccDays { set; get; }
        public string FinYear { get; set; }
        public Nullable<bool> Closed { get; set; }
        public Nullable<bool> Manual { get; set; }


        //Extra Fields
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string Book_Name { set; get; } 

    }
}