using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add();
        
        void Update();

        void Delete();

        bool SaveChanges();

    }
}