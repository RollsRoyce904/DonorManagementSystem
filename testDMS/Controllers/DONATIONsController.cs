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
    public class DONATIONsController : Controller
    {
        private DonorManagementDatabaseEntities data = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public DONATIONsController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        // GET: DONATIONs
        public ActionResult Index(string searchString)
        {
            if(searchString == null)
            {
                return View(dnRepo.GetDonations());
            }
            else
            {
                IEnumerable<DONATION> donations = (IEnumerable<DONATION>)dnRepo.FindBy(searchString);
                return View(donations);
            }   
        }

        // GET: DONATIONs/Details/5
        public ActionResult Details(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONATION donation = dnRepo.FindById(ida, idb);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: DONATIONs/Create
        public ActionResult Create()
        {
            List<string> grants = new List<string>();
            grants.Add("No");
            grants.Add("Yes");
            ViewBag.CodeId = new SelectList(data.CODES, "CodeId", "Fund");
            ViewBag.DonorId = new SelectList(data.DONOR, "DONORID", "FNAME");
            ViewBag.Grants = new SelectList(grants);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDonationViewModel CDVM)
        {
            DONATION donation = CDVM.donation;

            if (ModelState.IsValid)
            {
                dnRepo.Add(donation);
                return RedirectToAction("Index");
            }

            ViewBag.CodeId = new SelectList(data.CODES, "CodeId", "Fund", donation.CodeId);
            ViewBag.DonorId = new SelectList(data.DONOR, "DONORID", "FNAME", donation.DonorId);

            return View(donation);
        }

        // GET: DONATIONs/Edit/5
        public ActionResult Edit(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DONATION donation = dnRepo.FindById(ida, idb);

            if (donation == null)
            {
                return HttpNotFound();
            }

            ViewBag.CodeId = new SelectList(data.CODES, "CodeId", "Fund", donation.CodeId);
            ViewBag.DonorId = new SelectList(data.DONOR, "DONORID", "FNAME", donation.DonorId);

            return View(donation);
        }

        // POST: DONATIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationId,DonorId,Amount,TypeOf,DateRecieved,GiftMethod,DateGiftMade,CodeId,ImageUpload,GiftRestrictions")] DONATION dONATION)
        {
            if (ModelState.IsValid)
            {
                dnRepo.SaveDonation(dONATION);
                return RedirectToAction("Index");
            }

            ViewBag.CodeId = new SelectList(data.CODES, "CodeId", "Fund", dONATION.CodeId);
            ViewBag.DonorId = new SelectList(data.DONOR, "DONORID", "FNAME", dONATION.DonorId);

            return View(dONATION);
        }

        // GET: DONATIONs/Delete/5
        public ActionResult Delete(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DONATION donation = dnRepo.FindById(ida, idb);

            if (donation == null)
            {
                return HttpNotFound();
            }

            return View(donation);
        }

        // POST: DONATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ida, int idb)
        {
             dnRepo.Remove(ida, idb);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                data.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
