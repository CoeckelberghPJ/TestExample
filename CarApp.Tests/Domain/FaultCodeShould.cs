namespace CarApp.Tests.Domain;

public class FaultCodeShould
{
    private readonly string _code;
    private readonly FaultCodeLevel _level;
    private readonly string _description;

    public FaultCodeShould()
    {
        _code = "Code";
        _level = FaultCodeLevel.Warning;
        _description = "Description";
    }

    [Fact]
    public void BeCreated()
    {
        //Arrange
        //Act
        var faultCode = FaultCode.Create(_code, _level, _description);

        //Assert
        using (new AssertionScope())
        {
            faultCode.Code.Should().Be(_code);
            faultCode.Description.Should().Be(_description);
            faultCode.Level.Should().Be(_level);
        }
    }

    [Theory]
    [ClassData(typeof(StringNullOrEmptyData))]
    public void ThrowExceptionWhenCodeIsNullOrEmpty(string code)
    {
        //arrange
        //act
        Action sut = () => FaultCode.Create(code, _level, _description);

        //assert
        sut.Should().Throw<ArgumentException>();
    }

    [Theory]
    [ClassData(typeof(StringNullOrEmptyData))]
    public void ThrowExceptionWhenDescriptionIsNullOrEmpty(string description)
    {
        //arrange
        //act
        Action sut = () => FaultCode.Create(_code, _level, description);

        //assert
        sut.Should().Throw<ArgumentException>();
    }
}
