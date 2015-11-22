using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Band.Portable;
using Xamarin.Forms;

namespace QiMata.Monitor.BandApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Bands = new ObservableCollection<BandDeviceInfo>();

            LoadBandsCommand = new Command(async () => await LoadBands());
        }

        public ICommand LoadBandsCommand { get; private set; }

        public ObservableCollection<BandDeviceInfo> Bands { get; private set; }

        public override async Task Prepare()
        {
            await base.Prepare();

            // refresh the list
            LoadBandsCommand.Execute(null);
        }

        private async Task LoadBands()
        {
            Bands.Clear();
            IEnumerable<BandDeviceInfo> bands = await BandClientManager.Instance.GetPairedBandsAsync();
            foreach (var band in bands)
            {
                Bands.Add(band);
            }
        }
    }
}
