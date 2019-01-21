using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.WebServer.EntityMappers
{
    public static class SourceInstanceMapper
    {
        public static SourceInstance GetEntSourceInstanceFromDtoSourceInstance(Common.Data.Models.SourceInstanceDto dtoSourceInstance)
        {
            return new SourceInstance
            {
                Id = dtoSourceInstance.Id,
                SourceId = dtoSourceInstance.SourceId,
                WallId = dtoSourceInstance.WallId,
                X = dtoSourceInstance.X,
                Y = dtoSourceInstance.Y,
                Width = dtoSourceInstance.Width,
                Height = dtoSourceInstance.Height,
            };
        }

        public static Common.Data.Models.SourceInstanceDto GetDtoSourceInstanceFromEntSourceInstance(SourceInstance entSourceInstance)
        {
            return new Common.Data.Models.SourceInstanceDto
            {
                Id = entSourceInstance.Id,
                SourceId = entSourceInstance.SourceId,
                WallId = entSourceInstance.WallId,
                X = entSourceInstance.X,
                Y = entSourceInstance.Y,
                Width = entSourceInstance.Width,
                Height = entSourceInstance.Height
            };
        }

        public static ICollection<SourceInstance> GetEntSourceInstanceListFromDtoSourceInstanceList(IList<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList)
        {

            List<SourceInstance> entSourceInstanceList = new List<SourceInstance>();
            foreach(var dtoSourceInstance in dtoSourceInstanceList)          
                entSourceInstanceList.Add(GetEntSourceInstanceFromDtoSourceInstance(dtoSourceInstance));

            return entSourceInstanceList;           
        }

        public static List<Common.Data.Models.SourceInstanceDto> GetDtoSourceInstanceListFromEntSourceInstanceList(IEnumerable<SourceInstance> entSourceInstanceList)
        {

            List<Common.Data.Models.SourceInstanceDto> dtoSourceInstanceList = new List<Common.Data.Models.SourceInstanceDto>();
            foreach (var entSourceInstance in entSourceInstanceList)
                dtoSourceInstanceList.Add(GetDtoSourceInstanceFromEntSourceInstance(entSourceInstance));

            return dtoSourceInstanceList;
        }
    }
}
