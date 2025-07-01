using CommunityToolkit.Mvvm.ComponentModel;
using Core.Interfaces;
using Core.Notifications;
using MediatR;

namespace MiniMap.ViewModels;


//TODO: on appearing request direction monitoring start
// on disappearing request direction monitoring stop
public partial class NavigationViewModel : ObservableObject, INotificationHandler<DirectionDeltaChangedNotification>
{
    private ILocation _destination;//TODO: get this from the main view model VIA messenger (or from where it changes)
    // then _mediator.Send(new SetDestinationCommand(_destination));

    public async Task Handle(DirectionDeltaChangedNotification notification, CancellationToken cancellationToken)
    {
        //TODO: update the UI with the new direction delta
        throw new NotImplementedException();
    }
}
