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
            context.DONOR.Add(d);
            context.SaveChanges();
        }

        public void Edit(DONOR d)
        {
            context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        }

        public DONOR FindById(int? id)
        {
            var result = (from r in context.DONOR where r.DonorId == id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search)
        {
            var result = (from r in context.DONOR where r.FName == search || r.Gender == search ||
                          r.Email == search || r.LName == search || r.Init == search ||
                          r.Suffix == search || r.Title == search || r.Cell == search ||
                          r.CompanyName == search
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
            return context.DONOR;
        }

        public void Remove(int id)
        {
            DONOR d = context.DONOR.Find(id);
            context.DONOR.Remove(d);
            context.SaveChanges();
        }
    }
}