using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class SourceMapper
    {
        public static Source GetEntSourceFromDtoSource(Common.Data.Models.SourceDto dtoSource)
        {
            return new Source
            {
                Id = dtoSource.Id,
                Name = dtoSource.Name,
                HexColor = dtoSource.HexColor,
                DefaultWidth = dtoSource.DefaultWidth,
                DefaultHeight = dtoSource.DefaultHeight
            };
        }

        public static Common.Data.Models.SourceDto GetDtoSourceFromEntSource(Source entSource)
        {
            return new Common.Data.Models.SourceDto
            {
                Id = entSource.Id,
                Name = entSource.Name,
                HexColor = entSource.HexColor,
                DefaultWidth = entSource.DefaultWidth,
                DefaultHeight = entSource.DefaultHeight
            };
        }

        public static IEnumerable<Source> GetEntSourceListFromDtoSourceList(IEnumerable<Common.Data.Models.SourceDto> dtoSourceList)
        {

            List<Source> entSourceList = new List<Source>();
            foreach (var dtoSource in dtoSourceList)
                entSourceList.Add(GetEntSourceFromDtoSource(dtoSource));

            return entSourceList;
        }

        public static List<Common.Data.Models.SourceDto> GetDtoSourceListFromEntSourceList(IEnumerable<Source> entSourceList)
        {

            List<Common.Data.Models.SourceDto> dtoSourceList = new List<Common.Data.Models.SourceDto>();
            foreach (var entSource in entSourceList)
                dtoSourceList.Add(GetDtoSourceFromEntSource(entSource));

            return dtoSourceList;
        }
    }
}
