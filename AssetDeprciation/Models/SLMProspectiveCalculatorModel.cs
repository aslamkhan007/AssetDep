using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models
{
    public class SLMProspectiveCalculatorModel
    {


        public Nullable<decimal> AssetCost { set; get; }
        public Nullable<decimal> SalvageValue { set; get; }
        public Nullable<decimal> AssetLife { set; get; }
        public int AssetLife_Months { set; get; }
        public Nullable<decimal> rate { set; get; }
        public DateTime Put_In_Use { set; get; }

        //Result Fields
        public Nullable<decimal> DepreciationExpense { set; get; }
        public Nullable<decimal> AccumulatedDepreciation { set; get; }
        public Nullable<decimal> RemainingValue { set; get; }
        public Nullable<decimal> AnnualDepreciationExpense { set; get; }
        public int currentYear { set; get; }
    }
}