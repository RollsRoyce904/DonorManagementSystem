using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using testDMS.Models;

namespace testDMS.Controllers
{
    public class DONATIONsController : Controller
    {
        private DonorManagementDatabaseEntities db = new DonorManagementDatabaseEntities();

        // GET: DONATIONs
        public ActionResult Index(string searchString)
        {
            var dONATIONs = db.Donation.Include(d => d.CODE).Include(d => d.DONOR);

            if (!String.IsNullOrEmpty(searchString))
            {
                dONATIONs = dONATIONs.Where(d => d.DONOR.FNAME.Contains(searchString) || d.DONOR.LNAME.Contains(searchString));
            }
            return View(dONATIONs.ToList());
        }


        // GET: DONATIONs/Details/5
        public ActionResult Details(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONATION dONATION = db.Donation.Find(ida, idb);
            if (dONATION == null)
            {
                return HttpNotFound();
            }
            return View(dONATION);
        }

        // GET: DONATIONs/Create
        public ActionResult Create()
        {
            ViewBag.CodeId = new SelectList(db.Code, "CodeId", "Fund");
            ViewBag.DonorId = new SelectList(db.Donor, "DONORID", "FNAME");
            return View();
        }

        // POST: DONATIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationId,DonorId,Amount,TypeOf,DateRecieved,GiftMethod,DateGiftMade,CodeId,ImageUpload,GiftRestrictions")] DONATION dONATION)
        {
            if (ModelState.IsValid)
            {
                db.Donation.Add(dONATION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeId = new SelectList(db.Code, "CodeId", "Fund", dONATION.CodeId);
            ViewBag.DonorId = new SelectList(db.Donor, "DONORID", "FNAME", dONATION.DonorId);
            return View(dONATION);
        }

        // GET: DONATIONs/Edit/5
        public ActionResult Edit(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONATION dONATION = db.Donation.Find(ida , idb);
            if (dONATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeId = new SelectList(db.Code, "CodeId", "Fund", dONATION.CodeId);
            ViewBag.DonorId = new SelectList(db.Donor, "DONORID", "FNAME", dONATION.DonorId);
            return View(dONATION);
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
                db.Entry(dONATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeId = new SelectList(db.Code, "CodeId", "Fund", dONATION.CodeId);
            ViewBag.DonorId = new SelectList(db.Donor, "DONORID", "FNAME", dONATION.DonorId);
            return View(dONATION);
        }

        // GET: DONATIONs/Delete/5
        public ActionResult Delete(int? ida, int? idb)
        {
            if (ida == null || idb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONATION dONATION = db.Donation.Find(ida, idb);
            if (dONATION == null)
            {
                return HttpNotFound();
            }
            return View(dONATION);
        }

        // POST: DONATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ida, int idb)
        {
            DONATION dONATION = db.Donation.Find(ida, idb);
            db.Donation.Remove(dONATION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
