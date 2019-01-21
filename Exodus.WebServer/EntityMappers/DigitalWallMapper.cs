using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class DigitalWallMapper
    {

        public static DigitalWall GetEntDigitalWallFromDtoDigitalWall(Common.Data.Models.DigitalWallDto dtoDigitalWall)
        {
            return new DigitalWall
            {
                Id = dtoDigitalWall.Id,
                Wall = new Wall { Id = dtoDigitalWall.Id, Name = dtoDigitalWall.Name, Width = dtoDigitalWall.Width, Height = dtoDigitalWall.Height}
            };
        }

        public static Common.Data.Models.DigitalWallDto GetDtoDigitalWallFromEntDigitalWall(DigitalWall entDigitalWall)
        {
            return new Common.Data.Models.DigitalWallDto
            {
                Id = entDigitalWall.Id,
                Name = entDigitalWall.Wall.Name,
                Width = entDigitalWall.Wall.Width,
                Height = entDigitalWall.Wall.Height
            };
        }

        public static IEnumerable<DigitalWall> GetEntDigitalWallListFromDtoDigitalWallList(IEnumerable<Common.Data.Models.DigitalWallDto> dtoDigitalWallList)
        {

            List<DigitalWall> entDigitalWallList = new List<DigitalWall>();
            foreach (var dtoDigitalWall in dtoDigitalWallList)
                entDigitalWallList.Add(DigitalWallMapper.GetEntDigitalWallFromDtoDigitalWall(dtoDigitalWall));

            return entDigitalWallList;
        }

        public static List<Common.Data.Models.DigitalWallDto> GetDtoDigitalWallListFromEntDigitalWallList(IEnumerable<DigitalWall> entDigitalWallList)
        {

            List<Common.Data.Models.DigitalWallDto> dtoDigitalWallList = new List<Common.Data.Models.DigitalWallDto>();
            foreach (var entDigitalWall in entDigitalWallList)
                dtoDigitalWallList.Add(DigitalWallMapper.GetDtoDigitalWallFromEntDigitalWall(entDigitalWall));

            return dtoDigitalWallList;
        }

    }
}
