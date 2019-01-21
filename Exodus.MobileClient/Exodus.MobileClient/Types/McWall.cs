using Exodus.MobileClient.Types;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.MobileClient.Types
{
    public class McWall : McBase
    {
        #region Fields

        /// <summary>
        /// The wall id
        /// </summary>
        private int _wallId;

        /// <summary>
        /// The wall name
        /// </summary>
        private string _name;

        /// <summary>
        /// The wall width
        /// </summary>
        private int _width;

        /// <summary>
        /// The wall height
        /// </summary>
        private int _height;

        /// <summary>
        /// The collection of source instances for this wall.
        /// </summary>
        private ObservableCollection<McSourceInstance> _sourceInstances;

        #endregion

        #region Properties

        public int WallId
        {
            get { return _wallId; }
            set { SetProperty(ref _wallId, value); }
        }

        /// <summary>
        /// The wall name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// The wall width
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        /// <summary>
        /// The wall height
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        /// <summary>
        /// The collection of source instances for this wall.
        /// </summary>
        public ObservableCollection<McSourceInstance> SourceInstances
        {
            get { return _sourceInstances; }
            set
            {
                SetProperty(ref _sourceInstances, value);
            }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The wall constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="sourceInstances"></param>
        public McWall(string name, int width, int height, ObservableCollection<McSourceInstance> sourceInstances, int wallId = 0)
        {
            this.WallId = wallId;
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.SourceInstances = sourceInstances;
        }

        #endregion

    }
}
