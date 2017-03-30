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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;

namespace testDMS.Controllers
{
    public class DONORsController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public DONORsController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int count = 0;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            IEnumerable<DONOR> donor = new List<DONOR>();

            if (searchString != null)
            {
                page = 1;
                donor = (IEnumerable<DONOR>)drRepo.FindBy(searchString);
            }
            else
            {
                searchString = currentFilter;
                donor = drRepo.GetDonors;
            }

            ViewBag.CurrentFilter = searchString;

            count = drRepo.GetDonors.Count();

            DonorViewModel DonorList = new DonorViewModel
            {
                Donors = donor.Take(count).ToPagedList(pageNumber, pageSize)
            };

          
 
             return View(DonorList);
            
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

            ViewBag.CONTACTID = new SelectList(ddlData.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(ddlData.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);

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
            ViewBag.CONTACTID = new SelectList(ddlData.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(ddlData.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);
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
            ViewBag.CONTACTID = new SelectList(ddlData.CONTACT, "CONTACTID", "TYPEOF");
            ViewBag.MARKERID = new SelectList(ddlData.IDENTITYMARKER, "MARKERID", "MARKERTYPE");
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
            
            ViewBag.CONTACTID = new SelectList(ddlData.CONTACT, "CONTACTID", "TYPEOF", donor.ContactId);
            ViewBag.MARKERID = new SelectList(ddlData.IDENTITYMARKER, "MARKERID", "MARKERTYPE", donor.MarkerId);
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
                ddlData.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ExportToExcel()
        {
            // Step 1 - get the data from database
            var myData = ddlData.DONOR.ToList();

            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView();
            gridview.DataSource = myData;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            // set the header
            Response.AddHeader("content-disposition", "attachment; filename = Donors.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }
    }
}
