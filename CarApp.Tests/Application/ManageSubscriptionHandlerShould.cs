using CarApp.Application;
using CarApp.Core.Common;
using CarApp.Tests.Builder;
using Moq;

namespace CarApp.Tests.Application;

public class ManageSubscriptionHandlerShould
{
    private Car _car;
    private readonly ManageSubscriptionHandler _handler;
    private readonly Mock<ICarRepository> _carRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    public ManageSubscriptionHandlerShould()
    {
        _car = CarBuilder.Default().Build();

        _handler = new ManageSubscriptionHandler(_unitOfWork.Object);
        _carRepository.Setup(x => x.GetAsync(_car.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_car);
        _unitOfWork.Setup(x => x.CarRepository).Returns(_carRepository.Object);
    }

    [Fact]
    public async Task EnableSubscription()
    {
        //Arrange
        _car = CarBuilder.Default()
            .WithoutSubscription()
            .Build();
        _carRepository.Setup(x => x.GetAsync(_car.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_car);

        var command = new ManageSubscriptionCommand
        {
            Id = _car.Id,
            IsSubscriptionEnabled = true
        };

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert
        _car.IsHeatedSeatsEnabled.Should().BeTrue();
        _car.IsMaxPowerEnabled.Should().BeTrue();
        _car.TopSpeed.Should().Be(180);
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DisableSubscription()
    {
        //Arrange
        _car = CarBuilder.Default()
           .WithSubscription()
           .Build();
        _carRepository.Setup(x => x.GetAsync(_car.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_car);

        var command = new ManageSubscriptionCommand
        {
            Id = _car.Id,
            IsSubscriptionEnabled = false
        };

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert
        _car.IsHeatedSeatsEnabled.Should().BeFalse();
        _car.IsMaxPowerEnabled.Should().BeFalse();
        _car.TopSpeed.Should().Be(150);
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowExceptionWhenCarNotFound()
    {
        // GIVEN
        var command = new ManageSubscriptionCommand
        {
            Id = Guid.NewGuid(),
            IsSubscriptionEnabled = false
        };

        // WHEN
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // THEN
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
