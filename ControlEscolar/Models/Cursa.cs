using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Cursa
    {
        public int CursaID { get; set; }
        public int PersonaID { get; set; }
        public int MateriaID { get; set; }

        [Range(60,100,ErrorMessage="La nota debe estar entre 60 y 100")]
        [Display(Name="Nota")]
        [DisplayFormat(DataFormatString = "{0:###.##}", ApplyFormatInEditMode = true, NullDisplayText = "Sin calificar")]
        public decimal? Calificacion { get; set; }

        public virtual Estudiante Estudiante { get; set; }
        public virtual Materia Materia { get; set; }
    }
}