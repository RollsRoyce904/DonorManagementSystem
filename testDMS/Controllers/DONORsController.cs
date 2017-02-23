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
    public class DONORsController : Controller
    {
        private DonorManagementDatabaseEntities data = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public DONORsController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        public ActionResult Index(string searchString)
        {
            if(searchString == null)
            {
                return View(drRepo.GetDonors());
            }
            else
            {
                IEnumerable<DONOR> donor = (IEnumerable<DONOR>)drRepo.FindBy(searchString);
                return View(donor);
            }
            
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DONOR donor = drRepo.FindById(Convert.ToInt32(id));

            if (donor == null)
            {
                return HttpNotFound();
            }

            ViewBag.COMPANYID = new SelectList(data.Company, "COMPANYID", "COMPANYNAME", donor.COMPANYID);
            ViewBag.CONTACTID = new SelectList(data.Contact, "CONTACTID", "TYPEOF", donor.CONTACTID);
            ViewBag.MARKERID = new SelectList(data.IdentityMarker, "MARKERID", "MARKERTYPE", donor.MARKERID);

            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DONORID,FNAME,MINIT,LNAME,TITLE,SUFFIX,EMAIL,CELL,BIRTHDAY,GENDER,MARKERID,CONTACTID,COMPANYID")] DONOR donor)
        {
            if (ModelState.IsValid)
            {
                drRepo.Edit(donor);
                return RedirectToAction("Index");
            }
            ViewBag.COMPANYID = new SelectList(data.Company, "COMPANYID", "COMPANYNAME", donor.COMPANYID);
            ViewBag.CONTACTID = new SelectList(data.Contact, "CONTACTID", "TYPEOF", donor.CONTACTID);
            ViewBag.MARKERID = new SelectList(data.IdentityMarker, "MARKERID", "MARKERTYPE", donor.MARKERID);
            return View(donor);
        }

        public ActionResult Details(int? id)
        {
            DisplayDataViewModel displayData = new DisplayDataViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            displayData.Donors = drRepo.FindById(Convert.ToInt32(id));
            
            IEnumerable<DONATION> donation = (IEnumerable<DONATION>)dnRepo.GetDonations();

            displayData.Donations = (from d in donation
                                 where d.DonorId == displayData.Donors.DONORID
                                 select d);

            if (displayData.Donors == null)
            {
                return HttpNotFound();
            }
            return View(displayData);
        }


        public ActionResult Create()
        {
            ViewBag.COMPANYID = new SelectList(data.Company, "COMPANYID", "COMPANYNAME");
            ViewBag.CONTACTID = new SelectList(data.Contact, "CONTACTID", "TYPEOF");
            ViewBag.MARKERID = new SelectList(data.IdentityMarker, "MARKERID", "MARKERTYPE");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DONORID,FNAME,MINIT,LNAME,TITLE,SUFFIX,EMAIL,CELL,BIRTHDAY,GENDER,MARKERID,CONTACTID,COMPANYID")] DONOR donor)
        {
            if (ModelState.IsValid)
            {
                drRepo.Add(donor);
                return RedirectToAction("Index");
            }

            ViewBag.COMPANYID = new SelectList(data.Company, "COMPANYID", "COMPANYNAME", donor.COMPANYID);
            ViewBag.CONTACTID = new SelectList(data.Contact, "CONTACTID", "TYPEOF", donor.CONTACTID);
            ViewBag.MARKERID = new SelectList(data.IdentityMarker, "MARKERID", "MARKERTYPE", donor.MARKERID);
            return View(donor);
        }

        // GET: DOnor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONOR donor = drRepo.FindById(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: DONor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            drRepo.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult AddNote()
        {

            return View();
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
