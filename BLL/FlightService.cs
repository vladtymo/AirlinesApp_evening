using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // Data Transfer Object - DTO
    public class FlightDTO
    {
        public int Number { get; set; }
        public DateTime DepartureTime { get; set; }
        public int DispatchCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public int AirplaneId { get; set; }

        public CityDTO DispatchCity { get; set; }
        public CityDTO ArrivalCity { get; set; }
    }
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
    }

    public interface IFlightService
    {
        void Add(FlightDTO flight);
        IEnumerable<FlightDTO> GetAll();
        IEnumerable<FlightDTO> GetFlightsByDate(DateTime dateFrom);
        IEnumerable<string> GetCountries();
        void AddCountry(string name);
    }

    public class FlightService : IFlightService
    {
        IUnitOfWork unitOfWork = new UnitOfWork();
        Mapper mapper;

        public FlightService()
        {
            IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDTO, Flight>();
                cfg.CreateMap<Flight, FlightDTO>();

                cfg.CreateMap<CityDTO, City>();
                cfg.CreateMap<City, CityDTO>()
                    .ForMember(c => c.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            });

            mapper = new Mapper(config);
        }

        public void Add(FlightDTO flight)
        {
            //unitOfWork.FlightRepository.Insert(new Flight()
            //{
            //    Number = flight.Number,
            //    DepartureTime = flight.DepartureTime,
            //    DispatchCityId = flight.DispatchCityId,
            //    ArrivalCityId = flight.ArrivalCityId,
            //    AirplaneId = flight.AirplaneId
            //});
            unitOfWork.FlightRepository.Insert(mapper.Map<Flight>(flight));
            unitOfWork.Save();
        }

        public IEnumerable<FlightDTO> GetAll()
        {
            //return unitOfWork.FlightRepository.Get().Select(f => new FlightDTO()
            //{
            //    Number = f.Number,
            //    DepartureTime = f.DepartureTime,
            //    DispatchCityId = f.DispatchCityId,
            //    ArrivalCityId = f.ArrivalCityId,
            //    AirplaneId = f.AirplaneId,

            //    DispatchCity = new CityDTO()
            //    {
            //        Id = f.DispatchCity.Id,
            //        Name = f.DispatchCity.Name,
            //        CountryName = f.DispatchCity.Country.Name
            //    },
            //    ArrivalCity = new CityDTO()
            //    {
            //        Id = f.ArrivalCity.Id,
            //        Name = f.ArrivalCity.Name,
            //        CountryName = f.ArrivalCity.Country.Name
            //    },
            //});
            return mapper.Map<IEnumerable<FlightDTO>>(unitOfWork.FlightRepository.Get());
        }
        public IEnumerable<FlightDTO> GetFlightsByDate(DateTime dateFrom)
        {
            return mapper.Map<IEnumerable<FlightDTO>>(unitOfWork.FlightRepository.Get(f => f.DepartureTime >= dateFrom));
        }

        public IEnumerable<string> GetCountries()
        {
            return unitOfWork.CountryRepository.Get().Select(c => c.Name);
        }

        public void AddCountry(string name)
        {
            unitOfWork.CountryRepository.Insert(new Country { Name = name });
            unitOfWork.Save();
        }
    }
}
