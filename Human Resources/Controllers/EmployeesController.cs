using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Human_Resources;

namespace Human_Resources.Controllers
{
    public class EmployeesController : Controller
    {
        private Human_ResourcesEntities2 db = new Human_ResourcesEntities2();

        // GET: Employees
        public ActionResult Index()
        {
            var employee = db.Employee.Include(e => e.Departament1).Include(e => e.Position1);
            return View(employee.ToList());
        }

		public ActionResult Filtrar(String Mes)//Filtrado de todos empleados por mes
		{
			var filt = from s in db.Employee select s;
			if (!String.IsNullOrEmpty(Mes))
			{
				filt = filt.Where(j => j.Admission.ToString().Contains(Mes));
			}

			return View(filt);
		}

		// GET: Employees/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Departament = new SelectList(db.Departament, "Id", "Departamento");
            ViewBag.Position = new SelectList(db.Position, "Id", "Cargo");
            return View();
        }

        // POST: Employees/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Lastname,Telephone,Admission,Salary,Status,Departament,Position")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departament = new SelectList(db.Departament, "Id", "Departamento", employee.Departament);
            ViewBag.Position = new SelectList(db.Position, "Id", "Cargo", employee.Position);
            return View(employee);
        }

        public ActionResult Menu()
        {
            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departament = new SelectList(db.Departament, "Id", "Departamento", employee.Departament);
            ViewBag.Position = new SelectList(db.Position, "Id", "Cargo", employee.Position);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Lastname,Telephone,Admission,Salary,Status,Departament,Position")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departament = new SelectList(db.Departament, "Id", "Departamento", employee.Departament);
            ViewBag.Position = new SelectList(db.Position, "Id", "Cargo", employee.Position);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
