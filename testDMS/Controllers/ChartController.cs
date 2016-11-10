using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using testDMS.Models;
using System.Web.Mvc.Ajax;

namespace testDMS.Controllers
{
    public class ChartController : Controller
    {

        DonorManagementDatabaseEntities db = new DonorManagementDatabaseEntities();
        
        public ActionResult Index()
        {
            Report model = new Report();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetChartParams(Report reportModel)
        {

            string chosenCriteria = reportModel.Criteria;
            string chosenType = reportModel.Type;
            string chosenParams = reportModel.Params;
            char chosenEquivalance = reportModel.Equivalance;

            StringBuilder reportString = new StringBuilder();reportString.Append("Criteria: " + chosenCriteria + "<br/>");reportString.Append("Type: " + chosenType + "<br/>");reportString.Append("Params: " + chosenParams + "<br/>");reportString.Append("Equivalance: " + chosenEquivalance + "<br/>");

            //var chartXVals = from DONATION in db.DONATIONs
            //                 where 

            //return RedirectToAction("MakeChart");
            return Content(reportString.ToString());
        }

        public ActionResult MakeChart()
        {

            return View("~/Views/Chart/Index.cshtml");
        }
    }
}