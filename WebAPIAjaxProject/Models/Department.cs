using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIAjaxProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeID { get; set; }
        public decimal Budget { get; set; }
        
        virtual public Employee Manager { get; set; }
    }
}