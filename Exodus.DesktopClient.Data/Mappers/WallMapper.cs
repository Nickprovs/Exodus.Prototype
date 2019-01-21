using Exodus.DesktopClient.Data.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class WallMapper
    {
        public static DcWall GetDcWallFromDtoWall(Common.Data.Models.WallDto dtoWall)
        {
            return new DcWall(dtoWall.Name, dtoWall.Width, dtoWall.Height, new System.Collections.ObjectModel.ObservableCollection<DcSourceInstance>(), dtoWall.Id);
        }

        public static Common.Data.Models.WallDto GetDtoWallFromDcWall(DcWall dcWall)
        {
            return new Common.Data.Models.WallDto
            {
                Id = dcWall.WallId,
                Name = dcWall.Name,
                Width = dcWall.Width,
                Height = dcWall.Height
            };
        }

        public static ObservableCollection<DcWall> GetDcWallListFromDtoWallList(IEnumerable<Common.Data.Models.WallDto> dtoWallList)
        {

            ObservableCollection<DcWall> DcWallList = new ObservableCollection<DcWall>();
            foreach (var dtoWall in dtoWallList)
                DcWallList.Add(GetDcWallFromDtoWall(dtoWall));

            return DcWallList;
        }

        public static List<Common.Data.Models.WallDto> GetDtoWallListFromDcWallList(IEnumerable<DcWall> DcWallList)
        {

            List<Common.Data.Models.WallDto> dtoWallList = new List<Common.Data.Models.WallDto>();
            foreach (var DcWall in DcWallList)
                dtoWallList.Add(GetDtoWallFromDcWall(DcWall));

            return dtoWallList;
        }


    }
}
