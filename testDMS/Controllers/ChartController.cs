using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using testDMS.DAL;
using testDMS.Models;

namespace testDMS.Controllers
{
    public class ChartController : Controller
    {
        private DonorManagementDatabaseEntities ddlData = new DonorManagementDatabaseEntities();
        IDonorRepository drRepo;
        IDonationRepository dnRepo;
        ICodeListRepository clRepo;
        ICodeRepository cdRepo;

        public ChartController(IDonorRepository drRepo, IDonationRepository dnRepo, ICodeListRepository clRepo, ICodeRepository cdRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
            this.clRepo = clRepo;
            this.cdRepo = cdRepo;
        }

        public ActionResult Index()
        {
            LoadData();
            LoadSelectList();

            return View();
        }

        public ActionResult LoadSelectList()
        {
            ViewBag.Person = new SelectList(ddlData.DONOR, "DonorId", "FNAME");
            ViewBag.Department = new SelectList(ddlData.CODELIST, "Department", "Department");
            ViewBag.Gl = new SelectList(ddlData.CODELIST, "GL", "GL");

            var amountList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "Amount", Value="0", Selected=true },
                    new SelectListItem {Text = "0-100", Value="1" },
                    new SelectListItem {Text = "101-500", Value="2" },
                    new SelectListItem {Text = "501-1000", Value="3" },
                    new SelectListItem {Text = "1001-2000", Value="4" },
                    new SelectListItem {Text = "2001-4000", Value="5" },
                    new SelectListItem {Text = "4001-7000", Value="6" },
                    new SelectListItem {Text = "7001-10,000", Value="7" },
                    new SelectListItem {Text = "10,000+", Value="8" },
                }, "Value", "Text", 0);

            ViewBag.Amount = amountList;


            ViewBag.Fund = new SelectList(ddlData.CODELIST, "Funds", "Funds");

          

            ViewBag.GL = new SelectList(ddlData.CODELIST, "GL", "GL");
            

            ViewBag.Department = new SelectList(ddlData.CODELIST, "Department", "Department");
            
            ViewBag.Program = new SelectList(ddlData.CODELIST, "Program", "Program");

            
            ViewBag.Grant = new SelectList(ddlData.CODELIST, "Grant", "Grant");

            return View("~/Views/Chart/Index.cshtml");
        }

        public ActionResult LoadData()
        {
            IEnumerable<DONOR> Donors = drRepo.GetDonors;
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.GetDonations();
            IEnumerable<CODELIST> CodeList = clRepo.GetCodeList;
            //IEnumerable<CODES> Codes = cdRepo.GetCodes();


            ChartDispalyViewModel model = new ChartDispalyViewModel();
            model.Donors = Donors;
            model.Donations = Donations;
            model.CodeList = CodeList;


            return View("~/Views/Chart/Index.cshtml", model);
        }

        public ActionResult Search(string searchString, int amount, DateTime? date1, DateTime? date2, string department, string gl)
        {

            int amount1 = 0;
            int amount2 = 0;

            switch (amount)
            {
                case 0:
                    break;
                case 1:
                    amount1 = 1;
                    amount2 = 100;
                    break;
                case 2:
                    amount1 = 101;
                    amount2 = 500;
                    break;
                case 3:
                    amount1 = 501;
                    amount2 = 1000;
                    break;
                case 4:
                    amount1 = 1001;
                    amount2 = 2000;
                    break;
                case 5:
                    amount1 = 2001;
                    amount2 = 4000;
                    break;
                case 6:
                    amount1 = 4001;
                    amount2 = 7000;
                    break;
                case 7:
                    amount1 = 7001;
                    amount2 = 10000;
                    break;
                case 8:
                    amount1 = 10001;
                    amount2 = 1000000;
                    break;
                default:
                    break;
            };

            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.FindBy(searchString,
                amount1, amount2, date1, date2, department, gl);

            ChartDispalyViewModel model = new ChartDispalyViewModel();

            model.Donations = Donations;

            model.searchString = searchString;
            model.amount = amount;
            model.date1 = date1;
            model.date2 = date2;
            model.department = department;
            model.gl = gl;

            LoadSelectList();

            return View("~/Views/Chart/Index.cshtml", model);
        }


        public ActionResult ExportToExcel(string searchString, int? amount, DateTime? date1, DateTime? date2, string department, string gl)
        {
            int amount1 = 0;
            int amount2 = 0;

            switch (amount)
            {
                case 0:
                    break;
                case 1:
                    amount1 = 1;
                    amount2 = 100;
                    break;
                case 2:
                    amount1 = 101;
                    amount2 = 500;
                    break;
                case 3:
                    amount1 = 501;
                    amount2 = 1000;
                    break;
                case 4:
                    amount1 = 1001;
                    amount2 = 2000;
                    break;
                case 5:
                    amount1 = 2001;
                    amount2 = 4000;
                    break;
                case 6:
                    amount1 = 4001;
                    amount2 = 7000;
                    break;
                case 7:
                    amount1 = 7001;
                    amount2 = 10000;
                    break;
                case 8:
                    amount1 = 10001;
                    amount2 = 1000000;
                    break;
                default:
                    break;
            };

            var Donations = (IEnumerable<DONATION>)dnRepo.FindBy(searchString,
                amount1, amount2, date1, date2, department, gl);
            BindingList<DONATION> bList = new BindingList<DONATION>();

            foreach (var item in Donations)
            {
                bList.Add(item);
            }

            DateTime dt = DateTime.Now;
            string date = dt.ToShortDateString();

            // Step 1 - get the data from database
            //var myData = ddlData.DONATION.ToList();

            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView();
            gridview.DataSource = bList;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;

            // set the header
            Response.AddHeader("content-disposition", "attachment; filename = Report-" + date + ".xls");
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