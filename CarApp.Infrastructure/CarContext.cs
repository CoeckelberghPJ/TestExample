using CarApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
        : base(options) { }

        public DbSet<Car> Cars => Set<Car>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
