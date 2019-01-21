using Exodus.MobileClient.Interfaces;
using Exodus.MobileClient.PubSub.Args;
using Exodus.MobileClient.PubSub.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exodus.MobileClient.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Fields

        private IMcSignalrProxy _signalrProxy;

        private IEventAggregator _eventAggregator;

        private IMcDeviceContext _deviceContext;

        private string _ipAddress;

        #endregion

        #region Properties

        public DelegateCommand ConnectCommand { get; }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        #endregion

        #region Constructors and Destructors

        public LoginPageViewModel(IMcDeviceContext deviceContext, INavigationService navigationService, IMcSignalrProxy signalrProxy, IEventAggregator eventAggregator) : base(navigationService)
        {
            this.Title = "Login";

            this._signalrProxy = signalrProxy;
            this._eventAggregator = eventAggregator;
            this._deviceContext = deviceContext;

            this._eventAggregator.GetEvent<ConnectedEvent>().Subscribe(this.OnConnected, ThreadOption.UIThread);

            this.ConnectCommand = new DelegateCommand(this.OnConnect);
        }

        #endregion

        #region Private Methods

        private async void OnConnect()
        {
            bool ipIsValid = await this.IsValidIp(this.IpAddress);

            if(ipIsValid)
                this._signalrProxy.ConnectAsync(this.IpAddress, "9999");
        }

        private async void OnConnected(ConnectionEventArgs args)
        {
            //The / "indicates an absolute URI, which is good because it'll reset the navigation stack. We don't want to come back to login page unless connection issue.
            var result = await this._navigationService.NavigateAsync("/Index");

        }

        private async Task<bool> IsValidIp(string ipAddress)
        {
            IPHostEntry host;
            IPAddress validIp;

            try
            {
                //If the ip is avalid
                if (IPAddress.TryParse(ipAddress, out validIp))
                {
                   //If the host is responsive
                    host = await Dns.GetHostEntryAsync(ipAddress);
                    validIp = host.AddressList.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork);

                    //If we have any invalid network info... return false
                    if (validIp == null || host == null)
                        return false;
                    //Otherwise return true
                    else
                        return true;
                }
            }
            catch (SocketException)
            {
                Debug.WriteLine("Could not connect to host");
            }

            //If we couldn't make contact and we resulted in an exception, also return false;
            return false;
        }

        #endregion
    }
}
