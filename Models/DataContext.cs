using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace itec420.Models;

public class DataContext : IdentityDbContext<AppUser,AppRole,int>
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }
    public DbSet<Department> Department { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Department>().HasData(
            new List<Department>{
                new Department() {
                    Id = 1,
                    DepartmentName="Cardiology",
                    DepartmentDescription="A"
                },
                new Department() {
                    Id = 2,
                    DepartmentName="Neurology",
                    DepartmentDescription="B"
                },
                new Department() {
                    Id = 3,
                    DepartmentName="Oncology",
                    DepartmentDescription="C"
                },
                new Department() {
                    Id = 4,
                    DepartmentName="Dermatology",
                    DepartmentDescription="D"
                }
                }
        );
        modelBuilder.Entity<Doctor>().HasData(
            new List<Doctor>{
                new Doctor() { Id = 1, DoctorName="Michael Jordan", DoctorDepartment="Cardiology", PicOfDoc="/img/1.png"},
                new Doctor() { Id = 2, DoctorName="Jalen Brunson", DoctorDepartment="Neurology", PicOfDoc="/img/2.png"},
                new Doctor() { Id = 3, DoctorName="Lebron James", DoctorDepartment="Cardiology", PicOfDoc="/img/3.png"}
                }
        );
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.AppUser)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.AppUser)
            .WithMany()
            .HasForeignKey(a => a.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}