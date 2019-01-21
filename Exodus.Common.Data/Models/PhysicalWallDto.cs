using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.Common.Data.Models
{
    public class PhysicalWallDto
    {
        //Physical Wall specific things go here
        public int Id { get; set; }

        //Base class things go here
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
