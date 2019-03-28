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
    public class LicensesController : Controller
    {
        private Human_ResourcesEntities2 db = new Human_ResourcesEntities2();

        // GET: Licenses
        public ActionResult Index()
        {
            var license = db.License.Include(l => l.Employee1);
            return View(license.ToList());
        }

        // GET: Licenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = db.License.Find(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // GET: Licenses/Create
        public ActionResult Create()
        {
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: Licenses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee,Desde,Hasta,Motivo,Comentarios")] License license)
        {
            if (ModelState.IsValid)
            {
                db.License.Add(license);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", license.Employee);
            return View(license);
        }

        // GET: Licenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = db.License.Find(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", license.Employee);
            return View(license);
        }

        // POST: Licenses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,Desde,Hasta,Motivo,Comentarios")] License license)
        {
            if (ModelState.IsValid)
            {
                db.Entry(license).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee = new SelectList(db.Employee, "Id", "Name", license.Employee);
            return View(license);
        }

        // GET: Licenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = db.License.Find(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            License license = db.License.Find(id);
            db.License.Remove(license);
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
