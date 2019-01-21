using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Types
{
    public class DcSpaceSession : DcSession
    {
        #region Fields

        /// <summary>
        /// The digital wall in this space session.
        /// </summary>
        private DcDigitalWall _digitalWall;

        #endregion

        #region Properties

        /// <summary>
        /// The digital wall in this space session
        /// </summary>
        public DcDigitalWall DigitalWall
        {
            get { return this._digitalWall; }
            set { SetProperty(ref this._digitalWall, value); }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor. Takes in all parameters individually.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="digitalWall"></param>
        public DcSpaceSession(string name, DcDigitalWall digitalWall, int sessionId = 0) : base (name, sessionId)
        {
            this.DigitalWall = digitalWall;            
        }

        /// <summary>
        /// The constructor. Takes in parent object, but all child properties are taken in individually/seperately. 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="digitalWall"></param>
        public DcSpaceSession(DcSession session, DcDigitalWall digitalWall, int sessionId = 0) : base(session.Name, session.SessionId)
        {
            this.DigitalWall = digitalWall;
        }

        #endregion

    }
}
