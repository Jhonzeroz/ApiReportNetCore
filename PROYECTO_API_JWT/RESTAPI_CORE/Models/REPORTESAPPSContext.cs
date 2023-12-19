using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReportApis.Models
{
    public partial class REPORTESAPPSContext : DbContext
    {
        public REPORTESAPPSContext()
        {
        }

        public REPORTESAPPSContext(DbContextOptions<REPORTESAPPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<EstadoReporte> EstadoReportes { get; set; }
        public virtual DbSet<Reporte> Reportes { get; set; }
        public virtual DbSet<TipoReporte> TipoReportes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__ESTADOS__241E2E0134015D1A");

                entity.ToTable("ESTADOS");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_ESTADO");
            });

            modelBuilder.Entity<EstadoReporte>(entity =>
            {
                entity.HasKey(e => e.IdEstreporte)
                    .HasName("PK__ESTADO_R__03B674278B897C39");

                entity.ToTable("ESTADO_REPORTES");

                entity.Property(e => e.IdEstreporte).HasColumnName("ID_ESTREPORTE");

                entity.Property(e => e.NombreEstreporte)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_ESTREPORTE");
            });

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.HasKey(e => e.IdReporte)
                    .HasName("PK__REPORTES__11AC85965940E5BF");

                entity.ToTable("REPORTES");

                entity.Property(e => e.IdReporte).HasColumnName("ID_REPORTE");

                entity.Property(e => e.EstadoReporte).HasColumnName("ESTADO_REPORTE");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("OBSERVACION");

                entity.Property(e => e.TipoReporte).HasColumnName("TIPO_REPORTE");

                entity.Property(e => e.UserReporte).HasColumnName("USER_REPORTE");

                entity.HasOne(d => d.OEstadoReporte)
                    .WithMany(p => p.Reportes)
                    .HasForeignKey(d => d.EstadoReporte)
                    .HasConstraintName("FK__REPORTES__ESTADO__46E78A0C");

                entity.HasOne(d => d.OTipoReporte)
                    .WithMany(p => p.Reportes)
                    .HasForeignKey(d => d.TipoReporte)
                    .HasConstraintName("FK__REPORTES__TIPO_R__45F365D3");

                entity.HasOne(d => d.OUserReporte)
                    .WithMany(p => p.OInverseUserReporte)
                    .HasForeignKey(d => d.UserReporte)
                    .HasConstraintName("FK__REPORTES__USER_R__44FF419A");
            });

            modelBuilder.Entity<TipoReporte>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__TIPO_REP__DB4DE8AB0E143C82");

                entity.ToTable("TIPO_REPORTE");

                entity.Property(e => e.IdTipo).HasColumnName("ID_TIPO");

                entity.Property(e => e.NombreTipo)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_TIPO");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USUARIOS__95F484402281796D");

                entity.ToTable("USUARIOS");

                entity.HasIndex(e => e.CorreoUser, "UQ__USUARIOS__6FDEEF119D3AA0BB")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.CorreoUser)
                    .HasMaxLength(255)
                    .HasColumnName("CORREO_USER");

                entity.Property(e => e.EstadoUser).HasColumnName("ESTADO_USER");

                entity.Property(e => e.NombreUser)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_USER");

                entity.Property(e => e.PassUser)
                    .HasMaxLength(255)
                    .HasColumnName("PASS_USER");

                entity.HasOne(d => d.EstadoUserNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.EstadoUser)
                    .HasConstraintName("FK__USUARIOS__ESTADO__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
