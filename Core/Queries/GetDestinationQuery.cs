using Core.Interfaces;
using MediatR;

namespace Core.Queries;

public class GetDestinationQuery : IRequest<ILocation>
{
}
