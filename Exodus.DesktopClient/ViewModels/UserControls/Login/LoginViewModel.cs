using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.PubSub.Args;
using Exodus.DesktopClient.PubSub.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.ViewModels.UserControls.Login
{
    public class LoginViewModel : BindableBase
    {

        #region Fields

        private IDcSignalrProxy _signalrProxy;

        private IEventAggregator _eventAggregator;

        private string _ipAddress = GetLocalIPAddress();

        private string _port = "9999";

        private bool _loginError = false;

        #endregion

        #region Properties

        public DelegateCommand LoginCommand { get; set; }

        public string IpAddress
        {
            get { return this._ipAddress; }
            set { SetProperty(ref this._ipAddress, value); }
        }


        public string Port
        {
            get { return this._port; }
            set { SetProperty(ref this._port, value); }
        }


        public bool LoginError
        {
            get { return this._loginError; }
            set { SetProperty(ref this._loginError, value); }
        }

        #endregion

        #region Constructors and Destructors

        public LoginViewModel(IDcSignalrProxy signalrProxy, IEventAggregator eventAggregator)
        {
            this._signalrProxy = signalrProxy;
            this._eventAggregator = eventAggregator;
            this._eventAggregator.GetEvent<ConnectedEvent>().Subscribe(this.OnConnected);
            this.LoginCommand = new DelegateCommand(this.OnLoginRequest);
        }

        #endregion

        #region Public Methods

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Here we would try to login as a user. For this test app all we'd do in here is maybe some UI Cleanup. 
        /// The control shell is the one that will do the navigation.
        /// </summary>
        /// <param name="args"></param>
        private void OnConnected(ConnectionEventArgs args)
        {            

        }

        /// <summary>
        /// Here we request to connect to the signalr webserver
        /// </summary>
        private async void OnLoginRequest()
        {
            bool ipIsValid = await this.IsValidIp(this.IpAddress);
            if(ipIsValid)
                this._signalrProxy.ConnectAsync(this.IpAddress, this.Port);
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
