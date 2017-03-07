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

            ViewBag.CONTACTID = new SelectList(data.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(data.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);

            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonorId,FName,Init,LName,Title,Suffix,Email,Cell,Birthday,Gender,MarkerId,ContactId,CompanyName,Address,City,State,Zipcode,Phone")] DONOR donor)
        {
            if (ModelState.IsValid)
            {
                drRepo.SaveProduct(donor);
                return RedirectToAction("Index");
            }
            ViewBag.CONTACTID = new SelectList(data.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(data.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);
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
                                 where d.DonorId == displayData.Donors.DonorId
                                 select d);

            if (displayData.Donors == null)
            {
                return HttpNotFound();
            }
            return View(displayData);
        }

        //[HttpPost]---------Method to take in the new note for the donor
        //public ActionResult Details(int id, string notes)
        //{

        //    return View();
        //}


        public ActionResult Create()
        {
            ViewBag.CONTACTID = new SelectList(data.CONTACT, "CONTACTID", "TYPEOF");
            ViewBag.MARKERID = new SelectList(data.IDENTITYMARKER, "MARKERID", "MARKERTYPE");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonorId,FName,Init,LName,Title,Suffix,Email,Cell,Birthday,Gender,MarkerId,ContactId,CompanyName,Address,City,State,Zipcode,Phone")] DONOR donor)
        {
            if (ModelState.IsValid)
            {
                DONOR myDonor = donor;
                if (myDonor.FName == null && myDonor.LName == null)
                {
                    myDonor.FName = myDonor.CompanyName;
                }
                drRepo.Add(donor);
                return RedirectToAction("Index");
            }
            
            ViewBag.CONTACTID = new SelectList(data.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(data.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);
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
