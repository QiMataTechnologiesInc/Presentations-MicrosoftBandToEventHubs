using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using QiMata.Monitor.BandApp.ViewModels;
using Xamarin.Forms;

namespace QiMata.Monitor.BandApp.Views
{
    public partial class MonitorPage : BaseClientContentPage
    {

        public MonitorPage(BandDeviceInfo bandDeviceInfo)
            : base(bandDeviceInfo,null)
        {
            InitializeComponent();

            ViewModel = new MonitorViewModel(BandInfo,BandClient);
        }
    }
}
