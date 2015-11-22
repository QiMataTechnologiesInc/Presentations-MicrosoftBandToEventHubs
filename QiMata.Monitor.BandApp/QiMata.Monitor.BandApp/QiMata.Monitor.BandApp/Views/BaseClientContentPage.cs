using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;

namespace QiMata.Monitor.BandApp.Views
{
    public class BaseClientContentPage : BaseContentPage
    {
        protected BandClient BandClient;
        protected readonly BandDeviceInfo BandInfo;

        public BaseClientContentPage(BandDeviceInfo info, BandClient bandClient)
        {
            BandInfo = info;
            BandClient = bandClient;
        }
    }
}
