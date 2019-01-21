using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class SpaceSessionMapper
    {
        public static McSpaceSession GetMcSpaceSessionFromMcDigitalWallAndDtoSpaceSession(McDigitalWall appropriateDigitalWall, Common.Data.Models.SpaceSessionDto dtoSpaceSession)
        {
            return new McSpaceSession(dtoSpaceSession.Name, appropriateDigitalWall, dtoSpaceSession.Id);
        }

        public static Common.Data.Models.SpaceSessionDto GetDtoSpaceSessionFromMcSpaceSession(McSpaceSession McSpaceSession)
        {
            return new Common.Data.Models.SpaceSessionDto
            {
                Id = McSpaceSession.SessionId,
                Name = McSpaceSession.Name,
                DigitalWallId = McSpaceSession.DigitalWall.WallId
            };
        }

        public static ObservableCollection<McSpaceSession> GetMcSpaceSessionListFromMcDigitalWallListAndDtoSpaceSessionList(IEnumerable<McDigitalWall> McDigitalWallList, IEnumerable<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList)
        {

            ObservableCollection<McSpaceSession> McSpaceSessionList = new ObservableCollection<McSpaceSession>();
            foreach (var dtoSpaceSession in dtoSpaceSessionList)
            {
                var appropriateWall = McDigitalWallList.FirstOrDefault(d => d.WallId == dtoSpaceSession.DigitalWallId);
                if (appropriateWall == null)
                    continue;
                McSpaceSessionList.Add(GetMcSpaceSessionFromMcDigitalWallAndDtoSpaceSession(appropriateWall, dtoSpaceSession));
            }

            return McSpaceSessionList;
        }

        public static List<Common.Data.Models.SpaceSessionDto> GetDtoSpaceSessionListFromMcSpaceSessionList(ICollection<McSpaceSession> McSpaceSessionList)
        {

            List<Common.Data.Models.SpaceSessionDto> dtoSpaceSessionList = new List<Common.Data.Models.SpaceSessionDto>();
            foreach (var McSpaceSession in McSpaceSessionList)
                dtoSpaceSessionList.Add(GetDtoSpaceSessionFromMcSpaceSession(McSpaceSession));

            return dtoSpaceSessionList;
        }

    }

}
