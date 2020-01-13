using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository
{
    public interface ICRUD<T>
    {
        Task<T> GetDataByID(int id);
        IEnumerable<T> GetData(string sql);
        bool Crate(T enity);
        bool Update(T enity);
        bool Delete(T enity);
        bool SaveChange();
      
      
    }
}
