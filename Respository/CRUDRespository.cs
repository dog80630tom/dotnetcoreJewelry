using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository
{
    public class CRUDRespository<T>: ICRUD<T>
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
        public bool SaveChange() {
            using (var data = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChangesAsync();
                    data.CommitAsync();
                }
                catch (Exception ex)
                {
                    data.Rollback();
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<T> GetData(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {

                var products = conn.Query<T>(sql);
                return products;
            }
        }
    }
}
