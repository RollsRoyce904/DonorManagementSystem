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

        public DONATION FindById(int? idOne, int? idTwo)
        {
            var result = (from r in context.DONATION where r.DonationId == idOne && r.DonorId == idTwo select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search)
        {
            var result = (from d in context.DONATION where d.Amount.ToString() == search || d.DateGiftMade.ToString() == search || d.DateRecieved.ToString() == search ||
                          d.CODES.Department == search || d.CODES.GL == search || d.DONOR.FName == search || d.DONOR.LName == search || d.DONOR.CompanyName == search
                          select d);
            return result;
        }

        public IEnumerable FindBy(decimal amount1, decimal amount2)
        {
            var result = (from d in context.DONATION
                          where d.Amount >= amount1 && d.Amount <= amount2
                          select d);
            return result;
        }

        public IEnumerable FindBy(DateTime date1, DateTime date2)
        {
            var result = (from d in context.DONATION
                          where (date1 >= d.DateGiftMade && date2 <= d.DateGiftMade) || (date1 >= d.DateRecieved && date2 <= d.DateRecieved)
                          select d);
            return result;
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