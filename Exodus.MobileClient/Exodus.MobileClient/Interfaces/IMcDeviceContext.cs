using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.MobileClient.Interfaces
{
    public interface IMcDeviceContext
    {
        Task<T> BeginInvokeOnMainThreadAsync<T>(Func<T> a);
    }
}
