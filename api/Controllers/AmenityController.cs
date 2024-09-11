using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("amenities/{amenityId}")]
        public IActionResult GetAmenitiesById([FromRoute] int amenityId)
        {
            return Ok();
        }
    }
}