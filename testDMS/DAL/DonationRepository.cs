using System;
using System.Collections;
using System.Linq;
using testDMS.Models;

namespace testDMS.DAL
{
    public class DonationRepository : IDonationRepository
    {
        DonorManagementDatabaseEntities context;

        public DonationRepository(DonorManagementDatabaseEntities context)
        {
            this.context = context;
        }

        public void Add(DONATION d)
        {
            context.DONATION.Add(d);
            context.SaveChanges();
        }

        //public void Edit(DONATION d)
        //{
        //    context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        //}

        public IEnumerable FindBy(string search)
        {//
            var result = (from d in context.DONATION
                          where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
                            d.DONOR.FName == search || d.DONOR.LName == search || d.DONOR.CompanyName == search || d.Department == search || d.GL == search
                          select d);
            return result;
        }

        public DONATION FindById(int? idOne, int? idTwo)
        {
            var result = (from r in context.DONATION where r.DonationId == idOne && r.DonorId == idTwo select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search, decimal? amount1, decimal? amount2, DateTime? date1, DateTime? date2, string dep, string gl)
        {

            //returns data from only search string
            if (search != null && search.Length > 0)
            { 
                var result = (from d in context.DONATION
                              where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
                                     d.Department == search || d.GL == search ||d.DONOR.FName == search || d.DONOR.LName == search || d.DONOR.CompanyName == search
                              select d);

                if (amount1 > 0)
                {
                    result = result.Where(d => (d.Amount >= amount1 && d.Amount <= amount2));
                }

                if (date1 != null)
                {
                    result = result.Where(d => (d.DateGiftMade >= date1 && d.DateGiftMade <= date2) || (d.DateRecieved >= date1 & d.DateRecieved <= date2));
                }

                if (dep != null && dep.Length > 0)
                {
                    result = result.Where(d => d.Department == dep);
                }

                if (gl != null && gl.Length > 0)
                {
                    result = result.Where(d => d.GL == gl);
                }

                return result;
            }
            else
            {
                var result = from d in context.DONATION select d;
                if (amount1 > 0)
                {
                    result = result.Where(d => (d.Amount >= amount1 && d.Amount <= amount2));
                }

                if (date1 != null)
                {
                    result = result.Where(d => (d.DateGiftMade >= date1 && d.DateGiftMade <= date2) || (d.DateRecieved >= date1 & d.DateRecieved <= date2));
                }

                if (dep != null && dep.Length > 0)
                {
                    result = result.Where(d => d.Department == dep);
                }

                if (gl != null && gl.Length > 0)
                {
                    result = result.Where(d => d.GL == gl);
                }
                return result;
            }


        }

        public IEnumerable GetDonations()
        {
            return context.DONATION;
        }

        public void Remove(int ida, int idb)
        {
            DONATION d = context.DONATION.Find(ida, idb);
            context.DONATION.Remove(d);
            context.SaveChanges();
        }

        public void SaveDonation(DONATION d)
        {
            if (d.DonationId == 100)
            {
                context.DONATION.Add(d);
            }
            else
            {
                DONATION dbEntry = context.DONATION.Find(d.DonationId, d.DonorId);

                if (dbEntry != null)
                {
                    dbEntry.Amount = d.Amount;
                    dbEntry.TypeOf = d.TypeOf;
                    dbEntry.DateRecieved = d.DateRecieved;
                    dbEntry.GiftMethod = d.GiftMethod;
                    dbEntry.DateGiftMade = d.DateGiftMade;
                    //dbEntry.ImageUpload = d.ImageUpload;
                    //dbEntry.ImageMimeType = d.ImageMimeType;
                    dbEntry.GiftRestrictions = d.GiftRestrictions;
                    dbEntry.Notes = d.Notes;
                    dbEntry.Appeal = d.Appeal;
                    dbEntry.Department = d.Department;
                    dbEntry.Fund = d.Fund;
                    dbEntry.GL = d.GL;
                    dbEntry.GrantS = d.GrantS;
                    dbEntry.Program = d.Program;
                    dbEntry.FILES = d.FILES;
                }
            }

            context.SaveChanges();

        }
    }
}