using Core.Interfaces;
using MediatR;

namespace Core.Commands;

public class SaveLocationCommand : IRequest
{
    public SaveLocationCommand(ILocation location)
    {
        Location = location;
    }

    public ILocation Location { get; }
}
