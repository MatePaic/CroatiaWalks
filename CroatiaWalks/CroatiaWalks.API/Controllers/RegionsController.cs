using AutoMapper;
using CroatiaWalks.API.Models.Domain;
using CroatiaWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CroatiaWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Vodice",
            //        Code = "Vd",
            //        Area = 1100,
            //        Lat = 13,
            //        Long = 30,
            //        Population = 8000
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Zadar",
            //        Code = "Zd",
            //        Area = 1200,
            //        Lat = 12,
            //        Long = 31,
            //        Population = 50000
            //    }
            //};

            var regions = await regionRepository.GetAllAsync();

            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //    regionsDTO.Add(regionDTO);
            //}); 

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regions);
        }
    }
}
