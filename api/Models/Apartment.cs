using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Apartment
    {

        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxOccupancy { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateCreated { get; set; }
        public int BuildingId { get; set; }      
        public Building Building { get; set; } =  new Building();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<ApartmentPhoto> ApartmentPhotos { get; set; } = new List<ApartmentPhoto>();
        public ICollection<ApartmentAmenity> ApartmentAmenities { get; set; } = new List<ApartmentAmenity>();

    }

}