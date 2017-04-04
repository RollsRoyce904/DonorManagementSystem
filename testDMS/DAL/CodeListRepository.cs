using System;
using System.Collections.Generic;
using testDMS.Models;

namespace testDMS.DAL
{
    public class CodeListRepository : ICodeListRepository
    {
        DonorManagementDatabaseEntities context = new DonorManagementDatabaseEntities();

        public void Add(CODELIST codeList)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CODELIST> GetCodeList
        {
            get { return context.CODELIST; }
        }

        //public CODES GetDepartment(int ida, int idb)
        //{
        //    var result = (from d in context.CODES
        //                  where d.DonationId == ida && d.DonorId == idb
        //                  select d.Department);

        //    return result;
        //}
    }
}