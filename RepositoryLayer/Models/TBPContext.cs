using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepositoryLayer.Models
{
    public partial class TBPContext : DbContext
    {
        public TBPContext()
        {
        }

        public TBPContext(DbContextOptions<TBPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetilSpr> DetilSpr { get; set; }
        public virtual DbSet<HeaderSpr> HeaderSpr { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Proyek> Proyek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetilSpr>(entity =>
            {
                entity.ToTable("Detil_SPR");

                entity.Property(e => e.TanggalRencanaDiterima).HasColumnType("datetime");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Volume)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HeaderSpr>(entity =>
            {
                entity.ToTable("Header_SPR");

                entity.Property(e => e.KodeSpr)
                    .HasColumnName("KodeSPR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LokasiPeminta)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NamaPenyetuju1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NamaPenyetuju2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Peminta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TanggalMinta).HasColumnType("datetime");

                entity.Property(e => e.TujuanSpr)
                    .HasColumnName("TujuanSPR")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Material1)
                    .HasColumnName("Material")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyek>(entity =>
            {
                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Proyek1).HasColumnName("Proyek");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
