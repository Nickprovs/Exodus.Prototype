using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.Common.Data.Models
{
    public class SpaceSessionDto
    {
        //Space Session Specific Things Go Here
        public int Id { get; set; }
        public int DigitalWallId { get; set; }

        //Base Session Things Go Here
        public string Name { get; set; }
    }
}
