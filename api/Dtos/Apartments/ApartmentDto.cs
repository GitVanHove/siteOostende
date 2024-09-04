using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Apartments
{
    public class ApartmentDto
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

    }
}