namespace Core.Interfaces;

public interface INavigationController
{
    int GetDirectionDelta();
    void UpdateHeading(IHeading heading);
    void UpdateLocation(ILocation location);
    Task StartNavigation();
    void StopNavigation();
    void SetDestination(ILocation destination);
    ILocation GetDestination();

    //TEST
    ILocation CurrentLocation { get; }
    IHeading CurrentHeading { get; }
    //
}
