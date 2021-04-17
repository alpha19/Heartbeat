using LabSystems.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LabSystems.Domain.Context
{
    public class LabSystemsContext : DbContext
    {
        public LabSystemsContext()
        {
        }

        public LabSystemsContext(DbContextOptions<LabSystemsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabSystem>()
                .HasMany(l => l.DiskDrives)
                .WithOne()
                .HasForeignKey(l => l.LabSystemId)
                .IsRequired(true);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<LabSystem> LabSystems { get; set; }

        public virtual DbSet<DiskDrive> DiskDrives { get; set; }
    }
}
