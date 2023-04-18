using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GIO.Models
{
    public partial class GIOContext : DbContext
    {
        public GIOContext()
        {
        }

        public GIOContext(DbContextOptions<GIOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingStatus> BookingStatuses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DogName> DogNames { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Haulier> Hauliers { get; set; }
        public virtual DbSet<Scan> Scans { get; set; }
        public virtual DbSet<ScanError> ScanErrors { get; set; }
        public virtual DbSet<ScanStatus> ScanStatuses { get; set; }
        public virtual DbSet<Trailer> Trailers { get; set; }
        public virtual DbSet<TrailerType> TrailerTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=tcp:pidpdapldnsazr.database.windows.net,1433;Initial Catalog=GIO;Integrated Security=False;User ID=Piotr;Password=IAmNotADog2022;Pooling=true;min pool size=1;connection timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings", "Transactional");

                entity.HasIndex(e => e.BookingStatusId, "IX_BookingStatus");

                entity.HasIndex(e => e.DriverId, "IX_Drivers");

                entity.HasIndex(e => e.HaulierId, "IX_Hauliers");

                entity.HasIndex(e => e.TrailerId, "IX_Trailers");

                entity.HasIndex(e => e.VehicleId, "IX_Vehicles");

                entity.Property(e => e.BookingWindowFrom).HasColumnType("smalldatetime");

                entity.Property(e => e.BookingWindowTo).HasColumnType("smalldatetime");

                entity.Property(e => e.CreatedOn).HasColumnType("smalldatetime");

                entity.Property(e => e.CustomerRef)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_BookingStatuses");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Drivers");

                entity.HasOne(d => d.Haulier)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.HaulierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Hauliers");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TrailerId)
                    .HasConstraintName("FK_Bookings_Trailers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Vehicles");
            });

            modelBuilder.Entity<BookingStatus>(entity =>
            {
                entity.ToTable("BookingStatuses", "Administration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Countries", "Administration");

                entity.Property(e => e.CountryCode2)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.CountryCode3)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DogName>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Drivers", "Administration");

                entity.HasIndex(e => e.HaulierId, "IX_Hauliers");

                entity.Property(e => e.ManagerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Haulier)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.HaulierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drivers_Hauliers");
            });

            modelBuilder.Entity<Haulier>(entity =>
            {
                entity.ToTable("Hauliers", "Administration");

                entity.HasIndex(e => e.CountryId, "IX_Countries");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AddressLine2).HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Hauliers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hauliers_Countries");
            });

            modelBuilder.Entity<Scan>(entity =>
            {
                entity.ToTable("Scans", "Transactional");

                entity.HasIndex(e => e.BookingId, "IX_Bookings");

                entity.HasIndex(e => e.ScanErrorId, "IX_ScanErrors");

                entity.HasIndex(e => e.ScanStatusId, "IX_ScanStatuses");

                entity.HasIndex(e => e.VehicleId, "IX_Vehicles");

                entity.Property(e => e.CreatedOn).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Scans_Bookings");

                entity.HasOne(d => d.ScanError)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.ScanErrorId)
                    .HasConstraintName("FK_Scans_ScanErrors");

                entity.HasOne(d => d.ScanStatus)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.ScanStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scans_ScanStatuses");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Scans_Vehicles");
            });

            modelBuilder.Entity<ScanError>(entity =>
            {
                entity.ToTable("ScanErrors", "Administration");

                entity.Property(e => e.ErrorDescription)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ScanStatus>(entity =>
            {
                entity.ToTable("ScanStatuses", "Administration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.ToTable("Trailers", "Administration");

                entity.HasIndex(e => e.HaulierId, "IX_Hauliers");

                entity.HasIndex(e => e.TrailerTypeId, "IX_VehicleTypes");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Haulier)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.HaulierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trailers_Hauliers");

                entity.HasOne(d => d.TrailerType)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.TrailerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trailers_TrailerTypes");
            });

            modelBuilder.Entity<TrailerType>(entity =>
            {
                entity.ToTable("TrailerTypes", "Administration");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Administration");

                entity.HasIndex(e => e.UserRoleId, "IX_Users");

                entity.HasIndex(e => e.UserEmail, "UX_UserEmail")
                    .IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserManager)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles", "Administration");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicles", "Administration");

                entity.HasIndex(e => e.LastTrailerId, "IX_Trailers");

                entity.HasIndex(e => e.VehicleTypeId, "IX_VehicleTypes");

                entity.Property(e => e.LastSeen).HasColumnType("smalldatetime");

                entity.Property(e => e.RegPlate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.LastTrailer)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.LastTrailerId)
                    .HasConstraintName("FK_Vehicles_Trailers");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleTypes");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleTypes", "Administration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
