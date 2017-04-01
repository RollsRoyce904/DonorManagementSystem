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

namespace testDMS.Controllers
{
    public class DONATIONsController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;
        ICodeRepository cdRepo;

        public DONATIONsController(IDonorRepository drRepo, IDonationRepository dnRepo, ICodeRepository cdRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
            this.cdRepo = cdRepo;
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
                dvm.Donations = donations.Take(count).ToPagedList(pageNumber, pageSize);
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
                return View(dvm);
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
            ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund");

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

            List<string> Fund = new List<string>();
            Fund.Add("01");
            Fund.Add("02");
            Fund.Add("03");

            ViewBag.Fund = new SelectList(Fund, "Funds");

            List<string> GL = new List<string>();
            GL.Add("4110");
            GL.Add("4120");
            GL.Add("4130");
            GL.Add("4135");
            GL.Add("4140");
            GL.Add("4200");
            GL.Add("4201");
            GL.Add("4202");
            GL.Add("4310");
            GL.Add("4320");
            GL.Add("4330");
            GL.Add("4340");
            GL.Add("4400");
            GL.Add("4500");

            ViewBag.GL = new SelectList(GL, "GLCode");

            List<string> Department = new List<string>();
            Department.Add("01");
            Department.Add("02");
            Department.Add("03");
            Department.Add("04");
            Department.Add("05");
            Department.Add("06");
            Department.Add("07");
            Department.Add("08");
            Department.Add("09");
            Department.Add("10");
            Department.Add("11");
            Department.Add("12");
            Department.Add("13");
            Department.Add("14");
            Department.Add("15");

            ViewBag.Department = new SelectList(Department, "Department");

            List<string> Program = new List<string>();
            Program.Add("MED");
            Program.Add("PSYCH");
            Program.Add("CFID");
            Program.Add("EDUC");
            Program.Add("VPK");
            Program.Add("KIND");
            Program.Add("DS");
            Program.Add("ABA");
            Program.Add("SPEECH");
            Program.Add("OT & PT");
            Program.Add("TUTOR");
            Program.Add("TUTORC");
            Program.Add("FAC");
            Program.Add("IT");
            Program.Add("FD");
            Program.Add("MARKET");
            Program.Add("BO");
            Program.Add("BASICS");
            Program.Add("TEAM UP");
            Program.Add("WEBB");
            Program.Add("MGMT");
            Program.Add("FAAST");
            Program.Add("PROJSRCH");
            Program.Add("FIN");
            Program.Add("HR");
            Program.Add("INTAKE");
            Program.Add("CS");

            ViewBag.Program = new SelectList(Program, "Program");

            List<string> Grant = new List<string>();
            Grant.Add("ABLE");
            Grant.Add("AETNA FDN");
            Grant.Add("ALFRED DUPONT");
            Grant.Add("BANCROFT");
            Grant.Add("BAPTIST");
            Grant.Add("BOA-PROJSRCH");
            Grant.Add("CEO");
            Grant.Add("CF-CHARTRAND");
            Grant.Add("CF-KIND");
            Grant.Add("CF-RIVERSIDE");
            Grant.Add("CF-WEAVER");
            Grant.Add("DEERWOOD");
            Grant.Add("DOLLAR");
            Grant.Add("ELC");
            Grant.Add("FAAST");
            Grant.Add("FRYE FDN");
            Grant.Add("GOODING CTR");
            Grant.Add("HEAL");
            Grant.Add("HOLLAND");
            Grant.Add("HOPE");
            Grant.Add("HORACE MAX");
            Grant.Add("JAGUAR FDN");
            Grant.Add("JCA");
            Grant.Add("JCC");
            Grant.Add("JESSIE BALL");
            Grant.Add("KIRBO");
            Grant.Add("KESLER");
            Grant.Add("LEVY FDN");
            Grant.Add("LUCY GOODING");
            Grant.Add("NETWORK");
            Grant.Add("SELDERS");
            Grant.Add("STELLAR");
            Grant.Add("TD BANK");
            Grant.Add("TILTON");
            Grant.Add("TRUIST");
            Grant.Add("TURCK");
            Grant.Add("UNITED WAY");
            Grant.Add("WELLS FARGO");

            ViewBag.Grant = new SelectList(Grant, "Grant");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDonationViewModel CDVM, HttpPostedFileBase image = null)
        {
            DONATION donation = CDVM.donation;
            CODES code = CDVM.code;


            if (ModelState.IsValid)
            {

                //donation.ImageMimeType = image.ContentType;
                //donation.ImageUpload = new byte[image.ContentLength];
                //image.InputStream.Read(donation.ImageUpload, 0, image.ContentLength);

                cdRepo.Add(code);
                dnRepo.Add(donation);

                return RedirectToAction("Index");
            }



            //ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund", donation.CodeId);
            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);
            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");
            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");
            ViewBag.Fund = new SelectList(ddlData.CODES, "Fund");
            ViewBag.GL = new SelectList(ddlData.CODES, "GL");
            ViewBag.Department = new SelectList(ddlData.CODES, "Department");
            ViewBag.Program = new SelectList(ddlData.CODES, "Program");
            ViewBag.Grant = new SelectList(ddlData.CODES, "Grant");

