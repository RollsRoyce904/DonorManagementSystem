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

        public ChartController(IDonorRepository drRepo, IDonationRepository dnRepo)
        {
            this.drRepo = drRepo;
            this.dnRepo = dnRepo;
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
            ViewBag.Department = new SelectList(ddlData.CODES, "CodeId", "Department");
            ViewBag.Gl = new SelectList(ddlData.CODES, "CodeId", "GL");

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

            return View("~/Views/Chart/Index.cshtml");
        }

        public ActionResult LoadData()
        {
            IEnumerable<DONOR> Donors = (IEnumerable<DONOR>)drRepo.GetDonors;
            IEnumerable<DONATION> Donations = (IEnumerable<DONATION>)dnRepo.GetDonations();
            ChartDispalyViewModel model = new ChartDispalyViewModel();
            model.Donors = Donors;
            model.Donations = Donations;


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