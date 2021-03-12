using AssetDeprciation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers
{
    public class SLMProspectiveCalculatorController : Controller
    {
        // GET: SLMProspectiveCalculator
        public ActionResult SLMProspectiveCalculator()
        {
            return View();
        }


        public JsonResult SLMCalculation(SLMProspectiveCalculatorModel data)
        {
           if(!ModelState.Equals(null))
           {
               int count = 0;
               SLMProspectiveCalculatorModel model = new SLMProspectiveCalculatorModel();
               List<SLMProspectiveCalculatorModel> list = new List<SLMProspectiveCalculatorModel>();
               decimal monthUsed = 0;
               model.AccumulatedDepreciation = 0;
               model.AssetLife_Months = data.AssetLife_Months;
               decimal Put_In_Use_Month = data.Put_In_Use.Month;
               model.AnnualDepreciationExpense = Math.Round(Convert.ToDecimal((data.AssetCost - data.SalvageValue) / data.AssetLife),2);
               for(int i=0;i<=data.AssetLife;i++)
               {
               
                   if (Put_In_Use_Month < 4)
                   {
                       monthUsed = 4-Put_In_Use_Month;
                       model.DepreciationExpense = Math.Round((monthUsed / 12) * Convert.ToDecimal(model.AnnualDepreciationExpense),2);
                       model.AccumulatedDepreciation += model.DepreciationExpense;
                       list.Add(new SLMProspectiveCalculatorModel { AnnualDepreciationExpense = model.AnnualDepreciationExpense, DepreciationExpense = model.DepreciationExpense, currentYear = i,AccumulatedDepreciation=model.AccumulatedDepreciation });
                       Put_In_Use_Month = 5;
                   }
                   else
                   {


                       if (Put_In_Use_Month > 4)
                       {
                           int moon=Convert.ToInt16(Put_In_Use_Month);
                           switch (moon)
                           {
                               case 5:
                                   monthUsed = 11;

                                   break;

                               case 6:
                                   monthUsed = 10;
                                   break;
                               case 7:
                                   monthUsed = 9;
                                   break;
                               case 8:
                                   monthUsed = 8;
                                   break;
                               case 9:
                                   monthUsed = 7;
                                   break;
                               case 10:
                                   monthUsed = 6;
                                   break;
                               case 11:
                                   monthUsed = 5;
                                   break;
                               case 12:
                                   monthUsed = 4;
                                   break;
                               default:
                                   break;

                           }

                           model.DepreciationExpense = Math.Round((monthUsed / 12) * Convert.ToDecimal(model.AnnualDepreciationExpense),2);
                           model.AccumulatedDepreciation += model.DepreciationExpense;
                           list.Add(new SLMProspectiveCalculatorModel { AnnualDepreciationExpense = model.AnnualDepreciationExpense, DepreciationExpense = model.DepreciationExpense, currentYear = i, AccumulatedDepreciation = model.AccumulatedDepreciation });
                           Put_In_Use_Month = 4;
                       }

                       else if(model.AssetLife_Months-monthUsed>12)
                       {
                           model.DepreciationExpense = Math.Round(Convert.ToDecimal(model.AnnualDepreciationExpense),2);
                           model.AccumulatedDepreciation += model.DepreciationExpense;
                           monthUsed += 12;
                           list.Add(new SLMProspectiveCalculatorModel { AnnualDepreciationExpense = model.AnnualDepreciationExpense, DepreciationExpense = model.DepreciationExpense, currentYear = i, AccumulatedDepreciation = model.AccumulatedDepreciation });
                       }
                       else
                       {
                           decimal lifeleft=model.AssetLife_Months - monthUsed;
                           model.DepreciationExpense = Math.Round((lifeleft/ 12) * Convert.ToDecimal(model.AnnualDepreciationExpense),2);
                           model.AccumulatedDepreciation += model.DepreciationExpense;
                           monthUsed += lifeleft;
                           list.Add(new SLMProspectiveCalculatorModel { AnnualDepreciationExpense = model.AnnualDepreciationExpense, DepreciationExpense = model.DepreciationExpense, currentYear = i, AccumulatedDepreciation = model.AccumulatedDepreciation });
                       }

                   }
               }
               decimal tempqq= monthUsed;
               var temp = list.Count;
               return Json(new { Result = "OK", Records = list, TotalRecordCount = temp }, JsonRequestBehavior.AllowGet);

           }
            return Json(new { });
        }
    }
}