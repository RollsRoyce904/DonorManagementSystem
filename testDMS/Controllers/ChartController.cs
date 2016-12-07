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
        public ActionResult GetChartParams(Report reportModel)
        {

            string chosenCriteria = reportModel.Criteria;
            string chosenType = reportModel.Type;
            string chosenParams = reportModel.Params;
            char chosenEquivalance = reportModel.Equivalance;
            
            List<DONATION> donations = new List<DONATION>(db.DONATIONs.ToList());
            List<DONOR> donors = new List<DONOR>();
            List<CONTACT> contacts = new List<CONTACT>(db.CONTACTs.ToList());
            List<CODE> codes = new List<CODE>(db.CODES.ToList());
            List<COMPANY> companys = new List<COMPANY>(db.COMPANies.ToList());

            //switch (int.Parse(chosenCriteria))
            //{
            //    case 0:
            //        //do something
            //        break;
            //    case 1:
            //        //do something
            //        //decimal selectedAmount = decimal.Parse(chosenParams);
            //        //amounts = 
            //        //    from d in donations
            //        //    where d.Amount == selectedAmount
            //        //    select d;
            //        break;
            //    case 2:
            //        //do something
            //        break;
            //    case 3:
            //        //do something
            //        break;
            //    case 4:
            //        //do something
            //        break;
            //    case 5:
            //        //do something
            //        break;
            //    case 6:
            //        //do something
            //        break;
            //    case 7:
            //        //do something
            //        break;
            //    case 8:
            //        //do something
            //        break;
            //    case 9:
            //        //do something
            //        break;
            //    case 10:
            //        //do something
            //        break;
            //    default:
            //        Console.WriteLine(String.Format("Unknown command: {0}", chosenCriteria));
            //        break;
            //}

            //var donors = db.DONORs.Where(n => n.FNAME == chosenParams).Select(n => n.LNAME);
            //ViewData["results"] = amounts;
            //return View(amounts);
            return Json(donors.FindAll(null), JsonRequestBehavior.AllowGet);
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