using Exodus.DesktopClient.Data.Types;
using System.Collections.ObjectModel;

namespace Exodus.DesktopClient.Engine.Interfaces
{
    public interface IDcProfile
    {
        ObservableCollection<DcSource> Sources { get; set; }

        ObservableCollection<DcDigitalWall> DigitalWalls { get; set; }

        ObservableCollection<DcSpaceSession> SpaceSessions { get; set; }
    }
}
