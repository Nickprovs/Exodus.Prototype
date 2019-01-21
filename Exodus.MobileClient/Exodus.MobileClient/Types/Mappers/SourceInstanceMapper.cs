using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class SourceInstanceMapper
    {
        public static McSourceInstance GetMcSourceInstanceFromMcSourceAndDtoSourceInstance(McSource appropriateSource, Common.Data.Models.SourceInstanceDto dtoSourceInstance)
        {
            return new McSourceInstance(appropriateSource, dtoSourceInstance.WallId, dtoSourceInstance.X, dtoSourceInstance.Y, dtoSourceInstance.Width, dtoSourceInstance.Height, dtoSourceInstance.Id);
        }

        public static Common.Data.Models.SourceInstanceDto GetDtoSourceInstanceFromMcSourceInstance(McSourceInstance McSourceInstance)
        {
            return new Common.Data.Models.SourceInstanceDto
            {
                Id = McSourceInstance.SourceInstanceId,
                SourceId = McSourceInstance.SourceId,
                WallId = McSourceInstance.WallId,
                X = McSourceInstance.X,
                Y = McSourceInstance.Y,
                Width = McSourceInstance.Width,
                Height = McSourceInstance.Height
            };
        }

        public static ObservableCollection<McSourceInstance> GetMcSourceInstanceListFromMcSourceListAndDtoSourceInstanceList(IEnumerable<McSource> McSourceList, IEnumerable<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList)
        {

            ObservableCollection<McSourceInstance> McSourceInstanceList = new ObservableCollection<McSourceInstance>();
            foreach (var dtoSourceInstance in dtoSourceInstanceList)
            {
                McSource appropriateSource = McSourceList.FirstOrDefault(s => s.SourceId == dtoSourceInstance.SourceId);
                if (appropriateSource == null)
                    continue;
                McSourceInstanceList.Add(GetMcSourceInstanceFromMcSourceAndDtoSourceInstance(appropriateSource, dtoSourceInstance));
            }

            return McSourceInstanceList;
        }

        public static List<Common.Data.Models.SourceInstanceDto> GetDtoSourceInstanceListFromMcSourceInstanceList(ICollection<McSourceInstance> McSourceInstanceList)
        {

            List<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList = new List<Common.Data.Models.SourceInstanceDto>();
            foreach (var McSourceInstance in McSourceInstanceList)
                dtoSourceInstanceList.Add(GetDtoSourceInstanceFromMcSourceInstance(McSourceInstance));

            return dtoSourceInstanceList;
        }

    }
}
