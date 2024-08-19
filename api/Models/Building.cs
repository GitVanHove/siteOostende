using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int NumberOfFloors { get; set; }
        public DateTime DateBuilt { get; set; }
        public bool IsHouse { get; set; }
        // Relationships
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    
    }
}