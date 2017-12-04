using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ControlEscolar.Models;
using ControlEscolar.ViewModels;

namespace ControlEscolar.Controllers
{
    public class MateriaController : Controller
    {
        private ControlEscolarContext db = new ControlEscolarContext();

        //
        // GET: /Materia/

        public ActionResult Index()
        {
            var materias = db.Materias.Include(m => m.Departamento);
            return View(materias.ToList());
        }

        //
        // GET: /Materia/Details/5

        public ActionResult Details(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        //
        // GET: /Materia/Create

        public ActionResult Create()
        {
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ProfesoresAsignados(null);

            return View();
        }

        //
        // POST: /Materia/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Materia materia, string[] profesoresSeleccionados)
        {
            if (ModelState.IsValid)
            {
                db.Materias.Add(materia);
                db.SaveChanges();

                ActualizaProfesores(materia, profesoresSeleccionados);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", materia.DepartamentoID);

            ProfesoresAsignados(materia);
            
            return View(materia);
        }

        //
        // GET: /Materia/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", materia.DepartamentoID);
            
            ProfesoresAsignados(materia);

            return View(materia);
        }

        //
        // POST: /Materia/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Materia materia, string[] profesoresSeleccionados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia).State = EntityState.Modified;

                ActualizaProfesores(materia, profesoresSeleccionados);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", materia.DepartamentoID);
            
            ProfesoresAsignados(materia);

            return View(materia);
        }

        //
        // GET: /Materia/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        //
        // POST: /Materia/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materia materia = db.Materias.Find(id);
            db.Materias.Remove(materia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void ProfesoresAsignados(Materia materia)
        {
            var profesores = db.Profesores;
            HashSet<int> profesoresDeMateria;

            if (materia == null)
            {
                profesoresDeMateria = new HashSet<int>();
            }
            else
            {
                profesoresDeMateria = new HashSet<int>(materia.Profesores.Select(m => m.PersonaID));
            }

            var modelo = new List<ProfesoresAsignados>();

            foreach (var profesor in profesores)
            {
                modelo.Add(new ProfesoresAsignados
                {
                    PersonaID = profesor.PersonaID,
                    Nombre = profesor.NombreCompleto,
                    Asignado = profesoresDeMateria.Contains(profesor.PersonaID)
                });
            }

            ViewBag.Profesores = modelo;
        }

        public void ActualizaProfesores(Materia materia, string[] profesoresSeleccionados)
        {
            materia = db.Materias.Include(m => m.Profesores)
               .Where(p => p.MateriaID == materia.MateriaID).Single();
            
            /* Cuando no se selecciona ningún checkbox */
            if (profesoresSeleccionados == null)
            {
                materia.Profesores = new List<Profesor>();
                return;
            }

           

            var seleccionados = new HashSet<string>(profesoresSeleccionados);
            var asignados = new HashSet<int>(materia.Profesores.Select(m => m.PersonaID));

            foreach (var profesor in db.Profesores)
            {
                if (seleccionados.Contains(profesor.PersonaID.ToString()))
                {
                    if (!asignados.Contains(profesor.PersonaID))
                    {
                        materia.Profesores.Add(profesor);
                    }
                }
                else
                {
                    if (asignados.Contains(profesor.PersonaID))
                    {
                        materia.Profesores.Remove(profesor);
                    }
                }

            }
        }
    }
}