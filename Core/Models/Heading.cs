using Core.Interfaces;

namespace Core.Models;

public class Heading : IHeading
{
    public double North { get; set; }

    public Heading(double north)
    {
        North = north;
    }
}
