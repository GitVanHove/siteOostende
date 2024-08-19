using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime MoveInDate { get; set; }
        public DateTime DateCreated { get; set; }
        // Foreign Key
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();
    }

}