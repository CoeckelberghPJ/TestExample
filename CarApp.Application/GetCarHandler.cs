using Ardalis.GuardClauses;
using CarApp.Core.Common;
using CarApp.Core.Domain;
using MediatR;

namespace CarApp.Application;

public class GetCarHandler : IRequestHandler<GetCarCommand, CarModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CarModel> Handle(GetCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _unitOfWork.CarRepository.GetAsync(request.Id, cancellationToken);
        Guard.Against.Null(car);

        return new CarModel(car);
    }
}