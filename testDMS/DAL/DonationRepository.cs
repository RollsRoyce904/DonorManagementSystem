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
            context.Donation.Add(d);
            context.SaveChanges();
        }

        public void Edit(DONATION d)
        {
            context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        }

        public DONATION FindById(int? idOne, int? idTwo)
        {
            var result = (from r in context.Donation where r.DonationId == idOne && r.DonorId == idTwo select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search)
        {
            var result = (from d in context.Donation where d.Amount.ToString() == search ||
                          d.DONOR.FNAME == search || d.DONOR.LNAME == search
                          select d);
            return result;
        }

        public IEnumerable GetDonations()
        {
            return context.Donation;
        }

        public void Remove(int ida, int idb)
        {
            DONATION d = context.Donation.Find(ida, idb);
            context.Donation.Remove(d);
            context.SaveChanges();
        }
    }
}