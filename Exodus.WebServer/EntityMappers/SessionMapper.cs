using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class SessionMapper
    {

        public static Session GetEntSessionFromDtoSession(Common.Data.Models.SessionDto dtoSession)
        {
            return new Session
            {
                Id = dtoSession.Id,
                Name = dtoSession.Name
            };
        }

        public static Common.Data.Models.SessionDto GetDtoSessionFromEntSession(Session entSession)
        {
            return new Common.Data.Models.SessionDto
            {
                Id = entSession.Id,
                Name = entSession.Name
            };
        }

        public static IEnumerable<Session> GetEntSessionListFromDtoSessionList(IEnumerable<Common.Data.Models.SessionDto> dtoSessionList)
        {

            List<Session> entSessionList = new List<Session>();
            foreach (var dtoSession in dtoSessionList)
                entSessionList.Add(SessionMapper.GetEntSessionFromDtoSession(dtoSession));

            return entSessionList;
        }

        public static List<Common.Data.Models.SessionDto> GetDtoSessionListFromEntSessionList(IEnumerable<Session> entSessionList)
        {

            List<Common.Data.Models.SessionDto> dtoSessionList = new List<Common.Data.Models.SessionDto>();
            foreach (var entSession in entSessionList)
                dtoSessionList.Add(SessionMapper.GetDtoSessionFromEntSession(entSession));

            return dtoSessionList;
        }

    }
}
