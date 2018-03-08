using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

using WebAPIAjaxProject.Models;
using WebAPIAjaxProject.Utility;


namespace WebAPIAjaxProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private Exception SaveException = null;

        // GET: /Departments/List
        public ActionResult List()
        {
            return Json(db.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Null id"), JsonRequestBehavior.AllowGet);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return Json(new JsonMessage("Failure", "department id not found"), JsonRequestBehavior.AllowGet);
            }

            return Json(department, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([FromBody] Department department)
        {
            Department newdept = db.Departments.Add(department);
            if (newdept == null)
            {
                return Json(new JsonMessage("Failure", "department not found"), JsonRequestBehavior.AllowGet);
            }
            if (Save())
            {
                return Json(new JsonMessage("Success", "Department created, ID: " + newdept.Id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JsonMessage("Failure", SaveException.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Change([FromBody] Department department)
        {
            Department newdept = db.Departments.Find(department.Id);

            if (newdept == null)
            {
                return Json(new JsonMessage("Failure", "department not found"), JsonRequestBehavior.AllowGet);
            }

            newdept.EmployeeID = department.EmployeeID;
            newdept.Budget = department.Budget;
            newdept.Manager = department.Manager;
            newdept.Name = department.Name;

            if ( Save() )
            {
                return Json(new JsonMessage("Success", "Department changed, ID: " + newdept.Id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JsonMessage("Failure", SaveException.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ByName(string name)
        {
            List<Department> departments = db.Departments.Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Remove([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            department = db.Departments.Find(department.Id);

            db.Departments.Remove(department);

            if (Save())
            {
                return Json(new JsonMessage("Success", "Removed department"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JsonMessage("Failure", SaveException.Message), JsonRequestBehavior.AllowGet);
            }
        }

        private bool Save()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                SaveException = e;
                return false;
            }
        }
    }
}