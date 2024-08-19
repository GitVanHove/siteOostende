using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ApartmentPhoto
    {
        public int PhotoId { get; set; }
        public int ApartmentId { get; set; }
        public string PhotoUrls { get; set; } = string.Empty;
        public string Description { get; set; } =  string.Empty;
        // Navigation property
        public Apartment Apartment { get; set; } = new Apartment();
    }
}