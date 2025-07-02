using Core.Interfaces;
using MediatR;

namespace Core.Commands;

public class SaveCurrentLocationCommand : IRequest<ICurrentLocationSaveResult>
{
}
