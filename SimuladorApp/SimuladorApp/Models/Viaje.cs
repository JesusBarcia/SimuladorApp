using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorApp
{
    public class Viaje
    {
        public string origen { get; set; }
        public string destino { get; set; }
        public Xamarin.Essentials.Location origenGPS { get; set; }
        public Xamarin.Essentials.Location destinoGPS { get; set; }
        public double TiempoTerrestre()
        {
            return (this.carreteraHoras == 0) ? TiempoTerrestreconDescanso(this.velocidad, this.distanciaTerrestre) :
                        this.carreteraHoras;
        }

        public string origenOriginal { get; set; }
        public string destinoOriginal { get; set; }
        public double distanciaTerrestre { get; set; }
        public double costeKm { get; set; }
        public double costeKmAcarreoOrigen { get; set; }
        public double costeKmAcarreoDestino { get; set; }
        public double velocidad { get; set; }
        public double velocidadAcarreoOrigen { get; set; }
        public double velocidadAcarreoDestino { get; set; }
        public double carreteraHoras { get; set; }
        public DBPuerto puerto { get; set; }
        //        public datatable MisViajes { get; set; }
        public Boolean recargoCabezaTractora { get; set; }
        public Boolean recargoRefrigerado { get; set; }
        public Boolean recargoMercanciaPeligrosa { get; set; }
        public Boolean recargoAnimalesVivos { get; set; }
        public double peajes { get; set; }
        public Boolean fachadaAtlantica { get; set; }
        public Boolean fachadaMediterranea { get; set; }
        public Boolean fachadaCantabrica { get; set; }
        public Boolean altaFrecuencia { get; set; }
        public string naviera { get; set; }
        public string whereMisViajes { get; set; }
        public string whereMisViajesCondicion { get; set; }
        public double costeExternoKmTnCarretera { get; set; }
        public double costeExternoKmTnMaritimo { get; set; }
        public double costeEmisionCO2KmTnCarretera { get; set; }
        public double costeEmisionCO2KmTnMaritimo { get; set; }
        public double toneladasTransportadas { get; set; }
        public string resultadosOrden { get; set; }
        public Viaje(String cOrigen, String cDestino)
        {
            this.origen = cOrigen.ToUpper();
            this.destino = cDestino.ToUpper();
            this.velocidad = 65;
            this.velocidadAcarreoOrigen = 65;
            this.velocidadAcarreoDestino = 65;
            this.costeKm = 1.1;
            this.recargoCabezaTractora = false;
            this.recargoRefrigerado = false;
            this.recargoMercanciaPeligrosa = false;
            this.fachadaAtlantica = true;
            this.fachadaCantabrica = true;
            this.fachadaMediterranea = true;
            this.altaFrecuencia = false;
            this.naviera = "";
            this.whereMisViajes = "";
            this.whereMisViajesCondicion = String.Empty;
            this.peajes = 0;
            this.costeExternoKmTnCarretera = 0;
            this.costeExternoKmTnMaritimo = 0;
            this.costeEmisionCO2KmTnCarretera = 0;
            this.costeEmisionCO2KmTnMaritimo = 0;
            this.toneladasTransportadas = 18;
            this.resultadosOrden = "";
        }

        public double TiempoTerrestreconDescanso(double velocidad, double distancia)
        {

            //Calcula el tiempo de trayecto para una velocidad y distancia dada
            double tiempoConduccion = 0;
            double tiempoConduccionRestante = 0;
            double descanso = 0;
            double descansoAcumulado = 0;
            double retorno = 0;
            double margen = 0.99999000000000005;
            //Calcula el tiempo de conducción sin descanso
            tiempoConduccion = distancia / velocidad;
            tiempoConduccionRestante = tiempoConduccion;
            //Calcula el tiempo de descanso para 6 días
            while (tiempoConduccionRestante >= 54)
            {
                tiempoConduccionRestante -= 54;
                descansoAcumulado += descansoAcumulado + 96.5; //59.5
            }
            //Calculo del descanso del resto de tiempo

            switch (tiempoConduccionRestante)
            {
                case double n when (n < 4.49): descanso = 0; break;
                case double n when (n < (4.5 + margen)): descanso = 0.75; break;
                case double n when (n < 9): descanso = 0.75; break;
                case double n when (n < (9 + margen)): descanso = 0.75 + 0.75; break;
                case double n when (n < 13.5): descanso = 11.75; break;
                case double n when (n < (13.5 + margen)): descanso = 11.75 + 0.75; break;
                case double n when (n < 18): descanso = 12.5; break;
                case double n when (n < (18 + margen)): descanso = 12.5 + 0.75; break;
                case double n when (n < 22.5): descanso = 23.5; break;
                case double n when (n < (22.5 + margen)): descanso = 23.5 + 0.75; break;
                case double n when (n < 27): descanso = 24.25; break;
                case double n when (n < (27 + margen)): descanso = 24.25 + 0.75; break;
                case double n when (n < 31.5): descanso = 35.25; break;
                case double n when (n < (31.5 + margen)): descanso = 35.25 + 0.75; break;
                case double n when (n < 36): descanso = 36; break;
                case double n when (n < (36 + margen)): descanso = 36 + 0.75; break;
                case double n when (n < 40.5): descanso = 47; break;
                case double n when (n < (40.5 + margen)): descanso = 47 + 0.75; break;
                case double n when (n < 45): descanso = 47.75; break;
                case double n when (n < (45 + margen)): descanso = 47.75 + 0.75; break;
                case double n when (n < 49.5): descanso = 95.75; break;
                case double n when (n < (49.5 + margen)): descanso = 95.75 + 0.75; break;
                case double n when (n < 54): descanso = 96.5; break;
                case double n when (n < (54 + margen)): descanso = 96.5 + 0.75; break;
            }
            retorno = tiempoConduccion + descansoAcumulado + descanso;
            return retorno;
        }
        public async Task<Viaje> calcularMisViajes()
        {
            App._globalResultados = new List<Resultado>();
            int contador = 0;
            foreach (DBLinea lin in App._globalLineasSeleccionadas) // TODO: WhereMisViajes
            {
                Double distanciaOrigenPuerto = 0; Double distanciaPuertoDestino = 0; Double distanciaTotal = 0;
                Double tiempoOrigenPuerto = 0; Double tiempoPuertoDestino = 0; Double costeOrigenPuerto = 0;
                Double costePuertoDestino = 0;
                Double distanciaPuertoPuerto = 0; Double costePuertoPuerto = 0; Double tiempoPuertoPuerto = 0;
                Double costeTotal = 0; Double costeRecargos = 0;
                Resultado resultado = new Resultado();
                costeRecargos = costeRecargos + (this.recargoCabezaTractora ? lin.RecargoCabezaTractora : 0);
                costeRecargos = costeRecargos + (this.recargoRefrigerado ? lin.RecargoCCRR : 0);
                costeRecargos = costeRecargos + (this.recargoMercanciaPeligrosa ? lin.RecargoMercanciaPeligrosa : 0);
                costeRecargos = costeRecargos + (this.recargoAnimalesVivos ? lin.RecargoBAF : 0);
                costeRecargos = costeRecargos + this.peajes;

                distanciaPuertoDestino = await Funciones.DistanciaCarretera(lin.PuertoDestinoMappoint, this.destino);
                distanciaOrigenPuerto = await Funciones.DistanciaCarretera(this.origen, lin.PuertoOrigenMappoint);
                distanciaPuertoPuerto = lin.DistanciaMillas * 1.852;

                tiempoOrigenPuerto = TiempoTerrestreconDescanso(this.velocidadAcarreoOrigen, distanciaOrigenPuerto);
                tiempoPuertoPuerto = lin.TiempoTotalTransporteMaritimo;
                tiempoPuertoDestino = TiempoTerrestreconDescanso(this.velocidadAcarreoDestino, distanciaPuertoDestino);

                costeOrigenPuerto = this.costeKmAcarreoOrigen * distanciaOrigenPuerto;
                costePuertoPuerto = lin.FleteMaritimo;
                costePuertoDestino = this.costeKmAcarreoDestino * distanciaPuertoDestino;

                costeTotal = costeOrigenPuerto + costePuertoPuerto + costePuertoDestino + costeRecargos;

                contador = contador + 1;
                resultado.LineaID = contador;
                resultado.ID = lin.Id;
                resultado.Origen = this.origenOriginal;
                resultado.Destino = this.destinoOriginal;
                resultado.Linea = lin.PuertoOrigen + " ** " + lin.PuertoDestino;
                resultado.PuertoOrigen = lin.PuertoOrigen;
                resultado.PuertoDestino = lin.PuertoDestino;
                resultado.Naviera = lin.Naviera;
                resultado.Velocidad = this.velocidad;
                resultado.VelocidadAcarreoOrigen = this.velocidadAcarreoOrigen;
                resultado.VelocidadAcarreoDestino = this.velocidadAcarreoDestino;
                resultado.CosteKmTerrestre = this.costeKm;
                resultado.CosteKMMaritimo = lin.FleteMaritimo / (lin.DistanciaMillas * 1.852);
                resultado.CosteParalizacionHora = 0;
                resultado.DistanciaOrigenPuerto = distanciaOrigenPuerto;
                resultado.DistanciaPuertoDestino = distanciaPuertoDestino;
                resultado.TiempoOrigenPuerto = tiempoOrigenPuerto;
                resultado.TiempoPuertoDestino = tiempoPuertoDestino;
                resultado.CosteOrigenPuerto = costeOrigenPuerto;
                resultado.CostePuertoDestino = costePuertoDestino;
                resultado.DistanciaPuertoPuerto = distanciaPuertoPuerto;
                resultado.CostePuertoPuerto = costePuertoPuerto;
                resultado.CostePuertoPuertoconRecargos = costePuertoPuerto + costeRecargos;
                resultado.TiempoPuertoPuerto = tiempoPuertoPuerto;
                resultado.RecargoCabezTractora = lin.RecargoCabezaTractora;
                resultado.RecargoRefrigearado = lin.RecargoCCRR;
                resultado.RecargoMercanciaPeligrosa = lin.RecargoMercanciaPeligrosa;
                resultado.Peajes = this.peajes;
                distanciaTotal = distanciaOrigenPuerto + distanciaPuertoPuerto + distanciaPuertoDestino;
                if (distanciaTotal > 99999) { distanciaTotal = 99999; }
                resultado.DistanciaTotal = distanciaTotal;
                resultado.TiempoTotal = tiempoOrigenPuerto + tiempoPuertoPuerto + tiempoPuertoDestino;
                resultado.CosteTotalsinRecargos = costeOrigenPuerto + costePuertoPuerto + costePuertoDestino;

                resultado.CosteTotal = costeTotal;
                resultado.Observaciones = ""; //lin.Observaciones;
                resultado.Web = ""; //lin.Web
                resultado.Fachada = lin.Fachada;
                resultado.FrecuenciaMensual = lin.FrecuenciaMensual;
                resultado.NavieraID = lin.NavieraID;
                resultado.PuertoOrigenID = lin.PuertoOrigenID;
                resultado.PuertoDestinoID = lin.PuertoDestinoID;
                resultado.EnlaceComodalWeb = lin.EnlaceComodalWeb;
                if (resultado.NavieraID == 0)
                {
                    resultado.NavieraIDEnlace = "";
                }
                else
                {
                    resultado.NavieraIDEnlace = "<a href=\"./verNaviera.aspx?id=\"" + lin.NavieraID + "\" target=\"_blank\"> Información sobre la Naviera y el Servicio Marítimo.</a>";

                }
                if (resultado.PuertoOrigenID == 0)
                {
                    resultado.PuertoOrigenIDEnlace = "Puerto de Origen: Puerto de " + lin.PuertoOrigen;
                }
                else
                {
                    resultado.PuertoOrigenIDEnlace = "<a href=\"./verPuerto.aspx?id=" + resultado.PuertoOrigenID + "\" target=\"_blank\">Información sobre el Puerto de Origen: Puerto de " + resultado.PuertoOrigen + ".</a>";
                }

                if (resultado.PuertoDestinoID == 0)
                {
                    resultado.PuertoDestinoIDEnlace = "Puerto de Destino: Puerto de " + lin.PuertoDestino;
                }
                else
                {
                    resultado.PuertoDestinoIDEnlace = "<a href=\"./verPuerto.aspx?id=" + resultado.PuertoDestinoID + "\" target=\"_blank\">Información sobre el Puerto de Destino: Puerto de " + resultado.PuertoDestino + ".</a>";
                }
                resultado.CostesExternosTerrestre = this.costeExternoKmTnCarretera * this.toneladasTransportadas * this.distanciaTerrestre;
                resultado.CostesExternosOrigenPuerto = this.costeExternoKmTnCarretera * this.toneladasTransportadas * distanciaOrigenPuerto;
                resultado.CostesExternosPuertoDestino = this.costeExternoKmTnCarretera * this.toneladasTransportadas * distanciaPuertoDestino;
                resultado.CostesExternosPuertoPuerto = this.costeExternoKmTnMaritimo * this.toneladasTransportadas * distanciaPuertoPuerto;
                resultado.CostesExternosTotal = resultado.CostesExternosOrigenPuerto + resultado.CostesExternosPuertoDestino + resultado.CostesExternosPuertoPuerto;
                resultado.CosteEmisionCO2Terrestre = this.costeEmisionCO2KmTnCarretera * this.toneladasTransportadas * this.distanciaTerrestre;
                resultado.CosteEmisionCO2OrigenPuerto = this.costeEmisionCO2KmTnCarretera * this.toneladasTransportadas * distanciaOrigenPuerto;
                resultado.CosteEmisionCO2PuertoDestino = this.costeEmisionCO2KmTnCarretera * this.toneladasTransportadas * distanciaPuertoDestino;
                resultado.CosteEmisionCO2PuertoPuerto = this.costeEmisionCO2KmTnMaritimo * this.toneladasTransportadas * distanciaPuertoPuerto;
                resultado.CosteEmisionCO2Total = resultado.CosteEmisionCO2OrigenPuerto + resultado.CosteEmisionCO2PuertoDestino + resultado.CosteEmisionCO2PuertoPuerto;
                if (lin.FleteMensaje == string.Empty)
                {
                    resultado.FleteMensaje = "El coste del transporte marítimo es orientativo, para mayor precisión por favor consulte a la naviera.";
                }
                else
                {
                    resultado.FleteMensaje = lin.FleteMensaje;
                }
                resultado.Seleccionado = true;
                App._globalResultados.Add(resultado);
            }
            return this;
            //        Dim cFiltro As String
            //    cFiltro = Me.WhereMisViajes
            //    If Me.WhereMisViajesCondicion<> String.Empty Then
            //        If cFiltro<> String.Empty Then cFiltro = cFiltro & " AND "
            //        cFiltro = cFiltro & Me.WhereMisViajesCondicion
            //    End If
            //    cFiltro = cFiltro.Replace("LineaID", "ID")
            //    miTabla.DefaultView.RowFilter = cFiltro
            //    If Me.ResultadosOrden<> String.Empty Then
            //        miTabla.DefaultView.Sort = Me.ResultadosOrden
            //    End If
            //    Me.misViajes = miTabla.DefaultView.ToTable
            //End Sub

        }
    }
}