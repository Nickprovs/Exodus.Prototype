using Exodus.MobileClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exodus.MobileClient.Utilities
{
    public class McDeviceContext : IMcDeviceContext
    {
        public Task RunOnMainThreadAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    try
                    {
                        action();
                        tcs.SetResult(null);
                    }
                    catch (Exception e)
                    {
                        tcs.SetException(e);
                    }
                });

            return tcs.Task;
        }

        public Task<T> BeginInvokeOnMainThreadAsync<T> (Func<T> a)
        {
            var tcs = new TaskCompletionSource<T>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var result = a();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        } 
    }
}
