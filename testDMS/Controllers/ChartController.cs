using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using testDMS.Models;
using System.Web.Mvc.Ajax;
using System.Web.Helpers;

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
        public JsonResult GetChartParams(Report reportModel)
        {
            string chosenCriteria = reportModel.Criteria;
            string chosenType = reportModel.Type;
            string chosenParams = reportModel.Params;
            char chosenEquivalance = reportModel.Equivalance;

            var dictionary = new Dictionary<int, string>().WithDefaultValue("defaultValue");

            //var query = "SELECT * FROM dbo.DONOR WHERE DONORID " + chosenEquivalance + chosenParams;
            //var mydonors = db.Donor.SqlQuery(query);

            //LINQ for getting codeId's
            var codes =
                from i in db.Donation
                select i.CodeId;
            ViewBag.results = codes;
            return Json(codes, JsonRequestBehavior.AllowGet);
        }


        //not doing anything rn but might need it to later?
        public ActionResult MakeChart()
        {
            //var myChart = new Chart(width: 600, height: 400)
            //    .AddTitle("Chart Title")
            //    .AddSeries(
            //        name: "Employee",
            //        xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
            //        yValues: new[] { "2", "6", "4", "5", "3" })
            //        .Write();

            //myChart.Save("~/Content/")
 
            return PartialView();
        }
    }
}