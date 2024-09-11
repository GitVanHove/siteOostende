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
        public static AmenityDto toAmenityDto(this Amenity amenityModel)
        {
           return new AmenityDto
           {
             AmenityId = amenityModel.AmenityId,
             AmenityName = amenityModel.AmenityName,

           };

        }
    }
}