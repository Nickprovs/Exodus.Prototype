using Exodus.DesktopClient.PubSub.Args;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.PubSub.Events
{
    public class SelectedSessionChangedEvent : PubSubEvent <SelectedSessionChangedEventArgs> {}
}
