using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class SpaceSessionMapper
    {
        public static SpaceSession GetEntSpaceSessionFromDtoSpaceSession(Common.Data.Models.SpaceSessionDto dtoSpaceSession)
        {
            return new SpaceSession
            {
                Id = dtoSpaceSession.Id,
                DigitalWallId = dtoSpaceSession.DigitalWallId,
                Session = new Session { Id = dtoSpaceSession.Id, Name = dtoSpaceSession.Name }
            };
        }

        public static Common.Data.Models.SpaceSessionDto GetDtoSpaceSessionFromEntSpaceSession(SpaceSession entSpaceSession)
        {
            return new Common.Data.Models.SpaceSessionDto
            {
                Id = entSpaceSession.Id,
                DigitalWallId = entSpaceSession.DigitalWallId,
                Name = entSpaceSession.Session.Name
            };
        }

        public static IEnumerable<SpaceSession> GetEntSpaceSessionListFromDtoSpaceSessionList(IEnumerable<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList)
        {

            List<SpaceSession> entSpaceSessionList = new List<SpaceSession>();
            foreach (var dtoSpaceSession in dtoSpaceSessionList)
                entSpaceSessionList.Add(SpaceSessionMapper.GetEntSpaceSessionFromDtoSpaceSession(dtoSpaceSession));

            return entSpaceSessionList;
        }

        public static List<Common.Data.Models.SpaceSessionDto> GetDtoSpaceSessionListFromEntSpaceSessionList(IEnumerable<SpaceSession> entSpaceSessionList)
        {

            List<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList = new List<Common.Data.Models.SpaceSessionDto>();
            foreach (var entSpaceSession in entSpaceSessionList)
                dtoSpaceSessionList.Add(SpaceSessionMapper.GetDtoSpaceSessionFromEntSpaceSession(entSpaceSession));

            return dtoSpaceSessionList;
        }

    }
}
