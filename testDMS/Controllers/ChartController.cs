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

            ViewBag.Person = new SelectList(data.Donor, "DonorId", "FNAME");
            ViewBag.Department = new SelectList(data.Code, "CodeId", "Department");
            ViewBag.Gl = new SelectList(data.Code, "CodeId", "GL");

            return View(model);
        }


    }
}