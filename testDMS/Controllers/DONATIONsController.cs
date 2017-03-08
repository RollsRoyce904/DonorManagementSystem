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
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            ViewBag.DonorId = new SelectList(data.DONOR, "DONORID", "CompanyName");
            ViewBag.Grants = new SelectList(grants);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDonationViewModel CDVM, HttpPostedFileBase image = null)
        {
            DONATION donation = CDVM.donation;
            
            if (ModelState.IsValid)
            {
                
                    //donation.ImageMimeType = image.ContentType;
                    //donation.ImageUpload = new byte[image.ContentLength];
                    //image.InputStream.Read(donation.ImageUpload, 0, image.ContentLength);
               
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
        public ActionResult Edit([Bind(Include = "DonationId,DonorId,Amount,TypeOf,DateRecieved,GiftMethod,DateGiftMade,CodeId,ImageUpload,GiftRestrictions")] DONATION dONATION, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    dONATION.ImageMimeType = image.ContentType;
                    dONATION.ImageUpload = new byte[image.ContentLength];
                    image.InputStream.Read(dONATION.ImageUpload, 0, image.ContentLength);
                }
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

        public FileContentResult GetImage(int donationId, int donorId)
        {
            DONATION donation = dnRepo.FindById(donationId, donorId);
            if(donation != null)
            {
                return File(donation.ImageUpload, donation.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Upload(string ActionName)
        {
            var path = Server.MapPath("~/App_Data/Files");
            foreach(string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item];
                if(file.ContentLength == 0)
                {
                    continue;
                }
                string savedFileName = Path.Combine(path, Path.GetFileName(file.FileName));
                file.SaveAs(savedFileName);
            }
            return RedirectToAction(ActionName);
        }

        public ActionResult ExportToExcel()
        {
            // Step 1 - get the data from database
            var myData = data.DONATION.ToList();

            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView();
            gridview.DataSource = myData;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            // set the header
            Response.AddHeader("content-disposition", "attachment; filename = Donations.xls");
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
