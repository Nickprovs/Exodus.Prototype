using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class DigitalWallMapper
    {

        public static McDigitalWall GetMcDigitalWallFromDtoDigitalWallAndSourceInstances(Common.Data.Models.DigitalWallDto dtoDigitalWall, ObservableCollection<McSourceInstance> sourceInstances)
        {
            return new McDigitalWall(dtoDigitalWall.Name, dtoDigitalWall.Width, dtoDigitalWall.Height, sourceInstances, dtoDigitalWall.Id);
        }

        public static Common.Data.Models.DigitalWallDto GetDtoDigitalWallFromMcDigitalWall(McDigitalWall McDigitalWall)
        {
            return new Common.Data.Models.DigitalWallDto
            {
                Id = McDigitalWall.WallId,
                Name = McDigitalWall.Name,
                Width = McDigitalWall.Width,
                Height = McDigitalWall.Height
            };
        }

        public static ObservableCollection<McDigitalWall> GetMcDigitalWallListFromDtoDigitalWallList(IEnumerable<McSourceInstance> sourceInstances, IEnumerable<Common.Data.Models.DigitalWallDto> dtoDigitalWallList)
        {

            ObservableCollection<McDigitalWall> McDigitalWallList = new ObservableCollection<McDigitalWall>();
            foreach (var dtoDigitalWall in dtoDigitalWallList)
            {
                var matchingSourceInstanceList = sourceInstances.Where(s => s.WallId == dtoDigitalWall.Id);
                var matchingSourceInstanceOc = new ObservableCollection<McSourceInstance>(matchingSourceInstanceList);
                McDigitalWallList.Add(DigitalWallMapper.GetMcDigitalWallFromDtoDigitalWallAndSourceInstances(dtoDigitalWall, matchingSourceInstanceOc));
            }

            return McDigitalWallList;
        }

        public static List<Common.Data.Models.DigitalWallDto> GetDtoDigitalWallListFromMcDigitalWallList(IEnumerable<McDigitalWall> McDigitalWallList)
        {

            List<Common.Data.Models.DigitalWallDto> dtoDigitalWallList = new List<Common.Data.Models.DigitalWallDto>();
            foreach (var McDigitalWall in McDigitalWallList)
                dtoDigitalWallList.Add(DigitalWallMapper.GetDtoDigitalWallFromMcDigitalWall(McDigitalWall));

            return dtoDigitalWallList;
        }

    }
}
