using Exodus.MobileClient.Types;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.MobileClient.Interfaces
{
    public interface IMcSignalrProxy : IDisposable
    {
        ConnectionState ConnectionState { get; set; }

        IHubProxy HubProxy { get; set; }

        HubConnection Connection { get; set; }

        void ConnectAsync(string ipAddress, string port);

        void RequestStartupData();

        Task<int> AddSource(McSource source);

        Task<int> RemoveSource(int sourceId);

        Task<int> AddSourceInstance(McSourceInstance sourceInstance);

        Task<int> RemoveSourceInstance(int sourceInstanceId);

        Task<int> ModifySourceInstance(McSourceInstance sourceInstance);

        Task<int> AddDigitalWall(McDigitalWall digitalWall);

        Task<int> AddSpaceSession(McSpaceSession spaceSession, int digitalWallId);

        Task<int> RemoveSpaceSession(int spaceSessionId);

    }
}
