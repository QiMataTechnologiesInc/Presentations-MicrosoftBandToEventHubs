﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;

namespace QiMata.Monitor.BandApp.ViewModels
{
    public abstract class BaseClientViewModel : BaseViewModel
    {
        private bool isLoading;

        public BaseClientViewModel(BandDeviceInfo info, BandClient bandClient)
        {
            BandClient = bandClient;
            BandInfo = info;
        }

        public override async Task Prepare()
        {
            await base.Prepare();

            BandName = BandInfo.Name;
            IsConnected = BandClient.IsConnected;
        }

        public BandDeviceInfo BandInfo { get; protected set; }

        public BandClient BandClient { get; protected set; }

        public string BandName { get; private set; }

        public bool IsConnected { get; private set; }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }
    }
}
