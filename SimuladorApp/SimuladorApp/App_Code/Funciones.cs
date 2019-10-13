using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimuladorApp
{
    static public class Funciones
    {
        static public async Task<Location> Address2GPS(string address)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    return location;
                }
                else
                {
                    return default;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                return default;
            }
            catch (Exception ex)
            {
                return default;// Handle exception that may have occurred in geocoding
            }
        }

        static public async Task<Placemark> GPS2Address(Location point)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(point.Latitude, point.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    return placemark;
                }
                else
                {
                    return default;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                return default;
            }
            catch (Exception ex)
            {
                return default;// Handle exception that may have occurred in geocoding
            }
        }

        static public Double DistanciaCarreteraOLD(String origen, String destino)
        {
            var current = Connectivity.NetworkAccess;
            var Url = App._globalAPIURLDistancia;
            var UrlCompleta = Url + "?Origen=" + origen + "&Destino=" + destino;
            Double retorno = 0;
            if (current == NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string distancia = await RestApi.GetString(UrlCompleta);
                    Double.TryParse(distancia, out retorno);
                });
                return retorno;
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }

        static async public Task<double> DistanciaCarretera(string origen, string destino)
        {
            string Url = App._globalAPIURLDistancia;
            string UrlCompleta = Url + "?Origen=" + origen + "&Destino=" + destino;
            Double distancia = 0;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var cad = await RestApi.GetString(UrlCompleta);

                if (cad != null)
                {
                    double d = 0;
                    double.TryParse(cad, out d);
                    distancia = d;
                }
                return distancia;
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }
        static async public Task<string> DistanciaCarreteraNEW(string origen, string destino)
        {
            string Url = App._globalAPIURLDistancia;
            string UrlCompleta = Url + "?Origen=" + origen + "&Destino=" + destino;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var cad = await RestApi.GetString(UrlCompleta);
                return cad;
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }
        static public double DistanciaCarreteraOLD2(string origen, string destino)
        {
            string Url = App._globalAPIURLDistancia;
            string UrlCompleta = Url + "?Origen=" + origen + "&Destino=" + destino;
            Double distancia = 0;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var cadTask = RestApi.GetString(UrlCompleta);
                    var cad = await cadTask;

                    if (cad != null)
                    {
                        double d = 0;
                        double.TryParse(cad, out d);
                        distancia = d;
                    }
                });
                return distancia;

            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }



        public enum DistanceUnit { Miles, Kilometers };
        public static double ToRadian(this double value)
        {
            return (Math.PI / 180) * value;
        }
        public static double HaversineDistance(Location coord1, Location coord2, DistanceUnit unit = DistanceUnit.Kilometers)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = (coord2.Latitude - coord1.Latitude).ToRadian();
            var lng = (coord2.Longitude - coord1.Longitude).ToRadian();

            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(coord1.Latitude.ToRadian()) * Math.Cos(coord2.Latitude.ToRadian()) *
                     Math.Sin(lng / 2) * Math.Sin(lng / 2);

            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

            return R * h2;
        }

    }
}
