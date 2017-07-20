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
        public double HeatReleaseRate { get; set; }
        public double Density { get; set; }
        public double SpecificHeat { get; set; }
        public double Gravity { get; set; }
        public double CellSize { get; set; }
        public double Temperature { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Initial Values
            Density = 1.205;
            SpecificHeat = 1.0;
            Gravity = 9.82;
            Temperature = 20.0;

            parentStackLayout.BindingContext = this;
        }

        private async void Calculate_Clicked(object sender, EventArgs e)
        {
            var fireDiameter = Math.Pow((HeatReleaseRate / (Density * SpecificHeat * (273 + Temperature) * Math.Sqrt(Gravity))), 2.0 / 5.0);
            var ratio = fireDiameter / CellSize;            

            if (double.IsNaN(ratio))
            {
                await DisplayAlert("Result", "Value has to be numeric", "OK");
            }
            else if (double.IsInfinity(ratio))
            {
                await DisplayAlert("Result", "Cell Size or Ambient Conditions cannot be zero", "OK");
            }
            else
            {
                Application.Current.Properties["HeatReleaseRate_CheckMeshSize"] = HeatReleaseRate;
                Application.Current.Properties["Density_CheckMeshSize"] = Density;
                Application.Current.Properties["SpecificHeat_CheckMeshSize"] = SpecificHeat;
                Application.Current.Properties["Gravity_CheckMeshSize"] = Gravity;
                Application.Current.Properties["CellSize_CheckMeshSize"] = CellSize;
                Application.Current.Properties["Temperature_CheckMeshSize"] = Temperature;
                Application.Current.Properties["FireDiameter_CheckMeshSize"] = fireDiameter;
                Application.Current.Properties["Ratio_CheckMeshSize"] = ratio;

                await Navigation.PushAsync(new ResultPage());
            }
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}

