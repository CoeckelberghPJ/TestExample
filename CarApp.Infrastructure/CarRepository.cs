using CarApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure
{
    public class CarRepository : ICarRepository
    {
        private readonly CarContext _context;

        public CarRepository(CarContext context)
        {
            _context = context;
        }

        public async Task<Car?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
