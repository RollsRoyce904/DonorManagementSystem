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
using Microsoft.AspNet.Identity;

namespace testDMS.Controllers
{
    public class DONATIONsController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;
        private DonorManagementDatabaseEntities data = new DonorManagementDatabaseEntities();

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
            ViewBag.CurrentSort = sortOrder;

            if (searchString == null)
            {

                ViewBag.DonationSortParam = String.IsNullOrEmpty(sortOrder) ? "donationID_desc" : "";
                ViewBag.DateSortParam = sortOrder == "DateGiftRecieved" ? "dateRecieved_desc" : "DateGiftRecieved";

                var donations = from DONATION d in dnRepo.GetDonations()
                                select d;

                count = donations.Count();
                //used to remove features for non-admin users.
                string userId = User.Identity.GetUserId();
                var user = data.AspNetUsers.FirstOrDefault(p => p.Id == userId);
                ViewBag.role = user.NewRole;

                DonationViewModel dvm = new DonationViewModel();
                //dvm.Donations = donations.Take(count).ToPagedList(pageNumber, pageSize);

                switch (sortOrder)
                {
                    case "donationID_desc":
                        donations = donations.OrderByDescending(d => d.DONOR.FName);
                        break;
                    case "DateGiftRecieved":
                        donations = donations.OrderBy(d => d.DateRecieved);
                        break;
                    case "dateRecieved_desc":
                        donations = donations.OrderByDescending(d => d.DateRecieved);
                        break;
                    default:
                        donations = donations.OrderBy(d => d.DONOR.FName);
                        break;

                }
                return View(donations.Take(count).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                IEnumerable<DONATION> donation = (IEnumerable<DONATION>)dnRepo.FindBy(searchString);
                count = donation.Count();
                
                return View(donation.Take(count).ToPagedList(pageNumber, pageSize));
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
            
            List<SelectListItem> TypeOf = new List<SelectListItem>();
            TypeOf.Add(new SelectListItem { Text = "Pledge", Value = "Pledge", Selected = true });
            TypeOf.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            TypeOf.Add(new SelectListItem { Text = "Bequest", Value = "Bequest" });

            ViewBag.TypeOf = TypeOf;

            List<SelectListItem> GiftMethod = new List<SelectListItem>();
            GiftMethod.Add(new SelectListItem { Text = "Check", Value = "Check", Selected = true });
            GiftMethod.Add(new SelectListItem { Text = "ACH Transfer", Value = "ACH Transfer" });
            GiftMethod.Add(new SelectListItem { Text = "Credit Card", Value = "Credit Card" });
            GiftMethod.Add(new SelectListItem { Text = "Cash", Value = "Cash" });

            ViewBag.GiftMethod = GiftMethod;

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DONATION myDonation, IEnumerable<HttpPostedFileBase> image)
        {
            DONATION donation = myDonation;
            
            if (ModelState.IsValid)
            {
                foreach (var item in image)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        var check = new FILES
                        {
                            FileName = System.IO.Path.GetFileName(item.FileName),
                            ContentType = item.ContentType,
                            DonationId = donation.DonationId,
                            DonorId = donation.DonorId
                        };

                        using (var reader = new System.IO.BinaryReader(item.InputStream))
                        {
                            check.Content = reader.ReadBytes(item.ContentLength);
                        }

                        donation.FILES = new List<FILES> { check };
                    }
                }
                
                dnRepo.Add(donation);

                return RedirectToAction("Index");
            }

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");

            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");

