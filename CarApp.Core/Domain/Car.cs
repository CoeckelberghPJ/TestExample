using Ardalis.GuardClauses;
using CarApp.Core.Common;

namespace CarApp.Core.Domain;

public class Car : IEntity
{
    private readonly List<FaultCode> _faultCodes = new();

    protected Car()
    {
    }

    public static Car Create(Color color, string brand, string type, VinNumber vinNumber)
    {
        Guard.Against.Null(vinNumber);
        Guard.Against.NullOrWhiteSpace(brand);
        Guard.Against.NullOrWhiteSpace(type);

        return new Car
        {
            Id = Guid.NewGuid(),
            Color = color,
            Brand = brand,
            Type = type,
            VinNumber = vinNumber,
            IsSubscriptionActivated = false
        };
    }

    public Guid Id { get; private init; }
    public Color Color { get; private init; }
    public string Brand { get; private init; } = default!;
    public string Type { get; private init; } = default!;
    public VinNumber VinNumber { get; private init; } = default!;
    public int TopSpeed
    {
        get
        {
            if (_faultCodes.Any(x => x.Level == FaultCodeLevel.Critical))
            {
                return 50;
            }

            return IsSubscriptionActivated ? 180 : 150;
        }
    }
    public bool IsHeatedSeatsEnabled => IsSubscriptionActivated;
    public bool IsMaxPowerEnabled => IsSubscriptionActivated;
    public bool IsSubscriptionActivated { get; private set; }
    public bool IsRunning { get; private set; }
    public virtual IReadOnlyCollection<FaultCode> FaultCodes => _faultCodes;

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void AddFaultCode(FaultCode faultCode)
    {
        _faultCodes.Add(faultCode);
    }

    public void ClearFaultCodes()
    {
        _faultCodes.Clear();
    }

    public void EnableSubscription()
    {
        if (IsRunning)
        {
            throw new Exception("Invalid car state");
        }

        IsSubscriptionActivated = true;
    }

    public void DisableSubscription()
    {
        if (IsRunning)
        {
            throw new Exception("Invalid car state");
        }

        IsSubscriptionActivated = false;
    }
}
