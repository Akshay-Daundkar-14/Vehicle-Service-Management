using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } 
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRepresentative> ServiceRepresentatives { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<ServiceRecordItem> ServiceRecordItems { get; set; }
        public DbSet<ScheduledService> ScheduledServices { get; set; }
    }
}
