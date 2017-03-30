using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web;
using testDMS.Models;

namespace testDMS.DAL
{
    public class NoteRepository : INoteRepository
    {
        DonorManagementDatabaseEntities context = new DonorManagementDatabaseEntities();
        public void Add(NOTES note)
        {
            context.NOTES.Add(note);
            context.SaveChanges();
        }

        public IEnumerable<NOTES> GetNotes(int id)
        {
            var result = from n in context.NOTES
                         where n.DonorId == id
                         select n;
            return result;
        }
    }
}