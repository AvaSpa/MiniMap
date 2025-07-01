using Core.Commands;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.Queries;
using MediatR;

namespace Application;

public class LocationController : IRequestHandler<SaveLocationCommand>, IRequestHandler<SaveCurrentLocationCommand, ILocationSaveResult>, IRequestHandler<GetLocationsQuery, IEnumerable<ILocation>>
{
    private readonly ILocationService _locationService;
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationService locationService, ILocationRepository locationRepository)
    {
        _locationService = locationService;
        _locationRepository = locationRepository;
    }

    public async Task Handle(SaveLocationCommand request, CancellationToken cancellationToken)
    {
        await _locationRepository.SaveLocation(request.Location);
    }

    public async Task<ILocationSaveResult> Handle(SaveCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        var locationResult = await _locationService.GetCurrentLocation();

        if (locationResult.Status == LocationStatus.Success)
            await _locationRepository.SaveLocation(locationResult.Location);

        return new LocationSaveResult(locationResult.Status, locationResult);
    }

    public async Task<IEnumerable<ILocation>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetAllLocations(request.WithReload);
    }
}
