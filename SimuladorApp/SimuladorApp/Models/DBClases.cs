using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace SimuladorApp
{
    public class DBConfiguracion
    {
        public string velocidad { get; set; }
        public string velocidadAcarreoOrigen { get; set; }
        public string velocidadAcarreoDestino { get; set; }
        public string costekm { get; set; }
        public string costeKmAcarreoOrigen { get; set; }
        public string costeKmAcarreoDestino { get; set; }
        public string toneladasTransportadas { get; set; }
        public string costeExternoCarretera { get; set; }
        public string costeExternoMaritimo { get; set; }
        public string costeEmisionCO2Carretera { get; set; }
        public string costeEmisionCO2Maritimo { get; set; }
        public string altaFrecuencia { get; set; }
        public string peajes { get; set; }

    }
    public class DBLinea
    {
        public int Id { get; set; }
        public string PuertoOrigen { get; set; }
        public string PuertoDestino { get; set; }
        public string PuertoOrigenMappoint { get; set; }
        public string PuertoDestinoMappoint { get; set; }
        public int PuertoOrigenID { get; set; }
        public int PuertoDestinoID { get; set; }
        public string Linea { get; set; }
        public string Naviera { get; set; }
        public int NavieraID { get; set; }
        public string Frecuencia { get; set; }
        public int FrecuenciaMensual { get; set; }
        public string TipoTrafico { get; set; }
        public int DistanciaMillas { get; set; }
        public int TiempoPuertoOrigen { get; set; }
        public double TiempoTransito { get; set; }
        public int TiempoPuertoDestino { get; set; }
        public double TiempoTotalTransporteMaritimo { get; set; }
        public double FleteMaritimo { get; set; }
        public double RecargoCabezaTractora { get; set; } // Recargo Cabeza Tractora
        public double RecargoBAF { get; set; } // Recargo acompañado
        public int RecargoMercanciaPeligrosa { get; set; } // Mercancía peligrosa
        public double RecargoCCRR { get; set; } // Carga refrigerada
        public int CapPlataformas { get; set; }
        public int CapPasajeros { get; set; }
        public int CapCoches { get; set; }
        public bool AlternativaTerrestre { get; set; }
        public string Fachada { get; set; }
        public string FechaActualizacion { get; set; }
        public bool Visible { get; set; }
        public bool VisibleOld { get; set; }
        public string EnlaceComodalWeb { get; set; }
        public string FleteMensaje { get; set; }
        public int RecargoFlete { get; set; }
        public double PuertoOrigenLat { get; set; }
        public double PuertoOrigenLng { get; set; }
        public double PuertoDestinoLat { get; set; }
        public double PuertoDestinoLng { get; set; }
        public Location PuertoOrigenGPS
        {
            get
            {
                return new Location(this.PuertoOrigenLat, this.PuertoOrigenLng);
            }
        }
        public Location PuertoDestinoGPS
        {
            get
            {
                return new Location(this.PuertoDestinoLat, this.PuertoDestinoLng);
            }
        }

        public double distanciaRectaOrigenPuertoOrigen { get; set; }
        public double distanciaRectaPuertoDestinoDestino { get; set; }
        public double distanciaRectaOrigenPuertoDestino { get; set; }
        public double distanciaRectaPuertoDestinoOrigen { get; set; }
        public double distanciaRectaOrigenDestino { get; set; }

    }
    public class DBPuerto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string Terminal { get; set; }
        public string ServiciosComplementarios { get; set; }
        public string DatosContacto { get; set; }
        public string InformacionReservas { get; set; }
    }
    public class DBNaviera
    {
        public string Id { get; set; }
        public string Logo { get; set; }
        public string De { get; set; }
        public string A { get; set; }
        public string Horario { get; set; }
        public string Acompanado { get; set; }
        public string NoAcompanado { get; set; }
        public string MercanciasPeligrosas { get; set; }
        public string AnimalesVivos { get; set; }
        public string CargaRefrigerada { get; set; }
        public string DatosContacto { get; set; }
        public string InformacionReservas { get; set; }
        public string MercanciasPeligrosas1 { get; set; }
        public string AnimalesVivos1 { get; set; }
        public string CargaRefrigerada1 { get; set; }
        public string DatosContacto1 { get; set; }
        public string InformacionReservas1 { get; set; }
    }

    public class DB
    {

        static async public Task<List<DBLinea>> DBLineasLeer()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return await RestApi.Get<List<DBLinea>>(App._globalAPIURLLineas);
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }

        static async public Task<List<DBPuerto>> DBPuertosLeer()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return await RestApi.Get<List<DBPuerto>>(App._globalAPIURLPuertos);
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }
        static async public Task<List<DBNaviera>> DBNavierasLeer()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return await RestApi.Get<List<DBNaviera>>(App._globalAPIURLNavieras);
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }
        static async public Task<DBConfiguracion> DBConfiguracionLeer()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return await RestApi.Get<DBConfiguracion>(App._globalAPIURLConfiguracion);
                //return Newtonsoft.Json.JsonConvert.DeserializeObject<DBConfiguracion>(config);
            }
            else
            {
                throw new Exception("ERROR. No hay acceso a internet");
            }
        }


        static async public void DBLeerTodo()
        {
            if (App._globalDBLinea is null)
            {
                App._globalDBLinea = await DBLineasLeer();
            }
            if (App._globalDBNaviera is null)
            {
                App._globalDBNaviera = await DBNavierasLeer();
            }
            if (App._globalDBPuerto is null)
            {
                App._globalDBPuerto = await DBPuertosLeer();
            }
            if (App._globalDBConfiguracion is null)
            {
                App._globalDBConfiguracion = await DBConfiguracionLeer();
            }
        }
    }

}
