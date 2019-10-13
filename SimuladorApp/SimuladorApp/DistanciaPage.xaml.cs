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
    public partial class DistanciaPage : ContentPage
    {
        public DistanciaPage()
        {
            InitializeComponent();
            btnCalcular.Clicked += BtnCalcular_Clicked;
        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            this.lblResultado.Text = "";
            this.lblResultado1.Text = "";
            this.lblResultado2.Text = "";
            this.lblResultado3.Text = "";
            this.lblResultado4.Text = "";
            this.lblResultado5.Text = "";
            this.lblResultado6.Text = "";
            this.lblResultado7.Text = "";
            double d = 0; double d1 = 0; double d2 = 0; double d3 = 0;
            var cad = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad1 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad2 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad3 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad4 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad5 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad6 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            var cad7 = await Funciones.DistanciaCarreteraNEW(txtOrigen.Text, txtDestino.Text);
            double.TryParse(cad, out d);
            double.TryParse(cad1, out d1);
            double.TryParse(cad2, out d2);
            double.TryParse(cad3, out d3);
            this.lblResultado.Text = d.ToString();
            this.lblResultado1.Text = (d + 1).ToString();
            this.lblResultado2.Text = (d + 2).ToString();
            this.lblResultado3.Text = (d + 3).ToString();
            this.lblResultado4.Text = cad4;
            this.lblResultado5.Text = cad5;
            this.lblResultado6.Text = cad6;
            this.lblResultado7.Text = cad7;
        }
    }
}