using System.Collections.Generic;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface ICodeListRepository
    {
        void Add(CODELIST codeList);

        void Remove(int id);

        IEnumerable<CODELIST> GetCodeList { get; }

        //CODELIST GetDepartment(int ida, int idb);

    }
}
