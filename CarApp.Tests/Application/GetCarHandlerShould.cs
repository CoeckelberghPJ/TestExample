using CarApp.Application;
using CarApp.Core.Common;
using Moq;

namespace CarApp.Tests.Application;

public class GetCarHandlerShould
{
    private readonly Car _car;
    private readonly GetCarHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<ICarRepository> _carRepository = new();

    public GetCarHandlerShould()
    {
        _car = Builder<Car>.CreateNew().Build();

        _handler = new GetCarHandler(_unitOfWork.Object);
        _carRepository.Setup(x => x.GetAsync(_car.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_car);
        _unitOfWork.Setup(x => x.CarRepository).Returns(_carRepository.Object);
    }

    [Fact]
    public async Task ReturnCarModel()
    {
        //Arrange
        var command = new GetCarCommand
        {
            Id = _car.Id
        };

        var expected = new CarModel(_car);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThrowExceptionWhenCarNotFound()
    {
        // GIVEN
        var command = new GetCarCommand
        {
            Id = Guid.NewGuid()
        };

        // WHEN
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // THEN
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }
}
