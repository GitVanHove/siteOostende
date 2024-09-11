using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Amenity
{
    public class AmenityDto
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; } = string.Empty;
        // Navigation property
        public ICollection<ApartmentAmenityDto> ApartmentAmenities { get; set; } = new List<ApartmentAmenityDto>();
    }
}