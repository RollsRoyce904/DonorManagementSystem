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
        void Edit(DONOR d);
        void Remove(int id);
        IEnumerable GetDonors();
        DONOR FindById(int? id);
        IEnumerable FindBy(string search);
        //IEnumerable FindBy(DateTime search);
    }
} 
