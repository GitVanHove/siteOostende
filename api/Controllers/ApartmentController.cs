using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos.Apartments;
using api.Mappers;


namespace api.Controllers
{
    [Route("api/building")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ApartmentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("apartments/{buildingId}")]
        public IActionResult GetApartmentsByBuildingId([FromRoute] int buildingId)
        {
            // First, fetch the apartments from the database.
            var apartments = _context.Apartments
                                     .Where(a => a.BuildingId == buildingId)
                                     .ToList();

            // Then, convert each apartment to a DTO.
            var apartmentDtos = apartments.Select(a => a.ToApartmentDto()).ToList();

            return Ok(apartmentDtos);
        }


        [HttpGet("apartment/{id}")]
        public IActionResult GetApartmentById([FromRoute] int id)
        {
            var apartment = _context.Apartments
                                    .Include(a => a.Building)   // Include related building if needed
                                    .FirstOrDefault(a => a.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(apartment.ToApartmentDto());
        }

    }

}