            return View(donation);
        }

        public ActionResult AddDonation(int id)
        {
            DONOR donor = drRepo.FindById(id);

            CreateDonationViewModel cdvm = new CreateDonationViewModel();
            cdvm.donor = donor;

            List<SelectListItem> TypeOf = new List<SelectListItem>();

            TypeOf.Add(new SelectListItem { Text = "Pledge", Value = "Pledge", Selected = true });
            TypeOf.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            TypeOf.Add(new SelectListItem { Text = "Bequest", Value = "Bequest" });

            ViewBag.TypeOf = TypeOf;

            List<SelectListItem> GiftMethod = new List<SelectListItem>();

            GiftMethod.Add(new SelectListItem { Text = "Check", Value = "Check", Selected = true });
            GiftMethod.Add(new SelectListItem { Text = "ACH Transfer", Value = "ACH Transfer" });
            GiftMethod.Add(new SelectListItem { Text = "Credit Card", Value = "Credit Card" });
            GiftMethod.Add(new SelectListItem { Text = "Cash", Value = "Cash" });

            ViewBag.GiftMethod = GiftMethod;

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");

            return View("~/Views/DONATIONs/DonorCreate.cshtml", cdvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDonation(CreateDonationViewModel CDVM, int id, HttpPostedFileBase image)
        {
            DONATION donation = CDVM.donation;
            //DONOR donor = CDVM.donor;
            donation.DonorId = id;

            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0 )
                {
                    var check = new FILES
                    {
                        FileName = System.IO.Path.GetFileName(image.FileName),
                        ContentType = image.ContentType
                    };
                }
                //donation.ImageMimeType = image.ContentType;
                //donation.ImageUpload = new byte[image.ContentLength];
                //image.InputStream.Read(donation.ImageUpload, 0, image.ContentLength);

                dnRepo.Add(donation);
                return RedirectToAction("Details", "DONORs", new { id });
            }

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");

            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");


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

            List<SelectListItem> TypeOf = new List<SelectListItem>();

            TypeOf.Add(new SelectListItem { Text = "Pledge", Value = "Pledge", Selected = true });
            TypeOf.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            TypeOf.Add(new SelectListItem { Text = "Bequest", Value = "Bequest" });

            ViewBag.TypeOf = TypeOf;

            List<SelectListItem> GiftMethod = new List<SelectListItem>();

            GiftMethod.Add(new SelectListItem { Text = "Check", Value = "Check", Selected = true });
            GiftMethod.Add(new SelectListItem { Text = "ACH Transfer", Value = "ACH Transfer" });
            GiftMethod.Add(new SelectListItem { Text = "Credit Card", Value = "Credit Card" });
            GiftMethod.Add(new SelectListItem { Text = "Cash", Value = "Cash" });

            ViewBag.GiftMethod = GiftMethod;

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund", donation.Fund);

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL", donation.GL);

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department", donation.Department);

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program", donation.Program);

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName", donation.GrantS);


            return View(donation);
        }

        // POST: DONATIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationId,DonorId,Amount,TypeOf,DateRecieved,GiftMethod,DateGiftMade,GiftRestrictions,Notes,Fund,GL,Department,Program,GrantS,Appeal")] DONATION dONATION, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var check = new FILES
                    {
                        FileName = System.IO.Path.GetFileName(image.FileName),
                        ContentType = image.ContentType,
                        DonationId = dONATION.DonationId,
                        DonorId = dONATION.DonorId
                    };

                    using (var reader = new System.IO.BinaryReader(image.InputStream))
                    {
                        check.Content = reader.ReadBytes(image.ContentLength);
                    }

                    dONATION.FILES.Add(check);
                }

                dnRepo.SaveDonation(dONATION);

                return RedirectToAction("Index");
            }

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");

            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", dONATION.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");


            return View(dONATION);
        }

        public ActionResult EditDonation(int? ida, int? idb)
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

            List<SelectListItem> TypeOf = new List<SelectListItem>();

            TypeOf.Add(new SelectListItem { Text = "Pledge", Value = "Pledge", Selected = true });
            TypeOf.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            TypeOf.Add(new SelectListItem { Text = "Bequest", Value = "Bequest" });

            ViewBag.TypeOf = TypeOf;

            List<SelectListItem> GiftMethod = new List<SelectListItem>();

            GiftMethod.Add(new SelectListItem { Text = "Check", Value = "Check", Selected = true });
            GiftMethod.Add(new SelectListItem { Text = "ACH Transfer", Value = "ACH Transfer" });
            GiftMethod.Add(new SelectListItem { Text = "Credit Card", Value = "Credit Card" });
            GiftMethod.Add(new SelectListItem { Text = "Cash", Value = "Cash" });

            ViewBag.GiftMethod = GiftMethod;

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund", donation.Fund);

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", donation.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL", donation.GL);

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department", donation.Department);

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program", donation.Program);

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName", donation.GrantS);


            return View("~/Views/DONATIONs/DonorEdit.cshtml",donation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDonation([Bind(Include = "DonationId,DonorId,Amount,TypeOf,DateRecieved,GiftMethod,DateGiftMade,CodeId,ImageUpload,GiftRestrictions")] DONATION dONATION, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //dONATION.ImageMimeType = image.ContentType;
                    //dONATION.ImageUpload = new byte[image.ContentLength];
                    //image.InputStream.Read(dONATION.ImageUpload, 0, image.ContentLength);
                }
                dnRepo.SaveDonation(dONATION);
                return RedirectToAction("Details", "DONORs", new {id = dONATION.DonorId });
            }

            ViewBag.TypeOf = new SelectList(ddlData.DONATION, "TypeOf");

            ViewBag.GiftMethod = new SelectList(ddlData.DONATION, "GiftMethod");

            ViewBag.Fund = new SelectList(ddlData.FUNDS, "Fund", "Fund");

            ViewBag.DonorId = new SelectList(ddlData.DONOR, "DONORID", "FNAME", dONATION.DonorId);

            ViewBag.GL = new SelectList(ddlData.GLS, "GL", "GL");

            ViewBag.Department = new SelectList(ddlData.DEPARTMENTS, "Department", "Department");

            ViewBag.Program = new SelectList(ddlData.PROGRAMS, "Program", "Program");

            ViewBag.Grant = new SelectList(ddlData.GRANTS, "GrantName", "GrantName");


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

        public FileContentResult GetImageOne(int donationId, int donorId)
        {
            DONATION donation = dnRepo.FindById(donationId, donorId);

            IEnumerable<FILES> file = donation.FILES;

            if(file.Count() > 0)
            {
                byte[] firstPhoto = file.ElementAtOrDefault(0).Content;
                return File(firstPhoto, "image/png");
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetImageTwo(int donationId, int donorId)
        {
            DONATION donation = dnRepo.FindById(donationId, donorId);

            IEnumerable<FILES> file = donation.FILES;

            if (file.ElementAt(1).Content != null && file.ElementAt(1).Content.Length > 0)
            {
                byte[] secPhoto = file.ElementAtOrDefault(1).Content;
                return File(secPhoto, "image/png");
            }
            else
            {
                return null;
            }
            
        }

        public ActionResult RemoveImageOne(int donationId, int donorId)
        {
            DONATION donation = dnRepo.FindById(donationId, donorId);

            IEnumerable<FILES> file = donation.FILES;

            if (file.ElementAt(0).Content != null && file.ElementAt(0).Content.Length > 0)
            {
                donation.FILES.Remove(file.ElementAt(0));

                dnRepo.SaveDonation(donation);

                return RedirectToAction("Edit", new { ida = donationId, idb = donorId });
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
