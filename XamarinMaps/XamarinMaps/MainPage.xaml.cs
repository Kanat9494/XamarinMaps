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

namespace XamarinMaps
{
    public partial class MainPage : ContentPage
    {
        Position position;
        public MainPage()
        {
            InitializeComponent();

            Pin pinTokyo = new Pin()
            {
                Type = PinType.Place,
                Label = "Дом, милый дом",
                Address = "Ак-Орго, Бишкек, Кыргызстан",
                Position = position,
                Rotation = 33.3f,
                Tag = "id_bishkek",
            };

            //map.Pins.Add(pinTokyo);
            //map.MoveToRegion(new MapSpan(position, 2, 2)); 
        }

        async void GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                //var centerMap = new Xamarin.Forms.GoogleMaps.Position(location.La)
                position = new Position(location.Latitude, location.Longitude);
            }
            catch (Exception ex) { }
            
        }
    }
}
