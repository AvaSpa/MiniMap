using Core.Commands;
using Core.Interfaces;
using Core.Queries;
using MediatR;

namespace Application;

public class LocationController : IRequestHandler<SaveLocationCommand>, IRequestHandler<SaveCurrentLocationCommand, ILocation>, IRequestHandler<GetLocationsQuery, IEnumerable<ILocation>>
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

    public async Task<ILocation> Handle(SaveCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetCurrentLocation(); //TODO: handle all return cases properly after they are implemented in the method

        await _locationRepository.SaveLocation(location);

        return location;
    }

    public async Task<IEnumerable<ILocation>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetAllLocations(request.WithReload);
    }
}
