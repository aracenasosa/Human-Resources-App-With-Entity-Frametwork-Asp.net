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
    public class Holidays1Controller : Controller
    {
        private Human_ResourcesEntities2 db = new Human_ResourcesEntities2();

        // GET: Holidays1
        public ActionResult Index()
        {
            var holidays = db.Holidays.Include(h => h.Employee1);
            return View(holidays.ToList());
        }

        // GET: Holidays1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // GET: Holidays1/Create
        public ActionResult Create()
        {
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: Holidays1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee,Desde,Hasta,Correspondiente,Comentarios")] Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                db.Holidays.Add(holidays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", holidays.Employee);
            return View(holidays);
        }

        // GET: Holidays1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", holidays.Employee);
            return View(holidays);
        }

        // POST: Holidays1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,Desde,Hasta,Correspondiente,Comentarios")] Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holidays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", holidays.Employee);
            return View(holidays);
        }

        // GET: Holidays1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // POST: Holidays1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Holidays holidays = db.Holidays.Find(id);
            db.Holidays.Remove(holidays);
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
