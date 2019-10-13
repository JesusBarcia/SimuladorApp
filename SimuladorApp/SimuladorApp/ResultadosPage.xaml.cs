using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimuladorApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultadosPage : ContentPage
    {
        public ObservableCollection<Resultado> source;
        public ResultadosPage()
        {
            source = new ObservableCollection<Resultado>();
            var nombre = App._globalResultadosSeleccionados[0].PuertoOrigen;
            foreach (Resultado r in App._globalResultadosSeleccionados)
            {
                source.Add(r);
            };

            InitializeComponent();
            this.lstResultados.ItemsSource = source;
            this.lstResultados.ItemTapped += LstResultados_ItemTapped;
            this.lblOrigenDestino.Text = App.miViaje.origenOriginal + "-" + App.miViaje.destinoOriginal;
            this.lblCarreteraCoste.Text = (App.miViaje.costeKm * App.miViaje.distanciaTerrestre + App.miViaje.peajes).ToString("N2");
            this.lblCarreteraTiempo.Text = App.miViaje.TiempoTerrestre().ToString("N2");
            this.lblCarreteraDistancia.Text = App.miViaje.distanciaTerrestre.ToString("N2");
            this.lblCarreteraCostesExternos.Text = (App.miViaje.distanciaTerrestre * App.miViaje.costeExternoKmTnCarretera * App.miViaje.toneladasTransportadas).ToString("N2");
            this.lblCarreteraCosteEmisionCO2Terrestre.Text = (App.miViaje.distanciaTerrestre * App.miViaje.costeEmisionCO2KmTnCarretera * App.miViaje.toneladasTransportadas).ToString("N2");


            //this.lstResultados.ItemsSource = App._globalResultadosSeleccionados;

        }

        private async void LstResultados_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ResultadoPage(e.Item as Resultado), true);
        }
    }
}