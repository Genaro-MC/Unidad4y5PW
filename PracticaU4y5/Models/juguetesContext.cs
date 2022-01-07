using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PracticaU4y5.Models
{
    public partial class juguetesContext : DbContext
    {
        public juguetesContext()
        {
        }

        public juguetesContext(DbContextOptions<juguetesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dato> Datos { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Juguete> Juguetes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8");

            modelBuilder.Entity<Dato>(entity =>
            {
                entity.ToTable("datos");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Material)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("material");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Dato)
                    .HasForeignKey<Dato>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_datos especificos_juguete");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("empresa");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Juguete>(entity =>
            {
                entity.ToTable("juguete");

                entity.HasIndex(e => e.Idemp, "idemp");

                entity.Property(e => e.Idemp).HasColumnName("idemp");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdempNavigation)
                    .WithMany(p => p.Juguetes)
                    .HasForeignKey(d => d.Idemp)
                    .HasConstraintName("F1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.NombreReal)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
