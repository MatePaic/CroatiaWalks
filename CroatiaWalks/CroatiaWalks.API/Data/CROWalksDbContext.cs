using CroatiaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CroatiaWalks.API.Data
{
    public class CROWalksDbContext:DbContext
    {
        public CROWalksDbContext(DbContextOptions<CROWalksDbContext> options): base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
