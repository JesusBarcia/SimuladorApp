using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SimuladorApp
{
    class GoogleMapJSON
    {

        //onst FindNearbyWeatherUrl As String = "http://ws.geonames.org/findNearByWeatherJSON?lat={0}&lng={1}";
        const string FindNearbyWeatherUrl = "http://maps.google.com/maps/api/geocode/json?sensor=false&region=ES";

        const string RouteUrl = "http://maps.google.com/maps/api/directions/json?origin={0}&destination={1}&mode={2}&sensor=false&region=ES";
        const string GeocodeUrl = "http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false&region=ES";
        const string DistanciaMatrixUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&mode={2}&key=AIzaSyCEYxdeUCr9dPJP05lTNTURuxywEoGXO_Q&sensor=false&region=ES";
        //        const string uri = "http://www.pizzaboy.de/app/pizzaboy.json";

        public string Respuesta { get; set; }
        public async Task<string> GetDistanciaMatrix(string Origen, string Destino)
        {
            string formattedUri = string.Format(CultureInfo.InvariantCulture, DistanciaMatrixUrl, Origen, Destino, "driving");
            var uri = "https://www.mocky.io/v2/5185415ba171ea3a00704eed";
            return await Download(uri);
        }

        public async Task<string> Download(string uri)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            this.Respuesta = result;
            return result;
        }

        public string Distancia(string Origen, string Destino)
        {
            var json = Download("http://ip.jsontest.com"); //GetDistanciaMatrix(Origen, Destino);
                                                           //RootObject data = JsonConvert.DeserializeObject<RootObject>(json.ToString());
                                                           //double retorno = 0;
                                                           //if (Double.TryParse(data.rows[0].elements[0].distance.ToString(), out retorno))
                                                           //    return retorno;
                                                           //else
                                                           //    return 0;
            const string DistanciaMatrixUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&mode={2}&key=AIzaSyCEYxdeUCr9dPJP05lTNTURuxywEoGXO_Q&sensor=false&region=ES";

            return this.Respuesta;
        }
    }
}
//    'Se comprueba si está en la base de datos el origen y destino
//    Dim respuesta As String
//    Dim nDistanciaTotal As Double = 0, nTiempoTotal As Double = 0
//    Dim dr As System.Data.DataRow = Datos.ArchivoJSonGet(Origen, Destino)
//    If Not IsDBNull(dr.Item("ID")) Then
//        nDistanciaTotal = Funciones.Coma2Punto(dr.Item("DistanciaTotal"))
//        nTiempoTotal = Funciones.Coma2Punto(dr.Item("TiempoTotal"))
//    Else
//        respuesta = GoogleMapJSON.GetDistanciaMatrix(Origen, Destino)
//        Dim obj2 As Object
//        Dim js2 As JavaScriptSerializer = New JavaScriptSerializer()
//        obj2 = js2.Deserialize(Of Object) (respuesta)
//         If obj2("status") = "OK" Then
//             nDistanciaTotal = obj2("rows")(0)("elements")(0)("distance")("value")
//            nTiempoTotal = obj2("rows")(0)("elements")(0)("duration")("value")
//        ElseIf obj2("status").ToString.StartsWith("OVER_QUERY_LIMIT") Then
//            Throw New Exception("Error. Se ha rebasado el máximo de consultas a Google Maps")
//        ElseIf obj2("status").ToString.StartsWith("ZERO_RESULTS") Then
//            Throw New Exception("Error. No se ha encontrado ruta terrestre entre " & Origen & " y " & Destino)
//        ElseIf obj2("status").ToString.StartsWith("NOT_FOUND") Then
//            Throw New Exception("Error. No se ha encontrado " & Origen & " o " & Destino)
//        Else
//            nDistanciaTotal = 1000
//            nTiempoTotal = 1000
//        End If
//        Datos.ArchivoJSonSave(Origen, Destino, nDistanciaTotal, nTiempoTotal)
//    End If
//    Return nDistanciaTotal / 1000
//End Function


