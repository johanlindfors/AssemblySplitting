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
                var height = Window.Current.Bounds.Height;
                if (height <= 768)
                    return 4;
                else if (height <= 900)
                    return 5;
                return 6;
            }
        }
    }
}
