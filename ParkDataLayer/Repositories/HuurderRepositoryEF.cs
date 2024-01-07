using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Mappers; // Import the HuurderMapper
using System.Collections.Generic;
using System.Linq;
using System;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private readonly ParkbeheerContext _context;
        private readonly HuurderMapper _huurderMapper;

        // Constructor with context parameter
        public HuurderRepositoryEF(ParkbeheerContext context, HuurderMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _huurderMapper = mapper;
        }

        // Default constructor using the connection string
        public HuurderRepositoryEF(string connectionString)
        {
            // Ensure the context is initialized with the provided connection string
            _context = new ParkbeheerContext(connectionString);
            _huurderMapper = new HuurderMapper();
        }

        public Huurder GeefHuurder(int id)
        {
            var huurderEntity = _context.Huurders.Find(id);
            return _huurderMapper.MapToModel(huurderEntity);
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            var huurderEntities = _context.Huurders.Where(h => h.Naam == naam).ToList();
            return huurderEntities.Select(_huurderMapper.MapToModel).ToList();
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            // Implement the logic to check if a huurder with the given name and contactgegevens exists
            return _context.Huurders.Any(h => h.Naam == naam && h.Contactgegevens.Email == contact.Email && h.Contactgegevens.Tel == contact.Tel && h.Contactgegevens.Adres == contact.Adres);
        }

        public bool HeeftHuurder(int id)
        {
            return _context.Huurders.Any(h => h.Id == id);
        }

        public void UpdateHuurder(Huurder huurder)
        {
            var existingHuurder = _context.Huurders.Find(huurder.Id);
            if (existingHuurder != null)
            {
                // Update the existing huurder entity with the values from the provided huurder model
                _huurderMapper.MapToEntity(huurder, existingHuurder);

                // Save the changes to the database
                _context.SaveChanges();
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            var huurderEntity = _huurderMapper.MapToEntity(h);
            _context.Huurders.Add(huurderEntity);
            _context.SaveChanges();
            return _huurderMapper.MapToModel(huurderEntity);
        }
    }
}
