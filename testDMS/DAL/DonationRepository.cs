using System;
using System.Collections;
using System.Linq;
using System.Web;
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
        {
            var result = (from d in context.DONATION
                          where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search || d.DONOR.LName == search || d.DONOR.CompanyName == search
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
            var result = (from d in context.DONATION
                          where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
                                d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search ||
                                d.DONOR.LName == search || d.DONOR.CompanyName == search &&
                                (d.Amount >= amount1 && d.Amount <= amount2) &&
                                (d.DateRecieved >= date1 && d.DateRecieved <= date2) &&
                                d.CODES.Department == dep && d.CODES.GL == gl
                          select d);

            return result;
            //if ((amount1 == null && amount2 == null) && (date1 == null && date2 == null) && dep == null && gl == null)
            //{//search != null && 
            //    //returns data from only search string
            //    var result = (from d in context.DONATION
            //                  where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
            //                        d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search || 
            //                        d.DONOR.LName == search || d.DONOR.CompanyName == search
            //                  select d);

            //    return result;

            //}
            //else if ((amount1 != null && amount2 != null) && (date1 == null || date2 == null) && dep == null && gl == null)
            //{//search != null || 
            //    //returns data from amounts and search string if its not null
            //    var result = (from d in context.DONATION
            //                  where (d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
            //                         d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search || 
            //                         d.DONOR.LName == search || d.DONOR.CompanyName == search) && 
            //                        (d.Amount >= amount1 && d.Amount <= amount2)
            //                  select d);

            //    return result;

            //}
            //else if ((amount1 != null && amount2 != null) && (date1 != null && date2 != null))
            //{//search != null && 
            //    //returns data from search amounts and dates if theyre not null
            //    var result = (from d in context.DONATION
            //                  where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
            //                        d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search || 
            //                        d.DONOR.LName == search || d.DONOR.CompanyName == search && 
            //                        (d.Amount >= amount1 && d.Amount <= amount2) && 
            //                        (d.DateRecieved >= date1 && d.DateRecieved <= date2)
            //                  select d);

            //    return result;

            //}
            //else if ((amount1 != null && amount2 != null) && (date1 != null && date2 != null) && dep != null && gl == null)
            //{
            //    //returns data if dep isnt null
            //    var result = (from d in context.DONATION
            //                  where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
            //                        d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search ||
            //                        d.DONOR.LName == search || d.DONOR.CompanyName == search &&
            //                        (d.Amount >= amount1 && d.Amount <= amount2) &&
            //                        (d.DateRecieved >= date1 && d.DateRecieved <= date2) &&
            //                        d.CODES.Department == dep
            //                  select d);

            //    return result;
            //}
            //else if ((amount1 != null && amount2 != null) && (date1 != null && date2 != null) && dep != null & gl != null)
            //{
            //    //returns data if dep and gl isnt null
            //    var result = (from d in context.DONATION
            //                  where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
            //                        d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search ||
            //                        d.DONOR.LName == search || d.DONOR.CompanyName == search &&
            //                        (d.Amount >= amount1 && d.Amount <= amount2) &&
            //                        (d.DateRecieved >= date1 && d.DateRecieved <= date2) &&
            //                        d.CODES.Department == dep && d.CODES.GL == gl
            //                  select d);

            //    return result;
            //}
            //else
            //{

            //    var result = (from d in context.DONATION select d);

            //    return result;

            //}

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
                    dbEntry.CodeId = d.CodeId;
                    dbEntry.ImageUpload = d.ImageUpload;
                    dbEntry.ImageMimeType = d.ImageMimeType;
                    dbEntry.GiftRestrictions = d.GiftRestrictions;
                    dbEntry.Notes = d.Notes;
                }
            }

            context.SaveChanges();

        }
    }
}