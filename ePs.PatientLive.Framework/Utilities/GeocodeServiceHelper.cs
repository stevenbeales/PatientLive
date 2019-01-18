using ePs.PatientLive.Framework.GeocodeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ePs.PatientLive.Framework.Utilities
{
    public static class GeocodeServiceHelper
    {
        private const string KEY = "AnyDitnXXM66UvIgB_w8N2Azn4rQTM6bYnAUFI-pse5EXLw88z5gOPwQ31vzwkS9";

        public static ReverseGeocodeRequest GetReverseRequest(double lat, double lon)
        {
            ReverseGeocodeRequest reverseRequest = new ReverseGeocodeRequest();
            reverseRequest.Credentials = new Credentials();
            reverseRequest.Credentials.ApplicationId = KEY;

           Location point = new Location();
            point.Latitude = lat;
            point.Longitude = lon;

            reverseRequest.Location = point;

            return reverseRequest;
        }

        public static Task<GeocodeResponse> GetGeocodeByAddress(string address)
        {
            GeocodeRequest request = new GeocodeRequest();
            request.Credentials = new Credentials();
            request.Credentials.ApplicationId = KEY;

            request.Query = address;

            // Make the geocode request
            GeocodeServiceClient geocodeService = new GeocodeServiceClient(GeocodeServiceClient.EndpointConfiguration.BasicHttpBinding_IGeocodeService);
            return geocodeService.GeocodeAsync(request);
        }

        public static GeocodeService.GeocodeServiceClient GetClient()
        {
            return new GeocodeService.GeocodeServiceClient(GeocodeService.GeocodeServiceClient.EndpointConfiguration.BasicHttpBinding_IGeocodeService); 
        }

        public static async Task<string> GetPostalCode()
        {
            var geo = new Geolocator();
            Geoposition pos = await geo.GetGeopositionAsync().AsTask();
            var postCode = pos.CivicAddress.PostalCode ?? string.Empty;
            if (postCode.Length <= 0 && pos.Coordinate.Latitude != 0)
            {
                var reverseRequest = GeocodeServiceHelper.GetReverseRequest(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                var client = GeocodeServiceHelper.GetClient();
                var geocodeResponse = await Task.Run(() => client.ReverseGeocodeAsync(reverseRequest));
                if (geocodeResponse.Results != null)
                    postCode = geocodeResponse.Results[0].Address.PostalCode;
            }
            return postCode;
        }

        public static async Task<string> GetPostalCode(Geoposition pos)
        {
            var postCode = pos.CivicAddress.PostalCode ?? string.Empty;
            if (postCode.Length <= 0 && pos.Coordinate.Latitude != 0)
            {
                var reverseRequest = GeocodeServiceHelper.GetReverseRequest(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                var client = GeocodeServiceHelper.GetClient();
                var geocodeResponse = await Task.Run(() => client.ReverseGeocodeAsync(reverseRequest));
                if (geocodeResponse.Results != null)
                    postCode = geocodeResponse.Results[0].Address.PostalCode;
            }
            return postCode;
        }

        public static async Task<GeocodeService.GeocodeResponse> GetLocation()
        {
            var geo = new Geolocator();
            Geoposition pos = await geo.GetGeopositionAsync().AsTask();
            if (pos.Coordinate.Latitude != 0)
            {
                var reverseRequest = GeocodeServiceHelper.GetReverseRequest(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                var client = GeocodeServiceHelper.GetClient();
                return await Task.Run(() => client.ReverseGeocodeAsync(reverseRequest));
            }
            return new GeocodeService.GeocodeResponse();
        }

        public static async Task<GeocodeService.GeocodeResponse> GetLocation(Geoposition pos)
        {
            if (pos.Coordinate.Latitude != 0)
            {
                var reverseRequest = GeocodeServiceHelper.GetReverseRequest(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                var client = GeocodeServiceHelper.GetClient();
                return await Task.Run(() => client.ReverseGeocodeAsync(reverseRequest));
            }
            return new GeocodeService.GeocodeResponse();
        }
    }
}
