using System;

namespace TrallyRally.Helpers
{
    public class Haversine
    {
        protected const double EarthRadiusKm = 6376.5;

        public static double GetDistance(double lat1, double long1, double lat2, double long2)
        {
            /*
                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c
            */

            double lat1InRad = DegsToRad(lat1);
            double long1InRad = DegsToRad(long1);
            double lat2InRad = DegsToRad(lat2);
            double long2InRad = DegsToRad(long2);

            double longitude = long2InRad - long1InRad;
            double latitude = lat2InRad - lat1InRad;

            double a = Math.Pow(Math.Sin(latitude / 2.0), 2.0) +
                       Math.Cos(lat1InRad) * Math.Cos(lat2InRad) *
                       Math.Pow(Math.Sin(longitude / 2.0), 2.0);

            double c = 2.0 * Math.Asin(Math.Sqrt(a));

            double distance = EarthRadiusKm * c;

            return distance;
        }

        protected static double DegToRad(double degs)
        {
            return degs * (Math.PI / 180.0);
        }
    }
}
