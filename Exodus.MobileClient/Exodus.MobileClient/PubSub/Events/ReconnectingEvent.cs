﻿using Exodus.MobileClient.PubSub.Args;
using Prism.Events;

namespace Exodus.MobileClient.PubSub.Events
{
    public class ReconnectingEvent: PubSubEvent<ConnectionEventArgs> {}
}
