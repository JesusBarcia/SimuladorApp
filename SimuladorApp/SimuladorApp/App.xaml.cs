using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace SimuladorApp
{
    public partial class App : Application
    {
        public static IEnumerable<DBLinea> _globalDBLinea; //Leidas de la base de datos.
        public static IEnumerable<DBLinea> _globalLineasSeleccionadas;
        public static List<DBNaviera> _globalDBNaviera;
        public static List<DBPuerto> _globalDBPuerto;
        public static DBConfiguracion _globalDBConfiguracion;
        public static HttpClient _globalhttpclient;
        public static List<Resultado> _globalResultados;
        public static List<Resultado> _globalResultadosSeleccionados;
        public static Viaje miViaje;
        public const String _globalAPIURLConfiguracion = "http://simulador.shortsea.es/API/DBConfiguracionLeer.aspx";
        public const String _globalAPIURLLineas = "http://simulador.shortsea.es/API/DBLineasLista.aspx";
        public const String _globalAPIURLPuertos = "http://simulador.shortsea.es/API/DBPuertosLista.aspx";
        public const String _globalAPIURLNavieras = "http://simulador.shortsea.es/API/DBNavierasLista.aspx";
        public const String _globalAPIURLDistancia = "http://simulador.shortsea.es/API/distanciaKM.aspx";
        public static System.Globalization.CultureInfo cultureEN;



        public App()
        {
            InitializeComponent();
            if (_globalhttpclient is null)
            {
                _globalhttpclient = new HttpClient();
            }

            System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.Replace(",", ".");

            cultureEN = new System.Globalization.CultureInfo("en-US");


            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new DistanciaPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
