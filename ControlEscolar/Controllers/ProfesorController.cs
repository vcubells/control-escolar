using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ControlEscolar.Models;
using ControlEscolar.ViewModels;

namespace ControlEscolar.Controllers
{
    public class ProfesorController : Controller
    {
        private ControlEscolarContext db = new ControlEscolarContext();

        //
        // GET: /Profesor/

        public ActionResult Index(int? id, int? materiaID)
        {
            var modelo = new ProfesorDetalle();

            modelo.Profesores = db.Profesores
                .Include(p => p.Oficina)
                .Include(p => p.Materias.Select(c => c.Departamento))
                .OrderBy(p => p.Apellidos);

            /* Recuperar materias del Profesor seleccionado */
            if (id != null)
            {
                modelo.Materias = modelo.Profesores.Single(p => p.PersonaID == id.Value).Materias;
                ViewBag.PersonaID = id.Value;
            }
            
            /* Recuperar estudiantes que cursan la materia */
            if (materiaID != null)
            {
                modelo.Estudiantes = modelo.Materias.Single(m => m.MateriaID == materiaID.Value).Estudiantes;
                ViewBag.MateriaID = materiaID.Value;
            }
            
            return View(modelo);
        }

        //
        // GET: /Profesor/Details/5

        public ActionResult Details(int id = 0)
        {
            Profesor profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        //
        // GET: /Profesor/Create

        public ActionResult Create()
        {
            ViewBag.PersonaID = new SelectList(db.Oficinas, "PersonaID", "Ubicacion");
            
            MateriasAsignadas(null);

            return View();
        }

        //
        // POST: /Profesor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profesor profesor, string[] materiasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesor);

                db.SaveChanges();

                ActualizaMaterias(profesor, materiasSeleccionadas);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaID = new SelectList(db.Oficinas, "PersonaID", "Ubicacion", profesor.PersonaID);
            
            MateriasAsignadas(profesor);

            return View(profesor);
        }

        //
        // GET: /Profesor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Profesor profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaID = new SelectList(db.Oficinas, "PersonaID", "Ubicacion", profesor.PersonaID);

            MateriasAsignadas(profesor);

            return View(profesor);
        }

        //
        // POST: /Profesor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profesor profesor, string[] materiasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesor).State = EntityState.Modified;

                ActualizaMaterias(profesor, materiasSeleccionadas);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaID = new SelectList(db.Oficinas, "PersonaID", "Ubicacion", profesor.PersonaID);
            
            MateriasAsignadas(profesor);
            
            return View(profesor);
        }


        public void ActualizaMaterias(Profesor profesor, string[] materiasSeleccionadas)
        {
            profesor = db.Profesores.Include(m => m.Materias)
                .Where(p => p.PersonaID == profesor.PersonaID).Single();

            
            /* Cuando no se selecciona ningún checkbox */
            if (materiasSeleccionadas == null)
            {
                profesor.Materias = new List<Materia>();
                return;
            }

            
            var seleccionadas = new HashSet<string>(materiasSeleccionadas);
            var asignadas = new HashSet<int>( profesor.Materias.Select(m => m.MateriaID));

            foreach (var materia in db.Materias)
            {
                if (seleccionadas.Contains(materia.MateriaID.ToString()))
                {
                    if (!asignadas.Contains(materia.MateriaID))
                    {
                        profesor.Materias.Add(materia);
                    }
                }
                else
                {
                    if (asignadas.Contains(materia.MateriaID))
                    {
                        profesor.Materias.Remove(materia);
                    }
                }

            }


        }

        //
        // GET: /Profesor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Profesor profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        //
        // POST: /Profesor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesor profesor = db.Profesores.Find(id);
            db.Profesores.Remove(profesor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void MateriasAsignadas(Profesor profesor)
        {
            var materias = db.Materias;
            HashSet<int> materiasDelProfesor;

            if (profesor == null)
            {
                materiasDelProfesor = new HashSet<int>();
            }
            else
            {
                 materiasDelProfesor = new HashSet<int>(profesor.Materias.Select(m => m.MateriaID));
            }
            
            var modelo = new List<MateriasAsignadas>();
            
            foreach (var materia in materias)
            {
                modelo.Add(new MateriasAsignadas
                {
                    MateriaID = materia.MateriaID,
                    Titulo = materia.Titulo,
                    Asignada = materiasDelProfesor.Contains(materia.MateriaID)
                });
            }

            ViewBag.Materias = modelo;
        }
    }
}