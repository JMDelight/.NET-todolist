using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoListContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
        }
        public ToDoListContext() { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoList;integrated security=True");
        }
    }
}