using AssetDeprciation.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetDeprciation.ViewModel
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            ReportFieldModel = new ReportFieldModel();
            CompanyActYearlyDepReportModel = new CompanyActYearlyDepReportModel();
        }

        public ReportFieldModel ReportFieldModel { set; get; }

        public CompanyActYearlyDepReportModel CompanyActYearlyDepReportModel { set; get; }
    }
}