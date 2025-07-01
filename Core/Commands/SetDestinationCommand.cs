using Core.Interfaces;
using MediatR;

namespace Core.Commands;

public class SetDestinationCommand(ILocation destination) : IRequest
{
    public ILocation Destination { get; } = destination;
}
