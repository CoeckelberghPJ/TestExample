using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace CarApp.Core.Domain;

public class VinNumber : ValueObject
{
    protected VinNumber() { }

    public static VinNumber Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.InvalidFormat(value, nameof(VinNumber), "[A-Z]+");
        Guard.Against.InvalidInput(value, nameof(VinNumber), x => x.Length == 10);

        return new VinNumber
        {
            Value = value
        };
    }
    public string Value { get; private init; } = default!;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
