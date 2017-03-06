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

        public void Edit(DONATION d)
        {
            context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        }

        public DONATION FindById(int? idOne, int? idTwo)
        {
            var result = (from r in context.DONATION where r.DonationId == idOne && r.DonorId == idTwo select r).FirstOrDefault();
            return result;
        }

        public IEnumerable FindBy(string search)
        {
            var result = (from d in context.DONATION where d.Amount.ToString() == search ||
                          d.DONOR.FName == search || d.DONOR.LName == search
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
    }
}