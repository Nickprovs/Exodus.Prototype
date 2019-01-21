using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class SpaceSessionMapper
    {

        public static DcSpaceSession GetDcSpaceSessionFromDcDigitalWallAndDtoSpaceSession(DcDigitalWall appropriateDigitalWall, Common.Data.Models.SpaceSessionDto dtoSpaceSession)
        {
            return new DcSpaceSession(dtoSpaceSession.Name, appropriateDigitalWall, dtoSpaceSession.Id);
        }

        public static Common.Data.Models.SpaceSessionDto GetDtoSpaceSessionFromDcSpaceSession(DcSpaceSession DcSpaceSession)
        {
            return new Common.Data.Models.SpaceSessionDto
            {
                Id = DcSpaceSession.SessionId,
                Name = DcSpaceSession.Name,
                DigitalWallId = DcSpaceSession.DigitalWall.WallId
            };
        }

        public static ObservableCollection<DcSpaceSession> GetDcSpaceSessionListFromDcDigitalWallListAndDtoSpaceSessionList(IEnumerable<DcDigitalWall> dcDigitalWallList, IEnumerable<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList)
        {

            ObservableCollection<DcSpaceSession> DcSpaceSessionList = new ObservableCollection<DcSpaceSession>();
            foreach (var dtoSpaceSession in dtoSpaceSessionList)
            {
                var appropriateWall = dcDigitalWallList.FirstOrDefault(d => d.WallId == dtoSpaceSession.DigitalWallId);
                if (appropriateWall == null)
                    continue;
                DcSpaceSessionList.Add(GetDcSpaceSessionFromDcDigitalWallAndDtoSpaceSession(appropriateWall, dtoSpaceSession));
            }

            return DcSpaceSessionList;
        }

        public static List<Common.Data.Models.SpaceSessionDto> GetDtoSpaceSessionListFromDcSpaceSessionList(ICollection<DcSpaceSession> DcSpaceSessionList)
        {

            List<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList = new List<Common.Data.Models.SpaceSessionDto>();
            foreach (var DcSpaceSession in DcSpaceSessionList)
                dtoSpaceSessionList.Add(GetDtoSpaceSessionFromDcSpaceSession(DcSpaceSession));

            return dtoSpaceSessionList;
        }

    }
}
