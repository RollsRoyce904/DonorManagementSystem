using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using testDMS.DAL;
using testDMS.Models;
using PagedList;

namespace testDMS.Controllers
{
    public class DONATIONsController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;

        public DONATIONsController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
        }

        // GET: DONATIONs
        public ActionResult Index(string searchString, string sortOrder, string dateMade, string dateRecieved, int? page)
        {
            int count = 0;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (searchString == null)
            {

                ViewBag.DonationSortParam = String.IsNullOrEmpty(sortOrder) ? "donationID_desc" : "";
                ViewBag.DateSortParam = sortOrder == "DateGiftRecieved" ? "dateRecieved_desc" : "DateGiftRecieved";

                var donations = from DONATION d in dnRepo.GetDonations()
                                select d;

                count = donations.Count();

                DonationViewModel dvm = new DonationViewModel();
                //dvm.Donations = donations.Take(count).ToPagedList(pageNumber, pageSize);

                switch (sortOrder)
                {
                    case "DonationID":
                        donations = donations.OrderBy(d => d.DonationId);
                        break;
                    case "DateGiftRecieved":
                        donations = donations.OrderBy(d => d.DateRecieved);
                        break;
                    case "dateRecieved_desc":
                        donations = donations.OrderByDescending(d => d.DateRecieved);
                        break;
                    default:
                        donations = donations.OrderByDescending(d => d.DonationId);
                        break;

                }
                
                return View(donations.ToList());
            }
            else
            {
                IEnumerable<DONATION> donation = (IEnumerable<DONATION>)dnRepo.FindBy(searchString);
                count = donation.Count();

                DonationViewModel dvm = new DonationViewModel();
                dvm.Donations = donation.Take(count).ToPagedList(pageNumber, pageSize);

                return View(dvm);
            }
        }

        // GET: DONATIONs/Details/5
        public ActionResult Details(int? ida, int? idb)
        {
            //if both the donor and donation id = 0 return a bad request message
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

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FName");

            List<string> TypeOf = new List<string>();

            TypeOf.Add("Pledge");
            TypeOf.Add("Cash");
            TypeOf.Add("Bequest");

            ViewBag.TypeOf = new SelectList(TypeOf, "TypeOf");

            List<string> GiftMethod = new List<string>();

            GiftMethod.Add("Check");
            GiftMethod.Add("ACH Transfer");
            GiftMethod.Add("Credit Card");
            GiftMethod.Add("Cash");

            ViewBag.GiftMethod = new SelectList(GiftMethod, "GiftMethod");

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDonationViewModel CDVM, HttpPostedFileBase image = null)
        {
            DONATION donation = CDVM.donation;
            //CODES code = CDVM.code;


            if (ModelState.IsValid)
            {

                //donation.ImageMimeType = image.ContentType;
                //donation.ImageUpload = new byte[image.ContentLength];
                //image.InputStream.Read(donation.ImageUpload, 0, image.ContentLength);

                //cdRepo.Add(code);
                dnRepo.Add(donation);

                return RedirectToAction("Index");
            }



            //ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund", donation.CodeId);

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");

            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");
            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");


            return View(donation);
        }

        public ActionResult AddDonation(int id)
        {
            DONOR donor = drRepo.FindById(id);

            CreateDonationViewModel cdvm = new CreateDonationViewModel();
            cdvm.donor = donor;

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");

            return View("~/Views/DONATIONs/DonorCreate.cshtml", cdvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDonation(CreateDonationViewModel CDVM, int id, HttpPostedFileBase image = null)
        {
            DONATION donation = CDVM.donation;
            //DONOR donor = CDVM.donor;
            donation.DonorId = id;

            if (ModelState.IsValid)
            {

                //donation.ImageMimeType = image.ContentType;
                //donation.ImageUpload = new byte[image.ContentLength];
                //image.InputStream.Read(donation.ImageUpload, 0, image.ContentLength);

                dnRepo.Add(donation);
                return RedirectToAction("Details", "DONORs", new { id });
            }

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");


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

            List<string> TypeOf = new List<string>();

            TypeOf.Add("Pledge");
            TypeOf.Add("Cash");
            TypeOf.Add("Bequest");

            ViewBag.TypeOf = new SelectList(TypeOf, "TypeOf");

            List<string> GiftMethod = new List<string>();

            GiftMethod.Add("Check");
            GiftMethod.Add("ACH Transfer");
            GiftMethod.Add("Credit Card");
            GiftMethod.Add("Cash");

            ViewBag.GiftMethod = new SelectList(GiftMethod, "GiftMethod");

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");


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

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "FundID", "Fund");

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", dONATION.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GLID", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "DepartmentID", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "ProgramID", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantID", "GrantName");


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

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");
            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");

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
                ddlData.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileContentResult GetImage(int donationId, int donorId)
        {
            DONATION donation = dnRepo.FindById(donationId, donorId);

            if (donation != null)
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

            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item];

                if (file.ContentLength == 0)
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
            var myData = ddlData.DONATION.ToList();

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
