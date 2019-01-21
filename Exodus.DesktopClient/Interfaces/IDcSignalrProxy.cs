using Exodus.DesktopClient.Data.Types;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Interfaces
{
    public interface IDcSignalrProxy : IDisposable
    {

        IHubProxy HubProxy { get; set; }

        HubConnection Connection { get; set; }

        void ConnectAsync(string ipAddress, string port);

        void RequestStartupData();

        Task<int> AddSource(DcSource source);

        Task<int> RemoveSource(int sourceId);

        Task<int> AddSourceInstance(DcSourceInstance sourceInstance);

        Task<int> RemoveSourceInstance(int sourceInstanceId);

        Task<int> ModifySourceInstance(DcSourceInstance sourceInstance);

        Task<int> AddDigitalWall(DcDigitalWall digitalWall);

        Task<int> AddSpaceSession(DcSpaceSession spaceSession, int digitalWallId);

        Task<int> RemoveSpaceSession(int spaceSessionId);

    }
}
