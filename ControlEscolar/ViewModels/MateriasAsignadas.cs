using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlEscolar.ViewModels
{
    public class MateriasAsignadas
    {
        public int MateriaID { get; set; }
        public string Titulo { get; set; }
        public bool Asignada { get; set; }
    }
}