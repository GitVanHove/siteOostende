using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Buildings
{
    public class UpdateBuildingRequestDto
    {
        public string Address { get; set; } = string.Empty;
        public int NumberOfFloors { get; set; }
        public DateTime DateBuilt { get; set; }
        public bool IsHouse { get; set; }

    }
}