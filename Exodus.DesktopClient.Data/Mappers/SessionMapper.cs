using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class SessionMapper
    {
        public static DcSession GetDcSessionFromDtoSession(Common.Data.Models.SessionDto dtoSession)
        {
            return new DcSession(dtoSession.Name, dtoSession.Id);
        }

        public static Common.Data.Models.SessionDto GetDtoSessionFromDcSession(DcSession dcSession)
        {
            return new Common.Data.Models.SessionDto
            {
                Id = dcSession.SessionId,
                Name = dcSession.Name
            };
        }

        public static ObservableCollection<DcSession> GetDcSessionListFromDtoSessionList(IEnumerable<Common.Data.Models.SessionDto> dtoSessionList)
        {

            ObservableCollection<DcSession> DcSessionList = new ObservableCollection<DcSession>();
            foreach (var dtoSession in dtoSessionList)
                DcSessionList.Add(GetDcSessionFromDtoSession(dtoSession));

            return DcSessionList;
        }

        public static List<Common.Data.Models.SessionDto> GetDtoSessionListFromDcSessionList(IEnumerable<DcSession> DcSessionList)
        {

            List<Common.Data.Models.SessionDto> dtoSessionList = new List<Common.Data.Models.SessionDto>();
            foreach (var DcSession in DcSessionList)
                dtoSessionList.Add(GetDtoSessionFromDcSession(DcSession));

            return dtoSessionList;
        }
    }
}
