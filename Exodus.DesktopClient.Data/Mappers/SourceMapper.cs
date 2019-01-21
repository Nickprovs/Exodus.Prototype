using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Mappers
{
    public static class SourceMapper
    {
        public static DcSource GetDcSourceFromDtoSource(Common.Data.Models.SourceDto dtoSource)
        {
            return new DcSource(dtoSource.Name, dtoSource.DefaultWidth, dtoSource.DefaultHeight, dtoSource.HexColor, dtoSource.Id);
        }

        public static Common.Data.Models.SourceDto GetDtoSourceFromDcSource(DcSource dcSource)
        {
            //Removed the alpha values because WPF has shit in a weird format
            string nonAlphaHexColor = "#0000ff";
            string alphaHexColor = dcSource.Color.ToString();

            try
            {
                nonAlphaHexColor = $"{alphaHexColor.Substring(0, 1)}{alphaHexColor.Substring(3, 6)}";
            }
            catch (Exception e)
            {
                nonAlphaHexColor = "#0000ff";
                Debug.WriteLine("Error removing alpha value when parsing new source color");
            }

            return new Common.Data.Models.SourceDto
            {
                Id = dcSource.SourceId,
                Name = dcSource.Name,
                HexColor = nonAlphaHexColor,
                DefaultWidth = dcSource.DefaultWidth,
                DefaultHeight = dcSource.DefaultHeight
            };
        }

        public static ObservableCollection<DcSource> GetDcSourceListFromDtoSourceList(IEnumerable<Common.Data.Models.SourceDto> dtoSourceList)
        {

            ObservableCollection<DcSource> DcSourceList = new ObservableCollection<DcSource>();
            foreach (var dtoSource in dtoSourceList)
                DcSourceList.Add(GetDcSourceFromDtoSource(dtoSource));

            return DcSourceList;
        }

        public static List<Common.Data.Models.SourceDto> GetDtoSourceListFromDcSourceList(IEnumerable<DcSource> DcSourceList)
        {

            List<Common.Data.Models.SourceDto> dtoSourceList = new List<Common.Data.Models.SourceDto>();
            foreach (var DcSource in DcSourceList)
                dtoSourceList.Add(GetDtoSourceFromDcSource(DcSource));

            return dtoSourceList;
        }
    }
}
