using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class SessionMapper
    {
        public static McSession GetMcSessionFromDtoSession(Common.Data.Models.SessionDto dtoSession)
        {
            return new McSession(dtoSession.Name, dtoSession.Id);
        }

        public static Common.Data.Models.SessionDto GetDtoSessionFromMcSession(McSession McSession)
        {
            return new Common.Data.Models.SessionDto
            {
                Id = McSession.SessionId,
                Name = McSession.Name
            };
        }

        public static ObservableCollection<McSession> GetMcSessionListFromDtoSessionList(IEnumerable<Common.Data.Models.SessionDto> dtoSessionList)
        {

            ObservableCollection<McSession> McSessionList = new ObservableCollection<McSession>();
            foreach (var dtoSession in dtoSessionList)
                McSessionList.Add(GetMcSessionFromDtoSession(dtoSession));

            return McSessionList;
        }

        public static List<Common.Data.Models.SessionDto> GetDtoSessionListFromMcSessionList(IEnumerable<McSession> McSessionList)
        {

            List<Common.Data.Models.SessionDto> dtoSessionList = new List<Common.Data.Models.SessionDto>();
            foreach (var McSession in McSessionList)
                dtoSessionList.Add(GetDtoSessionFromMcSession(McSession));

            return dtoSessionList;
        }
    }
}
