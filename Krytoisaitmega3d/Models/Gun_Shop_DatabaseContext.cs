using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Krytoisaitmega3d.Models
{
    public partial class Gun_Shop_DatabaseContext : DbContext
    {
        public Gun_Shop_DatabaseContext()
        {
        }

        public Gun_Shop_DatabaseContext(DbContextOptions<Gun_Shop_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Gun> Guns { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pistol> Pistols { get; set; } = null!;
        public virtual DbSet<Pushka> Pushkas { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E-mail");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gun>(entity =>
            {
                entity.HasKey(e => e.IdGun);

                entity.ToTable("Gun");

                entity.Property(e => e.IdGun).HasColumnName("ID_Gun");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MachineId).HasColumnName("Machine_ID");

                entity.Property(e => e.NameGun)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Gun");

                entity.Property(e => e.PistolId).HasColumnName("Pistol_ID");

                entity.Property(e => e.PushkaId).HasColumnName("Pushka_ID");

                entity.Property(e => e.View)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Guns)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Machine");

                entity.HasOne(d => d.Pistol)
                    .WithMany(p => p.Guns)
                    .HasForeignKey(d => d.PistolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pistol");

                entity.HasOne(d => d.Pushka)
                    .WithMany(p => p.Guns)
                    .HasForeignKey(d => d.PushkaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pushka");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.IdMachine);

                entity.ToTable("Machine");

                entity.HasIndex(e => e.Name, "UQ_Name")
                    .IsUnique();

                entity.Property(e => e.IdMachine).HasColumnName("ID_Machine");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Order");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.GunId).HasColumnName("Gun_ID");

                entity.Property(e => e.IdOrder)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_Order");

                entity.HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client");

                entity.HasOne(d => d.Gun)
                    .WithMany()
                    .HasForeignKey(d => d.GunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gun");
            });

            modelBuilder.Entity<Pistol>(entity =>
            {
                entity.HasKey(e => e.IdPistol);

                entity.ToTable("Pistol");

                entity.HasIndex(e => e.NamePistol, "UQ_Name_Pistol")
                    .IsUnique();

                entity.Property(e => e.IdPistol).HasColumnName("ID_Pistol");

                entity.Property(e => e.NamePistol)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Pistol");
            });

            modelBuilder.Entity<Pushka>(entity =>
            {
                entity.HasKey(e => e.IdPushka);

                entity.ToTable("Pushka");

                entity.HasIndex(e => e.NamePushka, "UQ_Name_Pushka")
                    .IsUnique();

                entity.Property(e => e.IdPushka).HasColumnName("ID_Pushka");

                entity.Property(e => e.NamePushka)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Pushka");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
