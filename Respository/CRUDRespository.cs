using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository
{
    public class CRUDRespository<T> where T: class, ICRUD<T>
    {
        private DataContext _context;
        public CRUDRespository(DataContext context)
        {
            if (context == null)
            {

                throw new ArgumentNullException();

            }
            _context = context;
        
        }

        public bool Crate(T enity)
        {
            
            _context.Entry(enity).State = EntityState.Added;
            return true;
        }

        public bool Delete(T enity)
        {
            _context.Entry(enity).State = EntityState.Deleted;
            return true;
        }

        public  bool Update(T enity)
        {
            _context.Entry(enity).State = EntityState.Modified;
            return true;
        }
    }
}
