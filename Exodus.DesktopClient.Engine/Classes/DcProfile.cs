using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Engine.Interfaces;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Exodus.DesktopClient.Engine.Classes
{ 
    /// Wraps Dc Data
    /// </summary>
    public class DcProfile : BindableBase, IDcProfile
    {

        #region Fields

        private ObservableCollection<DcSource> _sources;

        private ObservableCollection<DcDigitalWall> _digitalWalls;

        private ObservableCollection<DcSpaceSession> _spaceSessions;

        #endregion

        #region Properties

        public ObservableCollection<DcSource> Sources
        {
            get { return _sources; }
            set { SetProperty(ref _sources, value); }
        }

        public ObservableCollection<DcSpaceSession> SpaceSessions
        {
            get { return _spaceSessions; }
            set { SetProperty(ref _spaceSessions, value); }
        }

        public ObservableCollection<DcDigitalWall> DigitalWalls
        {
            get { return _digitalWalls; }
            set { SetProperty(ref _digitalWalls, value); }
        }

        #endregion

        #region Constructors and Destructors

        public DcProfile()
        {
        }

        #endregion
    }
}
