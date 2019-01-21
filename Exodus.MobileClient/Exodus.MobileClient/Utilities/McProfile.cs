using Exodus.MobileClient.Interfaces;
using Exodus.MobileClient.Types;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.Utilities
{
    public class McProfile : BindableBase, IMcProfile, IDisposable
    {

        #region Fields

        private ObservableCollection<McSource> _sources;

        private ObservableCollection<McDigitalWall> _digitalWalls;

        private ObservableCollection<McSpaceSession> _spaceSessions;

        #endregion

        #region Properties

        public ObservableCollection<McSource> Sources
        {
            get { return _sources; }
            set { SetProperty(ref _sources, value); }
        }

        public ObservableCollection<McSpaceSession> SpaceSessions
        {
            get { return _spaceSessions; }
            set { SetProperty(ref _spaceSessions, value); }
        }

        public ObservableCollection<McDigitalWall> DigitalWalls
        {
            get { return _digitalWalls; }
            set { SetProperty(ref _digitalWalls, value); }
        }

        #endregion

        #region Constructors and Destructors

        public McProfile()
        {
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {

        }

        #endregion
    }
}
