using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ControlEscolar.Models;

namespace ControlEscolar.ViewModels
{
    public class ProfesorDetalle
    {
        public IEnumerable<Profesor> Profesores { get; set; }
        public IEnumerable<Materia> Materias { get; set; }
        public IEnumerable<Cursa> Estudiantes { get; set; }
    }
}