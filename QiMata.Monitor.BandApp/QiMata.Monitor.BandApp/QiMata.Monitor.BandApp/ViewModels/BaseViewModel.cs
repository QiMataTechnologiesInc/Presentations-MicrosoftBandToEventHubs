using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.Monitor.BandApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task Prepare()
        {
            // this fires each time the view model appears
            return Task.FromResult(true);
        }

        public virtual Task CleanUp()
        {
            // this fires each time the view model disappears
            return Task.FromResult(true);
        }

        public virtual Task Destroy()
        {
            // this fires each time the view model disappears
            return Task.FromResult(true);
        }
    }
}
