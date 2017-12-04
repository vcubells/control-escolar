using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Profesor : Persona
    {

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email no es válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Materia> Materias { get; set; }
        public virtual Oficina Oficina { get; set; }

    }
}