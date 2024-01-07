using ParkBusinessLayer.Model;
using ParkDataLayer.Model;

namespace ParkDataLayer.Mappers
{
    public class HuurderMapper
    {
        public Huurder MapToModel(HuurderEntity entity)
        {
            return new Huurder(
                entity.Id,
                entity.Naam,
                new Contactgegevens(entity.Contactgegevens.Email, entity.Contactgegevens.Tel, entity.Contactgegevens.Adres)
            );
        }

        public HuurderEntity MapToEntity(Huurder model)
        {
            return new HuurderEntity
            {
                Id = model.Id,
                Naam = model.Naam,
                Contactgegevens = new ContactgegevensEntity
                {
                    Email = model.Contactgegevens.Email,
                    Tel = model.Contactgegevens.Tel,
                    Adres = model.Contactgegevens.Adres
                }
            };
        }

        public void MapToEntity(Huurder model, HuurderEntity existingEntity)
        {
            // Update properties of the existing huurder entity with the values from the provided huurder model
            existingEntity.Naam = model.Naam;
            existingEntity.Contactgegevens.Email = model.Contactgegevens.Email;
            existingEntity.Contactgegevens.Tel = model.Contactgegevens.Tel;
            existingEntity.Contactgegevens.Adres = model.Contactgegevens.Adres;

            // Update other properties as needed
        }

    }
}
