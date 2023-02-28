using CarApp.Tests.Builder;

namespace CarApp.Tests.Domain;

public class CarShould
{
    private readonly Color _color;
    private readonly string _brand;
    private readonly string _type;
    private readonly VinNumber _vinNumber;
    private readonly Car _car;

    public CarShould()
    {
        _color = Color.Red;
        _brand = "Ferrari";
        _type = "Enzo";
        _vinNumber = Builder<VinNumber>.CreateNew().Build();

        _car = CarBuilder.Default().Build();
    }

    [Fact]
    public void BeCreated()
    {
        //Arrange
        //Act
        var car = Car.Create(_color, _brand, _type, _vinNumber);

        //Assert
        using (new AssertionScope())
        {
            car.Color.Should().Be(_color);
            car.Brand.Should().Be(_brand);
            car.Type.Should().Be(_type);
            car.VinNumber.Should().Be(_vinNumber);
            car.Id.Should().NotBeEmpty();
            car.IsMaxPowerEnabled.Should().BeFalse();
            car.IsHeatedSeatsEnabled.Should().BeFalse();
            car.TopSpeed.Should().Be(150);
        }
    }

    [Theory]
    [ClassData(typeof(StringNullOrEmptyData))]
    public void ThrowExceptionWhenBrandIsNullOrEmpty(string brand)
    {
        //arrange
        //act
        Action sut = () => Car.Create(_color, brand, _type, _vinNumber);

        //assert
        sut.Should().Throw<ArgumentException>();
    }

    [Theory]
    [ClassData(typeof(StringNullOrEmptyData))]
    public void ThrowExceptionWhenTypeIsNullOrEmpty(string type)
    {
        //arrange
        //act
        Action sut = () => Car.Create(_color, _brand, type, _vinNumber);

        //assert
        sut.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ThrowExceptionWhenVinNumberIsNull()
    {
        //arrange
        //act
        Action sut = () => Car.Create(_color, _brand, _type, null!);

        //assert
        sut.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void EnableSubscription()
    {
        //Arrange
        //Act
        _car.EnableSubscription();

        //Assert
        using (new AssertionScope())
        {
            _car.IsMaxPowerEnabled.Should().BeTrue();
            _car.IsHeatedSeatsEnabled.Should().BeTrue();
            _car.TopSpeed.Should().Be(180);
        }
    }

    [Fact]
    public void ThrowExceptionWhenEnablingSubscriptionWhileRunning()
    {
        //arrange
        _car.SetReadonlyProperty(x => x.IsRunning, true);

        //act
        Action sut = () => _car.EnableSubscription();

        //assert
        sut.Should().Throw<Exception>().WithMessage("Invalid car state");
    }

    [Fact]
    public void DisableSubscription()
    {
        //Arrange
        //Act
        _car.DisableSubscription();

        //Assert
        using (new AssertionScope())
        {
            _car.IsMaxPowerEnabled.Should().BeFalse();
            _car.IsHeatedSeatsEnabled.Should().BeFalse();
            _car.TopSpeed.Should().Be(150);
        }
    }

    [Fact]
    public void ThrowExceptionWhenDisablingSubscriptionWhileRunning()
    {
        //arrange
        _car.SetReadonlyProperty(x => x.IsRunning, true);

        //act
        Action sut = () => _car.DisableSubscription();

        //assert
        sut.Should().Throw<Exception>().WithMessage("Invalid car state");
    }

    [Fact]
    public void Start()
    {
        //Arrange
        //Act
        _car.Start();

        //Assert
        using (new AssertionScope())
        {
            _car.IsRunning.Should().BeTrue();
        }
    }

    [Fact]
    public void Stop()
    {
        //Arrange
        //Act
        _car.Stop();

        //Assert
        using (new AssertionScope())
        {
            _car.IsRunning.Should().BeFalse();
        }
    }

    [Fact]
    public void LimitTopSpeedWhenCriticalFaultCode()
    {
        //Arrange
        var faultCodes = Builder<FaultCode>.CreateListOfSize(1)
            .All()
            .With(x => x.Level, FaultCodeLevel.Critical)
            .Build();

        _car.SetReadonlyCollection(x => x.FaultCodes, faultCodes);

        //Act
        _car.Start();

        //Assert
        using (new AssertionScope())
        {
            _car.TopSpeed.Should().Be(50);
        }
    }

    [Fact]
    public void ClearFaultCodes()
    {
        //Arrange
        var faultCodes = Builder<FaultCode>.CreateListOfSize(1)
            .All()
            .With(x => x.Level, FaultCodeLevel.Critical)
            .Build();

        _car.SetReadonlyCollection(x => x.FaultCodes, faultCodes);

        //Act
        _car.ClearFaultCodes();

        //Assert
        using (new AssertionScope())
        {
            _car.FaultCodes.Should().BeEmpty();
        }
    }
}