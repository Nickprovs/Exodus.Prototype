using Exodus.MobileClient.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.Interfaces
{
    public interface IMcProfile : IDisposable
    {
        ObservableCollection<McSource> Sources { get; set; }

        ObservableCollection<McDigitalWall> DigitalWalls { get; set; }

        ObservableCollection<McSpaceSession> SpaceSessions { get; set; }
    }
}
