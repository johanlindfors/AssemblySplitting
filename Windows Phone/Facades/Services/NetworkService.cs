using Microsoft.Phone.Net.NetworkInformation;
using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Facades.Services
{
    public class NetworkService : INetworkService
    {
        public Task<bool> CheckForNetworkAvailability()
        {
            return Task.FromResult<bool>(NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None);
        }
    }
}
