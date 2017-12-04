using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ControlEscolar.Models;
using System.Data.Entity.Infrastructure;

namespace ControlEscolar.Controllers
{
    public class DepartamentoController : Controller
    {
        private ControlEscolarContext db = new ControlEscolarContext();

        //
        // GET: /Departamento/

        public ActionResult Index()
        {
            var departamentos = db.Departamentos.Include(d => d.Director);
            return View(departamentos.ToList());
        }

        //
        // GET: /Departamento/Details/5

        public ActionResult Details(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // GET: /Departamento/Create

        public ActionResult Create()
        {
            ViewBag.PersonaID = new SelectList(db.Profesores, "PersonaID", "Nombre");
            return View();
        }

        //
        // POST: /Departamento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Departamentos.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaID = new SelectList(db.Profesores, "PersonaID", "Nombre", departamento.PersonaID);
            return View(departamento);
        }

        //
        // GET: /Departamento/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaID = new SelectList(db.Profesores, "PersonaID", "Nombre", departamento.PersonaID);
            return View(departamento);
        }

        //
        // POST: /Departamento/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(departamento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entidad = ex.Entries.Single();
                var enDB = (Departamento)entidad.GetDatabaseValues().ToObject();
                var enVista = (Departamento)entidad.Entity;

                if (enDB.Nombre != enVista.Nombre)
                {
                    ModelState.AddModelError("Nombre", "Valor actual: " + enDB.Nombre);
                }

                /* ... Adicionar para los demás campos */

                ModelState.AddModelError(String.Empty, 
                    "Estás tratando de actualizar un registro que se modificó después del último acceso"
                    + ". Si quieres ignorar tus cambios selecciona Cancelar, "
                    + "si quieres sobreescribir la base de datos de todas maneras, "
                    + " seleccionar Guardar");

                departamento.Timestamp = enDB.Timestamp;
            }

            ViewBag.PersonaID = new SelectList(db.Profesores, "PersonaID", "Nombre", departamento.PersonaID);
            return View(departamento);
        }

        //
        // GET: /Departamento/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // POST: /Departamento/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamentos.Find(id);
            db.Departamentos.Remove(departamento);
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