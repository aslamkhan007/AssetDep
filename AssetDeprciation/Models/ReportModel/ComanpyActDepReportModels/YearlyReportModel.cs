using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace AssetDeprciation.Models.ReportModel
{
    public class CompanyActYearlyDepReportModel
    {
        public JQGrid YearlyReport { set; get; }
        public string GridNeeded { set; get; }
        public CompanyActYearlyDepReportModel()
        {
            YearlyReport = new JQGrid
            {
                Columns = new List<JQGridColumn>()
                {
                                     new JQGridColumn { DataField = "GroupCode", 
                                                        Width = 50 },
                                     new JQGridColumn { DataField = "SubGroupCode", 
                                                        Width = 100, 
                                                     },
                                     new JQGridColumn { DataField = "TotalOpeningBalance", 
                                                        Width = 100 },
                                     new JQGridColumn { DataField = "TotalQuantity", 
                                                        Width = 75 },
                                     new JQGridColumn { DataField = "TotalDepreciation",
                                                        },      
                                     new JQGridColumn { DataField = "TotalAmount",
                                                      }      
                }
            };
        }
        public string GroupCode { set; get; }
        public string SubGroupCode { set; get; }
        public float TotalOpeningBalance { set; get; }
        public float TotalQuantity { set; get; }
        public float TotalDepreciation { set; get; }
        public float TotalAmount { set; get; }
    }
}