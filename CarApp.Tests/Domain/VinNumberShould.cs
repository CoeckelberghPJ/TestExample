namespace CarApp.Tests.Domain;

public class VinNumberShould
{
    [Fact]
    public void BeCreated()
    {
        //Arrange
        var value = "AAABBBCCCD";

        //Act
        var vinNumber = VinNumber.Create(value);

        //Assert
        vinNumber.Value.Should().Be(value);
    }

    [Fact]
    public void BeEqual()
    {
        //Arrange
        var vinNumbers = Builder<VinNumber>.CreateListOfSize(2)
            .All()
            .With(x => x.Value, "VIN1")
            .Build();

        //Act
        //Assert
        vinNumbers[0].Should().Be(vinNumbers[1]);
    }
}
