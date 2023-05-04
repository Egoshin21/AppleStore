using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppleStore.Models
{
    public partial class aegoshinContext : DbContext
    {
        public aegoshinContext()
        {
        }

        public aegoshinContext(DbContextOptions<aegoshinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chek> Cheks { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Manufacture> Manufactures { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tovar> Tovars { get; set; } = null!;
        public virtual DbSet<TovarSale> TovarSales { get; set; } = null!;
        public virtual DbSet<TovarType> TovarTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=home.kolei.ru;database=aegoshin;uid=aegoshin;password=090903", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Chek>(entity =>
            {
                entity.HasKey(e => new { e.IdChek, e.TovarsIdTovars })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Chek");

                entity.HasIndex(e => e.TovarsIdTovars, "fk_Chek_Tovars1_idx");

                entity.Property(e => e.IdChek)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idChek");

                entity.Property(e => e.TovarsIdTovars).HasColumnName("Tovars_idTovars");

                entity.HasOne(d => d.TovarsIdTovarsNavigation)
                    .WithMany(p => p.Cheks)
                    .HasForeignKey(d => d.TovarsIdTovars)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Chek_Tovars1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClients)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdClients).HasColumnName("idClients");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fio)
                    .HasMaxLength(100)
                    .HasColumnName("FIO");

                entity.Property(e => e.Phone).HasMaxLength(45);
            });

            modelBuilder.Entity<Manufacture>(entity =>
            {
                entity.HasKey(e => e.IdManufacture)
                    .HasName("PRIMARY");

                entity.ToTable("Manufacture");

                entity.Property(e => e.IdManufacture).HasColumnName("idManufacture");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");

                entity.ToTable("Role");

                entity.Property(e => e.IdRole)
                    .ValueGeneratedNever()
                    .HasColumnName("idRole");

                entity.Property(e => e.Role1)
                    .HasMaxLength(45)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<Tovar>(entity =>
            {
                entity.HasKey(e => e.IdTovars)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ManufactureIdManufacture, "fk_Tovars_Manufacture1_idx");

                entity.HasIndex(e => e.TovarTypeIdTovarType, "fk_Tovars_TovarType1_idx");

                entity.Property(e => e.IdTovars).HasColumnName("idTovars");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.ManufactureIdManufacture).HasColumnName("Manufacture_idManufacture");

                entity.Property(e => e.Price).HasPrecision(10, 2);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.TovarTypeIdTovarType).HasColumnName("TovarType_idTovarType");

                entity.HasOne(d => d.ManufactureIdManufactureNavigation)
                    .WithMany(p => p.Tovars)
                    .HasForeignKey(d => d.ManufactureIdManufacture)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tovars_Manufacture1");

                entity.HasOne(d => d.TovarTypeIdTovarTypeNavigation)
                    .WithMany(p => p.Tovars)
                    .HasForeignKey(d => d.TovarTypeIdTovarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tovars_TovarType1");
            });

            modelBuilder.Entity<TovarSale>(entity =>
            {
                entity.HasKey(e => new { e.IdTovarSale, e.TovarsIdTovars, e.ClientsIdClients, e.UsersIdUser })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("TovarSale");

                entity.HasIndex(e => e.ClientsIdClients, "fk_TovarSale_Clients1_idx");

                entity.HasIndex(e => e.TovarsIdTovars, "fk_TovarSale_Tovars1_idx");

                entity.HasIndex(e => e.UsersIdUser, "fk_TovarSale_Users1_idx");

                entity.Property(e => e.IdTovarSale)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idTovarSale");

                entity.Property(e => e.TovarsIdTovars).HasColumnName("Tovars_idTovars");

                entity.Property(e => e.ClientsIdClients).HasColumnName("Clients_idClients");

                entity.Property(e => e.UsersIdUser).HasColumnName("Users_idUser");

                entity.Property(e => e.DateSale).HasColumnType("datetime");

                entity.HasOne(d => d.ClientsIdClientsNavigation)
                    .WithMany(p => p.TovarSales)
                    .HasForeignKey(d => d.ClientsIdClients)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TovarSale_Clients1");

                entity.HasOne(d => d.TovarsIdTovarsNavigation)
                    .WithMany(p => p.TovarSales)
                    .HasForeignKey(d => d.TovarsIdTovars)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TovarSale_Tovars1");

                entity.HasOne(d => d.UsersIdUserNavigation)
                    .WithMany(p => p.TovarSales)
                    .HasForeignKey(d => d.UsersIdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TovarSale_Users1");
            });

            modelBuilder.Entity<TovarType>(entity =>
            {
                entity.HasKey(e => e.IdTovarType)
                    .HasName("PRIMARY");

                entity.ToTable("TovarType");

                entity.Property(e => e.IdTovarType).HasColumnName("idTovarType");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RoleIdRole, "fk_Users_Role_idx");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Login).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.RoleIdRole).HasColumnName("Role_idRole");

                entity.Property(e => e.User1)
                    .HasMaxLength(45)
                    .HasColumnName("User");

                entity.HasOne(d => d.RoleIdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleIdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
