using CarApp.Core.Common;
using CarApp.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarContext db;

        public UnitOfWork(DbContextOptions<CarContext> options)
        {
            db = new CarContext(options);
        }

        private ICarRepository _carRepository;

        public ICarRepository CarRepository
        {
            get
            {
                if (_carRepository == null)
                {
                    _carRepository = new CarRepository(db);
                }
                return _carRepository;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => db.SaveChangesAsync(cancellationToken);
    }
}