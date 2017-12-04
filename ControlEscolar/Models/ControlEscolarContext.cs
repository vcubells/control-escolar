using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ControlEscolar.Models
{
    public class ControlEscolarContext : DbContext    
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Cursa> Inscripciones { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Profesor>()
                .HasOptional(p => p.Oficina)
                .WithRequired(p => p.Profesor);

            modelBuilder.Entity<Materia>()
                .HasMany(p => p.Profesores)
                .WithMany(p => p.Materias)
                .Map(t => t.MapLeftKey("MateriaID")
                    .MapRightKey("PersonaID")
                    .ToTable("ProfesorMateria"));

            modelBuilder.Entity<Departamento>()
                .HasOptional(x => x.Director);
        }

    }
}