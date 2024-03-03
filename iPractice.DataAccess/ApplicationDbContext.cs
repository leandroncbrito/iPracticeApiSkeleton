using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Psychologist>().HasKey(psychologist => psychologist.Id);
            modelBuilder.Entity<Client>().HasKey(client => client.Id);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Clients).WithMany(b => b.Psychologists);
            modelBuilder.Entity<Client>().HasMany(p => p.Psychologists).WithMany(b => b.Clients);
            
            modelBuilder.Entity<TimeSlot>().HasKey(timeSlot => timeSlot.Id);
            modelBuilder.Entity<Psychologist>().HasMany(psychologist => psychologist.TimeSlots).WithOne(a => a.Psychologist);
        }
    }
}
