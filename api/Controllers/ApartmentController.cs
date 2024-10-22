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
        public async Task<IActionResult> GetApartmentsByBuildingId([FromRoute] int buildingId)
        {
            // First, fetch the apartments from the database.
            var apartments = await _context.Apartments
                                     .Where(a => a.BuildingId == buildingId)
                                     .ToListAsync();

            // Then, convert each apartment to a DTO.
            var apartmentDtos = apartments.Select(a => a.ToApartmentDto()).ToList();

            return Ok(apartmentDtos);
        }


        [HttpGet("apartment/{id}")]
        public async Task<IActionResult> GetApartmentById([FromRoute] int id)
        {
            var apartment = await _context.Apartments
                            .Where(a => a.ApartmentId == id)
                            .Include(a => a.ApartmentAmenities)
                                .ThenInclude(aa => aa.Amenity)
                            .FirstOrDefaultAsync();

            if (apartment == null)
            {
                return NotFound(); // Return 404 if the apartment isn't found
            }

            var apartmentDto = apartment.ToApartmentDto();

            return Ok(apartmentDto);
        }

        [HttpPost("addApartment")]
        public async Task<IActionResult> Create([FromBody] CreateApartmentDto apartmentDto)
        {
        
            var building = await _context.Buildings.FindAsync(apartmentDto.BuildingId);
            if (building == null)
            {
                return NotFound("Building id not found");
            }

            var apartmentModel = apartmentDto.toCreateApartmentDto();

            apartmentModel.BuildingId = building.Id; 
            apartmentModel.Building = building; 

            await _context.Apartments.AddAsync(apartmentModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApartmentById), new { id = apartmentModel.ApartmentId }, apartmentModel.ToApartmentDto());
        }


        [HttpPut]
        [Route("apartment/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateApartmentRequestDto updateDto)
        {
            var apartment = await _context.Apartments
                            .Include(a => a.ApartmentAmenities)
                            .ThenInclude(aa => aa.Amenity) // Ensure the related Amenity is included
                            .FirstOrDefaultAsync(a => a.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound($"Apartment with ID {id} not found.");
            }

            apartment.ApartmentNumber = updateDto.ApartmentNumber;
            apartment.FloorNumber = updateDto.FloorNumber;
            apartment.Description = updateDto.Description;
            apartment.MaxOccupancy = updateDto.MaxOccupancy;
            apartment.PricePerNight = updateDto.PricePerNight;
            apartment.IsAvailable = updateDto.IsAvailable;

            foreach (var amenityDto in updateDto.ApartmentAmenities)
            {
                // Check if the amenity already exists
                var existingAmenity =  apartment.ApartmentAmenities
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

            await _context.SaveChangesAsync();

            return Ok(apartment.ToApartmentDto());
        }

        [HttpDelete]
        [Route("deleteApartment/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var apartment =  await _context.Apartments
                                            .Include(a => a.ApartmentAmenities)
                                            .FirstOrDefaultAsync(a => a.ApartmentId == id);

                    if (apartment == null)
                    {
                        return NotFound($"Apartment with ID {id} not found.");
                    }

                    _context.ApartmentAmenities.RemoveRange(apartment.ApartmentAmenities);

                    _context.Apartments.Remove(apartment);

                    await _context.SaveChangesAsync();

                    // Commit the transaction if all operations succeed
                    await transaction.CommitAsync();

                    return Ok($"Apartment with ID {id} and its associated amenities have been deleted.");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any operation fails
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"An error occurred while deleting the apartment: {ex.Message}");
                }
            }
        }

    }

}