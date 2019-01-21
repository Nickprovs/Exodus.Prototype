using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.MobileClient.Types
{
    public class McSession : McBase
    {
        #region Fields

        /// <summary>
        /// The session id
        /// </summary>
        private int _sessionId;

        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        #endregion


        #region Properties

        /// <summary>
        /// The session id
        /// </summary>
        public int SessionId
        {
            get { return _sessionId; }
            set { SetProperty(ref _sessionId, value); }
        }

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion


        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        public McSession(string name, int sessionId = 0)
        {
            this.SessionId = sessionId;
            this.Name = name;
        }

        #endregion

    }
}
