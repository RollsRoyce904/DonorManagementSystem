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
        public string GetChartParams(Report reportModel)
        {

            string chosenCriteria = reportModel.Criteria;
            string chosenType = reportModel.Type;
            string chosenParams = reportModel.Params;
            char chosenEquivalance = reportModel.Equivalance;

            //StringBuilder reportString = new StringBuilder();
            //reportString.Append("Criteria: " + chosenCriteria + "<br/>");
            //reportString.Append("Equivalance: " + chosenEquivalance + "<br/>");
            //reportString.Append("Params: " + chosenParams + "<br/>");
            //reportString.Append("Type: " + chosenType + "<br/>");

            var donors = db.DONORs.Where(n => n.FNAME == chosenParams).Select(n => n.LNAME);

            MakeChart();

            return donors.FirstOrDefault();
        }

       public ActionResult MakeChart()
       {
            return View();
       }

    }
}