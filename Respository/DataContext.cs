using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jewelry.Models;
using Jewelry.Models.ViewModels;

namespace Jewelry.Respository
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
        { 
        
        
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProducetDetail> ProducetDetils { get; set; }
        public DbSet<PicDetail> PicDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer_Question> Customer_Questions { get; set; }
        public DbSet<HomePageAD> HomePageADs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AdminRespond> AdminResponds { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Customer_Review> Customer_Reviews { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        public DbSet<Jewelry.Models.ViewModels.NewProductDetail> NewProductDetails { get; set; }

    }
}
