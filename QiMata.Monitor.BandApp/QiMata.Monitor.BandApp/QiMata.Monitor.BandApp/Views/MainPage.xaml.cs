using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using QiMata.Monitor.BandApp.ViewModels;
using Xamarin.Forms;

namespace QiMata.Monitor.BandApp.Views
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
        }

        private void BandItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var info = (BandDeviceInfo)e.SelectedItem;

                Navigation.PushAsync(new MonitorPage(info));
            }
        }
    }
}
