using Exodus.WebServer.EntityMappers;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exodus.WebServer
{
    /// <summary>
    /// Echoes messages sent using the Send message by calling the
    /// addMessage method on the client. Also reports to the console
    /// when clients connect and disconnect.
    /// </summary>
    public class MyHub : Hub
    {
        #region Startup Requests

        public void RequestSources()
        {
            using (var context = new ExodusPrototype1Entities())
            {
                Clients.Caller.ReceivedSources(SourceMapper.GetDtoSourceListFromEntSourceList(context.Sources));
            }
        }

        public void RequestSourceInstances()
        {
            using (var context = new ExodusPrototype1Entities())
            {
                Clients.Caller.ReceivedSourceInstances(SourceInstanceMapper.GetDtoSourceInstanceListFromEntSourceInstanceList(context.SourceInstances));
            }
        }

        public void RequestDigitalWalls()
        {
            using (var context = new ExodusPrototype1Entities())
            {
                Clients.Caller.ReceivedDigitalWalls(DigitalWallMapper.GetDtoDigitalWallListFromEntDigitalWallList(context.DigitalWalls));
            }
        }

        public void RequestSpaceSessions()
        {
            using (var context = new ExodusPrototype1Entities())
            {
                Clients.Caller.ReceivedSpaceSessions(SpaceSessionMapper.GetDtoSpaceSessionListFromEntSpaceSessionList(context.SpaceSessions));
            }
        }

        #endregion

        #region Non-Startup Requests

        public int AddSource(Common.Data.Models.SourceDto newSource)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var newSourceEnt = SourceMapper.GetEntSourceFromDtoSource(newSource);
                context.Sources.Add(newSourceEnt);
                context.SaveChanges();
                Clients.All.SourceAdded(SourceMapper.GetDtoSourceFromEntSource(newSourceEnt));

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Added Source With Id: {newSourceEnt.Id} and Name: {newSourceEnt.Name}");

                return newSourceEnt.Id;
            }
        }

        public int RemoveSource(int sourceId)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                //Try and get a matching thing
                var matchingSource = context.Sources.Find(sourceId);

                //If we don't have one, return an error
                if (matchingSource == null)
                    return -1;

                //If we do have a match... remove the associated source instances and notify the client of their removal
                var relatedSourceInstances = context.SourceInstances.Where(s => s.SourceId == sourceId).ToList();
                context.SourceInstances.RemoveRange(relatedSourceInstances);
                context.SaveChanges();
                foreach (var sourceInstance in relatedSourceInstances)
                    Clients.All.SourceInstanceRemoved(sourceInstance.Id, sourceInstance.WallId);


                //Then remove the source itself and notify the client of that too
                context.Sources.Remove(matchingSource);
                context.SaveChanges();
                Clients.All.SourceRemoved(sourceId);

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Removed Source With Id: {matchingSource.Id} and Name: {matchingSource.Name}");

                //Return id of deleted, indicating success...
                return sourceId;
            }
        }

        public int AddSourceInstance(Common.Data.Models.SourceInstanceDto newSourceInstance)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var newSourceInstanceEnt = SourceInstanceMapper.GetEntSourceInstanceFromDtoSourceInstance(newSourceInstance);
                context.SourceInstances.Add(newSourceInstanceEnt);
                context.SaveChanges();
                Clients.All.SourceInstanceAdded(SourceInstanceMapper.GetDtoSourceInstanceFromEntSourceInstance(newSourceInstanceEnt));

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Added Source Instance With Id: {newSourceInstanceEnt.Id}");

                return newSourceInstanceEnt.Id;
            }
        }

        public int RemoveSourceInstance(int sourceInstanceId)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var matchingSourceInstance = context.SourceInstances.Find(sourceInstanceId);
                context.SourceInstances.Remove(matchingSourceInstance);
                context.SaveChanges();
                Clients.All.SourceInstanceRemoved(matchingSourceInstance.Id, matchingSourceInstance.WallId);

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Removed Source Instance With Id: {matchingSourceInstance.Id}");

                return matchingSourceInstance.Id;
            }
        }

        public int ModifySourceInstance(Common.Data.Models.SourceInstanceDto existingSourceInstance)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var matchingSourceInstance = context.SourceInstances.Find(existingSourceInstance.Id);

                if (matchingSourceInstance == null)
                    return -1;

                var toCopy = SourceInstanceMapper.GetEntSourceInstanceFromDtoSourceInstance(existingSourceInstance);

                matchingSourceInstance.X = toCopy.X;
                matchingSourceInstance.Y = toCopy.Y;
                matchingSourceInstance.Width = toCopy.Width;
                matchingSourceInstance.Height = toCopy.Height;
                matchingSourceInstance.WallId = toCopy.WallId;

                context.SaveChanges();
                Clients.All.SourceInstanceModified(SourceInstanceMapper.GetDtoSourceInstanceFromEntSourceInstance(matchingSourceInstance));

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Modified Source Instance With Id: {matchingSourceInstance.Id}");

                return matchingSourceInstance.Id;
            }
        }

        public int AddDigitalWall(Common.Data.Models.DigitalWallDto digitalWall)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var newDigitalWallEnt = DigitalWallMapper.GetEntDigitalWallFromDtoDigitalWall(digitalWall);
                context.DigitalWalls.Add(newDigitalWallEnt);
                context.SaveChanges();
                Clients.All.DigitalWallAdded(DigitalWallMapper.GetDtoDigitalWallFromEntDigitalWall(newDigitalWallEnt));

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Added Digital Wall With Id: {newDigitalWallEnt.Id} and Name: {newDigitalWallEnt.Wall.Name}");

                return newDigitalWallEnt.Id;
            }

        }

        public int RemoveDigitalWall(int digitalWallId)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var matchingDigitalWall = context.DigitalWalls.Find(digitalWallId);
                context.DigitalWalls.Remove(matchingDigitalWall);
                context.SaveChanges();
                Clients.All.DigitalWallRemoved(matchingDigitalWall.Id);

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Removed Digital Wall With Id: {matchingDigitalWall.Id} and Name: {matchingDigitalWall.Wall.Name}");

                return matchingDigitalWall.Id;
            }
        }

        public int AddSpaceSession(Common.Data.Models.SpaceSessionDto spaceSession)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                var newSpaceSessionEnt = SpaceSessionMapper.GetEntSpaceSessionFromDtoSpaceSession(spaceSession);
                context.SpaceSessions.Add(newSpaceSessionEnt);
                context.SaveChanges();
                Clients.All.SpaceSessionAdded(SpaceSessionMapper.GetDtoSpaceSessionFromEntSpaceSession(newSpaceSessionEnt));

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Added Space Session With Id: {newSpaceSessionEnt.Id} and Name: {newSpaceSessionEnt.Session.Name}");

                return newSpaceSessionEnt.Id;
            }
        }

        public int RemoveSpaceSession(int spaceSessionId)
        {
            using (var context = new ExodusPrototype1Entities())
            {
                //First, let's find the related space session
                var matchingSpaceSession = context.SpaceSessions.Find(spaceSessionId);

                //Let's use that to find the related digital wall
                var matchingDigitalWall = context.Walls.Find(matchingSpaceSession?.DigitalWallId);

                //Delete the digital wall first... in theory, that sould have removed the spaceSession as well.
                if (matchingDigitalWall != null)
                    context.Walls.Remove(matchingDigitalWall);

                //Double check
                var matchingSession = context.Sessions.Find(spaceSessionId);

                if (matchingSpaceSession != null)
                    context.Sessions.Remove(matchingSession);

                context.SaveChanges();
                //Always delete from lowers level
                Clients.All.DigitalWallRemoved(matchingSpaceSession.DigitalWallId);
                Clients.All.SpaceSessionRemoved(matchingSpaceSession.Id);

                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Deleted Digital Wall With Id: {matchingDigitalWall.Id}");
                this.WriteToWindowConsole($"Client: {Context.ConnectionId} - Deleted Space Session With Id: {matchingSpaceSession.Id} and Name: {matchingSession.Name}" );


                return matchingSpaceSession.Id;
            }
        }

        #endregion


        public override Task OnConnected()
        {
            this.WriteToWindowConsole("Client connected: " + Context.ConnectionId);

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            this.WriteToWindowConsole("Client disconnected: " + Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
        /// </summary>
        /// <param name="message"></param>
        private void WriteToWindowConsole(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole(message));
        }

    }
}
