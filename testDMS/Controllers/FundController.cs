using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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


    public class FundController : Controller
    {
        private DonorManagementDatabaseEntities data = new DonorManagementDatabaseEntities();


        // GET: Fund
        public ActionResult Index()
        {
            return View(data.GLS);

        }


        // GET: Fund/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fund/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "GLID,GL")] GLS gls)
        {
            try
            {
                data.GLS.Add(gls);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

               // GET: Fund/Delete/5
        public ActionResult Delete(int id)
        {

            GLS gl = data.GLS.Find(id);
            if (gl == null)
            {
                return HttpNotFound();
            }
            return View(gl);

        }

        // POST: Fund/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                GLS gl = data.GLS.Find(id);
                data.GLS.Remove(gl);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
