using System;
using authtest.Models;
using Microsoft.EntityFrameworkCore;

namespace authtest
{
    public class Contextt : DbContext

    {
        public Contextt(DbContextOptions<Contextt> opt) : base(opt) { }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
