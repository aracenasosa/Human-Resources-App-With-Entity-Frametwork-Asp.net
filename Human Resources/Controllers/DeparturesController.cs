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
    public class DeparturesController : Controller
    {
        private Human_ResourcesEntities2 db = new Human_ResourcesEntities2();

        // GET: Departures
        public ActionResult Index()
        {
            var departure = db.Departure.Include(d => d.Employee1);
            return View(departure.ToList());
        }

        // GET: Departures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departure.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // GET: Departures/Create
        public ActionResult Create()
        {
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: Departures/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee,Tipo,Motivo,Fecha")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Departure.Add(departure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", departure.Employee);
            return View(departure);
        }

        // GET: Departures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departure.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", departure.Employee);
            return View(departure);
        }

        // POST: Departures/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,Tipo,Motivo,Fecha")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", departure.Employee);
            return View(departure);
        }

        // GET: Departures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departure.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // POST: Departures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departure departure = db.Departure.Find(id);
            db.Departure.Remove(departure);
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
