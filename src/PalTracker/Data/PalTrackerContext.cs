using Microsoft.EntityFrameworkCore;

namespace PalTracker.Data
{
    public class PalTrackerContext : DbContext
    {
        public PalTrackerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }
    }
}