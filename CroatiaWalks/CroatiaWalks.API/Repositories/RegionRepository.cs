using CroatiaWalks.API.Data;
using CroatiaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CroatiaWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly CROWalksDbContext cROWalksDbContext;

        public RegionRepository(CROWalksDbContext cROWalksDbContext)
        {
            this.cROWalksDbContext = cROWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await cROWalksDbContext.Regions.ToListAsync();
        }
    }
}
