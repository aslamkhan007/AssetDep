using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.Models.ReportModel.ComanpyActDepReportModels
{
    public class MonthlyReportModel
    {


        public string GroupCode { set; get; }
        public string SubGroupCode { set; get; }
        public string AssetCode { set; get; }
        public string AccYear { set; get; }
        public float Apr { set; get; }
        public float May { set; get; }
        public float Jun { set; get; }
        public float Jul { set; get; }
        public float Aug { set; get; }
        public float Sep { set; get; }
        public float Oct { set; get; }
        public float Nov { set; get; }
        public float Dec { set; get; }
        public float Jan { set; get; }
        public float Feb { set; get; }
        public float Mar { set; get; }

        public float TotalDepreciation { set; get; }
    }
}