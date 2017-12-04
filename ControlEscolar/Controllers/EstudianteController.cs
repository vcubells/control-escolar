using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ControlEscolar.Models;

namespace ControlEscolar.Controllers
{
    public class EstudianteController : Controller
    {
        private ControlEscolarContext db = new ControlEscolarContext();

        //
        // GET: /Estudiante/

        public ActionResult Index(string criterio, string busqueda)
        {
            ViewBag.NombreSort = String.IsNullOrEmpty(criterio) ? "Nombre desc" : "";
            ViewBag.FechaSort = criterio == "Fecha" ? "Fecha desc" : "Fecha";

            var estudiantes = from s in db.Estudiantes select s;

            if (!String.IsNullOrEmpty(busqueda))
            {
                estudiantes = estudiantes.Where(n => n.Nombre.Contains(busqueda) 
                    ||  n.Apellidos.Contains(busqueda));
            }

            switch (criterio)
            {
                case "Nombre desc":
                    estudiantes = estudiantes.OrderByDescending(n => n.Nombre);
                    break;
                case "Fecha":
                    estudiantes = estudiantes.OrderBy(f => f.FechaIngreso);
                    break;
                case "Fecha desc":
                    estudiantes = estudiantes.OrderByDescending(f => f.FechaIngreso);
                    break;
                default:
                    estudiantes = estudiantes.OrderBy(n => n.Nombre);
                    break;

            }
            return View(estudiantes.ToList());
        }

        //
        // GET: /Estudiante/Details/5

        public ActionResult Details(int id = 0)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        //
        // GET: /Estudiante/Create
        [Authorize(Roles="puedeEditar")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Estudiante/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "puedeEditar")]
        public ActionResult Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estudiante);
        }

        //
        // GET: /Estudiante/Edit/5
        [Authorize(Roles = "puedeEditar")]
        public ActionResult Edit(int id = 0)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        //
        // POST: /Estudiante/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "puedeEditar")]
        public ActionResult Edit(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudiante);
        }

        //
        // GET: /Estudiante/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        //
        // POST: /Estudiante/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiante);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}