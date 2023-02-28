using CarApp.Core.Domain;

namespace CarApp.Application;

public class CarModel
{
    public CarModel(Car car)
    {
        Id = car.Id;
        Brand = car.Brand;
        Type = car.Type;
    }

    public Guid Id { get; init; }
    public string Brand { get; init; }
    public string Type { get; init; }
}
