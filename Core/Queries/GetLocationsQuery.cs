using Core.Interfaces;
using MediatR;

namespace Core.Queries;

public class GetLocationsQuery : IRequest<IEnumerable<ILocation>>
{
    public bool WithReload { get; set; } = false;

    public GetLocationsQuery()
    {
    }

    public GetLocationsQuery(bool withReload)
    {
        WithReload = withReload;
    }
}
