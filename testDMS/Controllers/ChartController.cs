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
        private DonorManagementDatabaseEntities data = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public ChartController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        public ActionResult Index()
        {
            IEnumerable<DONOR> Donors = (IEnumerable<DONOR>)drRepo.GetDonors();
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.GetDonations();
            ChartDispalyViewModel model = new ChartDispalyViewModel();
            model.Donors = Donors;
            model.Donations = Donations;

            ViewBag.Person = new SelectList(data.DONOR, "DonorId", "FNAME");
            ViewBag.Department = new SelectList(data.CODES, "CodeId", "Department");
            ViewBag.Gl = new SelectList(data.CODES, "CodeId", "GL");

            return View(model);
        }

        public ActionResult Search(string searchString)
        {
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.FindBy(searchString);
            return View();
        }

        public ActionResult AmountSearch(decimal amount1, decimal amount2)
        {
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.FindBy(amount1, amount2);
            return View();
        }

        public ActionResult ByDate(DateTime date1, DateTime date2)
        {
            IEnumerable<DONATION> Donation1 = (IEnumerable<DONATION>)dnRepo.FindBy(date1, date2);

            return View();
        }
    }
}