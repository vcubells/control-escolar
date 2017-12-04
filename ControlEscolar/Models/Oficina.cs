using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Oficina
    {
        [Key]
        public int PersonaID { get; set; }

        [MaxLength(50, ErrorMessage="La ubicación no puede contener más de 50 caracteres")]
        [Display(Name="Ubicación")]
        public string Ubicacion { get; set; }

        public virtual Profesor Profesor { get; set; }
    }
}