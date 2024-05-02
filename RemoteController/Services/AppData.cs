using System.Net;

namespace RemoteController.Services
{
    public static class AppData
    {
        public static List<string> GetApi()
        {
            var host = Dns.GetHostAddresses(Dns.GetHostName(), System.Net.Sockets.AddressFamily.InterNetwork);
            var list = new List<string>();
            foreach (var ip in host)
            {
                list.Add(ip.ToString());
            }
            return list;
        }
    }
}
