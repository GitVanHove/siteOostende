using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;


namespace api.Controllers
{
    [Route("api/apartment")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ApartmentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("building/{buildingId}")]
        public IActionResult GetApartmentsByBuildingId([FromRoute] int buildingId)
        {
            var apartments = _context.Apartments
                                     .Where(a => a.BuildingId == buildingId)
                                     .ToList();

            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public IActionResult GetApartmentById([FromRoute] int id)
        {
            var apartment = _context.Apartments
                                    .Include(a => a.Building) // Include related building if needed
                                    .FirstOrDefault(a => a.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(apartment);
        }

    }

}