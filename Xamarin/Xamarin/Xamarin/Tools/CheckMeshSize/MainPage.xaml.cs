using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Tools.CheckMeshSize
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public string HeatReleaseRate { get; set; }

        //private double density;

        //public double Density
        //{
        //    get { return density; }
        //    set { density = value; }
        //}


        public double Density { get; set; }
        public string SpecificHeat { get; set; }
        public string Gravity { get; set; }
        public string CellSize { get; set; }
        public string Temperature { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Initial Values
            Density = 1.205;
            SpecificHeat = "1.0";
            Gravity = "9.82";
            Temperature = "20.0";

            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //if (double.TryParse(heatReleaseRate.Value, out double _heatReleaseRate) &&
            //    double.TryParse(density.Value, out double _density) &&
            //    double.TryParse(specificHeat.Value, out double _specificHeat) &&
            //    double.TryParse(gravity.Value, out double _gravity) &&
            //    double.TryParse(cellSize.Value, out double _cellSize) &&
            //    double.TryParse(temperature.Value, out double _temperature))
            //{
                //var fireDiameter = Math.Pow((_heatReleaseRate / (_density * _specificHeat * (273 + _temperature) * Math.Sqrt(_gravity))), 2.0 / 5.0);
                //var ratio = fireDiameter / _cellSize;

                //DisplayAlert("Result", ratio.ToString(), "OK");
            //}
            //else
            //{
            //    DisplayAlert("Result", "Value has to be numeric", "OK");
            //}
        }

        public class Data
        {

        }
    }
}