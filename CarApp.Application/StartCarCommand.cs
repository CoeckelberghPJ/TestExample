using MediatR;

namespace CarApp.Application;

public class StartCarCommand : IRequest
{
    public Guid Id { get; init; }
}
