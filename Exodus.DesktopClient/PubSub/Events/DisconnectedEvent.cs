using Exodus.DesktopClient.PubSub.Args;
using Prism.Events;

namespace Exodus.DesktopClient.PubSub.Events
{
    public class DisconnectedEvent : PubSubEvent<ConnectionEventArgs> {}
}
