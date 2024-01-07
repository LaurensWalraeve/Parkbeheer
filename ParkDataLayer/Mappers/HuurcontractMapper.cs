using ParkBusinessLayer.Model;
using ParkDataLayer.Model;

namespace ParkDataLayer.Mappers
{
    public class HuurcontractMapper
    {
        public Huurcontract MapToModel(HuurcontractEntity entity)
        {
            // Implement the logic to map from data layer entity to business layer model
            return new Huurcontract(
                entity.Id,
                new Huurperiode(entity.Huurperiode_StartDatum, entity.Huurperiode_Aantaldagen),
                new Huurder(entity.Huurder.Id, entity.Huurder.Naam, new Contactgegevens(entity.Huurder.Contactgegevens.Email, entity.Huurder.Contactgegevens.Tel, entity.Huurder.Contactgegevens.Adres)),
                new Huis(entity.Huis.Id, entity.Huis.Straat, entity.Huis.Nr, entity.Huis.Actief, new Park(entity.Huis.Park.Id, entity.Huis.Park.Naam, entity.Huis.Park.Locatie))
            );
        }

        public HuurcontractEntity MapToEntity(Huurcontract model)
        {
            // Implement the logic to map from business layer model to data layer entity
            return new HuurcontractEntity
            {
                Id = model.Id,
                Huurperiode_Aantaldagen = model.Huurperiode.Aantaldagen,
                Huurperiode_StartDatum = model.Huurperiode.StartDatum,
                HuurderId = model.Huurder.Id,
                HuisId = model.Huis.Id

                // Map other properties as needed
            };



        }
        public HuurcontractEntity MapToEntity(Huurcontract model, HuurcontractEntity existingEntity = null)
        {
            // Implement the logic to map from business layer model to data layer entity
            // Ensure to handle relationships and nested objects if needed

            // If an existing entity is provided, update its properties; otherwise, create a new entity
            var entity = existingEntity ?? new HuurcontractEntity();

            entity.Id = model.Id;
            entity.Huurperiode_Aantaldagen = model.Huurperiode.Aantaldagen;
            entity.Huurperiode_StartDatum = model.Huurperiode.StartDatum;
            entity.HuurderId = model.Huurder.Id;
            entity.HuisId = model.Huis.Id;
            // Map other properties as needed

            return entity;
        }
    }
}
