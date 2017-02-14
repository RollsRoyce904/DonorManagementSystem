using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testDMS.Models;

namespace testDMS.DAL
{
    public class DonorRepository : IDonorRepository
    {
        DonorManagementDatabaseEntities context = new DonorManagementDatabaseEntities(); 
        public void Add(DONOR d)
        {
            context.Donor.Add(d);
            context.SaveChanges();
        }

        public void Edit(DONOR d)
        {
            context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        }

        public DONOR FindById(int id)
        {
            var result = (from r in context.Donor where r.DONORID == id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search)
        {
            var result = (from r in context.Donor where r.FNAME == search || r.GENDER == search ||
                          r.EMAIL == search || r.LNAME == search || r.MINIT == search ||
                          r.SUFFIX == search || r.TITLE == search || r.CELL == search ||
                          r.COMPANY.COMPANYNAME == search
                          select r);

            return result;
        }

        //public IEnumerable FindBy(DateTime search)
        //{
        //    var result = (from r in context.Donor where 
        //                  r.BIRTHDAY.Month == search.Month && 
        //                  r.BIRTHDAY.Day == search.Day && 
        //                  r.BIRTHDAY.Year == search.Year
        //                  select r);
        //    return result;
        //}

        public IEnumerable GetDonors()
        {
            return context.Donor;
        }

        public void Remove(int id)
        {
            DONOR d = context.Donor.Find(id);
            context.Donor.Remove(d);
            context.SaveChanges();
        }
    }
}