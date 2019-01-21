using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class PhysicalWallMapper
    {
        public static PhysicalWall GetEntPhysicalWallFromDtoPhysicalWall(Common.Data.Models.PhysicalWallDto dtoPhysicalWall)
        {
            return new PhysicalWall
            {
                Id = dtoPhysicalWall.Id,
            };
        }

        public static Common.Data.Models.PhysicalWallDto GetDtoPhysicalWallFromEntPhysicalWall(PhysicalWall entPhysicalWall)
        {
            return new Common.Data.Models.PhysicalWallDto
            {
                Id = entPhysicalWall.Id,
            };
        }

        public static IEnumerable<PhysicalWall> GetEntPhysicalWallListFromDtoPhysicalWallList(IEnumerable<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList)
        {

            List<PhysicalWall> entPhysicalWallList = new List<PhysicalWall>();
            foreach (var dtoPhysicalWall in dtoPhysicalWallList)
                entPhysicalWallList.Add(PhysicalWallMapper.GetEntPhysicalWallFromDtoPhysicalWall(dtoPhysicalWall));

            return entPhysicalWallList;
        }

        public static List<Common.Data.Models.PhysicalWallDto> GetDtoPhysicalWallListFromEntPhysicalWallList(IEnumerable<PhysicalWall> entPhysicalWallList)
        {

            List<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList = new List<Common.Data.Models.PhysicalWallDto>();
            foreach (var entPhysicalWall in entPhysicalWallList)
                dtoPhysicalWallList.Add(PhysicalWallMapper.GetDtoPhysicalWallFromEntPhysicalWall(entPhysicalWall));

            return dtoPhysicalWallList;
        }
    }
}
