using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Mappers;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private readonly ParkbeheerContext _context;
        private readonly HuisMapper _huisMapper;

        // Constructor with context parameter
        public HuizenRepositoryEF(ParkbeheerContext context, HuisMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _huisMapper = mapper;
        }

        // Default constructor using the connection string
        public HuizenRepositoryEF(string connectionString)
        {
            // Ensure the context is initialized with the provided connection string
            _context = new ParkbeheerContext(connectionString);
            _huisMapper = new HuisMapper();
        }

        public Huis GeefHuis(int id)
        {
            // Implement logic to retrieve a Huis entity by id from the database
            var huisEntity = _context.Huizen.FirstOrDefault(h => h.Id == id);

            // Map the entity to the business layer model
            return huisEntity != null ? _huisMapper.MapToModel(huisEntity) : null;
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            // Implement logic to check if a Huis with the specified parameters exists in the database
            return _context.Huizen.Any(h =>
                h.Straat == straat &&
                h.Nr == nummer &&
                h.Park.Id == park.Id);
        }

        public bool HeeftHuis(int id)
        {
            // Implement logic to check if a Huis with the specified id exists in the database
            return _context.Huizen.Any(h => h.Id == id);
        }

        public void UpdateHuis(Huis huis)
        {
            // Implement logic to update a Huis entity in the database
            var existingHuisEntity = _context.Huizen.Find(huis.Id);

            if (existingHuisEntity != null)
            {
                // Update the existing Huis entity with the values from the provided Huis model
                _huisMapper.MapToEntity(huis, existingHuisEntity);

                // Save the changes to the database
                _context.SaveChanges();
            }
        }

        public Huis VoegHuisToe(Huis h)
        {

            var huisEntity = _huisMapper.MapToEntity(h);

            var allParks = _context.Parken.ToList();

            bool check = false;

            foreach (var park in allParks)
            {
                if (huisEntity.Park.Id == park.Id) 
                {
                    check = true;
                    break;
                }

            }

            if (check == true) 
            {
                huisEntity.ParkId = huisEntity.Park.Id;
                huisEntity.Park = null;
            }


            _context.Huizen.Add(huisEntity);
            _context.SaveChanges();

            return _huisMapper.MapToModel(huisEntity);


        }
    }
}
