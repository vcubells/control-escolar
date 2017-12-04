using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Estudiante : Persona
    {

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [Display(Name = "Fecha de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }

        public virtual ICollection<Cursa> Materias { get; set; }

    }
}