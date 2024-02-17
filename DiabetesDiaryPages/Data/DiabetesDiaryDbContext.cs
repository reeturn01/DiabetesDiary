using System;
using System.Collections.Generic;
using DiabetesDiaryPages.Models;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Data;

public partial class DiabetesDiaryDbContext : DbContext
{
    public DiabetesDiaryDbContext()
    {
    }

    public DiabetesDiaryDbContext(DbContextOptions<DiabetesDiaryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Covek> Covek { get; set; }

    public virtual DbSet<DatotekaMetapodatoci> DatotekaMetapodatoci { get; set; }

    public virtual DbSet<Dijabeticar> Dijabeticar { get; set; }

    public virtual DbSet<Doktor> Doktor { get; set; }

    public virtual DbSet<Faksimil> Faksimil { get; set; }

    public virtual DbSet<Hrana> Hrana { get; set; }

    public virtual DbSet<Insulin> Insulin { get; set; }

    public virtual DbSet<Inventar> Inventar { get; set; }

    public virtual DbSet<LentiMerenjeShekjer> LentiMerenjeShekjer { get; set; }

    public virtual DbSet<Medikament> Medikament { get; set; }

    public virtual DbSet<NacinPrigotvuvanjeHrana> NacinPrigotvuvanjeHrana { get; set; }

    public virtual DbSet<Obrok> Obrok { get; set; }

    public virtual DbSet<TipDijabetes> TipDijabetes { get; set; }

    public virtual DbSet<TipInsulin> TipInsulin { get; set; }

    public virtual DbSet<Zapis> Zapis { get; set; }

    public virtual DbSet<ZapisHrana> ZapisHrana { get; set; }

    public virtual DbSet<ZapisInsulin> ZapisInsulin { get; set; }

    public virtual DbSet<ZapisInsulinDoziranInsulin> ZapisInsulinDoziranInsulin { get; set; }

    public virtual DbSet<ZapisSoIzmerenSekjer> ZapisSoIzmerenSekjer { get; set; }

    public virtual DbSet<ZdrastvenRezultat> ZdrastvenRezultat { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Name=DiabetesDiaryPages:DataConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Covek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_covek_id");

            entity.Property(e => e.Embg).IsFixedLength();
        });

        modelBuilder.Entity<DatotekaMetapodatoci>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_datoteka_metapodatoci_id");

            entity.Property(e => e.Opis).HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<Dijabeticar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dijabeticar_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Covek).WithOne(p => p.Dijabeticar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dijabeticar_id");

            entity.HasOne(d => d.TipDijabetes).WithMany(p => p.Dijabeticar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dijabeticar_tip_dijabetes_id");
        });

        modelBuilder.Entity<Doktor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_doktor_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Covek).WithOne(p => p.Doktor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_doktor_id");
        });

        modelBuilder.Entity<Faksimil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_faksimil_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Dijabeticar).WithMany(p => p.Faksimil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dijabeticar_id");

            entity.HasOne(d => d.Doktor).WithMany(p => p.Faksimil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_doktor_id");

            entity.HasOne(d => d.DatotekaMetapodatoci).WithOne(p => p.Faksimil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_faksimil_id");
        });

        modelBuilder.Entity<Hrana>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_hrana_id");

            entity.Property(e => e.Proizvoditel).HasDefaultValueSql("''::character varying");

            entity.HasOne(d => d.NacinPrigotvuvanjeHrana).WithMany(p => p.Hrana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_hrana_nacin_prigotvuvanje_hrana_id");
        });

        modelBuilder.Entity<Insulin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Insulin_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Medikament).WithOne(p => p.Insulin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Insulin_id_Medikament_id");

            entity.HasOne(d => d.TipInsulin).WithMany(p => p.Insulin).HasConstraintName("FK_Insulin_tip_insulin_id_Tip_insulin_id");
        });

        modelBuilder.Entity<Inventar>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.MedikamentId, e.RokNaTraenje }).HasName("PK_inventar_id");

            entity.HasOne(d => d.Dijabeticar).WithMany(p => p.Inventar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dijabeticar_id");

            entity.HasOne(d => d.Medikament).WithMany(p => p.Inventar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_medikament_id");
        });

        modelBuilder.Entity<LentiMerenjeShekjer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Lenti_merenje_shekjer_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Medikament).WithOne(p => p.LentiMerenjeShekjer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lenti_merenje_shekjer_id_Medikament_id");
        });

        modelBuilder.Entity<Medikament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Medikament_id");
        });

        modelBuilder.Entity<NacinPrigotvuvanjeHrana>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Nacin_prigotvuvanje_hrana_id");
        });

        modelBuilder.Entity<Obrok>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.Data, e.HranaId }).HasName("PK_obrok_id");

            entity.HasOne(d => d.Hrana).WithMany(p => p.Obrok)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_obrok_hrana_id");

            entity.HasOne(d => d.ZapisHrana).WithMany(p => p.Obrok)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_obrok_dijabeticar");
        });

        modelBuilder.Entity<TipDijabetes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tip_dijabetes_id");
        });

        modelBuilder.Entity<TipInsulin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tip_insulin_id");
        });

        modelBuilder.Entity<Zapis>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.Data }).HasName("PK_zapis_id");

            entity.HasOne(d => d.Dijabeticar).WithMany(p => p.Zapis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_id");
        });

        modelBuilder.Entity<ZapisHrana>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.Data }).HasName("PK_zapis_hrana_id");

            entity.HasOne(d => d.Zapis).WithOne(p => p.ZapisHrana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_hrana_id");
        });

        modelBuilder.Entity<ZapisInsulin>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.Data }).HasName("PK_zapis_insulin_id");

            entity.HasOne(d => d.Zapis).WithOne(p => p.ZapisInsulin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_insulin_id");
        });

        modelBuilder.Entity<ZapisInsulinDoziranInsulin>(entity =>
        {
            entity.HasOne(d => d.Insulin).WithMany(p => p.ZapisInsulinDoziranInsulin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_insulin_doziran_insulin_id");

            entity.HasOne(d => d.ZapisInsulin).WithMany(p => p.ZapisInsulinDoziranInsulin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_insulin_doziran_insulin");
        });

        modelBuilder.Entity<ZapisSoIzmerenSekjer>(entity =>
        {
            entity.HasKey(e => new { e.DijabeticarId, e.Data }).HasName("PK_zapis_so_izmeren_sekjer_id");

            entity.HasOne(d => d.Zapis).WithOne(p => p.ZapisSoIzmerenSekjer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapis_so_izmeren_sekjer_id");
        });

        modelBuilder.Entity<ZdrastvenRezultat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_zdrastven_rezultat_id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Dijabeticar).WithMany(p => p.ZdrastvenRezultat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zdrastven_rezultat_dijabeticar_id");

            entity.HasOne(d => d.DatotekaMetapodatoci).WithOne(p => p.ZdrastvenRezultat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zdrastven_rezultat_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
