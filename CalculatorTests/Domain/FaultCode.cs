using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace CarApp.Core.Domain;

public class FaultCode : ValueObject
{
    protected FaultCode() { }

    public static FaultCode Create(string code, FaultCodeLevel level, string description)
    {
        Guard.Against.NullOrWhiteSpace(code);
        Guard.Against.NullOrWhiteSpace(description);

        return new FaultCode
        {
            Code = code,
            Description = description,
            Level = level
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Description;
        yield return Level;
    }

    public string Code { get; private init; } = default!;
    public string Description { get; private init; } = default!;
    public FaultCodeLevel Level { get; private init; } = default!;
}
