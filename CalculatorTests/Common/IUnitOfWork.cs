using CarApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Common
{
    public interface IUnitOfWork
    {
        ICarRepository CarRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
