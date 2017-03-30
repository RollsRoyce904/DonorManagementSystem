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
    public class CODELISTsController : Controller
    {
        private DonorManagementDatabaseEntities db = new DonorManagementDatabaseEntities();

        // GET: CODELISTs
        public ActionResult Index()
        {
            return View(db.CODELIST.ToList());
        }

        // GET: CODELISTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CODELIST cODELIST = db.CODELIST.Find(id);
            if (cODELIST == null)
            {
                return HttpNotFound();
            }
            return View(cODELIST);
        }

        // GET: CODELISTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CODELISTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeId,Fund,GL,Department,Program,Grants,Appeal")] CODELIST cODELIST)
        {
            if (ModelState.IsValid)
            {
                db.CODELIST.Add(cODELIST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cODELIST);
        }

        // GET: CODELISTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CODELIST cODELIST = db.CODELIST.Find(id);
            if (cODELIST == null)
            {
                return HttpNotFound();
            }
            return View(cODELIST);
        }

        // POST: CODELISTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeId,Fund,GL,Department,Program,Grants,Appeal")] CODELIST cODELIST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cODELIST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cODELIST);
        }

        // GET: CODELISTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CODELIST cODELIST = db.CODELIST.Find(id);
            if (cODELIST == null)
            {
                return HttpNotFound();
            }
            return View(cODELIST);
        }

        // POST: CODELISTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CODELIST cODELIST = db.CODELIST.Find(id);
            db.CODELIST.Remove(cODELIST);
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
