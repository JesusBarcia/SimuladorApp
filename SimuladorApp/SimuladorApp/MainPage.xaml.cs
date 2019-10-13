using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimuladorApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //13-10-2019 22:25h
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.txtOrigen.Text = "Madrid";
            this.txtDestino.Text = "Roma, Italia";

            CargarVariablesGlobales();
            // DB.DBLineasLeer();

            this.btnCalcularRutas.Clicked += BtnCalcularRutas_Clicked;
            this.txtOrigen.Unfocused += TxtOrigen_Unfocused;
            this.txtDestino.Unfocused += TxtDestino_Unfocused;
            this.btnCalcularRutas.IsEnabled = false;

            //LineasLeer();
        }

        private async void TxtListResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            var n = (sender as Picker).SelectedIndex;
            //_ = DisplayAlert("ELEMENTO", n.ToString(), "OK");
            await Navigation.PushAsync(new ResultadoPage(App._globalResultadosSeleccionados[n]));
        }

        public async void CargarVariablesGlobales()
        {
            //var configTask = DB.DBConfiguracionLeer();
            //App._globalDBConfiguracion = await configTask;
            App._globalDBConfiguracion = await DB.DBConfiguracionLeer();

            var lineaTask = DB.DBLineasLeer();
            App._globalDBLinea = await lineaTask;
            var puertoTask = DB.DBPuertosLeer();
            App._globalDBPuerto = await puertoTask;
            var navieraTask = DB.DBNavierasLeer();
            App._globalDBNaviera = await navieraTask;

            if (App._globalDBConfiguracion is null)
            {
            }
            else
            {
                this.txtVelocidad.Text = App._globalDBConfiguracion.velocidad;
                this.txtVelocidadAcarreoOrigen.Text = App._globalDBConfiguracion.velocidadAcarreoOrigen;
                this.txtVelocidadAcarreoDestino.Text = App._globalDBConfiguracion.velocidadAcarreoDestino;
                this.txtCosteKm.Text = App._globalDBConfiguracion.costekm;
                this.txtCosteKmAcarreoOrigen.Text = App._globalDBConfiguracion.costeKmAcarreoOrigen;
                this.txtCosteKmAcarreoDestino.Text = App._globalDBConfiguracion.costeKmAcarreoDestino;
                this.txtToneladasTransportadas.Text = App._globalDBConfiguracion.toneladasTransportadas;
                this.txtPeajes.Text = App._globalDBConfiguracion.peajes;
            }
            if (App._globalDBLinea is null)
            {

            }
            else
            {
                if (this.txtListBoxLineas.Items.Count == 0)
                {
                    this.txtListBoxLineas.Items.Add("TODAS");
                    this.txtListBoxLineas.Items.Add("RECOMENDADAS");
                    foreach (DBLinea p in App._globalDBLinea)
                    {
                        this.txtListBoxLineas.Items.Add(p.PuertoOrigen + " - " + p.PuertoDestino);
                    }
                }
            }
        }

        private async void TxtOrigen_Unfocused(object sender, FocusEventArgs e)
        {
            var location = await Funciones.Address2GPS((sender as Entry).Text);
            var placemark = await Funciones.GPS2Address(location);
            if (location != null)
            {
                //this.lblOrigenGPS.Text = "Ln: " + location.Longitude + " Lt: " + location.Latitude;
                //this.lblOrigenLarge.Text = "Country: "  + placemark.CountryName + " SubAdminArea: " + placemark.SubAdminArea + " Locality: " +  placemark.Locality;
                this.lblOrigenLarge.Text = placemark.Locality + "-" + placemark.SubAdminArea + "-" + placemark.CountryName;
                if (App.miViaje is null)
                {
                    App.miViaje = new Viaje(this.txtOrigen.Text, this.txtDestino.Text);
                }
                App.miViaje.origenOriginal = (sender as Entry).Text;
                App.miViaje.origenGPS = location;
                App.miViaje.origen = placemark.Locality + " " + placemark.SubAdminArea + " " + placemark.CountryName;
                this.imgOrigen.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("check1.png") : ImageSource.FromFile("Images/check1.png");
            }
            else
            {
                this.imgOrigen.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("cross1.png") : ImageSource.FromFile("Images/cross1.png");
                this.lblOrigenLarge.Text = "";
            }
            this.btnCalcularRutas.IsEnabled = (lblOrigenLarge.Text != "" & lblDestinoLarge.Text != "");
        }

        private async void TxtDestino_Unfocused(object sender, FocusEventArgs e)
        {
            var location = await Funciones.Address2GPS((sender as Entry).Text);
            var placemark = await Funciones.GPS2Address(location);
            if (location != null)
            {
                //this.lblOrigenGPS.Text = "Ln: " + location.Longitude + " Lt: " + location.Latitude;
                //this.lblOrigenLarge.Text = "Country: "  + placemark.CountryName + " SubAdminArea: " + placemark.SubAdminArea + " Locality: " +  placemark.Locality;
                this.lblDestinoLarge.Text = placemark.Locality + "-" + placemark.SubAdminArea + "-" + placemark.CountryName;
                if (App.miViaje is null)
                {
                    App.miViaje = new Viaje(this.txtOrigen.Text, this.txtDestino.Text);
                }
                App.miViaje.destinoOriginal = (sender as Entry).Text;
                App.miViaje.destinoGPS = location;
                App.miViaje.destino = placemark.Locality + " " + placemark.SubAdminArea + " " + placemark.CountryName;
                this.imgDestino.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("check1.png") : ImageSource.FromFile("Images/check1.png");
            }
            else
            {
                this.imgDestino.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("cross1.png") : ImageSource.FromFile("Images/cross1.png");
                this.lblDestinoLarge.Text = "";
            }
            this.btnCalcularRutas.IsEnabled = (lblOrigenLarge.Text != "" & lblDestinoLarge.Text != "");

        }

        private async void BtnCalcularRutas_Clicked(object sender, EventArgs e)
        {
            this.btnCalcularRutas.IsEnabled = false;
            if (App.miViaje is null)
            {
                App.miViaje = new Viaje(this.txtOrigen.Text, this.txtDestino.Text);
            }
            //var current = Connectivity.NetworkAccess;
            //var Url = "http://simulador.shortsea.es/API/distanciaKM.aspx";
            //var UrlCompleta = Url + "?Origen=" + this.txtOrigen.Text + "&Destino=" + this.txtDestino.Text;
            //if (current == NetworkAccess.Internet)
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        string distancia = await RestApi.GetString(UrlCompleta);
            //        if (distancia != null)
            //        {
            //            this.lblDistancia.Text = distancia;
            //        }

            //    });
            //}
            //else
            //{
            //    DisplayAlert("ERROR", "No hay acceso a internet", "OK");
            //}

            App.miViaje.origen = this.lblOrigenLarge.Text;
            App.miViaje.destino = this.lblDestinoLarge.Text;
            App.miViaje.origenOriginal = this.txtOrigen.Text;
            App.miViaje.destinoOriginal = this.txtDestino.Text;
            App.miViaje.distanciaTerrestre = await Funciones.DistanciaCarretera(App.miViaje.origen, App.miViaje.destino);
            TxtOrigen_Unfocused(this.txtOrigen, new FocusEventArgs(this.txtOrigen, false));
            TxtDestino_Unfocused(this.txtDestino, new FocusEventArgs(this.txtDestino, false));

            var styleEN = new System.Globalization.NumberStyles();
            styleEN = System.Globalization.NumberStyles.AllowDecimalPoint;

            Double nValor = 0;
            Double.TryParse(this.txtVelocidad.Text, styleEN, App.cultureEN, out nValor); App.miViaje.velocidad = nValor;
            Double.TryParse(this.txtVelocidadAcarreoOrigen.Text, styleEN, App.cultureEN, out nValor); App.miViaje.velocidadAcarreoOrigen = nValor;
            Double.TryParse(this.txtVelocidadAcarreoDestino.Text, styleEN, App.cultureEN, out nValor); App.miViaje.velocidadAcarreoDestino = nValor;
            Double.TryParse(this.txtCosteKm.Text, styleEN, App.cultureEN, out nValor); App.miViaje.costeKm = nValor;
            Double.TryParse(this.txtCosteKmAcarreoOrigen.Text, styleEN, App.cultureEN, out nValor); App.miViaje.costeKmAcarreoOrigen = nValor;
            Double.TryParse(this.txtCosteKmAcarreoDestino.Text, styleEN, App.cultureEN, out nValor); App.miViaje.costeKmAcarreoDestino = nValor;
            Double.TryParse(this.txtToneladasTransportadas.Text, styleEN, App.cultureEN, out nValor); App.miViaje.toneladasTransportadas = nValor;

            Double.TryParse(App._globalDBConfiguracion.costeExternoCarretera, styleEN, App.cultureEN, out nValor); App.miViaje.costeExternoKmTnCarretera = nValor;
            Double.TryParse(App._globalDBConfiguracion.costeExternoMaritimo, styleEN, App.cultureEN, out nValor); App.miViaje.costeExternoKmTnMaritimo = nValor;
            Double.TryParse(App._globalDBConfiguracion.costeEmisionCO2Carretera, styleEN, App.cultureEN, out nValor); App.miViaje.costeEmisionCO2KmTnCarretera = nValor;
            Double.TryParse(App._globalDBConfiguracion.costeEmisionCO2Maritimo, styleEN, App.cultureEN, out nValor); App.miViaje.costeEmisionCO2KmTnMaritimo = nValor;

            Double.TryParse(this.txtPeajes.Text, styleEN, App.cultureEN, out nValor); App.miViaje.peajes = nValor;
            App.miViaje.fachadaAtlantica = this.txtFachadaAtlantica.IsChecked;
            App.miViaje.fachadaMediterranea = this.txtFachadaMediterranea.IsChecked;
            App.miViaje.fachadaCantabrica = this.txtFachadaCantabrica.IsChecked;
            App.miViaje.altaFrecuencia = this.txtAltaFrecuencia.IsChecked;
            App.miViaje.recargoMercanciaPeligrosa = this.txtRecargoMercanciaPeligrosa.IsChecked;
            App.miViaje.recargoRefrigerado = this.txtRecargoRefrigerado.IsChecked;
            App.miViaje.carreteraHoras = 0;
            App.miViaje.whereMisViajes = ""; //CalcularWhere();
            App.miViaje.whereMisViajesCondicion = CalcularWhereCondicion();

            // Calcular distancias en línea recta
            foreach (DBLinea lin in App._globalDBLinea)
            {

                lin.distanciaRectaOrigenPuertoOrigen = Funciones.HaversineDistance(App.miViaje.origenGPS, lin.PuertoOrigenGPS);
                lin.distanciaRectaOrigenPuertoDestino = Funciones.HaversineDistance(App.miViaje.origenGPS, lin.PuertoDestinoGPS);
                lin.distanciaRectaPuertoDestinoOrigen = Funciones.HaversineDistance(App.miViaje.destinoGPS, lin.PuertoOrigenGPS);
                lin.distanciaRectaPuertoDestinoDestino = Funciones.HaversineDistance(App.miViaje.destinoGPS, lin.PuertoDestinoGPS);
                if (lin.distanciaRectaOrigenPuertoOrigen + lin.distanciaRectaPuertoDestinoDestino < lin.distanciaRectaOrigenPuertoDestino + lin.distanciaRectaPuertoDestinoOrigen)
                {
                    lin.distanciaRectaOrigenDestino = lin.distanciaRectaOrigenPuertoOrigen + lin.distanciaRectaPuertoDestinoDestino;
                }
                else
                {
                    lin.distanciaRectaOrigenDestino = lin.distanciaRectaOrigenPuertoOrigen + lin.distanciaRectaPuertoDestinoDestino;
                }
            }
            //FILTAR Y ORDENAR RESULTADOS
            App._globalLineasSeleccionadas = from j in App._globalDBLinea where CalcularWhere(j) orderby j.distanciaRectaOrigenDestino descending select j;
            //App._globalLineasSeleccionadas = App._globalLineasSeleccionadas;



            App.miViaje = await App.miViaje.calcularMisViajes();
            var results = (from j in App._globalResultados orderby j.CosteTotal ascending select j);
            switch (this.txtResultadosOrdenadosPor.SelectedIndex)
            {
                case 0:
                    results = (from j in App._globalResultados orderby j.CosteTotal ascending select j);
                    break;
                case 1:
                    results = (from j in App._globalResultados orderby j.TiempoTotal ascending select j);
                    break;
                case 2:
                    results = (from j in App._globalResultados orderby j.DistanciaTotal ascending select j);
                    break;
            }
            App._globalResultadosSeleccionados = new List<Resultado>();
            foreach (Resultado r in results)
            {
                App._globalResultadosSeleccionados.Add(r);
            }

            //App._globalResultadosSeleccionados = results as List<Resultado>;
            //App._globalResultadosSeleccionados = new List<Resultado>();

            //int contador = 0;
            //this.txtListResultados.Items.Clear();
            //foreach (Resultado r in results)
            //{
            //    contador += 1;
            //    var contadorString = ("000" + contador.ToString());
            //    contadorString = contadorString.Substring(contadorString.Length - 3, 3);
            //    App._globalResultadosSeleccionados.Add(r);
            //    this.txtListResultados.Items.Add($"{contadorString}-{r.Linea} ,Coste :{r.CosteTotal}€, Tiempo: {r.TiempoTotal}, Distancia:{r.DistanciaTotal}");
            //}

            await DisplayAlert("RESULTADOS", App._globalLineasSeleccionadas.Count() + " líneas calculadas", "OK");

            this.btnCalcularRutas.IsEnabled = true;
            await Navigation.PushAsync(new ResultadosPage(), true);

            //If Me.rbDistancia.Checked Then
            //    'criterioRecomendado = " ( DistanciaTotal<=" + CType(miViaje.distanciaTerrestre, String).Replace(",", ".") & " ) "
            //    miViaje.ResultadosOrden = "DistanciaTotal"
            //End If
            //If Me.RBCoste.Checked Then
            //    'criterioRecomendado = " ( CosteTotal<=" + CType(miViaje.costeKm * miViaje.distanciaTerrestre, String).Replace(",", ".") & " ) "
            //    miViaje.ResultadosOrden = "CosteTotal"
            //End If
            //If Me.RBTiempo.Checked Then
            //    'criterioRecomendado = " ( TiempoTotal<=" + CType(miViaje.tiempoTerrestre, String).Replace(",", ".") & " ) "
            //    miViaje.ResultadosOrden = "TiempoTotal"
            //End If
        }

        public Boolean CalcularWhere(DBLinea j)
        {
            Boolean retorno = true;

            //'CriterioIDLinea
            //For x = 2 To Me.txtListBoxLineas.Items.Count - 1
            //    If Me.txtListBoxLineas.Items(x).Selected Then
            //        If criterioIDLinea<> String.Empty Then criterioIDLinea += " OR "
            //        criterioIDLinea += "x.LineaID=" + Me.txtListBoxLineas.Items(x).Value + " "
            //    End If
            //Next
            //If criterioIDLinea<> String.Empty Then
            //    criterioIDLinea = "( " & criterioIDLinea & ")"
            //End If

            //CriterioAltaFrecuencia
            if (this.txtAltaFrecuencia.IsChecked)
            {
                if (j.FrecuenciaMensual < Double.Parse(App._globalDBConfiguracion.altaFrecuencia)) retorno = false;
            }

            //'CriterioFachada
            if (!this.txtFachadaAtlantica.IsChecked)
            {
                if (j.Fachada == "ATLANTICA") retorno = false;
            }
            if (!this.txtFachadaMediterranea.IsChecked)
            {
                if (j.Fachada == "MEDITERRANEA") retorno = false;
            }
            if (!this.txtFachadaCantabrica.IsChecked)
            {
                if (j.Fachada == "CANTABRICA") retorno = false;
            }

            return retorno;
        }
        public string CalcularWhereCondicion()
        {
            string criterioRecomendado = string.Empty;
            //CriterioRecomendado
            //if (this.txtListBoxLineas.Items[1].Selected)    //RECOMENDADAS
            //{
            //    if (this.txtDistancia.isChecked)
            //    {
            //        criterioRecomendado = " ( distanciaTotal<=" + CType(miViaje.distanciaTerrestre, string).Replace(",", ".") & " ) ";
            //        miViaje.resultadosOrden = "DistanciaTotal";
            //    }
            //    if (this.txtCoste.isChecked)
            //    {
            //        criterioRecomendado = " ( CosteTotal<=" + CType(miViaje.costeKm * miViaje.distanciaTerrestre, string).Replace(",", ".") & " ) ";
            //        miViaje.ResultadosOrden = "CosteTotal";
            //    }
            //    if (this.txtTiempo.isChecked)
            //    {
            //        criterioRecomendado = " ( TiempoTotal<=" + CType(miViaje.tiempoTerrestre, string).Replace(",", ".") & " ) ";
            //        miViaje.ResultadosOrden = "TiempoTotal";
            //    }
            //}
            return criterioRecomendado;
        }

    }
}


