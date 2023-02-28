using Ardalis.GuardClauses;
using CarApp.Core.Common;
using CarApp.Core.Domain;
using MediatR;

namespace CarApp.Application;

public class ManageSubscriptionHandler : IRequestHandler<ManageSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ManageSubscriptionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ManageSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var car = await _unitOfWork.CarRepository.GetAsync(request.Id, cancellationToken);
        Guard.Against.Null(car);

        if (request.IsSubscriptionEnabled)
        {
            car.EnableSubscription();
        }
        else
        {
            car.DisableSubscription();
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}