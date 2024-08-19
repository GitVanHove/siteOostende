using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; } = string.Empty;
        // Navigation property
        public ICollection<ApartmentAmenity> ApartmentAmenities { get; set; } = new List<ApartmentAmenity>();
    }
}