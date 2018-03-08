using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPIAjaxProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base() {}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}