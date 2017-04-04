using System.Collections.Generic;
using System.Linq;
using testDMS.Models;

namespace testDMS.DAL
{
    public class CodeRepository : ICodeRepository
    {
        DonorManagementDatabaseEntities context = new DonorManagementDatabaseEntities();

        public void Add(CODES code)
        {
            context.CODES.Add(code);
            context.SaveChanges();
        }

        public IEnumerable<CODES> GetCodes()
        {
            var results = (from c in context.CODES
                           select c);

            return results;
        }

        public void Remove(int id)
        {
            CODES c = context.CODES.Find(id);
            context.CODES.Remove(c);
            context.SaveChanges();
        }
    }
}