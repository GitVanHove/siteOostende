using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ApartmentAmenity
    {
        public int ApartmentId { get; set; }
        public int AmenityId { get; set; }
        public int Amount {get; set;}
        // Navigation properties
        public Apartment Apartment { get; set; } = new Apartment();
        public Amenity Amenity { get; set; } = new Amenity();
    }
}