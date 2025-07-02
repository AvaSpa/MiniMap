using Core.Interfaces;

namespace Core.Models;

public class Heading : IHeading
{
    public int North { get; set; }

    public Heading(int north)
    {
        North = north;
    }
}
