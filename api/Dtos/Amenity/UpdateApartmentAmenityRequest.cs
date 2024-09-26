using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Dtos.Apartments;

namespace api.Dtos.Amenity
{
    public class UpdateApartmentAmenityRequest
    {
        public int AmenityId { get; set; }
        public int Amount { get; set; }
        
    }
}