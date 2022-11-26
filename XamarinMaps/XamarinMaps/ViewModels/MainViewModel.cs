using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinMaps.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel() { }

        public class VehicleLocations
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }   
        }

        internal async Task<List<VehicleLocations>> LoadVehicles()
        {
            List<VehicleLocations> vehicleLocations = new List<VehicleLocations>()
             {
                 new VehicleLocations {Latitude = 42.849566, Longitude = 74.514990},
                 new VehicleLocations {Latitude = 42.853027, Longitude = 74.514132}, 
                 new VehicleLocations {Latitude = 42.850762, Longitude = 74.514303},
                 new VehicleLocations {Latitude = 42.848733, Longitude = 74.517329},
             };

            return vehicleLocations;
        }
    }
}
