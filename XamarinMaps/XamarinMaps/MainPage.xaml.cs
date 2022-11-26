using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XamarinMaps.ViewModels;
using XamarinMaps.ViewModels;

namespace XamarinMaps
{
    public partial class MainPage : ContentPage
    {
        Position position;
        MainViewModel mainViewModel;
        double headerNorthValue;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();
        }

        async Task GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                position = new Position(location.Latitude, location.Longitude);
            }
            catch (Exception ex) { }
            
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            //await GetLocation();
            var contents = await mainViewModel.LoadVehicles();

            if (contents != null)
            {
                foreach (var item in contents)
                {
                    Pin VehiclePins = new Pin()
                    {
                        Label = "Buses",
                        Type = PinType.Place,
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                        Position = new Position(item.Latitude, item.Longitude),
                    };

                    map.Pins.Add(VehiclePins);
                }
            }

            var position2 = new Position(42.851863, 74.517350);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position2, Distance.FromMeters(500 )));
        }

        void UpdateButton_Clicked(object sender, EventArgs e)
        {
            var position2 = new Position(42.851863, 74.517350);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position2, Distance.FromMeters(500)));

            Device.StartTimer(TimeSpan.FromSeconds(5), () => TimerStarted());
        }

        private bool TimerStarted()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
                Compass.ReadingChanged += Compass_ReadingChanged;

                map.Pins.Clear();
                map.Polylines.Clear();

                var contents = await mainViewModel.LoadVehicles();

                if (contents != null)
                {
                    foreach (var item in contents)
                    {
                        Pin VehiclePins = new Pin()
                        {
                            Label = "Buses",
                            Type = PinType.Place,
                            Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                            Position = new Position(item.Latitude, item.Longitude),
                            Rotation = ToRotationPoints(headerNorthValue)
                        };

                        map.Pins.Add(VehiclePins);
                    }
                }
            });

            Compass.Stop();
            return true;
        }

        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            headerNorthValue = data.HeadingMagneticNorth;
        }

        private float ToRotationPoints(double headerNorthValue)
        {
            return (float)headerNorthValue;
        }
    }
}
