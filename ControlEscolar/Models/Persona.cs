using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Persona
    {
        public int PersonaID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede ser mayor a 50 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(50, ErrorMessage = "Los apellidos no pueden ser mayor a 50 caracteres")]
        public string Apellidos { get; set; }

        [Display(Name = "Nombre completo")]
        public string NombreCompleto
        {
            get
            {
                return Apellidos + ", " + Nombre;
            }
        }
    }
}