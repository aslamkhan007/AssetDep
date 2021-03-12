using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models
{
    public class CompanyActHistoryModel
    {
        public int TranNo { get; set; }
        public string BranchCode { get; set; }
        public string AssetCode { get; set; }
        public Nullable<System.DateTime> UseDate { get; set; }
        public string SubAssetCode { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> Salvage { get; set; }
        public Nullable<decimal> Accdepvalue { get; set; }
        public Nullable<decimal> WDVvalue { get; set; }
        public Nullable<decimal> OldRate { get; set; }
        public Nullable<decimal> NewRate { get; set; }
        public Nullable<double> UsedLife { get; set; }
        public Nullable<double> NewLife { get; set; }
        public Nullable<double> BalanceLife { get; set; }
        public Nullable<decimal> CalulatedRate { get; set; }
        public string oldMethod { get; set; }
        public string Method { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> BalLifeInDay { get; set; }
    }
}