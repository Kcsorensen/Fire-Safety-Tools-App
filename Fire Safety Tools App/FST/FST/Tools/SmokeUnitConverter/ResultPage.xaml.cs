using FST.Persistance;
using SQLite;
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
    public partial class ResultPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private double _result;

        public string PrerequesitsText { get; set; }
        public string ResultText { get; set; }

        public ResultPage(double result)
        {
            InitializeComponent();

            _result = result;

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            var db = await _connection.Table<SmokeUnitTable>().FirstAsync();

            if (db.SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            {

                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {

                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SmokePotentialBurnedFuel)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {

                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SootYield)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg" + Environment.NewLine +
                        " - " + SmokeUnits.SmokePotentialArgos + " = " + db.S0 + " ob";

                    ResultText = string.Format("The {0} is equivalent to {1} = {2}.", SmokeUnits.SmokePotentialArgos, SmokeUnits.SootYield, _result);
                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SmokeProduction)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {

                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {

                }
            }

            BindingContext = this;

            base.OnAppearing();
        }
    }
}