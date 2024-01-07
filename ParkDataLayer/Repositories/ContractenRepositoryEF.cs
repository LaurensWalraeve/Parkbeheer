using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
        private readonly ParkbeheerContext _context;
        private readonly HuurcontractMapper _huurcontractMapper;


        private readonly HuizenRepositoryEF _huizenRepository;
        private readonly HuurderRepositoryEF _huurderRepository;


        private readonly HuisMapper _huisMapper;
        private readonly HuurderMapper _huurderMapper;


        // Constructor with context parameter
        public ContractenRepositoryEF(ParkbeheerContext context, HuurcontractMapper mapper, HuurderRepositoryEF huurder, HuizenRepositoryEF huizen, HuisMapper huisMapper, HuurderMapper huurderMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _huurcontractMapper = mapper;


            _huizenRepository = huizen;
            _huurderRepository = huurder;
            _huisMapper = huisMapper;
            _huurderMapper = huurderMapper;

        }

        // Default constructor using the connection string
        public ContractenRepositoryEF(string connectionString)
        {
            // Ensure the context is initialized with the provided connection string
            _context = new ParkbeheerContext(connectionString);
            _huurcontractMapper = new HuurcontractMapper();
            _huisMapper = new HuisMapper();
            _huurderMapper = new HuurderMapper();

            _huizenRepository = new HuizenRepositoryEF(connectionString);
            _huurderRepository = new HuurderRepositoryEF(connectionString);


        }

        public void AnnuleerContract(Huurcontract contract)
        {
            var contractEntity = _context.Huurcontracten.Find(contract.Id);
            if (contractEntity != null)
            {
                _context.Huurcontracten.Remove(contractEntity);
                _context.SaveChanges();
            }
        }

        public Huurcontract GeefContract(string id)
        {
            var contractEntity = _context.Huurcontracten
                                         .Include(c => c.Huurder)
                                         .Include(c => c.Huis)
                                         .ThenInclude(h => h.Park) // If you need to include nested relations
                                         .SingleOrDefault(c => c.Id == id);

            return contractEntity != null ? _huurcontractMapper.MapToModel(contractEntity) : null;
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {
            var query = _context.Huurcontracten.AsQueryable();

            if (dtBegin != default(DateTime))
            {
                query = query.Where(c => c.Huurperiode_StartDatum >= dtBegin);
            }

            if (dtEinde.HasValue)
            {
                query = query.Where(c => c.Huurperiode_EindDatum <= dtEinde);
            }

            var contractEntities = query.ToList();
            return contractEntities.Select(_huurcontractMapper.MapToModel).ToList();
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
            return _context.Huurcontracten.Any(c =>
                c.Huurperiode_StartDatum == startDatum &&
                c.HuurderId == huurderid &&
                c.HuisId == huisid);
        }

        public bool HeeftContract(string id)
        {
            return _context.Huurcontracten.Any(c => c.Id == id);
        }

        public void UpdateContract(Huurcontract contract)
        {
            var existingContract = _context.Huurcontracten.Find(contract.Id);
            if (existingContract != null)
            {
                _huurcontractMapper.MapToEntity(contract, existingContract);
                _context.SaveChanges();
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            // Check if Huurder exists, if not, add to database
            if (!_context.Huurders.Any(h => h.Id == contract.Huurder.Id))
            {
                var tempHuurder = new Huurder(contract.Huurder.Naam, contract.Huurder.Contactgegevens);
                /*_huurderRepository.VoegHuurderToe(tempHuurder);*/


                var huurderEntity = _huurderMapper.MapToEntity(tempHuurder);
                _context.Huurders.Add(huurderEntity);

                _context.SaveChanges();
            }

            // Check if Huis exists, if not, add to database
            if (!_context.Huizen.Any(h => h.Id == contract.Huis.Id))
            {
                var tempHuis = new Huis(contract.Huis.Straat, contract.Huis.Nr, contract.Huis.Park);
                /*_huizenRepository.VoegHuisToe(tempHuis);*/


                var huisEntity = _huisMapper.MapToEntity(tempHuis);

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
            }

            // Add Huurcontract
            var contractEntity = _huurcontractMapper.MapToEntity(contract);
            _context.Huurcontracten.Add(contractEntity);
            _context.SaveChanges();
        }
    }
}
