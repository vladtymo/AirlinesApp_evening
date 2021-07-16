using System;

namespace DAL
{
    public interface IUnitOfWork
    {
        void Save();
        GenericRepository<Client> ClientRepository { get; }
        GenericRepository<Flight> FlightRepository { get; }
        GenericRepository<Country> CountryRepository { get; }
        // ...
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AirlinesDbContext context = new AirlinesDbContext();

        private GenericRepository<Client> clientRepository;
        private GenericRepository<Flight> flightRepository;
        private GenericRepository<Country> countryRepository;
        // ... 

        public GenericRepository<Client> ClientRepository
        {
            get
            {
                // lazy loading
                if (this.clientRepository == null)
                {
                    this.clientRepository = new GenericRepository<Client>(context);
                }
                return clientRepository;
            }
        }
        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<Country>(context);
                }
                return countryRepository;
            }
        }
        public GenericRepository<Flight> FlightRepository
        {
            get
            {
                if (this.flightRepository == null)
                {
                    this.flightRepository = new GenericRepository<Flight>(context);
                }
                return flightRepository;
            }
        }
        // ...

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}