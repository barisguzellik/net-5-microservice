using System;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.DbContexts
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
    }
}
