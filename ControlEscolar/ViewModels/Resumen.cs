using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.ViewModels
{
    public class Resumen
    {
        [Display(Name = "Fecha de Inscripción")]
        [DisplayFormat(DataFormatString="{0:d}")]
        public DateTime FechaInscripcion { get; set; }

        [Display(Name = "Estudiantes inscritos")]
        public int TotalEstudiantes { get; set; }
    }
}