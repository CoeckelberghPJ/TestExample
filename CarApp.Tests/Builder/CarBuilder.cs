using CarApp.Core.Domain;

namespace CarApp.Tests.Builder;

public class CarBuilder
{
    private VinNumber _vinNumber;
    private string _brand;
    private string _type;
    private Color _color;
    private bool _isSubscriptionActivated;
    private bool _isRunning;

    protected CarBuilder() {
        _vinNumber = Builder<VinNumber>.CreateNew()
            .With(x => x.Value, "0123456789")
            .Build();

        _brand = "Brand";
        _type = "Type";
        _color = Color.Blue;
        _isSubscriptionActivated = false;
        _isRunning = false;
    }

    public static CarBuilder Default() => new();

    public CarBuilder WithBrand(string brand)
    {
        _brand = brand;
        return this;
    }

    public CarBuilder WithType(string type)
    {
        _type = type;
        return this;
    }

    public CarBuilder WithColor(Color color)
    {
        _color = color;
        return this;
    }

    public CarBuilder WithVinNumber(VinNumber vinNumber)
    {
        _vinNumber = vinNumber;
        return this;
    }

    public CarBuilder WithSubscription()
    {
        _isSubscriptionActivated = true;
        return this;
    }

    public CarBuilder WithoutSubscription()
    {
        _isSubscriptionActivated = false;   
        return this;
    }

    public CarBuilder WithIsRunning(bool isRunning)
    {
        _isRunning = isRunning;
        return this;
    }

    public Car Build()
        => Builder<Car>.CreateNew()
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Brand, _brand)
            .With(x => x.Type, _type)
            .With(x => x.VinNumber, _vinNumber)
            .With(x => x.Color, _color)
            .With(x => x.IsSubscriptionActivated, _isSubscriptionActivated)
            .With(x => x.IsRunning, _isRunning)
            .Build();
    
}
