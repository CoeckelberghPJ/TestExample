using CarApp.Application;

namespace CarApp.Tests.Application
{
    public class CarModelShould
    {
        [Fact]
        public void BeConstructed()
        {
            //Arrange
            var car = Builder<Car>.CreateNew().Build();

            //Act
            var result = new CarModel(car);

            //Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(car.Id);
                result.Brand.Should().Be(car.Brand);
                result.Type.Should().Be(car.Type);
            }
        }
    }
}
