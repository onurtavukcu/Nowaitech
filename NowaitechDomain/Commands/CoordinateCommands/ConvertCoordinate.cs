﻿using NowaitechShared.Models.CoordinateModels;

namespace NowaitechDomain.Commands.CoordinateCommands
{
    public static class ConvertCoordinate
    {
        public static LatLonCoordinate ConvertOperation(string east, string north)
        {
            var eastResult = StringToGpsCoordinate(east);

            var northResult = StringToGpsCoordinate(north);

            var eastLaLong = eastResult.degree + eastResult.minute / 60.0f + eastResult.second / 3600.0f;
            var northLaLong = northResult.degree + northResult.minute / 60.0f + northResult.second / 3600.0f;

            var LatLangInstance = new LatLonCoordinate();

            LatLangInstance.Longitude = eastLaLong;
            LatLangInstance.Latitude = northLaLong;

            return LatLangInstance;
        }

        public static GpsCoordinate StringToGpsCoordinate(string LanLongString)
        {
            var result = new GpsCoordinate();
            string[] parts = LanLongString.Split('°', '\'', '\'');

            result.degree = int.Parse(parts[0]);
            result.minute = int.Parse(parts[1]);
            result.second = float.Parse(parts[2]) / 100000f;

            return result;
        }
    }
}
