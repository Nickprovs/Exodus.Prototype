using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Types
{
    public class DcSourceInstance : DcSource
    {
        #region Fields

        private int _sourceInstanceId;

        private int _wallId;

        private int _x;

        private int _y;

        private int _width;

        private int _height;

        #endregion

        #region Properties

        public int SourceInstanceId
        {
            get { return this._sourceInstanceId; }
            set { SetProperty(ref this._sourceInstanceId, value); }
        }

        public int WallId
        {
            get { return this._wallId; }
            set { SetProperty(ref this._wallId, value); }
        }

        public int X
        {
            get { return this._x; }
            set { SetProperty(ref this._x, value); }
        }

        public int Y
        {
            get { return this._y; }
            set { SetProperty(ref this._y, value); }
        }

        public int Width
        {
            get { return this._width; }
            set { SetProperty(ref this._width, value); }
        }

        public int Height
        {
            get { return this._height; }
            set { SetProperty(ref this._height, value); }
        }

        #endregion


        #region Constructors and Destructors

        public DcSourceInstance(int sourceId, int wallId, string name, int defaultWidth, int defaultHeight, string hexColor, int x, int y, int width, int height, int sourceInstanceId = 0) 
            : base(name, defaultWidth, defaultHeight, hexColor, sourceId)
        {
            this.SourceInstanceId = sourceInstanceId;
            this.WallId = wallId;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public DcSourceInstance(DcSource source, int wallId, int x, int y, int width, int height, int sourceInstanceId = 0)
            : base(source.Name, source.DefaultWidth, source.DefaultHeight, source.Color, source.SourceId)
        {
            this.SourceInstanceId = sourceInstanceId;
            this.WallId = wallId;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        #endregion
    }
}
