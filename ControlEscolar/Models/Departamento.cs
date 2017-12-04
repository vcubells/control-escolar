using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }

        [Required(ErrorMessage="El nombre del dpto es obligatorio")]
        [MaxLength(50, ErrorMessage="Longitud máxima 50 caracteres")]
        [Display(Name = "Departamento")]
        public string Nombre { get; set; }
        
        [Display(Name = "Director") ]
        public int? PersonaID { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Profesor Director { get; set; }
        public virtual ICollection<Materia> Materias { get; set; }
    }
}