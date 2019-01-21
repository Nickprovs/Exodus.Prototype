using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Types
{
    public class DcDigitalWall : DcWall
    {

        /// <summary>
        /// The constructor. Takes in all properties indivudually.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="sourceInstances"></param>
        public DcDigitalWall(string name, int width, int height, ObservableCollection<DcSourceInstance> sourceInstances, int wallId = 0) : base(name, width, height, sourceInstances, wallId) { }


        /// <summary>
        /// The constructor. Takes in base. The other child properties are taken indivudually.
        /// </summary>
        /// <param name="wall"></param>
        public DcDigitalWall(DcWall wall) : base(wall.Name, wall.Width, wall.Height, wall.SourceInstances, wall.WallId) { }
    } 
}
