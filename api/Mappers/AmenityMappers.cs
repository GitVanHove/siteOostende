using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Amenity;
using api.Models;

namespace api.Mappers
{
  public static class AmenityMappers
  {
    public static AmenityDto ToAmenityDto(this Amenity amenityModel)
    {
      return new AmenityDto
      {
        AmenityId = amenityModel.AmenityId,
        AmenityName = amenityModel.AmenityName,
        // Map ApartmentAmenities collection
        /*ApartmentAmenities = amenityModel.ApartmentAmenities
                               .Select(aa => aa.toApartmentAmenityDto()) // Map each ApartmentAmenity to ApartmentAmenityDto
                               .ToList()*/
      };

    }

    public static ApartmentAmenityDto ToApartmentAmenityDto(this ApartmentAmenity apartmentAmenity)
    {
      return new ApartmentAmenityDto
      {
        ApartmentId = apartmentAmenity.ApartmentId,
        AmenityId = apartmentAmenity.AmenityId,
        Amount = apartmentAmenity.Amount,
        Amenity = new AmenityDto
        {
          AmenityId = apartmentAmenity.Amenity.AmenityId,
          AmenityName = apartmentAmenity.Amenity.AmenityName
        }
      };
    }

    public static Amenity ToAmenityToCreateDto(this CreateAmenityRequestDto amenityDto)
    {
      return new Amenity
      {
        AmenityName = amenityDto.AmenityName
      };
    }
  }
}