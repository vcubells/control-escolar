using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ControlEscolar.Models;
using System.Data.Entity;

namespace ControlEscolar.DAL
{
    public class ControlEscolarInitializer : DropCreateDatabaseIfModelChanges<ControlEscolarContext>
    {
        protected override void Seed(ControlEscolarContext context)
        {
            /* Estudiantes */
            var estudiantes = new List<Estudiante>
            {
                new Estudiante { Nombre = "Maria", Apellidos = "Bacallao", FechaIngreso = DateTime.Parse("2013-12-23") },
                new Estudiante { Nombre = "Juan", Apellidos = "Peréz", FechaIngreso = DateTime.Parse("2011-07-24") },
                new Estudiante { Nombre = "Mariana", Apellidos = "Rodríguez", FechaIngreso = DateTime.Parse("2012-09-22") },
                new Estudiante { Nombre = "Camilo", Apellidos = "González", FechaIngreso = DateTime.Parse("2013-08-21") },
                new Estudiante { Nombre = "Alberto", Apellidos = "Pupo", FechaIngreso = DateTime.Parse("2010-01-23") },
                new Estudiante { Nombre = "Sofia", Apellidos = "González", FechaIngreso = DateTime.Parse("2001-05-20") }
            };

            foreach (var e in estudiantes)
            {
                context.Estudiantes.Add(e);
            }

            context.SaveChanges();

            /* Profesores */

            var profesores = new List<Profesor>
            {
                new Profesor { Nombre = "Juan", Apellidos = "Del Rio", Email = "jdrio@midominio.com" },
                new Profesor { Nombre = "Elena", Apellidos = "Martínez", Email = "emartinez@midominio.com" },
                new Profesor { Nombre = "Albertina", Apellidos = "Socorro", Email = "asocorro@midominio.com" },
                new Profesor { Nombre = "Antonio", Apellidos = "Juárez", Email = "ajuarez@midominio.com" },
                new Profesor { Nombre = "Juan Alberto", Apellidos = "Castillo", Email = "jacastillo@midominio.com" }
            };

            foreach (var p in profesores)
            {
                context.Profesores.Add(p);
            }

            context.SaveChanges();

            /* Departamentos */

            var dptos = new List<Departamento>
            {
                new Departamento { Nombre ="Sistemas", PersonaID = 7},
                new Departamento { Nombre ="Ciencias Sociales", PersonaID = 8},
                new Departamento { Nombre ="Ciencias Naturales", PersonaID = 9}
            };

            foreach (var d in dptos)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();

            /* Materias */

            var materias = new List<Materia>
            {
                new Materia { Titulo = "Programación", Creditos = 5, DepartamentoID = 1, Profesores = new List<Profesor>()},
                new Materia { Titulo = "Etica", Creditos = 3, DepartamentoID = 2, Profesores = new List<Profesor>()},
                new Materia { Titulo = "Bases de Datos", Creditos = 8, DepartamentoID = 1, Profesores = new List<Profesor>()},
                new Materia { Titulo = "Matemáticas", Creditos = 3, DepartamentoID = 3, Profesores = new List<Profesor>()},
                new Materia { Titulo = "Física", Creditos = 5, DepartamentoID = 3, Profesores = new List<Profesor>()},
                new Materia { Titulo = "Historia", Creditos = 5, DepartamentoID = 2, Profesores = new List<Profesor>()}
            };

            foreach (var m in materias)
            {
                context.Materias.Add(m);
            }
            context.SaveChanges();

            /* Asociar Materias con Profesores */

            materias[0].Profesores.Add(profesores[0]);
            materias[1].Profesores.Add(profesores[1]);
            materias[2].Profesores.Add(profesores[2]);
            materias[3].Profesores.Add(profesores[3]);
            materias[4].Profesores.Add(profesores[4]);
            materias[5].Profesores.Add(profesores[1]);
            materias[0].Profesores.Add(profesores[3]);

            context.SaveChanges();

            /* Inscripciones de Estudiantes a Materias */

            var inscripciones = new List<Cursa>
            {
                new Cursa { PersonaID = 1, MateriaID = 1, Calificacion = 90 },
                new Cursa { PersonaID = 1, MateriaID = 2, Calificacion = 80 },
                new Cursa { PersonaID = 2, MateriaID = 3, Calificacion = 100 },
                new Cursa { PersonaID = 2, MateriaID = 4, Calificacion = 90 },
                new Cursa { PersonaID = 3, MateriaID = 1, Calificacion = 70 },
                new Cursa { PersonaID = 4, MateriaID = 1                    },
                new Cursa { PersonaID = 5, MateriaID = 5, Calificacion = 60 },
                new Cursa { PersonaID = 5, MateriaID = 6                    },
                new Cursa { PersonaID = 6, MateriaID = 2, Calificacion = 85 }
            };

            foreach (var i in inscripciones)
            {
                context.Inscripciones.Add(i);
            }
            context.SaveChanges();

            var oficinas = new List<Oficina>
            {
                new Oficina { Ubicacion = "Edificio A Piso 5", PersonaID = 7 },
                new Oficina { Ubicacion = "Edificio B Piso 3", PersonaID = 8 },
                new Oficina { Ubicacion = "Edificio C Piso 1", PersonaID = 9 }
            };

            foreach (var o in oficinas)
            {
                context.Oficinas.Add(o);
            }
            context.SaveChanges();
        }
    }
}