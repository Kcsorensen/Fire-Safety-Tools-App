using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SmokeUnitConverter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public DataModel DataModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            DataModel = new DataModel()
            {
                D010Log = 0.0,
                S = 0.0,
                S0 = 0.0,
                Ys = 0.0,
                Hrr = 1000,
                Pod = 8700,
                DeltaHAir = 0,
                DeltaHMat = 14000,
                Rho0 = 1.205
            };

            DataModel.SelectedConvertFrom = SmokeUnits.SmokePotentialArgos;
            DataModel.SelectedConvertTo = SmokeUnits.SootYield;

            BindingContext = DataModel;
        }

        protected override void OnAppearing()
        {


            base.OnAppearing();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            // TODO: Mangler Clear-funktion.
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            // TODO: Mangler InfoPage-funktion.
        }
    }
}