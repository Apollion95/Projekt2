using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Projekt2v2.ScaffoldModel
{
    public partial class WypozyczalniaFilmowDBContext : DbContext
    {
        public WypozyczalniaFilmowDBContext()
        {
        }

        public WypozyczalniaFilmowDBContext(DbContextOptions<WypozyczalniaFilmowDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dostepnosc> Dostepnoscs { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<KontaktKlient> KontaktKlients { get; set; }
        public virtual DbSet<Pracownik> Pracowniks { get; set; }
        public virtual DbSet<Wynajem> Wynajems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BR95J8J\\SQLEXPRESS;Initial Catalog=WypozyczalniaFilmowDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Dostepnosc>(entity =>
            {
                entity.HasKey(e => e.IdNosnika);

                entity.ToTable("dostepnosc");

                entity.Property(e => e.IdNosnika)
                    .HasMaxLength(10)
                    .HasColumnName("ID_Nosnika")
                    .IsFixedLength(true);

                entity.Property(e => e.IlośćDostępnychKopii)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TypNosnika)
                    .HasMaxLength(40)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.IdFilmu);

                entity.ToTable("Film");

                entity.Property(e => e.IdFilmu)
                    .HasMaxLength(10)
                    .HasColumnName("ID_filmu")
                    .IsFixedLength(true);

                entity.Property(e => e.CenaZaDobe)
                    .HasMaxLength(40)
                    .IsFixedLength(true);

                entity.Property(e => e.Gatunek)
                    .HasMaxLength(40)
                    .IsFixedLength(true);

                entity.Property(e => e.IdNosnika)
                    .HasMaxLength(10)
                    .HasColumnName("ID_Nosnika")
                    .IsFixedLength(true);

                entity.Property(e => e.Nazwa)
                    .HasMaxLength(40)
                    .IsFixedLength(true);

                entity.Property(e => e.Wydawca)
                    .HasMaxLength(40)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdNosnikaNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.IdNosnika)
                    .HasConstraintName("FK_Film_dostepnosc");
            });

            modelBuilder.Entity<KontaktKlient>(entity =>
            {
                entity.HasKey(e => e.IdKlienta);

                entity.ToTable("Kontakt_Klient");

                entity.Property(e => e.IdKlienta)
                    .HasMaxLength(50)
                    .HasColumnName("id_klienta");

                entity.Property(e => e.Haslo).HasMaxLength(50);

                entity.Property(e => e.Imie).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Nazwa).HasMaxLength(50);

                entity.Property(e => e.Nazwisko).HasMaxLength(50);

                entity.Property(e => e.NumerTelefonu)
                    .HasMaxLength(50)
                    .HasColumnName("numer_telefonu");
            });

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.HasKey(e => e.IdPracownik);

                entity.ToTable("Pracownik");

                entity.Property(e => e.IdPracownik)
                    .HasMaxLength(10)
                    .HasColumnName("ID_Pracownik")
                    .IsFixedLength(true);

                entity.Property(e => e.ImiePracownik)
                    .HasMaxLength(40)
                    .HasColumnName("Imie_Pracownik")
                    .IsFixedLength(true);

                entity.Property(e => e.NazwiskoPracownik)
                    .HasMaxLength(40)
                    .HasColumnName("Nazwisko_Pracownik")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(40)
                    .HasColumnName("telefon")
                    .IsFixedLength(true);

                entity.Property(e => e.Wiek)
                    .HasMaxLength(40)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Wynajem>(entity =>
            {
                entity.HasKey(e => e.IdWypozyczenia);

                entity.ToTable("Wynajem");

                entity.Property(e => e.IdWypozyczenia)
                    .HasMaxLength(10)
                    .HasColumnName("ID_Wypozyczenia")
                    .IsFixedLength(true);

                entity.Property(e => e.DataWypozyczenia).HasColumnType("date");

                entity.Property(e => e.DataZwrotu).HasColumnType("date");

                entity.Property(e => e.IdFilmu)
                    .HasMaxLength(10)
                    .HasColumnName("ID_filmu")
                    .IsFixedLength(true);

                entity.Property(e => e.IdKlienta)
                    .HasMaxLength(50)
                    .HasColumnName("id_klienta");

                entity.HasOne(d => d.IdFilmuNavigation)
                    .WithMany(p => p.Wynajems)
                    .HasForeignKey(d => d.IdFilmu)
                    .HasConstraintName("FK_Wynajem_Film");

                entity.HasOne(d => d.IdKlientaNavigation)
                    .WithMany(p => p.Wynajems)
                    .HasForeignKey(d => d.IdKlienta)
                    .HasConstraintName("FK_Wynajem_Kontakt_Klient");

                entity.HasOne(d => d.IdWypozyczeniaNavigation)
                    .WithOne(p => p.Wynajem)
                    .HasForeignKey<Wynajem>(d => d.IdWypozyczenia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wynajem_Pracownik");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
