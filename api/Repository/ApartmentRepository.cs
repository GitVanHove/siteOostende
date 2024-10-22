using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDBContext _context;
        public ApartmentRepository(ApplicationDBContext dBContext)
        {
            _context = dBContext;
        }
        public async Task<List<Apartment>> GetApartmentByBuildingIdAsync(int buildingId)
        {
            return await _context.Apartments.Where(a => a.BuildingId == buildingId).ToListAsync();
        }
    }
}