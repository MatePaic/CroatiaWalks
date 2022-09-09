using AutoMapper;
using CroatiaWalks.API.Models.Domain;
using CroatiaWalks.API.Models.DTO;
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            //Pass details to Repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id}, regionDTO); //201
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var deletedRegion = await regionRepository.DeleteAsync(id);

            //If null NotFound
            if (deletedRegion == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var deletedRegionDTO = new Models.DTO.Region
            {
                Id = deletedRegion.Id,
                Code = deletedRegion.Code,
                Area = deletedRegion.Area,
                Lat = deletedRegion.Lat,
                Long = deletedRegion.Long,
                Name = deletedRegion.Name,
                Population = deletedRegion.Population
            };

            //return Ok response
            return Ok(deletedRegionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain model
            var updatedRegion = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            //Update Region using repositroy
            updatedRegion = await regionRepository.UpdateAsync(id, updatedRegion);

            //If Null then NotFound
            if (updatedRegion == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var updatedRegionDTO = new Models.DTO.Region
            {
                Id = updatedRegion.Id,
                Code = updatedRegion.Code,
                Area = updatedRegion.Area,
                Lat = updatedRegion.Lat,
                Long = updatedRegion.Long,
                Name = updatedRegion.Name,
                Population = updatedRegion.Population
            };

            //Return Ok reponse
            return Ok(updatedRegionDTO);
        }
    }
}
