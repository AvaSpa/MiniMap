using Core.Commands;
using Core.Interfaces;
using Core.Queries;
using MediatR;

namespace Application.Handlers;

public class LocationHandler : IRequestHandler<SaveLocationCommand>,
    IRequestHandler<SaveCurrentLocationCommand, ICurrentLocationSaveResult>,
    IRequestHandler<GetLocationsQuery, IEnumerable<ILocation>>
{
    private readonly ILocationController _locationController;

    public LocationHandler(ILocationController locationController)
    {
        _locationController = locationController;
    }

    public async Task Handle(SaveLocationCommand request, CancellationToken cancellationToken)
    {
        await _locationController.SaveLocation(request.Location);
    }

    public async Task<ICurrentLocationSaveResult> Handle(SaveCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        return await _locationController.SaveCurrentLocation();
    }

    public async Task<IEnumerable<ILocation>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationController.GetAllLocations(request.WithReload);
    }
}
