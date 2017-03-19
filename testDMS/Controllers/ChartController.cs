using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testDMS.Models;
using testDMS.DAL;

namespace testDMS.Controllers
{
    public class ChartController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public ChartController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        public ActionResult Index()
        {
           

            ViewBag.Person = new SelectList(ddlData.DONOR, "DonorId", "FNAME");
            ViewBag.Department = new SelectList(ddlData.CODES, "CodeId", "Department");
            ViewBag.Gl = new SelectList(ddlData.CODES, "CodeId", "GL");

            var amountList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "Amount", Value="0", Selected=true },
                    new SelectListItem {Text = "0-100", Value="1" },
                    new SelectListItem {Text = "101-500", Value="2" },
                    new SelectListItem {Text = "501-1000", Value="3" },
                    new SelectListItem {Text = "1001-2000", Value="4" },
                    new SelectListItem {Text = "2001-4000", Value="5" },
                    new SelectListItem {Text = "4001-7000", Value="6" },
                    new SelectListItem {Text = "7001-10,000", Value="7" },
                    new SelectListItem {Text = "10,000+", Value="8" },
                }, "Value", "Text", 0);

            ViewBag.Amount = amountList;
            LoadData();

            return View();
        }

        public ActionResult LoadData()
        {
            IEnumerable<DONOR> Donors = (IEnumerable<DONOR>)drRepo.GetDonors();
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.GetDonations();
            ChartDispalyViewModel model = new ChartDispalyViewModel();
            model.Donors = Donors;
            model.Donations = Donations;

            return PartialView("~/Views/Chart/_ReportData.cshtml", model);
        }

        public ActionResult Search(string searchString)
        {
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.FindBy(searchString);
            return View();
        }

        [HttpPost]
        public ActionResult AmountSearch(Option option)
        {
            Decimal amount1 = 0;
            Decimal amount2 = 0;

            switch (option.Value)
            {
                case "0":
                    break;
                case "1":
                    amount1 = 0;
                    amount2 = 100;
                    break;
                case "2":
                    amount1 = 101;
                    amount2 = 500;
                    break;
                case "3":
                    amount1 = 501;
                    amount2 = 1000;
                    break;
                case "4":
                    amount1 = 1001;
                    amount2 = 2000;
                    break;
                case "5":
                    amount1 = 2001;
                    amount2 = 4000;
                    break;
                case "6":
                    amount1 = 4001;
                    amount2 = 7000;
                    break;
                case "7":
                    amount1 = 7001;
                    amount2 = 10000;
                    break;
                case "8":
                    amount1 = 10001;
                    amount2 = 1000000;
                    break;
                default:

                    break;
            };

            ChartDispalyViewModel model = new ChartDispalyViewModel();

            model.Donations = (IEnumerable<DONATION>)dnRepo.FindBy(amount1, amount2);

            return PartialView("~/Views/Chart/_ReportData.cshtml", model);
        }

        public ActionResult ByDate(DateTime date1, DateTime date2)
        {
            IEnumerable<DONATION> Donation1 = (IEnumerable<DONATION>)dnRepo.FindBy(date1, date2);

            return View();
        }
    }
}