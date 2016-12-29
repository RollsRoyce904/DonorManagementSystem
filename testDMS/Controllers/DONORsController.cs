using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testDMS.Models;

namespace testDMS.Controllers
{
    public class DONORsController : Controller
    {
        private DonorManagementDatabaseEntities db = new DonorManagementDatabaseEntities();

        // GET: DONORs[ValidateAntiForgeryToken]
        
        public ActionResult Index(string searchString)
        {
            var dONORs = db.DONORs.Include(d => d.COMPANY).Include(d => d.CONTACT).Include(d => d.IDENTITYMARKER);

            if (!String.IsNullOrEmpty(searchString))
            {
                dONORs = dONORs.Where(s => s.LNAME.Contains(searchString));
            }

            return View(dONORs.ToList());
        }

        // GET: DONORs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONOR dONOR = db.DONORs.Find(id);
            if (dONOR == null)
            {
                return HttpNotFound();
            }
            return View(dONOR);
        }

        // GET: DONORs/Create
        public ActionResult Create()
        {
            ViewBag.COMPANYID = new SelectList(db.COMPANies, "COMPANYID", "COMPANYNAME");
            ViewBag.CONTACTID = new SelectList(db.CONTACTs, "CONTACTID", "TYPEOF");
            ViewBag.MARKERID = new SelectList(db.IDENTITYMARKERs, "MARKERID", "MARKERTYPE");
            return View();
        }

        // POST: DONORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DONORID,FNAME,MINIT,LNAME,TITLE,SUFFIX,EMAIL,CELL,BIRTHDAY,GENDER,MARKERID,CONTACTID,COMPANYID")] DONOR dONOR)
        {
            if (ModelState.IsValid)
            {
               
                db.DONORs.Add(dONOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COMPANYID = new SelectList(db.COMPANies, "COMPANYID", "COMPANYNAME", dONOR.COMPANYID);
            ViewBag.CONTACTID = new SelectList(db.CONTACTs, "CONTACTID", "TYPEOF", dONOR.CONTACTID);
            ViewBag.MARKERID = new SelectList(db.IDENTITYMARKERs, "MARKERID", "MARKERTYPE", dONOR.MARKERID);
            return View(dONOR);
        }

        // GET: DONORs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONOR dONOR = db.DONORs.Find(id);
            if (dONOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.COMPANYID = new SelectList(db.COMPANies, "COMPANYID", "COMPANYNAME", dONOR.COMPANYID);
            ViewBag.CONTACTID = new SelectList(db.CONTACTs, "CONTACTID", "TYPEOF", dONOR.CONTACTID);
            ViewBag.MARKERID = new SelectList(db.IDENTITYMARKERs, "MARKERID", "MARKERTYPE", dONOR.MARKERID);
            return View(dONOR);
        }

        // POST: DONORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DONORID,FNAME,MINIT,LNAME,TITLE,SUFFIX,EMAIL,CELL,BIRTHDAY,GENDER,MARKERID,CONTACTID,COMPANYID")] DONOR dONOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COMPANYID = new SelectList(db.COMPANies, "COMPANYID", "COMPANYNAME", dONOR.COMPANYID);
            ViewBag.CONTACTID = new SelectList(db.CONTACTs, "CONTACTID", "TYPEOF", dONOR.CONTACTID);
            ViewBag.MARKERID = new SelectList(db.IDENTITYMARKERs, "MARKERID", "MARKERTYPE", dONOR.MARKERID);
            return View(dONOR);
        }

        // GET: DONORs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONOR dONOR = db.DONORs.Find(id);
            if (dONOR == null)
            {
                return HttpNotFound();
            }
            return View(dONOR);
        }

        // POST: DONORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONOR dONOR = db.DONORs.Find(id);
            db.DONORs.Remove(dONOR);
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
