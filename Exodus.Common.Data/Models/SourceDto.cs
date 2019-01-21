using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.Common.Data.Models
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public string HexColor { get; set; }
    }
}
