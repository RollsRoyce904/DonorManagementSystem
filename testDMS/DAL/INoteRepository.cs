using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface INoteRepository
    {
        void Add(NOTES note);

        IEnumerable<NOTES> GetNotes(int id);
    }
}