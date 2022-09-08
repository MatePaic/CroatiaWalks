using CroatiaWalks.API.Models.Domain;

namespace CroatiaWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
