using MediatR;

namespace CarApp.Application;

public class ManageSubscriptionCommand : IRequest
{
    public Guid Id { get; init; }

    public bool IsSubscriptionEnabled { get; set; }
}
