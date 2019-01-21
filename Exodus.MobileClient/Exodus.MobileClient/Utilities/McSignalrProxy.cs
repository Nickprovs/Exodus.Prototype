﻿using Exodus.Common.Data.Models;
using Exodus.MobileClient.Interfaces;
using Exodus.MobileClient.PubSub.Args;
using Exodus.MobileClient.PubSub.Events;
using Exodus.MobileClient.Types;
using Exodus.MobileClient.Types.Mappers;
using Microsoft.AspNet.SignalR.Client;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exodus.MobileClient.Utilities
{
    public class McSignalrProxy : IMcSignalrProxy, IDisposable
    {
        #region Fields

        private readonly IMcProfile _profile;

        private readonly IEventAggregator _eventAggregator;

        private readonly Dictionary<StartupCacheKey, IEnumerable<object>> _startupCache = new Dictionary<StartupCacheKey, IEnumerable<object>>();

        private bool _startupCacheProcessed;

        #endregion

        #region Properties

        public IHubProxy HubProxy { get; set; }
        public HubConnection Connection { get; set; }
        public ConnectionState ConnectionState { get; set; } = ConnectionState.Disconnected;

        #endregion

        #region Constructors and Destructors

        public McSignalrProxy(IEventAggregator eventAggregator, IMcProfile profile)
        {
            this._eventAggregator = eventAggregator;
            this._profile = profile;
        }

        #endregion

        #region Methods

        #region Connection Handling

        /// <summary>
        /// Creates and connects the hub connection and hub proxy. This method
        /// is called asynchronously from SignInButton_Click.
        /// </summary>
        public async void ConnectAsync(string ipAddress, string port)
        {
            var ServerUri = "http://" + ipAddress + ":" + port;


            this.Connection = new HubConnection(ServerUri);
            this.Connection.Closed += OnConnectionClosed;
            this.Connection.StateChanged += this.Connection_StateChanged;
            HubProxy = Connection.CreateHubProxy("MyHub");

            this.WireReceiveMethods();

            try
            {
                await this.Connection.Start();
                this.RequestStartupData();
            }
            catch (HttpRequestException)
            {
                Debug.WriteLine("Unable to connect to server: Start server before connecting clients.");
                return;
            }
        }

        private void Connection_StateChanged(StateChange args)
        {
            this.ConnectionState = args.NewState;

            switch (args.NewState)
            {
                case ConnectionState.Connected:
                    this._eventAggregator.GetEvent<ConnectedEvent>().Publish(new ConnectionEventArgs(args.OldState, args.NewState));
                    break;
                case ConnectionState.Connecting:
                    this._eventAggregator.GetEvent<ConnectingEvent>().Publish(new ConnectionEventArgs(args.OldState, args.NewState));
                    break;
                case ConnectionState.Disconnected:
                    this._eventAggregator.GetEvent<DisconnectedEvent>().Publish(new ConnectionEventArgs(args.OldState, args.NewState));
                    break;
                case ConnectionState.Reconnecting:
                    this._eventAggregator.GetEvent<ReconnectingEvent>().Publish(new ConnectionEventArgs(args.OldState, args.NewState));
                    break;

            }
        }

        private void OnConnectionClosed()
        {

        }

        #endregion

        #region Hub Interaction

        private void WireReceiveMethods()
        {
            //Startup
            HubProxy.On<IEnumerable<SourceDto>>("ReceivedSources", sources => this.ReceivedSources(sources));
            HubProxy.On<IEnumerable<SourceInstanceDto>>("ReceivedSourceInstances", sourceInstances => this.ReceivedSourceInstances(sourceInstances));
            HubProxy.On<IEnumerable<DigitalWallDto>>("ReceivedDigitalWalls", digitalWalls => this.ReceivedDigitalWalls(digitalWalls));
            HubProxy.On<IEnumerable<SpaceSessionDto>>("ReceivedSpaceSessions", spaceSessions => this.ReceivedSpaceSessions(spaceSessions));

            //Non-Startup
            HubProxy.On<SourceDto>("SourceAdded", source => this.SourceAdded(source));
            HubProxy.On<int>("SourceRemoved", sourceId => this.SourceRemoved(sourceId));

            HubProxy.On<SourceInstanceDto>("SourceInstanceAdded", sourceInstance => this.SourceInstanceAdded(sourceInstance));
            HubProxy.On<int, int>("SourceInstanceRemoved", (sourceInstanceId, wallId) => this.SourceInstanceRemoved(sourceInstanceId, wallId));
            HubProxy.On<SourceInstanceDto>("SourceInstanceModified", sourceInstanceId => this.SourceInstanceModified(sourceInstanceId));


            HubProxy.On<DigitalWallDto>("DigitalWallAdded", digitalWall => this.DigitalWallAdded(digitalWall));
            HubProxy.On<int>("DigitalWallRemoved", digitalWallId => this.DigitalWallRemoved(digitalWallId));

            HubProxy.On<SpaceSessionDto>("SpaceSessionAdded", spaceSession => this.SpaceSessionAdded(spaceSession));
            HubProxy.On<int>("SpaceSessionRemoved", spaceSessionId => this.SpaceSessionRemoved(spaceSessionId));
        }

        #region Send

        public void RequestStartupData()
        {
            this.RequestSources();
            this.RequestSourceInstances();
            this.RequestDigitalWalls();
            this.RequestSpaceSessions();
        }

        public void RequestSources()
        {
            HubProxy.Invoke("RequestSources");
        }

        public void RequestSourceInstances()
        {
            HubProxy.Invoke("RequestSourceInstances");
        }

        public void RequestDigitalWalls()
        {
            HubProxy.Invoke("RequestDigitalWalls");
        }

        public void RequestSpaceSessions()
        {
            HubProxy.Invoke("RequestSpaceSessions");
        }

        public async Task<int> AddSource(McSource source)
        {
            int id = await HubProxy.Invoke<int>("AddSource", SourceMapper.GetDtoSourceFromMcSource(source));
            return id;
        }

        public async Task<int> RemoveSource(int sourceId)
        {
            int id = await HubProxy.Invoke<int>("RemoveSource", sourceId);
            return id;
        }

        public async Task<int> AddSourceInstance(McSourceInstance sourceInstance)
        {
            int id = await HubProxy.Invoke<int>("AddSourceInstance", SourceInstanceMapper.GetDtoSourceInstanceFromMcSourceInstance(sourceInstance));
            return id;
        }

        public async Task<int> RemoveSourceInstance(int sourceInstanceId)
        {
            int id = await HubProxy.Invoke<int>("RemoveSourceInstance", sourceInstanceId);
            return id;
        }

        public async Task<int> ModifySourceInstance(McSourceInstance sourceInstance)
        {
            var sourceInstanceDto = SourceInstanceMapper.GetDtoSourceInstanceFromMcSourceInstance(sourceInstance);
            int id = await HubProxy.Invoke<int>("ModifySourceInstance", sourceInstanceDto);
            return id;
        }

        public async Task<int> AddDigitalWall(McDigitalWall digitalWall)
        {
            int id = await HubProxy.Invoke<int>("AddDigitalWall", DigitalWallMapper.GetDtoDigitalWallFromMcDigitalWall(digitalWall));
            return id;
        }

        public async Task<int> RemoveDigitalWall(int digitalWallId)
        {
            int id = await HubProxy.Invoke<int>("RemoveDigitalWall", digitalWallId);
            return id;
        }

        public async Task<int> AddSpaceSession(McSpaceSession spaceSession, int digitalWallId)
        {
            SpaceSessionDto newSession = SpaceSessionMapper.GetDtoSpaceSessionFromMcSpaceSession(spaceSession);
            newSession.DigitalWallId = digitalWallId;
            int id = await HubProxy.Invoke<int>("AddSpaceSession", newSession);
            return id;
        }

        public async Task<int> RemoveSpaceSession(int spaceSessionId)
        {
            int id = await HubProxy.Invoke<int>("RemoveSpaceSession", spaceSessionId);
            return id;
        }


        #endregion

        #region Receive

        //Startup 

        private void ReceivedSources(IEnumerable<SourceDto> sources)
        {
            this._startupCache.Add(StartupCacheKey.Sources, sources);
            this.TryProcessStartupCache();
        }

        private void ReceivedSourceInstances(IEnumerable<SourceInstanceDto> sourceInstances)
        {
            this._startupCache.Add(StartupCacheKey.SourceInstances, sourceInstances);
            this.TryProcessStartupCache();
        }

        private void ReceivedDigitalWalls(IEnumerable<DigitalWallDto> digitalWalls)
        {
            this._startupCache.Add(StartupCacheKey.DigitalWalls, digitalWalls);
            this.TryProcessStartupCache();
        }

        private void ReceivedSpaceSessions(IEnumerable<SpaceSessionDto> spaceSessions)
        {
            this._startupCache.Add(StartupCacheKey.SpaceSessions, spaceSessions);
            this.TryProcessStartupCache();
        }

        //Non-Startup
        private void SourceAdded(SourceDto source)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this._profile.Sources.Add(SourceMapper.GetMcSourceFromDtoSource(source));
            });
        }

        private void SourceRemoved(int sourceId)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var source in this._profile.Sources.ToList())
                    if (source.SourceId == sourceId)
                        this._profile.Sources.Remove(source);

            });
        }

        private void SourceInstanceAdded(SourceInstanceDto sourceInstance)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingSource = this._profile.Sources.FirstOrDefault(s => s.SourceId == sourceInstance.SourceId);
                var newSourceInstance = SourceInstanceMapper.GetMcSourceInstanceFromMcSourceAndDtoSourceInstance(matchingSource, sourceInstance);
                var matchingWall = this._profile.DigitalWalls.FirstOrDefault(w => w.WallId == newSourceInstance.WallId);
                matchingWall.SourceInstances.Add(newSourceInstance);
            });
        }

        private void SourceInstanceRemoved(int sourceInstanceId, int associatedWallId)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingWall = this._profile.DigitalWalls.FirstOrDefault(w => w.WallId == associatedWallId);
                var matchingSourceInstance = matchingWall?.SourceInstances.FirstOrDefault(s => s.SourceInstanceId == sourceInstanceId);
                matchingWall?.SourceInstances.Remove(matchingSourceInstance);
            });
        }

        private void SourceInstanceModified(SourceInstanceDto sourceInstance)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingWall = this._profile.DigitalWalls.FirstOrDefault(w => w.WallId == sourceInstance.WallId);
                var matchingSourceInstance = matchingWall?.SourceInstances.FirstOrDefault(s => s.SourceInstanceId == sourceInstance.Id);

                var toCopy = SourceInstanceMapper.GetMcSourceInstanceFromMcSourceAndDtoSourceInstance((McSource)matchingSourceInstance, sourceInstance);

                matchingSourceInstance.X = toCopy.X;
                matchingSourceInstance.Y = toCopy.Y;
                matchingSourceInstance.Width = toCopy.Width;
                matchingSourceInstance.Height = toCopy.Height;
                matchingSourceInstance.WallId = toCopy.WallId;
            });
        }

        private void DigitalWallAdded(DigitalWallDto digitalWall)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this._profile.DigitalWalls.Add(DigitalWallMapper.GetMcDigitalWallFromDtoDigitalWallAndSourceInstances(digitalWall, new ObservableCollection<McSourceInstance>()));
            });
        }

        private void DigitalWallRemoved(int digitalWallId)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingWall = this._profile.DigitalWalls.FirstOrDefault(w => w.WallId == digitalWallId);
                this._profile.DigitalWalls.Remove(matchingWall);
            });
        }

        private void SpaceSessionAdded(SpaceSessionDto spaceSession)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingDigitalWall = this._profile.DigitalWalls.FirstOrDefault(w => w.WallId == spaceSession.DigitalWallId);
                this._profile.SpaceSessions.Add(SpaceSessionMapper.GetMcSpaceSessionFromMcDigitalWallAndDtoSpaceSession(matchingDigitalWall, spaceSession));
            });
        }

        private void SpaceSessionRemoved(int spaceSessionId)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var matchingSpaceSession = this._profile.SpaceSessions.FirstOrDefault(s => s.SessionId == spaceSessionId);
                this._profile.SpaceSessions.Remove(matchingSpaceSession);
            });
        }

        #endregion

        #endregion

        public void TryProcessStartupCache()
        {
            //If we've already processed the startup cache, we don't need to.
            if (this._startupCacheProcessed)
                return;

            //If the startup cache does not contain all the necessary data, we can't process it yet.
            if ((this._startupCache.ContainsKey(StartupCacheKey.Sources) &&
               this._startupCache.ContainsKey(StartupCacheKey.SourceInstances) &&
               this._startupCache.ContainsKey(StartupCacheKey.DigitalWalls) &&
               this._startupCache.ContainsKey(StartupCacheKey.SpaceSessions)) == false)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                this._profile.Sources = SourceMapper.GetMcSourceListFromDtoSourceList((IEnumerable<SourceDto>)this._startupCache[StartupCacheKey.Sources]);
                var sourceInstances = SourceInstanceMapper.GetMcSourceInstanceListFromMcSourceListAndDtoSourceInstanceList(this._profile.Sources, (IEnumerable<SourceInstanceDto>)this._startupCache[StartupCacheKey.SourceInstances]);
                this._profile.DigitalWalls = DigitalWallMapper.GetMcDigitalWallListFromDtoDigitalWallList(sourceInstances, (IEnumerable<DigitalWallDto>)this._startupCache[StartupCacheKey.DigitalWalls]);
                this._profile.SpaceSessions = SpaceSessionMapper.GetMcSpaceSessionListFromMcDigitalWallListAndDtoSpaceSessionList(this._profile.DigitalWalls, (IEnumerable<SpaceSessionDto>)this._startupCache[StartupCacheKey.SpaceSessions]);
            });

            this._startupCacheProcessed = true;
        }

        public void Dispose()
        {
            Connection?.Stop();
            Connection?.Dispose();
        }

        #endregion

    }

    enum StartupCacheKey
    {
        Sources = 0,
        SourceInstances = 1,
        DigitalWalls = 2,
        SpaceSessions = 3
    }
}
