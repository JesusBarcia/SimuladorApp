using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimuladorApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultadoPage : ContentPage
    {
        public Resultado result;
        public ResultadoPage(Resultado r)
        {
            InitializeComponent();
            this.result = r;
            this.Title = r.OrigenDestino + ". Línea: " + r.Linea;
            TablaRellenarDatos();
        }
        public void TablaRellenarDatos()
        {

            this.lbl10.Text = result.Origen + "-" + result.PuertoOrigen;
            this.lbl20.Text = result.CosteOrigenPuerto.ToString("0");
            this.lbl21.Text = result.TiempoOrigenPuerto.ToString("0.00");
            this.lbl22.Text = result.DistanciaOrigenPuerto.ToString("0");
            this.lbl23.Text = result.CostesExternosOrigenPuerto.ToString("0");
            this.lbl24.Text = result.CosteEmisionCO2OrigenPuerto.ToString("0");

            this.lbl30.Text = result.PuertoOrigen + "-" + result.PuertoDestino;
            this.lbl40.Text = result.CostePuertoPuerto.ToString("0");
            this.lbl41.Text = result.TiempoPuertoPuerto.ToString("0.00");
            this.lbl42.Text = result.DistanciaPuertoPuerto.ToString("0");
            this.lbl43.Text = result.CostesExternosPuertoPuerto.ToString("0");
            this.lbl44.Text = result.CosteEmisionCO2PuertoPuerto.ToString("0");

            this.lbl50.Text = result.PuertoDestino + "-" + result.Destino;
            this.lbl60.Text = result.CostePuertoDestino.ToString("0");
            this.lbl61.Text = result.TiempoPuertoDestino.ToString("0.00");
            this.lbl62.Text = result.DistanciaPuertoDestino.ToString("0");
            this.lbl63.Text = result.CostesExternosPuertoDestino.ToString("0");
            this.lbl64.Text = result.CosteEmisionCO2PuertoDestino.ToString("0");

            this.lbl70.Text = "Total: " + result.OrigenDestino + " Línea: " + result.Linea;
            this.lbl80.Text = result.CosteTotal.ToString("0");
            this.lbl81.Text = result.TiempoTotal.ToString("0.00");
            this.lbl82.Text = result.DistanciaTotal.ToString("0");
            this.lbl83.Text = result.CostesExternosTotal.ToString("0");
            this.lbl84.Text = result.CosteEmisionCO2Total.ToString("0");

            this.lbl90.Text = result.FleteMensaje;

        }
    }
}