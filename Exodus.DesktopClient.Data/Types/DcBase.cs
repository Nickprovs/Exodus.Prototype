using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Data.Types
{
    public class DcBase : BindableBase
    {

        #region Fields

        /// <summary>
        /// Determines whether or not the item is selected in the list.
        /// </summary>
        public bool _isListItemSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Determines whether or not the item is selected in the list.
        /// </summary>
        public bool IsListItemSelected
        {
            get { return _isListItemSelected; }
            set { SetProperty(ref _isListItemSelected, value); }
        }

        #endregion
    }
}
