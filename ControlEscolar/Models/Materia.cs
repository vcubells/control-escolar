using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControlEscolar.Models
{
    public class Materia
    {
        public int MateriaID { get; set; }
        
        [Required(ErrorMessage="El título de la materia es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage="Los créditos son obligatorios")]
        [Display(Name="Créditos")]
        [Range(3,12, ErrorMessage="Los crédtios deben estar entre 3 y 12")]
        public int Creditos { get; set; }

        [Display(Name="Departamento")]
        public int DepartamentoID { get; set; }

        public virtual ICollection<Cursa> Estudiantes { get; set; }
        public virtual ICollection<Profesor> Profesores { get; set; }
        public virtual Departamento Departamento { get; set; }

    }
}