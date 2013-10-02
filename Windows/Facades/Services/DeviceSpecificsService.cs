using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Facades.Services
{
    public class DeviceSpecificsService : IDeviceSpecificsService
    {
        public int MaxRows
        {
            get
            {
                int height = (int)Window.Current.Bounds.Height;
                int controlHeight = height - 200; // Header elements on page
                return controlHeight / 140;
            }
        }
    }
}
