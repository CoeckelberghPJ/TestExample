using Ardalis.GuardClauses;
using CarApp.Core.Common;
using CarApp.Core.Domain;
using MediatR;

namespace CarApp.Application;

public class StartCarHandler : IRequestHandler<StartCarCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public StartCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(StartCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _unitOfWork.CarRepository.GetAsync(request.Id, cancellationToken);
        Guard.Against.Null(car);

        car.Start();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}