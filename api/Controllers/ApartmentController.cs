using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos.Apartments;
using api.Mappers;
using api.Dtos.Buildings;
using api.Models;


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
                            .Where(a => a.ApartmentId == id)
                            .Include(a => a.ApartmentAmenities)
                                .ThenInclude(aa => aa.Amenity)
                            .FirstOrDefault();

            if (apartment == null)
            {
                return NotFound(); // Return 404 if the apartment isn't found
            }

            var apartmentDto = apartment.ToApartmentDto();

            return Ok(apartmentDto);
        }

        [HttpPost("addApartment")]
        public IActionResult Create([FromBody] CreateApartmentDto apartmentDto)
        {
            var buildingId = _context.Buildings.Find(apartmentDto.BuildingId);
            if (buildingId == null)
            {
                return NotFound("Building id not found");
            }

            var apartmentModel = apartmentDto.toCreateApartmentDto();
            _context.Apartments.Add(apartmentModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetApartmentById), new { apartmentId = apartmentModel.ApartmentId }, apartmentModel.ToApartmentDto());

        }


        [HttpPut]
        [Route("apartment/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateApartmentRequestDto updateDto)
        {
            var apartment = _context.Apartments
                            .Include(a => a.ApartmentAmenities)
                            .ThenInclude(aa => aa.Amenity) // Ensure the related Amenity is included
                            .FirstOrDefault(a => a.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound($"Apartment with ID {id} not found.");
            }

            // Update apartment properties
            apartment.ApartmentNumber = updateDto.ApartmentNumber;
            apartment.FloorNumber = updateDto.FloorNumber;
            apartment.Description = updateDto.Description;
            apartment.MaxOccupancy = updateDto.MaxOccupancy;
            apartment.PricePerNight = updateDto.PricePerNight;
            apartment.IsAvailable = updateDto.IsAvailable;

            // Handle amenities
            foreach (var amenityDto in updateDto.ApartmentAmenities)
            {
                // Check if the amenity already exists
                var existingAmenity = apartment.ApartmentAmenities
                                              .FirstOrDefault(a => a.AmenityId == amenityDto.AmenityId);

                if (existingAmenity != null)
                {
                    // Update the existing amenity's amount
                    existingAmenity.Amount = amenityDto.Amount;
                }
                else
                {
                    // Fetch the Amenity from the database (to avoid issues with empty names)
                    var amenityFromDb = _context.Amenities.FirstOrDefault(a => a.AmenityId == amenityDto.AmenityId);
                    if (amenityFromDb == null)
                    {
                        return BadRequest($"Amenity with ID {amenityDto.AmenityId} not found.");
                    }

                    // Add the new apartmentamenity
                    apartment.ApartmentAmenities.Add(new ApartmentAmenity
                    {
                        AmenityId = amenityFromDb.AmenityId,
                        Amount = amenityDto.Amount,
                        Amenity = amenityFromDb
                    });
                }
            }

            // Remove any apartmentamenities that are not in the updateDto
            var amenitiesToRemove = apartment.ApartmentAmenities
                                              .Where(a => !updateDto.ApartmentAmenities
                                              .Any(ua => ua.AmenityId == a.AmenityId))
                                              .ToList();

            foreach (var amenity in amenitiesToRemove)
            {
                apartment.ApartmentAmenities.Remove(amenity);
            }

            // Save changes
            _context.SaveChanges();

            return Ok(apartment.ToApartmentDto());
        }

        [HttpDelete]
        [Route("apartment/{id}")]
        public IActionResult Delete([FromBody] int id)
        {
            return Ok();
        }

    }

}