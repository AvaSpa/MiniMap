using Core.Commands;
using Core.Interfaces;
using MediatR;

namespace Application;

public class LocationController : IRequestHandler<SaveLocationCommand>, IRequestHandler<SaveCurrentLocationCommand>
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

    public async Task Handle(SaveCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetCurrentLocation();

        await _locationRepository.SaveLocation(location);
    }
}
