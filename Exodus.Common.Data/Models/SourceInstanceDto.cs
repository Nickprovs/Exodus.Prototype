using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.Common.Data.Models
{
    public class SourceInstanceDto
    {
        //Source Instance Stuff
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int WallId { get; set; }

        //Source Specific Stuff
        public string Name { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public string HexColor { get; set; }
    }
}
