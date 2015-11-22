using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.Azure.EventHub;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Sensors;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace QiMata.Monitor.BandApp.ViewModels
{
    class MonitorViewModel : BaseClientViewModel
    {
        private Task _uploadEventTask;
        private ConcurrentQueue<BandHeartRateReading> _bandHeartRateReadings;
        private BandSensorManager _sensorManager;
        private EventHubSasClient _eventHubSasClient;

        public MonitorViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            _bandHeartRateReadings = new ConcurrentQueue<BandHeartRateReading>();
            HubName = "test";
            DeviceName = "TestDevice";
            ServiceNamespace = "bandapptest";
            SharedAccessSignature = "Endpoint=sb://bandapptest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=bpvFoZ+tmfm1S1dK1hJfv0tarkhLr9e/NOffm4CCIis=";
        }

        public override async Task Prepare()
        {
            BandClient = await BandClientManager.Instance.ConnectAsync(BandInfo);
            _sensorManager = BandClient.SensorManager;
            await _sensorManager.HeartRate.RequestUserConsent();
            _sensorManager.HeartRate.ReadingChanged += (sender, args) =>
            {
                _bandHeartRateReadings.Enqueue(args.SensorReading);

                if (_uploadEventTask == null || (_uploadEventTask != null && _uploadEventTask.IsCompleted))
                {
                    _uploadEventTask = UploadToEventHubs();
                }
            };
        }

        private async Task UploadToEventHubs()
        {
            if (_eventHubSasClient != null)
            {
                while (!_bandHeartRateReadings.IsEmpty)
                {
                    BandHeartRateReading reading;
                    _bandHeartRateReadings.TryDequeue(out reading);
                    await _eventHubSasClient.SendMessageAsync(JsonConvert.SerializeObject(reading));
                }
            }
        }

        private string _sharedAccessSignature;

        public string SharedAccessSignature
        {
            get { return _sharedAccessSignature; }
            set
            {
                _sharedAccessSignature = value;
                this.OnPropertyChanged();
            }
        }

        private string _serviceNamespace;

        public string ServiceNamespace
        {
            get { return _serviceNamespace; }
            set
            {
                _serviceNamespace = value;
                this.OnPropertyChanged();
            }
        }

        private string _hubName;

        public string HubName
        {
            get { return _hubName; }
            set
            {
                _hubName = value;
                this.OnPropertyChanged();
            }
        }

        private string _deviceName;

        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                this.OnPropertyChanged();
            }
        }

        private Command _onClick;

        public Command OnClick
        {
            get
            {
                return _onClick ?? (_onClick = new Command(async () =>
                {
                    try
                    {
                        _eventHubSasClient = new EventHubSasClient(SharedAccessSignature, ServiceNamespace, HubName, DeviceName);
                        await _sensorManager.HeartRate.StartReadingsAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw ex;
                    }
                    
                }));
            }
        }
    }
}
