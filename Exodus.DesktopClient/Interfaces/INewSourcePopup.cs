using Exodus.DesktopClient.Data.Types;
using Prism.Interactivity.InteractionRequest;

namespace Exodus.DesktopClient.Interfaces
{
    public interface INewSourcePopup : IConfirmation
    {
        DcSource NewSource { get; set; }
    }
}
