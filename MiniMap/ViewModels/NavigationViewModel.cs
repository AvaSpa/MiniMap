using CommunityToolkit.Mvvm.ComponentModel;
using Core.Interfaces;
using Core.Notifications;
using MediatR;

namespace MiniMap.ViewModels;


//TODO: on appearing request direction monitoring start
// on disappearing request direction monitoring stop
public partial class NavigationViewModel : ObservableObject, INotificationHandler<HeadingChangedNotification>
{
    private ILocation _destination;//TODO: get this from the main view model VIA messenger (or from where it changes)

    public async Task Handle(HeadingChangedNotification notification, CancellationToken cancellationToken)
    {
        //TODO: change the arrow direction to point to the current location
        // request DirectionToLocation and adapt the following code:
        //  double compassHeading = ...; // from Compass.Reading.HeadingMagneticNorth
        //  double targetBearing = ...;  // from GetDirectionToLocation(...).North

        //  double difference = (targetBearing - compassHeading + 360) % 360;
        // process the difference to update the UI accordingly
    }
}
