using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class WallMapper
    {

        public static Wall GetEntWallFromDtoWall(Common.Data.Models.WallDto dtoWall)
        {
            return new Wall
            {
                Id = dtoWall.Id,
                Name = dtoWall.Name,
                Width = dtoWall.Width,
                Height = dtoWall.Height,
            };
        }

        public static Common.Data.Models.WallDto GetDtoWallFromEntWall(Wall entWall)
        {
            return new Common.Data.Models.WallDto
            {
                Id = entWall.Id,
                Name = entWall.Name,
                Width = entWall.Width,
                Height = entWall.Height,
            };
        }

        public static IEnumerable<Wall> GetEntWallListFromDtoWallList(IEnumerable<Common.Data.Models.WallDto> dtoWallList)
        {

            List<Wall> entWallList = new List<Wall>();
            foreach (var dtoWall in dtoWallList)
                entWallList.Add(WallMapper.GetEntWallFromDtoWall(dtoWall));

            return entWallList;
        }

        public static List<Common.Data.Models.WallDto> GetDtoWallListFromEntWallList(IEnumerable<Wall> entWallList)
        {

            List<Common.Data.Models.WallDto> dtoWallList = new List<Common.Data.Models.WallDto>();
            foreach (var entWall in entWallList)
                dtoWallList.Add(WallMapper.GetDtoWallFromEntWall(entWall));

            return dtoWallList;
        }


    }
}
