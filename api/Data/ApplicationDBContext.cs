using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ApartmentPhoto> ApartmentPhotos { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApartmentAmenity> ApartmentAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the primary key for ApartmentPhoto explicitly
            modelBuilder.Entity<ApartmentPhoto>()
                .HasKey(p => p.PhotoId);

            // Configure the one-to-many relationship between Apartments and ApartmentPhotos
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.ApartmentPhotos)
                .WithOne(p => p.Apartment)
                .HasForeignKey(p => p.ApartmentId);

            // Configure the many-to-many relationship between Apartments and Amenities
            modelBuilder.Entity<ApartmentAmenity>()
                .HasKey(aa => new { aa.ApartmentId, aa.AmenityId });

            modelBuilder.Entity<ApartmentAmenity>()
                .HasOne(aa => aa.Apartment)
                .WithMany(a => a.ApartmentAmenities)
                .HasForeignKey(aa => aa.ApartmentId);

            modelBuilder.Entity<ApartmentAmenity>()
                .HasOne(aa => aa.Amenity)
                .WithMany(a => a.ApartmentAmenities)
                .HasForeignKey(aa => aa.AmenityId);
        }

    }
}