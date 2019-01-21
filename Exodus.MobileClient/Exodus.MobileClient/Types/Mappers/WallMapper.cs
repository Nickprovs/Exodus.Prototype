using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class WallMapper
    {
        public static McWall GetMcWallFromDtoWall(Common.Data.Models.WallDto dtoWall)
        {
            return new McWall(dtoWall.Name, dtoWall.Width, dtoWall.Height, new System.Collections.ObjectModel.ObservableCollection<McSourceInstance>(), dtoWall.Id);
        }

        public static Common.Data.Models.WallDto GetDtoWallFromMcWall(McWall McWall)
        {
            return new Common.Data.Models.WallDto
            {
                Id = McWall.WallId,
                Name = McWall.Name,
                Width = McWall.Width,
                Height = McWall.Height
            };
        }

        public static ObservableCollection<McWall> GetMcWallListFromDtoWallList(IEnumerable<Common.Data.Models.WallDto> dtoWallList)
        {

            ObservableCollection<McWall> McWallList = new ObservableCollection<McWall>();
            foreach (var dtoWall in dtoWallList)
                McWallList.Add(GetMcWallFromDtoWall(dtoWall));

            return McWallList;
        }

        public static List<Common.Data.Models.WallDto> GetDtoWallListFromMcWallList(IEnumerable<McWall> McWallList)
        {

            List<Common.Data.Models.WallDto> dtoWallList = new List<Common.Data.Models.WallDto>();
            foreach (var McWall in McWallList)
                dtoWallList.Add(GetDtoWallFromMcWall(McWall));

            return dtoWallList;
        }


    }
}
