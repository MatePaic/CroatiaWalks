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

        public async Task<Region> GetAsync(Guid id)
        {
            return await cROWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await cROWalksDbContext.AddAsync(region);
            await cROWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await cROWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            //Delete the Region
            cROWalksDbContext.Regions.Remove(region);
            await cROWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await cROWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await cROWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
