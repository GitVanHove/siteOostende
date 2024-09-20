using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Buildings;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult GetAll()
        {
            var buildings = _context.Buildings.ToList();

            return Ok(buildings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var building = _context.Buildings.Find(id);

            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        [HttpPost("addBuilding")]
        public IActionResult Create([FromBody] CreateBuildingRequestDto buildingDto)
        {
            var buildingModel = buildingDto.ToBuildingFromCreateDto();
            _context.Buildings.Add(buildingModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = buildingModel.Id }, buildingModel.ToBuildingDto());
        }
    }
}