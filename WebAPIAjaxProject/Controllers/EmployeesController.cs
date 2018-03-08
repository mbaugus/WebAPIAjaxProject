using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

using WebAPIAjaxProject.Models;
using WebAPIAjaxProject.Utility;

//using deny = System.Web.Mvc.JsonRequestBehavior.AllowGet;
namespace WebAPIAjaxProject.Controllers
{
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult ActiveEmployees()
        {
            List<Employee> employees = db.Employees.Where(e => e.Active == true).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return Json(db.Employees.ToList(), JsonRequestBehavior.AllowGet);
        }

        // Employees/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);

            Employee employee = db.Employees.Find(id);

            if(employee == null)
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Change([FromBody] Employee employee)
        {
            Employee oldEmployee = db.Employees.Find(employee.Id);
            oldEmployee = oldEmployee.CopyData(employee);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Status = "Failure", e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was update"), JsonRequestBehavior.AllowGet);
        }

        // /Employees/Create [POST]
        public ActionResult Create ([System.Web.Http.FromBody] Employee employee)
        {
            db.Employees.Add(employee);

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return Json(new { Status = "Failure", e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was created"), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Remove([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            employee = db.Employees.Find(employee.Id);

            db.Employees.Remove(employee);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Status = "Failure", e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Sucess", "Employee was removed"), JsonRequestBehavior.AllowGet);
        }

      
    }
}