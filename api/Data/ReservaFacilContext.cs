using Microsoft.EntityFrameworkCore;
using User.Model;
using Suite.Model;
using Motel.Model;
using SuiteType.Model;
using Reservation.Model;

namespace Reserva.Facil.Context;

public class ReservaFacilContext : DbContext
{
  public DbSet<UserModel> Users { get; set; }
  public DbSet<SuiteModel> Suites { get; set; }
  public DbSet<MotelModel> Motels { get; set; }
  public DbSet<SuiteTypeModel> SuiteTypes { get; set; }
  public DbSet<ReservationModel> Reservations { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=user.sqlite");
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<SuiteModel>()
        .HasOne(s => s.Motel)
        .WithMany(m => m.Suites)
        .HasForeignKey(s => s.IdMotel);

    modelBuilder.Entity<SuiteModel>()
        .HasOne(s => s.SuiteType)
        .WithMany(st => st.Suites)
        .HasForeignKey(s => s.IdTypeSuite);

    modelBuilder.Entity<ReservationModel>()
        .HasOne(r => r.User)
        .WithMany()
        .HasForeignKey(r => r.IdUser);

    modelBuilder.Entity<ReservationModel>()
        .HasOne(r => r.Suite)
        .WithMany()
        .HasForeignKey(r => r.IdSuite);

    modelBuilder.Entity<ReservationModel>()
        .HasOne(r => r.Motel)
        .WithMany()
        .HasForeignKey(r => r.IdMotel);

    base.OnModelCreating(modelBuilder);
  }
}
