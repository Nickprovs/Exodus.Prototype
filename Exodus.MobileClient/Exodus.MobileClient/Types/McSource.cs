﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Exodus.MobileClient.Types
{
    public class McSource : McBase
    {
        #region Fields
        /// <summary>
        /// The Id.
        /// </summary>
        private int _sourceId;

        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        /// <summary>
        /// The default width
        /// </summary>
        private int _defaultWidth;

        /// <summary>
        /// The default height
        /// </summary>
        private int _defaultHeight;

        /// <summary>
        /// The color of the source
        /// </summary>
        private Color _color;

        #endregion

        #region Properties

        /// <summary>
        /// The Id.
        /// </summary>
        public int SourceId
        {
            get { return _sourceId; }
            set { SetProperty(ref _sourceId, value); }
        }

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// The default width
        /// </summary>
        public int DefaultWidth
        {
            get { return _defaultWidth; }
            set { SetProperty(ref _defaultWidth, value); }
        }

        /// <summary>
        /// The default height
        /// </summary>
        public int DefaultHeight
        {
            get { return _defaultHeight; }
            set { SetProperty(ref _defaultHeight, value); }
        }

        /// <summary>
        /// The color of the source
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// The constructor for a DC source. Takes in a name, default size, and color as a hex string.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultWidth"></param>
        /// <param name="defaultHeight"></param>
        /// <param name="hexColor"></param>
        public McSource(string name, int defaultWidth, int defaultHeight, string hexColor, int sourceId = 0)
        {
            this.SourceId = sourceId;
            this.Name = name;
            this.DefaultWidth = defaultWidth;
            this.DefaultHeight = defaultHeight;
            this.Color = Color.FromHex(hexColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultWidth"></param>
        /// <param name="defaultHeight"></param>
        /// <param name="color"></param>
        public McSource(string name, int defaultWidth, int defaultHeight, Color color, int sourceId = 0)
        {
            this.SourceId = sourceId;
            this.Name = name;
            this.DefaultWidth = defaultWidth;
            this.DefaultHeight = defaultHeight;
            this.Color = color;
        }
        #endregion
    }
}
