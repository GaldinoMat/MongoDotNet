using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ApiMongoDB.Data.Collections
{
    public class Infected
    {
        public Infected(DateTime birthDate, string gender, double latitude, double longitude)
        {
            this.birthDate = birthDate;
            this.gender = gender;
            this.localization = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public DateTime birthDate { get; set; }
        public string gender { get; set; }
        public GeoJson2DGeographicCoordinates localization { get; set; }
    }
}