            return View(donation);
        }

        public ActionResult AddDonation(int id)
        {

            //ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund");

            DONOR donor = drRepo.FindById(id);

            CreateDonationViewModel cdvm = new CreateDonationViewModel();
            cdvm.donor = donor;

            List<string> Fund = new List<string>();
            Fund.Add("01");
            Fund.Add("02");
            Fund.Add("03");

            ViewBag.Fund = new SelectList(Fund, "Funds");

            List<string> GL = new List<string>();
            GL.Add("4110");
            GL.Add("4120");
            GL.Add("4130");
            GL.Add("4135");
            GL.Add("4140");
            GL.Add("4200");
            GL.Add("4201");
            GL.Add("4202");
            GL.Add("4310");
            GL.Add("4320");
            GL.Add("4330");
            GL.Add("4340");
            GL.Add("4400");
            GL.Add("4500");

            ViewBag.GL = new SelectList(GL, "GLCode");

            List<string> Department = new List<string>();
            Department.Add("01");
            Department.Add("02");
            Department.Add("03");
            Department.Add("04");
            Department.Add("05");
            Department.Add("06");
            Department.Add("07");
            Department.Add("08");
            Department.Add("09");
            Department.Add("10");
            Department.Add("11");
            Department.Add("12");
            Department.Add("13");
            Department.Add("14");
            Department.Add("15");

            ViewBag.Department = new SelectList(Department, "Department");

            List<string> Program = new List<string>();
            Program.Add("MED");
            Program.Add("PSYCH");
            Program.Add("CFID");
            Program.Add("EDUC");
            Program.Add("VPK");
            Program.Add("KIND");
            Program.Add("DS");
            Program.Add("ABA");
            Program.Add("SPEECH");
            Program.Add("OT & PT");
            Program.Add("TUTOR");
            Program.Add("TUTORC");
            Program.Add("FAC");
            Program.Add("IT");
            Program.Add("FD");
            Program.Add("MARKET");
            Program.Add("BO");
            Program.Add("BASICS");
            Program.Add("TEAM UP");
            Program.Add("WEBB");
            Program.Add("MGMT");
            Program.Add("FAAST");
            Program.Add("PROJSRCH");
            Program.Add("FIN");
            Program.Add("HR");
            Program.Add("INTAKE");
            Program.Add("CS");

            ViewBag.Program = new SelectList(Program, "Program");

            List<string> Grant = new List<string>();
            Grant.Add("ABLE");
            Grant.Add("AETNA FDN");
            Grant.Add("ALFRED DUPONT");
            Grant.Add("BANCROFT");
            Grant.Add("BAPTIST");
            Grant.Add("BOA-PROJSRCH");
            Grant.Add("CEO");
            Grant.Add("CF-CHARTRAND");
            Grant.Add("CF-KIND");
            Grant.Add("CF-RIVERSIDE");
            Grant.Add("CF-WEAVER");
            Grant.Add("DEERWOOD");
            Grant.Add("DOLLAR");
            Grant.Add("ELC");
            Grant.Add("FAAST");
            Grant.Add("FRYE FDN");
            Grant.Add("GOODING CTR");
            Grant.Add("HEAL");
            Grant.Add("HOLLAND");
            Grant.Add("HOPE");
            Grant.Add("HORACE MAX");
            Grant.Add("JAGUAR FDN");
            Grant.Add("JCA");
            Grant.Add("JCC");
            Grant.Add("JESSIE BALL");
            Grant.Add("KIRBO");
            Grant.Add("KESLER");
            Grant.Add("LEVY FDN");
            Grant.Add("LUCY GOODING");
            Grant.Add("NETWORK");
            Grant.Add("SELDERS");
            Grant.Add("STELLAR");
            Grant.Add("TD BANK");
            Grant.Add("TILTON");
            Grant.Add("TRUIST");
            Grant.Add("TURCK");
            Grant.Add("UNITED WAY");
            Grant.Add("WELLS FARGO");

            ViewBag.Grant = new SelectList(Grant, "Grant");


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
                return RedirectToAction("Details","DONORs",new {id});
            }



            //ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund", donation.CodeId);
            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);
            ViewBag.Fund = new SelectList(ddlData.CODES, "Fund");
            ViewBag.GL = new SelectList(ddlData.CODES, "GL");
            ViewBag.Department = new SelectList(ddlData.CODES, "Department");
            ViewBag.Program = new SelectList(ddlData.CODES, "Program");
            ViewBag.Grant = new SelectList(ddlData.CODES, "Grant");

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

            //ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund", donation.CodeId);
            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

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

            // ViewBag.CodeId = new SelectList(ddlData.CODES, "CodeId", "Fund", dONATION.CodeId);
            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", dONATION.DonorId);

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
