using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class PhysicalWallMapper
    {

        public static McPhysicalWall GetMcPhysicalWallFromDtoPhysicalWallAndSourceInstances(Common.Data.Models.PhysicalWallDto dtoPhysicalWall, ObservableCollection<McSourceInstance> sourceInstances)
        {
            return new McPhysicalWall(new McPhysicalWall(dtoPhysicalWall.Name, dtoPhysicalWall.Width, dtoPhysicalWall.Height, sourceInstances, dtoPhysicalWall.Id));
        }

        public static Common.Data.Models.PhysicalWallDto GetDtoPhysicalWallFromMcPhysicalWall(McPhysicalWall McPhysicalWall)
        {
            return new Common.Data.Models.PhysicalWallDto
            {
                Id = McPhysicalWall.WallId,
                Name = McPhysicalWall.Name,
                Width = McPhysicalWall.Width,
                Height = McPhysicalWall.Height
            };
        }

        public static ObservableCollection<McPhysicalWall> GetMcPhysicalWallListFromDtoPhysicalWallList(IEnumerable<McSourceInstance> sourceInstances, IEnumerable<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList)
        {

            ObservableCollection<McPhysicalWall> McPhysicalWallList = new ObservableCollection<McPhysicalWall>();
            foreach (var dtoPhysicalWall in dtoPhysicalWallList)
            {
                var matchingSourceInstanceList = sourceInstances.Where(s => s.WallId == dtoPhysicalWall.Id);
                var matchingSourceInstanceOc = new ObservableCollection<McSourceInstance>(matchingSourceInstanceList);
                McPhysicalWallList.Add(PhysicalWallMapper.GetMcPhysicalWallFromDtoPhysicalWallAndSourceInstances(dtoPhysicalWall, matchingSourceInstanceOc));
            }

            return McPhysicalWallList;
        }

        public static List<Common.Data.Models.PhysicalWallDto> GetDtoPhysicalWallListFromMcPhysicalWallList(IEnumerable<McPhysicalWall> McPhysicalWallList)
        {

            List<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList = new List<Common.Data.Models.PhysicalWallDto>();
            foreach (var McPhysicalWall in McPhysicalWallList)
                dtoPhysicalWallList.Add(PhysicalWallMapper.GetDtoPhysicalWallFromMcPhysicalWall(McPhysicalWall));

            return dtoPhysicalWallList;
        }

    }
}
