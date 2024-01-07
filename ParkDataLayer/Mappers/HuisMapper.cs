using ParkBusinessLayer.Model;
using ParkDataLayer.Model;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public Huis MapToModel(HuisEntity entity)
        {
            return new Huis(
                entity.Id,
                entity.Straat,
                entity.Nr,
                entity.Actief,
                new Park(entity.Park.Id, entity.Park.Naam, entity.Park.Locatie)
            );
        }

        public HuisEntity MapToEntity(Huis model)
        {
            return new HuisEntity
            {
                Id = model.Id,
                Straat = model.Straat,
                Nr = model.Nr,
                Actief = model.Actief,
                Park = new ParkEntity
                {
                    Id = model.Park.Id,
                    Naam = model.Park.Naam,
                    Locatie = model.Park.Locatie
                }
            };
        }
        public HuisEntity MapToEntity(Huis model, HuisEntity existingEntity = null)
        {
            // Implement the logic to map from business layer model to data layer entity
            // Ensure to handle relationships and nested objects if needed

            // If an existing entity is provided, update its properties; otherwise, create a new entity
            var entity = existingEntity ?? new HuisEntity();

            entity.Id = model.Id;
            entity.Straat = model.Straat;
            entity.Nr = model.Nr;
            entity.Actief = model.Actief;

            // Map Park property
            if (model.Park != null)
            {
                entity.ParkId = model.Park.Id; // Assuming ParkId is a foreign key property
                // You may also need to handle updating or adding Park entities if necessary
            }

            return entity;
        }
    }
}
