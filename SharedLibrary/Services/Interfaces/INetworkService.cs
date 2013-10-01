﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface INetworkService
    {
        Task<bool> CheckForNetworkAvailability();
    }
}
