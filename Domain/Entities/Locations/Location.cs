using Microsoft.EntityFrameworkCore; // [Owned] için bu kütüphaneyi eklemelisin

namespace HelpPawApi.Domain.Entities.Locations
{
    [Owned]
    public class Location
    {

        private double _lat ;

        public double latitude
        {
            get => _lat; set
            {
                if (value < -90 || value > 90)
                    throw new ArgumentException("Latitude -90 ile 90 derece arasında olmalıdır.");

                _lat = value;
            }
        }

        private double _long;
        public double longitude
        {
            get => _long; set
            {
                if (value < -180 || value > 180)
                    throw new ArgumentException("Longitude -180 ile 180 derece arasında olmalıdır.");

                _long = value;
            }
        }
        public Location(double lat,double lng)
        {
            latitude = lat;
            longitude = lng;    
        }

        public Location()
        {
            
        }
    }
}
