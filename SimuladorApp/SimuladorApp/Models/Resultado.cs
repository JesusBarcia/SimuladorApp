using System;
using System.Collections.Generic;
using System.Text;

namespace SimuladorApp
{
    public class Resultado
    {
        public double ID { get; set; }
        public double LineaID { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string OrigenDestino { get { return this.Origen + "-" + this.Destino; } }
        public string Linea { get; set; }
        public string PuertoOrigen { get; set; }
        public string PuertoDestino { get; set; }
        public string PuertoOrigenPuertoDestino { get { return this.PuertoOrigen + "-" + this.PuertoDestino; } }
        public string Naviera { get; set; }
        public double Velocidad { get; set; }
        public double VelocidadAcarreoOrigen { get; set; }
        public double VelocidadAcarreoDestino { get; set; }
        public double CosteKmTerrestre { get; set; }
        public double CosteKMMaritimo { get; set; }
        public double CosteParalizacionHora { get; set; }
        public double DistanciaOrigenPuerto { get; set; }
        public double DistanciaPuertoPuerto { get; set; }
        public double DistanciaPuertoDestino { get; set; }
        public double TiempoOrigenPuerto { get; set; }
        public double TiempoPuertoPuerto { get; set; }
        public double TiempoPuertoDestino { get; set; }
        public double CosteOrigenPuerto { get; set; }
        public double CostePuertoPuerto { get; set; }
        public double CostePuertoPuertoconRecargos { get; set; }
        public double CostePuertoDestino { get; set; }
        public double Peajes { get; set; }
        public double RecargoCabezTractora { get; set; }
        public double RecargoRefrigearado { get; set; }
        public double RecargoMercanciaPeligrosa { get; set; }
        public double RecargoAnimalesVivos { get; set; }
        public double DistanciaTotal { get; set; }
        public double TiempoTotal { get; set; }
        public double CosteTotalsinRecargos { get; set; }
        public double CosteTotal { get; set; }
        public string Observaciones { get; set; }
        public string Web { get; set; }
        public string Fachada { get; set; }
        public double FrecuenciaMensual { get; set; }
        public double CostesExternosTerrestre { get; set; }
        public double CostesExternosOrigenPuerto { get; set; }
        public double CostesExternosPuertoDestino { get; set; }
        public double CostesExternosPuertoPuerto { get; set; }
        public double CostesExternosTotal { get; set; }
        public double CosteEmisionCO2Terrestre { get; set; }
        public double CosteEmisionCO2OrigenPuerto { get; set; }
        public double CosteEmisionCO2PuertoDestino { get; set; }
        public double CosteEmisionCO2PuertoPuerto { get; set; }
        public double CosteEmisionCO2Total { get; set; }
        public int NavieraID { get; set; }
        public int PuertoOrigenID { get; set; }
        public int PuertoDestinoID { get; set; }
        public string NavieraIDEnlace { get; set; }
        public string PuertoOrigenIDEnlace { get; set; }
        public string PuertoDestinoIDEnlace { get; set; }
        public string EnlaceComodalWeb { get; set; }
        public string FleteMensaje { get; set; }
        public Boolean Seleccionado { get; set; }
    }



}

