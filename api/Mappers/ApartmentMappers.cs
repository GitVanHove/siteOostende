using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Apartments;
using api.Models;

namespace api.Mappers
{
    public static class ApartmentMapper
    {
        public static ApartmentDto ToApartmentDto(this Apartment apartmentModel)
        {
            return new ApartmentDto
            {
                ApartmentId = apartmentModel.ApartmentId,
                ApartmentNumber = apartmentModel.ApartmentNumber,
                FloorNumber =  apartmentModel.FloorNumber,
                Description = apartmentModel.Description,
                MaxOccupancy = apartmentModel.MaxOccupancy,
                PricePerNight = apartmentModel.PricePerNight,
                IsAvailable = apartmentModel.IsAvailable,
                DateCreated = apartmentModel.DateCreated,
                BuildingId = apartmentModel.BuildingId
            };
        }
    }
}