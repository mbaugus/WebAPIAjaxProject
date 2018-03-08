using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPIAjaxProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public bool Active { get; set; }

        public Employee CopyData(Employee e)
        {
            Name = e.Name;
            Salary = e.Salary;
            Active = e.Active;
            return this;
        }
    }
}