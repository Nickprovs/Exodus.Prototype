using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Exodus.MobileClient.Types
{
    public class McSourceInstance : McSource
    {
        #region Fields

        private int _sourceInstanceId;

        private int _wallId;

        private int _x;

        private int _y;

        private int _width;

        private int _height;

        private Rectangle _position;

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
            set
            {
                SetProperty(ref this._x, value);
                this._position.Left = value;
                this.RaisePropertyChanged(nameof(this.Position));
            }
        }

        public int Y
        {
            get { return this._y; }
            set
            {
                SetProperty(ref this._y, value);
                this._position.Top = value;
                this.RaisePropertyChanged(nameof(this.Position));
            }
        }

        public int Width
        {
            get { return this._width; }
            set
            {
                SetProperty(ref this._width, value);
                this._position.Width = value;
                this.RaisePropertyChanged(nameof(this.Position));
            }
        }

        public int Height
        {
            get { return this._height; }
            set
            {
                SetProperty(ref this._height, value);
                this._position.Height = value;
                this.RaisePropertyChanged(nameof(this.Position));
            }
        }

        //Specifically used for a few xamarin bindings. Not meant for setting. Property Changed raised via individual pos / size properties
        public Rectangle Position
        {
            get { return this._position; }
        }

        #endregion

        #region Constructors and Destructors

        public McSourceInstance(int sourceId, int wallId, string name, int defaultWidth, int defaultHeight, string hexColor, int x, int y, int width, int height, int sourceInstanceId = 0)
            : base(name, defaultWidth, defaultHeight, hexColor, sourceId)
        {
            this.SourceInstanceId = sourceInstanceId;
            this.WallId = wallId;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public McSourceInstance(McSource source, int wallId, int x, int y, int width, int height, int sourceInstanceId = 0)
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
