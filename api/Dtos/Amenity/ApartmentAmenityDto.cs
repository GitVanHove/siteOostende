using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Dtos.Apartments;

namespace api.Dtos.Amenity
{
    public class ApartmentAmenityDto
    {
         public int ApartmentId { get; set; }
        public int AmenityId { get; set; }
        public int Amount {get; set;}
        // Navigation properties
        [JsonIgnore]
        public ApartmentDto Apartment { get; set; } = new ApartmentDto();
        public AmenityDto Amenity { get; set; } = new AmenityDto();
    }
}