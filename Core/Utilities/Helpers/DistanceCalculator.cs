using Geolocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public static class DistanceCalculator
    {
        public static Boundaries GetBoundaries(LatLng point, int radius)
        {
            CoordinateBoundaries boundaries = new CoordinateBoundaries(point.Latitude, point.Longitude, radius,DistanceUnit.Kilometers);
            return new Boundaries()
            {
                MinLatitude = boundaries.MinLatitude,
                MaxLatitude = boundaries.MaxLatitude,
                MaxLongitude = boundaries.MaxLongitude,
                MinLongitude = boundaries.MinLongitude
            };
        }
        public static double GetDistance(LatLng point1, LatLng point2)
        {
            return GeoCalculator.GetDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, 2, DistanceUnit.Meters);
        }
    }
    public class LatLng
    {
        public LatLng(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class Boundaries
    {
        public double MinLatitude { get; set; }
        public double MaxLatitude { get; set; }
        public double MinLongitude { get; set; }
        public double MaxLongitude { get; set; }
    }
}
