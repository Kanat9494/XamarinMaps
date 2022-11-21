using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace XamarinMaps
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Pin pinTokyo = new Pin()
            {
                Type = PinType.Place,
                Label = "Дом, милый дом",
                Address = "Ак-Орго, Бишкек, Кыргызстан",
                Position = new Position(42.851889, 74.517327),
                Rotation = 33.3f,
                Tag = "id_bishkek",
            };

            map.Pins.Add(pinTokyo);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinTokyo.Position, Distance.FromMeters(5000))); 
        }
    }
}
