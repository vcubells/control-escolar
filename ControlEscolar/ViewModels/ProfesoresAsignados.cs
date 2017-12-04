using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlEscolar.ViewModels
{
    public class ProfesoresAsignados
    {
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public bool Asignado { get; set; }
    }
}