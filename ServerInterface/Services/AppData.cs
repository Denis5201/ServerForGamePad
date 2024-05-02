using System.Net;
using System.Net.NetworkInformation;

namespace ServerInterface.Services
{
    public static class AppData
    {
        public static List<string> GetApi()
        {
            var api = new List<string>();
            var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var net in netInterfaces)
            {
                var address = net.GetIPProperties().UnicastAddresses.LastOrDefault();
                if (address?.PrefixOrigin == PrefixOrigin.Dhcp)
                {
                    api.Add(address.Address.ToString());
                }
            }
            return api;
        }
    }
}
