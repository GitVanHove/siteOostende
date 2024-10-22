using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Buildings;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/building")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public BuildingController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var buildings = await _context.Buildings.ToListAsync();

            return Ok(buildings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound("Building was not found");
            }

            return Ok(building);
        }

        [HttpPost("addBuilding")]
        public async Task<IActionResult> Create([FromBody] CreateBuildingRequestDto buildingDto)
        {
            var buildingModel = buildingDto.ToBuildingFromCreateDto();
            await _context.Buildings.AddAsync(buildingModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = buildingModel.Id }, buildingModel.ToBuildingDto());
        }

        [HttpPut]
        [Route("deleteBuilding/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBuildingRequestDto updateDto)
        {
            var buildingModel = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);

            if(buildingModel == null)
            {
                return NotFound("Building not found.");
            }

            buildingModel.Address = updateDto.Address;
            buildingModel.NumberOfFloors = updateDto.NumberOfFloors;
            buildingModel.DateBuilt = updateDto.DateBuilt;
            buildingModel.IsHouse = updateDto.IsHouse;

           await _context.SaveChangesAsync();

            return Ok(buildingModel.ToBuildingDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var buildingModel = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);

            if(buildingModel == null)
            {
                return NotFound("Building not found or doesn't exsist.");
            }

            _context.Buildings.Remove(buildingModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}