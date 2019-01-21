using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class SourceInstanceMapper
    {
        public static DcSourceInstance GetDcSourceInstanceFromDcSourceAndDtoSourceInstance(DcSource appropriateSource, Common.Data.Models.SourceInstanceDto dtoSourceInstance)
        {
            return new DcSourceInstance(appropriateSource, dtoSourceInstance.WallId, dtoSourceInstance.X, dtoSourceInstance.Y, dtoSourceInstance.Width, dtoSourceInstance.Height, dtoSourceInstance.Id);
        }

        public static Common.Data.Models.SourceInstanceDto GetDtoSourceInstanceFromDcSourceInstance(DcSourceInstance DcSourceInstance)
        {
            return new Common.Data.Models.SourceInstanceDto
            {
                Id = DcSourceInstance.SourceInstanceId,
                SourceId = DcSourceInstance.SourceId,
                WallId = DcSourceInstance.WallId,
                X = DcSourceInstance.X,
                Y = DcSourceInstance.Y,
                Width = DcSourceInstance.Width,
                Height = DcSourceInstance.Height
            };
        }

        public static ObservableCollection<DcSourceInstance> GetDcSourceInstanceListFromDcSourceListAndDtoSourceInstanceList(IEnumerable<DcSource> dcSourceList, IEnumerable<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList)
        {

            ObservableCollection<DcSourceInstance> DcSourceInstanceList = new ObservableCollection<DcSourceInstance>();
            foreach (var dtoSourceInstance in dtoSourceInstanceList)
            {
                DcSource appropriateSource = dcSourceList.FirstOrDefault(s => s.SourceId == dtoSourceInstance.SourceId);
                if (appropriateSource == null)
                    continue;
                DcSourceInstanceList.Add(GetDcSourceInstanceFromDcSourceAndDtoSourceInstance(appropriateSource,dtoSourceInstance));
            }

            return DcSourceInstanceList;
        }

        public static List<Common.Data.Models.SourceInstanceDto> GetDtoSourceInstanceListFromDcSourceInstanceList(ICollection<DcSourceInstance> DcSourceInstanceList)
        {

            List<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList = new List<Common.Data.Models.SourceInstanceDto>();
            foreach (var DcSourceInstance in DcSourceInstanceList)
                dtoSourceInstanceList.Add(GetDtoSourceInstanceFromDcSourceInstance(DcSourceInstance));

            return dtoSourceInstanceList;
        }

    }
}
