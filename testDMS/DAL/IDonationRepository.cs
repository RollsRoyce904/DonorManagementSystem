using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface IDonationRepository
    {
        void Add(DONATION d);
        void Edit(DONATION d);
        void Remove(int ida, int idb);
        IEnumerable GetDonations();
        DONATION FindById(int? idOne, int? idTwo);
        IEnumerable FindBy(string search);
    }
}
