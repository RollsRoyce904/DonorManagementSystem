using System;
using System.Collections.Generic;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface IDonationRepository
    {
        void Add(DONATION d);
        //void Edit(DONATION d);
        void SaveDonation(DONATION d);
        void Remove(int ida, int idb);
        IEnumerable<DONATION> GetDonations();
        DONATION FindById(int? idOne, int? idTwo);
        IEnumerable<DONATION> FindBy(string search);
        IEnumerable<DONATION> FindBy(string search, decimal? amount1, decimal? amount2, DateTime? date1, DateTime? date2, string dep, string gl);
    }
}
