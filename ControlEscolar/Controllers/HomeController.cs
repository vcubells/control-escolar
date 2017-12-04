using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlEscolar.Models;
using ControlEscolar.ViewModels;

namespace ControlEscolar.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ControlEscolarContext db = new ControlEscolarContext();

        public ActionResult Index()
        {
            var resumen = from e in db.Estudiantes
                          group e by e.FechaIngreso into grupo
                          select new Resumen()
                          {
                              FechaInscripcion = grupo.Key,
                              TotalEstudiantes = grupo.Count()
                          };

            return View(resumen);
        }

        public ActionResult About()
        {
            ViewBag.Message = "NO hay nada que decir";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Anónimo";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
