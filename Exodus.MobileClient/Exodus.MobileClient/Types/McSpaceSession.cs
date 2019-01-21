using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.MobileClient.Types
{
    public class McSpaceSession : McSession
    {
        #region Fields

        /// <summary>
        /// The digital wall in this space session.
        /// </summary>
        private McDigitalWall _digitalWall;

        #endregion

        #region Properties

        /// <summary>
        /// The digital wall in this space session
        /// </summary>
        public McDigitalWall DigitalWall
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
        public McSpaceSession(string name, McDigitalWall digitalWall, int sessionId = 0) : base(name, sessionId)
        {
            this.DigitalWall = digitalWall;
        }

        /// <summary>
        /// The constructor. Takes in parent object, but all child properties are taken in individually/seperately. 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="digitalWall"></param>
        public McSpaceSession(McSession session, McDigitalWall digitalWall, int sessionId = 0) : base(session.Name, session.SessionId)
        {
            this.DigitalWall = digitalWall;
        }

        #endregion
    }
}
