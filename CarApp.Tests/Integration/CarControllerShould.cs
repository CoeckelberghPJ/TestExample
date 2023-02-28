using CarApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CarApp.Tests.Integration;

public class CarControllerShould
{
    [Fact]
    public async Task ReturnCar()
    {
        //Arrange
        await using var application = new WebApplicationFactory<Program>();

        //Quick and dirty adding a record in DB as it's inmemory anyway
        Car car;
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;

            car = Builder<Car>.CreateNew().Build();

            using (var carContext = provider.GetRequiredService<CarContext>())
            {
                await carContext.Database.EnsureCreatedAsync();

                await carContext.Cars.AddAsync(car);
                await carContext.SaveChangesAsync();
            }
        }

        using var client = application.CreateClient();

        //Act
        var response = await client.GetAsync($"/car/{car.Id}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
