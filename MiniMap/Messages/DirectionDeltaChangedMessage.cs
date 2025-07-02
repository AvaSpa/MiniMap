using Core.Interfaces;

namespace MiniMap.Messages;

public class DirectionDeltaChangedMessage
{
    public int Delta { get; }
    public int Bearing { get; }
    public ILocation Location { get; }

    public DirectionDeltaChangedMessage(int delta, int bearing, ILocation location)
    {
        Delta = delta;
        Bearing = bearing;
        Location = location;
    }
}
