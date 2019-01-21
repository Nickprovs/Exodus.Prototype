using Exodus.DesktopClient.Data.Types;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Interfaces
{
    public interface INewSpaceSessionPopup : IConfirmation
    {
        DcSpaceSession NewSpaceSession { get; set; }
    }
}
