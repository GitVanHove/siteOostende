using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Apartments;

namespace api.Dtos.Buildings
{

    public class BuildingDto
    {
         public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int NumberOfFloors { get; set; }
        public DateTime DateBuilt { get; set; }
        public bool IsHouse { get; set; }
        public ICollection<ApartmentDto> Apartments { get; set; } = new List<ApartmentDto>();
    }


}