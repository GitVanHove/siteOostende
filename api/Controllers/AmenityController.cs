using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Amenity")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        readonly ApplicationDBContext _context;

        public AmenityController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("amenities")]
        public IActionResult GetAmenities()
        {
            var amenities = _context.Amenities.ToList();

            var amenitiesDto = amenities.Select(a => a.toAmenityDto()).ToList();
            return Ok(amenitiesDto);
        }

        [HttpGet("amenities/{amenityId}")]
        public IActionResult GetAmenitiesById([FromRoute] int amenityId)
        {
            // Correct the query: Use 'Where' to filter, not 'Include'
            var amenities = _context.Amenities
                                    .Where(a => a.AmenityId == amenityId)
                                    .Include(a => a.ApartmentAmenities) // Use Include only for related data loading
                                    .ToList();

            // Convert the result to DTOs
            var amenitiesDto = amenities.Select(a => a.toAmenityDto()).ToList();
            return Ok(amenitiesDto);
        }
    }
}