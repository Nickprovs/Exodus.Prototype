using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class DigitalWallMapper
    {

        public static DcDigitalWall GetDcDigitalWallFromDtoDigitalWallAndSourceInstances(Common.Data.Models.DigitalWallDto dtoDigitalWall, ObservableCollection<DcSourceInstance> sourceInstances)
        {
            return new DcDigitalWall(dtoDigitalWall.Name, dtoDigitalWall.Width, dtoDigitalWall.Height, sourceInstances, dtoDigitalWall.Id);
        }

        public static Common.Data.Models.DigitalWallDto GetDtoDigitalWallFromDcDigitalWall(DcDigitalWall DcDigitalWall)
        {
            return new Common.Data.Models.DigitalWallDto
            {
                Id = DcDigitalWall.WallId,
                Name = DcDigitalWall.Name,
                Width = DcDigitalWall.Width,
                Height = DcDigitalWall.Height          
            };
        }

        public static ObservableCollection<DcDigitalWall> GetDcDigitalWallListFromDtoDigitalWallList(IEnumerable<DcSourceInstance> sourceInstances, IEnumerable<Common.Data.Models.DigitalWallDto> dtoDigitalWallList)
        {

            ObservableCollection<DcDigitalWall> DcDigitalWallList = new ObservableCollection<DcDigitalWall>();
            foreach (var dtoDigitalWall in dtoDigitalWallList)
            {
                var matchingSourceInstanceList = sourceInstances.Where(s => s.WallId == dtoDigitalWall.Id);
                var matchingSourceInstanceOc = new ObservableCollection<DcSourceInstance>(matchingSourceInstanceList);
                DcDigitalWallList.Add(DigitalWallMapper.GetDcDigitalWallFromDtoDigitalWallAndSourceInstances(dtoDigitalWall, matchingSourceInstanceOc));
            }

            return DcDigitalWallList;
        }

        public static List<Common.Data.Models.DigitalWallDto> GetDtoDigitalWallListFromDcDigitalWallList(IEnumerable<DcDigitalWall> DcDigitalWallList)
        {

            List<Common.Data.Models.DigitalWallDto> dtoDigitalWallList = new List<Common.Data.Models.DigitalWallDto>();
            foreach (var DcDigitalWall in DcDigitalWallList)
                dtoDigitalWallList.Add(DigitalWallMapper.GetDtoDigitalWallFromDcDigitalWall(DcDigitalWall));

            return dtoDigitalWallList;
        }

    }
}
