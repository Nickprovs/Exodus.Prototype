using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.Types.Mappers
{
    public static class SourceMapper
    {
        public static McSource GetMcSourceFromDtoSource(Common.Data.Models.SourceDto dtoSource)
        {
            return new McSource(dtoSource.Name, dtoSource.DefaultWidth, dtoSource.DefaultHeight, dtoSource.HexColor, dtoSource.Id);
        }

        public static Common.Data.Models.SourceDto GetDtoSourceFromMcSource(McSource McSource)
        {
            return new Common.Data.Models.SourceDto
            {
                Id = McSource.SourceId,
                Name = McSource.Name,
                HexColor = McSource.Color.ToString(),
                DefaultWidth = McSource.DefaultWidth,
                DefaultHeight = McSource.DefaultHeight
            };
        }

        public static ObservableCollection<McSource> GetMcSourceListFromDtoSourceList(IEnumerable<Common.Data.Models.SourceDto> dtoSourceList)
        {

            ObservableCollection<McSource> McSourceList = new ObservableCollection<McSource>();
            foreach (var dtoSource in dtoSourceList)
                McSourceList.Add(GetMcSourceFromDtoSource(dtoSource));

            return McSourceList;
        }

        public static List<Common.Data.Models.SourceDto> GetDtoSourceListFromMcSourceList(IEnumerable<McSource> McSourceList)
        {

            List<Common.Data.Models.SourceDto> dtoSourceList = new List<Common.Data.Models.SourceDto>();
            foreach (var McSource in McSourceList)
                dtoSourceList.Add(GetDtoSourceFromMcSource(McSource));

            return dtoSourceList;
        }
    }
}
