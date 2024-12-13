using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PhoenixDataAccess.Models
{
    public partial class PhoenixContext : DbContext
    {
        public PhoenixContext()
        {
        }

        public PhoenixContext(DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Guest> Guests { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomInventory> RoomInventories { get; set; } = null!;
        public virtual DbSet<RoomService> RoomServices { get; set; } = null!;

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseSqlServer("Data Source=LAPTOP-V8LGUHRO;Initial Catalog=Phoenix;Trusted_Connection=True;User=basilisktf;Password=basilisktf;TrustServerCertificate=True");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job_title");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.HasIndex(e => e.IdNumber, "UQ_Guests")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("citizenship");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_number");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.BookDate)
                    .HasColumnType("datetime")
                    .HasColumnName("book_date");

                entity.Property(e => e.CheckIn)
                    .HasColumnType("datetime")
                    .HasColumnName("check_in");

                entity.Property(e => e.CheckOut)
                    .HasColumnType("datetime")
                    .HasColumnName("check_out");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.GuestUsername)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("guest_username");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.ReservationMethod)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("reservation_method");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("room_number");

                entity.HasOne(d => d.GuestUsernameNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.GuestUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationsGuests");

                entity.HasOne(d => d.RoomNumberNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationsRooms");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.GuestLimit).HasColumnName("guest_limit");

                entity.Property(e => e.RoomType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("room_type");
            });

            modelBuilder.Entity<RoomInventory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InventoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("inventory_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("room_number");

                entity.HasOne(d => d.InventoryNameNavigation)
                    .WithMany(p => p.RoomInventories)
                    .HasForeignKey(d => d.InventoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomInventoriesInventories");

                entity.HasOne(d => d.RoomNumberNavigation)
                    .WithMany(p => p.RoomInventories)
                    .HasForeignKey(d => d.RoomNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomInventoriesRooms");
            });

            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("employee_number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.FridayRosterFinish).HasColumnName("friday_roster_finish");

                entity.Property(e => e.FridayRosterStart).HasColumnName("friday_roster_start");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.MondayRosterFinish).HasColumnName("monday_roster_finish");

                entity.Property(e => e.MondayRosterStart).HasColumnName("monday_roster_start");

                entity.Property(e => e.OutsourcingCompany)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("outsourcing_company");

                entity.Property(e => e.SaturdayRosterFinish).HasColumnName("saturday_roster_finish");

                entity.Property(e => e.SaturdayRosterStart).HasColumnName("saturday_roster_start");

                entity.Property(e => e.SundayRosterFinish).HasColumnName("sunday_roster_finish");

                entity.Property(e => e.SundayRosterStart).HasColumnName("sunday_roster_start");

                entity.Property(e => e.ThursdayRosterFinish).HasColumnName("thursday_roster_finish");

                entity.Property(e => e.ThursdayRosterStart).HasColumnName("thursday_roster_start");

                entity.Property(e => e.TuesdayRosterFinish).HasColumnName("tuesday_roster_finish");

                entity.Property(e => e.TuesdayRosterStart).HasColumnName("tuesday_roster_start");

                entity.Property(e => e.WednesdayRosterFinish).HasColumnName("wednesday_roster_finish");

                entity.Property(e => e.WednesdayRosterStart).HasColumnName("wednesday_roster_start");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
