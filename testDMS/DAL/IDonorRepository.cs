using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testDMS.Models;

namespace testDMS.DAL
{
    public interface IDonorRepository
    {
        void Add(DONOR d);

        //void Edit(DONOR d);

        void Remove(int id);

        void SaveProduct(DONOR product);

        IEnumerable<DONOR> GetDonors { get; }

        DONOR FindById(int? id);

        IEnumerable<DONOR> FindBy(string search);

        //IEnumerable FindBy(DateTime search);
    }
} 
