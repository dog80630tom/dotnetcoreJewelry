using Jewelry.Models;
using Jewelry.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Services
{
    public class ProductServices 
    {
        private CRUDRespository<Product> _ProductNew;
        private CRUDRespository<PicDetail> _PicNew;

    

        public bool CreateProduct(Product product,PicDetail pic) {
            try
            {

                _ProductNew.Crate(product);
                _PicNew.Crate(pic);
                _ProductNew.SaveChange();
            }
            catch (Exception ex) {


                return false;
            }
            return true;
        }
        public bool UpdateProduct(Product product, PicDetail pic) {
            try
            {
                _ProductNew.Update(product);
                _PicNew.Update(pic);
                _ProductNew.SaveChange();
            }
            catch (Exception ex)
            {


                return false;
            }
            return true;
        }
        public bool DeleteProduct(int id)
        {
            try
            {
                string sql = "select * from product where id=" + id;
                Product product = _ProductNew.GetData(sql).FirstOrDefault();
                sql = "select * from PicDetail where id=" + id;
                PicDetail pic = _PicNew.GetData(sql).FirstOrDefault();
                _ProductNew.Delete(product);
                _PicNew.Delete(pic);
                _ProductNew.SaveChange();
            }
            catch (Exception ex)
            {


                return false;
            }
            return true;
        }

    }
}
