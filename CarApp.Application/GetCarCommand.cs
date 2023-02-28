using MediatR;

namespace CarApp.Application;

public class GetCarCommand : IRequest<CarModel>
{
    public Guid Id { get; init; }
}
