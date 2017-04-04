using System.Collections.Generic;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface ICodeRepository
    {
        void Add(CODES code);

        void Remove(int id);

        IEnumerable<CODES> GetCodes();


    }
}
