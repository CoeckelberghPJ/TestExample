using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Domain
{
    public interface ICarRepository
    {
        public Task<Car?> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}
