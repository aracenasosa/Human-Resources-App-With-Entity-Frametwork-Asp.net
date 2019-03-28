using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Human_Resources.Controllers
{
    public class ModuleReportController : Controller
    {
		private Human_ResourcesEntities2 db = new Human_ResourcesEntities2();

		// GET: ModuleReport
		public ActionResult EmployeeActive()//Stored Procedures
        {
            return View(db.EmpleadosActivos());
        }

		public ActionResult Filtrar(String Name, String Departament)//Filtrado Por Nombre y Departamento Empleados Activos
		{
			//ViewBag.Departament = new SelectList(db.EmpleadosActivos(), "Departament1", "Name", "Lastname");
			var filt = from s in db.EmpleadosActivos() select s;

			if (!String.IsNullOrEmpty(Name))
			{
				filt = filt.Where(j => j.Name.Contains(Name));
			}	

			if (!String.IsNullOrEmpty(Departament))
			{
				return View(filt.Where(x => x.Departament.ToString().Contains(Departament)));
			}
			else
			{
				return View(filt);
			}

		}

		public ActionResult Filtrar(String Mes)//Filtrado Entradas de empleados por mes
		{
			var filt = from s in db.Employee select s;
			if (!String.IsNullOrEmpty(Mes))
			{
				filt = filt.Where(j => j.Admission.ToString().Contains(Mes));
			}

			return View(filt);
		}

		public ActionResult Filtrar2(String Mes)//Filtrado de Empleados Inactivos por Mes
		{
			var filt = from s in db.EmpleadosInactivos() select s;
			if (!String.IsNullOrEmpty(Mes))
			{
				filt = filt.Where(j => j.Admission.ToString().Contains(Mes));
			}

			return View(filt);
		}

		public ActionResult Filtrar3(String Tipo)//Filtrado Por Mes de la Salida de Empleados
		{
			var filt = from s in db.Departure select s;
			if (!String.IsNullOrEmpty(Tipo))
			{
				filt = filt.Where(j => j.Tipo.Contains(Tipo));
			}

			return View(filt);
		}


		public ActionResult EmployeeInactive()//Stored Procedures
		{
			return View(db.EmpleadosInactivos());
		}

		public ActionResult ModuloProcesos()//Modulo de Procesos
		{
			return View();
		}

		public ActionResult ModuloMantenimiento()//Modulo de Mantenimiento
		{
			return View();
		}

		public ActionResult ModuloReportes()//Modulo de Reportes
		{
			return View();
		}
	}
}