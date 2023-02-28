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
            CarRepository = new CarRepository(db);
        }

        public ICarRepository CarRepository { get; private init; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => db.SaveChangesAsync(cancellationToken);
    }
}