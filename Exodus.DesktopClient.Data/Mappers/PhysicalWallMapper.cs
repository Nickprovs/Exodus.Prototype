using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Exodus.Common.Data.Mappers
{
    public static class PhysicalWallMapper
    {

        public static DcPhysicalWall GetDcPhysicalWallFromDtoPhysicalWallAndSourceInstances(Common.Data.Models.PhysicalWallDto dtoPhysicalWall, ObservableCollection<DcSourceInstance> sourceInstances)
        {
            return new DcPhysicalWall(new DcPhysicalWall(dtoPhysicalWall.Name, dtoPhysicalWall.Width, dtoPhysicalWall.Height, sourceInstances, dtoPhysicalWall.Id));
        }

        public static Common.Data.Models.PhysicalWallDto GetDtoPhysicalWallFromDcPhysicalWall(DcPhysicalWall DcPhysicalWall)
        {
            return new Common.Data.Models.PhysicalWallDto
            {
                Id = DcPhysicalWall.WallId,
                Name = DcPhysicalWall.Name,
                Width = DcPhysicalWall.Width,
                Height = DcPhysicalWall.Height
            };
        }

        public static ObservableCollection<DcPhysicalWall> GetDcPhysicalWallListFromDtoPhysicalWallList(IEnumerable<DcSourceInstance> sourceInstances, IEnumerable<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList)
        {

            ObservableCollection<DcPhysicalWall> DcPhysicalWallList = new ObservableCollection<DcPhysicalWall>();
            foreach (var dtoPhysicalWall in dtoPhysicalWallList)
            {
                var matchingSourceInstanceList = sourceInstances.Where(s => s.WallId == dtoPhysicalWall.Id);
                var matchingSourceInstanceOc = new ObservableCollection<DcSourceInstance>(matchingSourceInstanceList);
                DcPhysicalWallList.Add(PhysicalWallMapper.GetDcPhysicalWallFromDtoPhysicalWallAndSourceInstances(dtoPhysicalWall, matchingSourceInstanceOc));
            }

            return DcPhysicalWallList;
        }

        public static List<Common.Data.Models.PhysicalWallDto> GetDtoPhysicalWallListFromDcPhysicalWallList(IEnumerable<DcPhysicalWall> DcPhysicalWallList)
        {

            List<Common.Data.Models.PhysicalWallDto> dtoPhysicalWallList = new List<Common.Data.Models.PhysicalWallDto>();
            foreach (var DcPhysicalWall in DcPhysicalWallList)
                dtoPhysicalWallList.Add(PhysicalWallMapper.GetDtoPhysicalWallFromDcPhysicalWall(DcPhysicalWall));

            return dtoPhysicalWallList;
        }

    }
}
