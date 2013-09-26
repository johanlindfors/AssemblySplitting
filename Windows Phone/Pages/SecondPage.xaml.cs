﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System.Diagnostics;

namespace Pages
{
    public partial class SecondPage : PhoneApplicationPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            foreach (var item in NavigationContext.QueryString)
            {
                Debug.WriteLine("Parameter '{0}' = '{1}'", item.Key, item.Value);
            }
        }
    }
}