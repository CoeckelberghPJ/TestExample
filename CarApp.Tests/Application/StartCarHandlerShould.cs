using CarApp.Application;
using CarApp.Core.Common;
using Moq;

namespace CarApp.Tests.Application;

public class StartCarHandlerShould
{
    private readonly Car _car;
    private readonly StartCarHandler _handler;
    private readonly Mock<ICarRepository> _carRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    public StartCarHandlerShould()
    {
        _car = Builder<Car>.CreateNew()
            .With(x => x.IsRunning, false)            
            .Build();

        _handler = new StartCarHandler(_unitOfWork.Object);
        _carRepository.Setup(x => x.GetAsync(_car.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_car);
        _unitOfWork.Setup(x => x.CarRepository).Returns(_carRepository.Object);
    }

    [Fact]
    public async Task HandleCommand()
    {
        //Arrange
        var command = new StartCarCommand
        {
            Id = _car.Id
        };

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert
        _car.IsRunning.Should().BeTrue();
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowExceptionWhenCarNotFound()
    {
        // GIVEN
        var command = new StartCarCommand
        {
            Id = Guid.NewGuid()
        };

        // WHEN
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // THEN
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
