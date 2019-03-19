using System;
using System.Net;
using System.Threading.Tasks;
using CoreFoundation;
using MeusPedidosiOS.Contracts;
using MeusPedidosiOS.Models;
using Plugin.Connectivity;
using SystemConfiguration;

namespace MeusPedidosiOS.Services.ConnectionService
{
    public class ConnectionService : IConnectionService
    {
        private static NetworkReachability _defaultRouteReachability;

        public static event EventHandler ReachabilityChanged;

        public async Task<Response> CheckConnection()
        {

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Please turn on your internet settings.",
                    };
                }

                var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                    "google.com");
                if (!isReachable)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Verifique sua conexao com a rede.",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public bool CheckConnectionTest() 
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }
            else 
            {
                return true;
            }           
        }

        public Response CheckConnectionApi()
        {

            try
            {

                bool isConnected = IsNetworkAvailable();

                if (!isConnected)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Please turn on your internet settings.",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public bool IsNetworkAvailable()
        {
            if (_defaultRouteReachability == null)
            {
                _defaultRouteReachability = new NetworkReachability(new IPAddress(0));
                _defaultRouteReachability.SetNotification(OnChange);
                _defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            NetworkReachabilityFlags flags;

            return _defaultRouteReachability.TryGetFlags(out flags) &&
                IsReachableWithoutRequiringConnection(flags);
        }

        private static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            // Since the network stack will automatically try to get the WAN up,
            // probe that
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;

            return isReachable && noConnectionRequired;
        }

        private static void OnChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChanged;
            if (h != null)
                h(null, EventArgs.Empty);
        }


    }
}
